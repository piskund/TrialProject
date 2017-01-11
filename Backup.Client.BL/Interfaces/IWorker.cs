using Backup.Client.BL.Unity;

namespace Backup.Client.BL.Interfaces
{
    /// <summary>
    /// Represents item in a work queue.
    /// </summary>
    public interface IWorker
    {
        /// <summary>
        /// Performs the required work.
        /// </summary>
        void DoWork();
    }
}