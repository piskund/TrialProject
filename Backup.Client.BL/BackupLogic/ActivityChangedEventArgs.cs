// -------------------------------------------------------------------------------------------------------------
//  ActivityChangedEventArgs.cs created by DEP on 2017/01/14
// -------------------------------------------------------------------------------------------------------------

using System;
using Backup.Common.DTO;
using Backup.Common.Entities;

namespace Backup.Client.BL.BackupLogic
{
    /// <summary>
    /// Used on notificaton about an activity status change.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class ActivityChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityChangedEventArgs"/> class.
        /// </summary>
        /// <param name="scheduledBackup">The scheduled backup.</param>
        /// <param name="activityStatus">The activity status.</param>
        public ActivityChangedEventArgs(ScheduledBackup scheduledBackup, ActivityStatusType activityStatus)
        {
            ScheduledBackup = scheduledBackup;
            ActivityStatus = activityStatus;
        }

        /// <summary>
        /// Gets or sets the scheduled backup.
        /// </summary>
        /// <value>
        /// The scheduled backup.
        /// </value>
        public ScheduledBackup ScheduledBackup { get; set; }

        /// <summary>
        /// Gets or sets the activity status.
        /// </summary>
        /// <value>
        /// The activity status.
        /// </value>
        public ActivityStatusType ActivityStatus { get; set; }
    }
}