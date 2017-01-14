// -------------------------------------------------------------------------------------------------------------
//  ScheduledBackupJob.cs created by DEP on 2017/01/14
// -------------------------------------------------------------------------------------------------------------

using System;
using Backup.Client.BL.Interfaces;
using Backup.Common.DTO;
using Backup.Common.Entities;
using CodeContracts;

namespace Backup.Client.BL.BackupLogic
{
    /// <summary>
    /// Incapsulates backup configuration and action required.
    /// </summary>
    /// <seealso cref="Backup.Client.BL.Interfaces.IScheduledBackupJob" />
    internal class ScheduledBackupJob : IScheduledBackupJob
    {
        private readonly IBackupStrategy _backupStrategy;
        private readonly ScheduledBackup _scheduledBackup;
        private ActivityStatusType _activityStatus;

        /// <summary>
        /// Occurs when [activity status changed].
        /// </summary>
        public event EventHandler<ActivityChangedEventArgs> ActivityStatusChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduledBackupJob" /> class.
        /// </summary>
        /// <param name="scheduledBackup">The scheduled backup.</param>
        /// <param name="backupStrategy">The backup strategy.</param>
        public ScheduledBackupJob(ScheduledBackup scheduledBackup, IBackupStrategy backupStrategy)
        {
            Requires.NotNull(scheduledBackup, nameof(scheduledBackup));
            Requires.NotNull(backupStrategy, nameof(backupStrategy));
            _scheduledBackup = scheduledBackup;
            _backupStrategy = backupStrategy;
        }

        /// <summary>
        /// Gets the activity status.
        /// </summary>
        /// <value>
        /// The activity status.
        /// </value>
        public ActivityStatusType ActivityStatus
        {
            get { return _activityStatus; }
            private set
            {
                _activityStatus = value;
                ActivityStatusChanged?.Invoke(this, new ActivityChangedEventArgs(_scheduledBackup, _activityStatus));
            }
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
        public BackupConfig BackupConfig => _scheduledBackup.BackupConfig;

        /// <summary>
        /// Performs the required action.
        /// </summary>
        public void Execute()
        {
            ActivityStatus = ActivityStatusType.InProgress;
            try
            {
                _backupStrategy.DoWork(BackupConfig);
                ActivityStatus = ActivityStatusType.Succeeded;
            }
            catch (Exception)
            {
                ActivityStatus = ActivityStatusType.Failed;
                throw;
            }
        }
    }
}