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

namespace Backup.Client.BL.BackupLogic
{
    /// <summary>
    /// Manages backup jobs in form of an queue (FIFO).
    /// </summary>
    public class ScheduledJobManager
    {
        /// <summary>
        /// The task's queue
        /// </summary>
        private readonly BlockingCollection<IScheduledJob> _queue = new BlockingCollection<IScheduledJob>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduledJobManager" /> class.
        /// </summary>
        /// <param name="scheduledJobs">The scheduled jobs.</param>
        internal ScheduledJobManager(IEnumerable<IScheduledJob> scheduledJobs)
        {
            QueueProducer(scheduledJobs);
        }

        /// <summary>
        /// Executes all jobs from the queue one by one asynchronously.
        /// </summary>
        public async Task StartAsync(CancellationToken cancellationToken, bool isScheduledExecution = true)
        {
            while (!_queue.IsCompleted)
            {
                await ExecuteJob(cancellationToken, isScheduledExecution);
                if (cancellationToken.IsCancellationRequested)
                    break;
            }
        }

        /// <summary>
        /// Executes top job from the queue.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="isScheduledExecution">if set to <c>true</c> (by default) the scheduled time will take into account.
        /// Otherwise jobs would be executed one by one immediately.
        /// </param>
        /// <returns> Awaitable task. </returns>
        private async Task ExecuteJob(CancellationToken cancellationToken, bool isScheduledExecution)
        {
            if (!_queue.IsCompleted)
            {
                var job = _queue.Take();
                // Start job immediately if delay is not required.
                var delay = TimeSpan.FromSeconds(0);

                if (isScheduledExecution)
                {
                    var startTime = job.ScheduledDateTime;
                    if (startTime > DateTime.UtcNow)
                    {
                        delay = startTime - DateTime.UtcNow;
                    }
                }

                var t = Task.Run(async delegate
                {
                    await Task.Delay(delay, cancellationToken);
                    job.Execute();
                }, cancellationToken);
                await t;
            }
        }

        /// <summary>
        /// Builds the queue.
        /// </summary>
        /// <param name="scheduledJobs">The scheduled jobs.</param>
        private void QueueProducer(IEnumerable<IScheduledJob> scheduledJobs)
        {
            var sortedJobs = scheduledJobs.OrderBy(j => j.ScheduledDateTime);
            foreach (var job in sortedJobs)
            {
                _queue.Add(job);
            }
            _queue.CompleteAdding();
        }
    }
}