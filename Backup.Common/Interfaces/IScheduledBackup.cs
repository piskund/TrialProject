// -------------------------------------------------------------------------------------------------------------
//  IScheduledBackup.cs created by DEP on 2017/01/14
// -------------------------------------------------------------------------------------------------------------

using System;

namespace Backup.Common.Interfaces
{
    /// <summary>
    /// Defines scheduled backup.
    /// </summary>
    public interface IScheduledBackup
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