// -------------------------------------------------------------------------------------------------------------
//  IActivityTracker.cs created by DEP on 2017/01/14
// -------------------------------------------------------------------------------------------------------------

using Backup.Common.DTO;

namespace Backup.Client.BL.Interfaces
{
    /// <summary>
    /// Defines tracked activity contract.
    /// </summary>
    public interface IActivityTracker
    {
        /// <summary>
        /// Gets the activity status.
        /// </summary>
        /// <value>
        /// The activity status.
        /// </value>
        ActivityStatusType ActivityStatus { get; }
    }
}