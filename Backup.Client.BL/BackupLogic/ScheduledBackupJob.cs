// -------------------------------------------------------------------------------------------------------------
//  ScheduledBackupJob.cs created by DEP on 2017/01/12
// -------------------------------------------------------------------------------------------------------------

using System;
using Backup.Client.BL.Interfaces;
using Backup.Common.Interfaces;
using CodeContracts;

namespace Backup.Client.BL.BackupLogic
{
    /// <summary>
    /// Incapsulates backup configuration and action required.
    /// </summary>
    /// <seealso cref="Backup.Client.BL.Interfaces.IScheduledBackupJob" />
    internal class ScheduledBackupJob : IScheduledBackupJob
    {
        private readonly IScheduledBackup _scheduledBackup;
        private readonly IBackupStrategy _backupStrategy;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduledBackupJob" /> class.
        /// </summary>
        /// <param name="scheduledBackup">The scheduled backup.</param>
        /// <param name="backupStrategy">The backup strategy.</param>
        public ScheduledBackupJob(IScheduledBackup scheduledBackup, IBackupStrategy backupStrategy)
        {
            Requires.NotNull(scheduledBackup, nameof(scheduledBackup));
            Requires.NotNull(backupStrategy, nameof(backupStrategy));
            _scheduledBackup = scheduledBackup;
            _backupStrategy = backupStrategy;
        }

        /// <summary>
        /// Gets the scheduled date time.
        /// </summary>
        /// <value>
        /// The scheduled date time.
        /// </value>
        public DateTime ScheduledDateTime => _scheduledBackup.ScheduledDateTime;

        /// <summary>
        /// Gets the backup configuration.
        /// </summary>
        /// <value>
        /// The backup configuration.
        /// </value>
        public IBackupConfig BackupConfig => _scheduledBackup.BackupConfig;

        /// <summary>
        /// Performs the required action.
        /// </summary>
        public void Execute()
        {
            _backupStrategy.DoWork(BackupConfig);
        }
    }
}