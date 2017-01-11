using System;

namespace Backup.Common.DTO
{
    /// <summary>
    ///     Holds information about scheduled backup.
    /// </summary>
    [Serializable]
    public class ScheduledBackup
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ScheduledBackup" /> class.
        /// </summary>
        /// <param name="scheduledDateTime">The scheduled date time.</param>
        /// <param name="backupConfig">The backup configuration.</param>
        public ScheduledBackup(DateTime scheduledDateTime, BackupConfig backupConfig)
        {
            ScheduledDateTime = scheduledDateTime;
            BackupConfig = backupConfig;
        }

        /// <summary>
        ///     Gets or sets the scheduled date time.
        /// </summary>
        /// <value>
        ///     The scheduled date time.
        /// </value>
        public DateTime ScheduledDateTime { get; set; }

        /// <summary>
        ///     Gets or sets the backup configuration.
        /// </summary>
        /// <value>
        ///     The backup configuration.
        /// </value>
        public BackupConfig BackupConfig { get; set; }
    }
}