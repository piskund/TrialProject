// -------------------------------------------------------------------------------------------------------------
//  IScheduledBackupJob.cs created by DEP on 2017/01/12
// -------------------------------------------------------------------------------------------------------------

using Backup.Common.Interfaces;

namespace Backup.Client.BL.Interfaces
{
    /// <summary>
    /// Represents the action required to perform on the backup configuration.
    /// </summary>
    /// <seealso cref="Backup.Common.Interfaces.IScheduledBackup" />
    internal interface IScheduledBackupJob : IScheduledBackup
    {
        /// <summary>
        /// Performs the required action.
        /// </summary>
        void Execute();
    }
}