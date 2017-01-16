// -------------------------------------------------------------------------------------------------------------
//  BackupManager.cs created by DEP on 2017/01/12
// -------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Backup.Client.BL.Interfaces;
using Backup.Common.Logger;
using CodeContracts;

namespace Backup.Client.BL.BackupLogic
{
    /// <summary>
    /// Manages backup jobs in form of an queue (FIFO).
    /// </summary>
    internal class ScheduledJobsManager : IDisposable
    {
        /// <summary>
        /// The task's queue
        /// </summary>
        private BlockingCollection<IScheduledJob> _queue = new BlockingCollection<IScheduledJob>();

        /// <summary>
        /// The cancellation token source
        /// </summary>
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduledJobsManager"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public ScheduledJobsManager(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is working.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is working; otherwise, <c>false</c>.
        /// </value>
        public bool IsWorking => (_queue.Count > 0) && !_queue.IsCompleted;

        /// <summary>
        /// Proceeds the scheduled jobs asynchronous.
        /// </summary>
        /// <param name="scheduledJobs">The scheduled jobs.</param>
        internal async Task ProceedScheduledJobsAsync(IEnumerable<IScheduledJob> scheduledJobs)
        {
            if (IsWorking)
            {
                _logger.LogInfo("The queue is stale and starting to reinitialize with new list of jobs.");
                // Cancel current queue proceeding.
                _cancellationTokenSource.Cancel();
                ResetQueue(_queue);
                _queue = new BlockingCollection<IScheduledJob>();
                _cancellationTokenSource = new CancellationTokenSource();
            }

            BuildQueueSortedByScheduledTime(_queue, scheduledJobs);

            // Start processing the queue.
            await ProceedQueueAsync(_cancellationTokenSource.Token);
        }

        /// <summary>
        /// Executes all jobs from the queue one by one asynchronously.
        /// </summary>
        private async Task ProceedQueueAsync(CancellationToken cancellationToken)
        {
            _logger.LogInfo($"The queue is starting to proceed on {DateTime.UtcNow}");
            while (!_queue.IsCompleted)
            {
                await ExecuteJob(cancellationToken);
                if (cancellationToken.IsCancellationRequested)
                {
                    _logger.LogInfo($"The queue proceedin has been canceled on {DateTime.UtcNow}");
                    break;
                }
            }
            _logger.LogInfo($"The queue is finished to proceed on {DateTime.UtcNow}");
        }

        /// <summary>
        /// Executes top job from the queue.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns> Awaitable task. </returns>
        private async Task ExecuteJob(CancellationToken cancellationToken)
        {
            if (!_queue.IsCompleted)
            {
                var job = _queue.Take();
                // Start job immediately if delay is not required.
                var delay = TimeSpan.FromSeconds(0);
                var startTime = job.ScheduledDateTime;
                if (startTime > DateTime.UtcNow)
                {
                    delay = startTime - DateTime.UtcNow;
                }

                _logger.LogInfo($"Executing the job scheduled on {job.ScheduledDateTime}. Delay to start: {delay}");
                var t = Task.Run(async delegate
                {
                    await Task.Delay(delay, cancellationToken);
                    job.Execute();
                }, cancellationToken);
                await t;
                _logger.LogInfo($"Job scheduled on {job.ScheduledDateTime} has been executed.");
            }
        }

        /// <summary>
        /// Resets the queue.
        /// </summary>
        /// <param name="queue">The queue.</param>
        private static void ResetQueue(BlockingCollection<IScheduledJob> queue)
        {
            Requires.NotNull(queue, nameof(queue));
            while(!queue.IsCompleted)
            {
                queue.Take();
            }

            queue.Dispose();
        }

        /// <summary>
        /// Builds the queue sorted by scheduled time.
        /// </summary>
        /// <param name="queue">The queue.</param>
        /// <param name="scheduledJobs">The scheduled jobs.</param>
        private void BuildQueueSortedByScheduledTime(
            BlockingCollection<IScheduledJob> queue, 
            IEnumerable<IScheduledJob> scheduledJobs)
        {
            _logger.LogInfo($"Building queue for {scheduledJobs.Count()} jobs.");

            var sortedJobs = scheduledJobs.OrderBy(j => j.ScheduledDateTime);
            foreach (var job in sortedJobs)
            {
                queue.Add(job);
            }
            queue.CompleteAdding();
        }

        #region IDisposable Support
        private bool _disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _cancellationTokenSource.Cancel();
                    ResetQueue(_queue);
                }

                _disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}