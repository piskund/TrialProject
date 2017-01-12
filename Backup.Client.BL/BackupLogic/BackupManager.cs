﻿// -------------------------------------------------------------------------------------------------------------
//  BackupManager.cs created by DEP on 2017/01/12
// -------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backup.Client.BL.Interfaces;

namespace Backup.Client.BL
{
    public class BackupManager
    {
        /// <summary>
        /// The task's queue
        /// </summary>
        private readonly BlockingCollection<IScheduledBackupJob> _queue = new BlockingCollection<IScheduledBackupJob>();

        /// <summary>
        /// Initializes a new instance of the <see cref="BackupManager" /> class.
        /// </summary>
        /// <param name="scheduledJobs">The scheduled jobs.</param>
        /// <param name="worker">The worker.</param>
        internal BackupManager(IEnumerable<IScheduledBackupJob> scheduledJobs)
        {
            QueueProducer(scheduledJobs);
        }

        /// <summary>
        /// Executes all tasks from the queue one by one.
        /// </summary>
        internal void ExecuteJobs(bool isScheduledExecution)
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
                    await Task.Delay(delay);
                    job.Execute();
                });
                t.Wait();

                // Execute tasks one by one;
                ExecuteJobs(isScheduledExecution);
            }
        }

        /// <summary>
        /// Builds the queue.
        /// </summary>
        /// <param name="scheduledJobs">The scheduled jobs.</param>
        /// <param name="action">The action.</param>
        private void QueueProducer(IEnumerable<IScheduledBackupJob> scheduledJobs)
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