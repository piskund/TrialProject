// -------------------------------------------------------------------------------------------------------------
//  IRemoteRequestsFacade.cs created by DEP on 2017/01/16
// -------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using Backup.Common.Entities;

namespace Backup.Client.BL.Interfaces
{
    public interface IRemoteRequestsFacade
    {
        /// <summary>
        /// Gets the backups.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ScheduledBackup> GetBackups();

        /// <summary>
        /// Saves the specified activity information.
        /// </summary>
        /// <param name="activityInfo">The activity information.</param>
        void Save(ActivityInfo activityInfo);
    }
}