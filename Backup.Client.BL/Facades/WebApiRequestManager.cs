// -------------------------------------------------------------------------------------------------------------
//  WebApiRequestsManager.cs created by DEP on 2017/01/13
// -------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Backup.Client.BL.Interfaces;
using Backup.Common.Entities;
using Newtonsoft.Json;

namespace Backup.Client.BL.Facades
{
    /// <summary>
    /// Hides under the hood request to the WebApi service.
    /// </summary>
    /// <seealso cref="Backup.Client.BL.Interfaces.IRequestManager" />
    /// <seealso cref="System.IDisposable" />
    public class WebApiRequestManager : IRequestManager, IDisposable
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebApiRequestManager"/> class.
        /// </summary>
        /// <param name="webServiceUrl">The web service URL.</param>
        public WebApiRequestManager(string webServiceUrl)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(webServiceUrl) };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Gets the scheduled backups asynchronous.
        /// </summary>
        /// <returns>The collection of scheduled backups.</returns>
        public async Task<IEnumerable<ScheduledBackup>> GetScheduledBackupsAsync(string ipAddress)
        {
            IEnumerable<ScheduledBackup> backups = null;
            var response = await _httpClient.GetAsync($"/api/backup/?ipAddress={ipAddress}");
            if (response.IsSuccessStatusCode)
            {
                backups = await response.Content.ReadAsAsync<IEnumerable<ScheduledBackup>>();
            }
            return backups;
        }

        /// <summary>
        /// Gets the client information asynchronous.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns>Client info</returns>
        public async Task<ClientInfo> GetClientInfoAsync(string ipAddress)
        {
            ClientInfo clientInfo = null;
            var response = await _httpClient.GetAsync("/api/Client/?ipAddress={IpAddress}");
            if (response.IsSuccessStatusCode)
            {
                clientInfo = await response.Content.ReadAsAsync<ClientInfo>();
            }
            return clientInfo;
        }

        /// <summary>
        /// Saves the activity asynchronous.
        /// </summary>
        /// <param name="activityInfo">The activity information.</param>
        /// <returns>The status of save.</returns>
        public async Task<HttpStatusCode> SaveActivityAsync(ActivityInfo activityInfo)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Activity/SaveActivity", activityInfo);
            return response.StatusCode;
        }

        /// <summary>
        /// Registers the new client asynchronous.
        /// </summary>
        /// <param name="clientInfo">The client information.</param>
        /// <returns>The status of registration.</returns>
        public async Task<HttpStatusCode> RegisterNewClientAsync(ClientInfo clientInfo)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Client/RegisterClient", clientInfo);
            return response.StatusCode;
        }

        /// <summary>
        /// Registers the new user asynchronous.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        public void SetAccessToken(string userName, string password)
        {
            var pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", userName),
                new KeyValuePair<string, string>("Password", password)
            };
            var content = new FormUrlEncodedContent(pairs);

            var response = _httpClient.PostAsync("/Token", content).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var tokenDictionary =
                JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
            var token = tokenDictionary["access_token"];

            if (!string.IsNullOrWhiteSpace(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }

        #region IDisposable Support
        private bool _disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _httpClient.Dispose();
                }
                _disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}