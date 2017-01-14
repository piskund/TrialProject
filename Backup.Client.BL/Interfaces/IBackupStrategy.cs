// -------------------------------------------------------------------------------------------------------------
//  IBackupWorker.cs created by DEP on 2017/01/12
// -------------------------------------------------------------------------------------------------------------

using Backup.Common.Entities;

namespace Backup.Client.BL.Interfaces
{
    /// <summary>
    /// Represents an item in a work queue.
    /// </summary>
    public interface IBackupStrategy
    {
        /// <summary>
        ///     Performs the required work.
        /// </summary>
        /// <param name="config">The configuration.</param>
        void DoWork(BackupConfig config);
    }
}