// -------------------------------------------------------------------------------------------------------------
//  IActivityFacade.cs created by DEP on 2017/01/12
// -------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Net;
using Backup.Common.DTO;

namespace Backup.Client.BL.Interfaces
{
    /// <summary>
    /// Isolates clients from concrete implemetation of activity management.
    /// </summary>
    public interface IActivityFacade
    {
        /// <summary>
        /// Gets all activities.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        IEnumerable<ActivityInfo> GetAllActivities(IPAddress address);

        /// <summary>
        /// Saves the specified activity information.
        /// </summary>
        /// <param name="activityInfo">The activity information.</param>
        void Save(ActivityInfo activityInfo);

        /// <summary>
        /// Saves the batch.
        /// </summary>
        /// <param name="activities">The activities.</param>
        void SaveBatch(IEnumerable<ActivityInfo> activities);
    }
}