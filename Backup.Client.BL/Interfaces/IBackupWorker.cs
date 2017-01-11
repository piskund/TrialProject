using Backup.Client.BL.Unity;
using Backup.Common.Interfaces;

namespace Backup.Client.BL.Interfaces
{
    /// <summary>
    ///     Represents an item in a work queue.
    /// </summary>
    public interface IBackupWorker
    {
        /// <summary>
        ///     Performs the required work.
        /// </summary>
        /// <param name="config">The configuration.</param>
        [ActivityLog]
        void DoWork(IBackupConfig config);
    }
}