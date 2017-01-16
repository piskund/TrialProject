// -------------------------------------------------------------------------------------------------------------
//  ClientFacade.cs created by DEP on 2017/01/16
// -------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Backup.Client.BL.Helpers;
using Backup.Client.BL.Interfaces;
using Backup.Common.Entities;
using Backup.Common.Helpers;
using CodeContracts;

namespace Backup.Client.BL.Facades
{
    /// <summary>
    /// Screens requests to the remote service.
    /// </summary>
    /// <seealso cref="Backup.Client.BL.Interfaces.IRemoteRequestsFacade" />
    public class RemoteRequestsFacade : IRemoteRequestsFacade
    {
        private readonly IRequestManager _requestManager;
        private readonly ClientInfo _clientInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteRequestsFacade" /> class.
        /// </summary>
        /// <param name="requestManager">The request manager.</param>
        /// <param name="clientRegistrationHelper">The client registration helper.</param>
        public RemoteRequestsFacade(IRequestManager requestManager, ClientRegistrationHelper clientRegistrationHelper)
        {
            Requires.NotNull(requestManager, nameof(requestManager));
            Requires.NotNull(clientRegistrationHelper, nameof(clientRegistrationHelper));
            Requires.NotNull(clientRegistrationHelper.ClientInfo, nameof(clientRegistrationHelper.ClientInfo));
            Requires.NotNullOrEmpty(clientRegistrationHelper.ClientInfo.ClientIpAddress, nameof(clientRegistrationHelper.ClientInfo.ClientIpAddress));

            _requestManager = requestManager;
            _clientInfo = clientRegistrationHelper.ClientInfo;
            SetAccessToken(_clientInfo);
        }

        /// <summary>
        /// Gets the scheduled backups.
        /// </summary>
        /// <returns>The collection of the scheduled backups.</returns>
        public IEnumerable<ScheduledBackup> GetBackups()
        {
            return _requestManager.GetScheduledBackupsAsync(_clientInfo.ClientIpAddress).Result;
        }

        /// <summary>
        /// Saves the specified activity information.
        /// </summary>
        /// <param name="activityInfo">The activity information.</param>
        public void Save(ActivityInfo activityInfo)
        {
            Requires.NotNull(activityInfo, nameof(activityInfo));
            _requestManager.SaveActivityAsync(activityInfo);
        }

        /// <summary>
        /// Logons the client if already registered or register and logon.
        /// </summary>
        /// <param name="clientInfo">The client information.</param>
        private async Task LogonOrRegisterAsync(ClientInfo clientInfo)
        {
            var registeredClient = await _requestManager.GetClientInfoAsync(clientInfo.ClientIpAddress);
            if (registeredClient == null)
            {
                 await _requestManager.RegisterNewClientAsync(clientInfo);
            }
        }

        /// <summary>
        /// Gets the access token.
        /// </summary>
        /// <returns></returns>
        private void SetAccessToken(ClientInfo clientInfo)
        {
            LogonOrRegisterAsync(clientInfo).Wait();
            _requestManager.SetAccessToken(clientInfo.CredentialInfo.UserName, clientInfo.CredentialInfo.Password);
        }
    }
}