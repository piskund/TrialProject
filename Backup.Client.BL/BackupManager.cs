// -------------------------------------------------------------------------------------------------------------
//  BackupManager.cs created by DEP on 2017/01/12
// -------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backup.Client.BL.Interfaces;
using Backup.Common.Interfaces;

namespace Backup.Client.BL
{
    public class BackupManager
    {
        /// <summary>
        /// The task's queue
        /// </summary>
        private readonly BlockingCollection<Task> _queue = new BlockingCollection<Task>();

        /// <summary>
        ///     Initializes a new instance of the <see cref="BackupManager" /> class.
        /// </summary>
        /// <param name="scheduledJobs">The scheduled jobs.</param>
        /// <param name="worker">The worker.</param>
        public BackupManager(IEnumerable<IScheduledJob> scheduledJobs, IBackupWorker worker) :
            this(scheduledJobs, job => worker.DoWork(job.BackupConfig))
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BackupManager" /> class.
        /// </summary>
        /// <param name="scheduledJobs">The scheduled jobs.</param>
        /// <param name="action">The action.</param>
        internal BackupManager(IEnumerable<IScheduledJob> scheduledJobs, Action<IScheduledJob> action)
        {
            QueueProducer(scheduledJobs, action);
        }

        /// <summary>
        ///     Executes all.
        /// </summary>
        public void ExecuteAll()
        {
            while (!_queue.IsCompleted)
                Execute();
        }

        /// <summary>
        ///     Executes this instance.
        /// </summary>
        private void Execute()
        {
            if (!_queue.IsCompleted)
            {
                var task = _queue.Take();
                task.Start();
                task.Wait();
            }
        }

        /// <summary>
        ///     Queues the producer.
        /// </summary>
        /// <param name="scheduledJobs">The scheduled jobs.</param>
        /// <param name="action">The action.</param>
        private void QueueProducer(IEnumerable<IScheduledJob> scheduledJobs, Action<IScheduledJob> action)
        {
            var dict = scheduledJobs.ToDictionary(j => j.ScheduledDateTime);
            var sortedByScheduledTime = new SortedList<DateTime, IScheduledJob>(dict);

            foreach (var keyValuePair in sortedByScheduledTime)
            {
                var job = keyValuePair.Value;
                _queue.Add(new Task(() => action(job)));
            }
            _queue.CompleteAdding();
        }
    }
}