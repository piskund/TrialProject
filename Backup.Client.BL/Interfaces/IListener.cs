// -------------------------------------------------------------------------------------------------------------
//  IBackupController.cs created by DEP on 2017/01/15
// -------------------------------------------------------------------------------------------------------------

namespace Backup.Client.BL.Interfaces
{
    /// <summary>
    /// Manages set of backup jobs.
    /// </summary>
    public interface IListener
    {
        /// <summary>
        /// Starts the listening.
        /// </summary>
        void StartListen();

        /// <summary>
        /// Starts the listening with given interval.
        /// </summary>
        /// <param name="timerInterval">The timer interval.</param>
        void StartListen(int timerInterval);
    }
}