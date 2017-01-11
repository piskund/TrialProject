using System;

namespace Backup.Common.Interfaces
{
    public interface IScheduledJob
    {
        /// <summary>
        /// Gets the scheduled date time.
        /// </summary>
        /// <value>
        /// The scheduled date time.
        /// </value>
        DateTime ScheduledDateTime { get; }

        /// <summary>
        /// Gets the backup configuration.
        /// </summary>
        /// <value>
        /// The backup configuration.
        /// </value>
        IBackupConfig BackupConfig { get; }
    }
}