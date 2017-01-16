// -------------------------------------------------------------------------------------------------------------
//  IRequestManager.cs created by DEP on 2017/01/15
// -------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Backup.Common.Entities;

namespace Backup.Client.BL.Interfaces
{
    /// <summary>
    /// Defines contracts of remote requests.
    /// </summary>
    public interface IRequestManager
    {
        /// <summary>
        /// Gets the client information asynchronous.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns>The client information.</returns>
        Task<ClientInfo> GetClientInfoAsync(string ipAddress);

        /// <summary>
        /// Registers the new client asynchronous.
        /// </summary>
        /// <param name="clientInfo">The client information.</param>
        /// <returns>Status of registration.</returns>
        Task<HttpStatusCode> RegisterNewClientAsync(ClientInfo clientInfo);

        /// <summary>
        /// Sets the access token for authorized requests.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        void SetAccessToken(string userName, string password);

        /// <summary>
        /// Gets the scheduled backups asynchronous.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns>Collection of scheduled backups.</returns>
        Task<IEnumerable<ScheduledBackup>> GetScheduledBackupsAsync(string ipAddress);

        /// <summary>
        /// Saves the activity asynchronous.
        /// </summary>
        /// <param name="activityInfo">The activity information.</param>
        /// <returns>Status of save.</returns>
        Task<HttpStatusCode> SaveActivityAsync(ActivityInfo activityInfo);
    }
}