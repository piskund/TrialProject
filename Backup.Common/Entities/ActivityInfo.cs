// -------------------------------------------------------------------------------------------------------------
//  ActivityInfo.cs created by DEP on 2017/01/14
// -------------------------------------------------------------------------------------------------------------

using System;
using Backup.Common.DTO;
using Backup.Common.Interfaces;
using CodeContracts;

namespace Backup.Common.Entities
{
    /// <summary>
    /// Contains information about a client's backup activity.
    /// </summary>
    /// <seealso cref="Backup.Common.Interfaces.IEntity" />
    [Serializable]
    public class ActivityInfo : IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityInfo" /> class.
        /// </summary>
        /// <param name="scheduledBackup">The scheduled backup.</param>
        /// <param name="status">The status.</param>
        public ActivityInfo(ScheduledBackup scheduledBackup, ActivityStatusType status = ActivityStatusType.NotStarted)
        {
            Requires.NotNull(scheduledBackup, nameof(scheduledBackup));
            ScheduledBackup = scheduledBackup;
            ActivityStatus = status;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        ///     Gets or sets the backup configuration.
        /// </summary>
        /// <value>
        ///     The backup configuration.
        /// </value>
        public virtual ScheduledBackup ScheduledBackup { get; set; }

        /// <summary>
        ///     Gets or sets the activity status.
        /// </summary>
        /// <value>
        ///     The activity status.
        /// </value>
        public ActivityStatusType ActivityStatus { get; set; }
    }
}