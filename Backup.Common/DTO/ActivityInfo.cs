using System;
using Backup.Common.Entities;
using CodeContracts;

namespace Backup.Common.DTO
{
    /// <summary>
    ///     Contains information about a client's backup activity.
    /// </summary>
    [Serializable]
    public class ActivityInfo
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ActivityInfo" /> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <param name="status">The status.</param>
        public ActivityInfo(BackupConfig config, ActivityStatusType status = ActivityStatusType.NotStarted)
        {
            Requires.NotNull(config, nameof(config));
            BackupConfig = config;
            ActivityStatus = status;
        }

        /// <summary>
        ///     Gets or sets the backup configuration.
        /// </summary>
        /// <value>
        ///     The backup configuration.
        /// </value>
        public BackupConfig BackupConfig { get; set; }

        /// <summary>
        ///     Gets or sets the activity status.
        /// </summary>
        /// <value>
        ///     The activity status.
        /// </value>
        public ActivityStatusType ActivityStatus { get; set; }
    }
}