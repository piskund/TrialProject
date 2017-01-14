// -------------------------------------------------------------------------------------------------------------
//  IScheduledBackupJob.cs created by DEP on 2017/01/14
// -------------------------------------------------------------------------------------------------------------

namespace Backup.Client.BL.Interfaces
{
    /// <summary>
    /// Represents the action required to perform on the backup configuration.
    /// </summary>
    internal interface IScheduledBackupJob : IScheduledJob, IActivityTracker
    {
    }
}