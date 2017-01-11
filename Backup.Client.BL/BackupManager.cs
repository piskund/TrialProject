using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backup.Common.Interfaces;

namespace Backup.Client.BL
{
    public class BackupManager
    {
        private readonly BlockingCollection<Task> _queue = new BlockingCollection<Task>();

        public BackupManager(IEnumerable<IScheduledJob> scheduledJobs, Action<IScheduledJob> action)
        {
            QueueProducer(scheduledJobs, action);
        }

        internal void QueueProducer(IEnumerable<IScheduledJob> scheduledJobs, Action<IScheduledJob> action)
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

        public void Execute()
        {
            if (!_queue.IsCompleted)
            {
                var task = _queue.Take();
                task.Start();
                task.Wait();
            }
        }

        public void ExecuteAll()
        {
            while (!_queue.IsCompleted)
            {
                Execute();
            }
        }
    }
}