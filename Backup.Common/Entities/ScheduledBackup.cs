using System;
using Backup.Common.DTO;
using Backup.Common.Interfaces;

namespace Backup.Common.Entities
{
    /// <summary>
    /// Holds information about scheduled backup.
    /// </summary>
    /// <seealso cref="Backup.Common.Interfaces.IEntity" />
    /// <seealso cref="Backup.Common.Interfaces.IScheduledBackup" />
    [Serializable]
    public class ScheduledBackup : IEntity, IScheduledBackup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduledBackup"/> class.
        /// </summary>
        public ScheduledBackup()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduledBackup" /> class.
        /// </summary>
        /// <param name="scheduledDateTime">The scheduled date time.</param>
        /// <param name="backupConfig">The backup configuration.</param>
        public ScheduledBackup(DateTime scheduledDateTime, BackupConfig backupConfig)
        {
            ScheduledDateTime = scheduledDateTime;
            BackupConfig = backupConfig;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

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

        /// <summary>
        /// Gets the backup configuration (explicit interface implementation).
        /// </summary>
        /// <value>
        /// The backup configuration.
        /// </value>
        IBackupConfig IScheduledBackup.BackupConfig => BackupConfig;
    }
}