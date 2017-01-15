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

namespace Backup.Client.BL
{
    public class WebApiRequestManager : IRequestManager, IDisposable
    {
        private const string WebServiceUrl = "http://localhost:59901";
        private const string DefaultUserName = "john.dough@fake.com";
        private const string DefaultPassword = "1q2w3e";
        private const string IpAddress = "";
        private readonly HttpClient _httpClient;

        internal WebApiRequestManager()
        {
            _httpClient = new HttpClient {BaseAddress = new Uri(WebServiceUrl)};
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var status = RegisterNewUserAsync(DefaultUserName, DefaultPassword).Result;

            var token = GetAccessToken(DefaultUserName, DefaultPassword);
            if (!string.IsNullOrWhiteSpace(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }

        /// <summary>
        /// Gets the scheduled backups asynchronous.
        /// </summary>
        /// <returns>The collection of scheduled backups.</returns>
        public async Task<IEnumerable<ScheduledBackup>> GetScheduledBackupsAsync()
        {
            IEnumerable<ScheduledBackup> backups = null;
            var response = await _httpClient.GetAsync($"/api/backup/?ipAddress={IpAddress}");
            if (response.IsSuccessStatusCode)
            {
                backups = await response.Content.ReadAsAsync<IEnumerable<ScheduledBackup>>();
            }
            return backups;
        }

        /// <summary>
        /// Registers the new user asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>The status of registration.</returns>
        private async Task<HttpStatusCode> RegisterNewUserAsync(string email, string password)
        {
            var registerModel = new
            {
                Email = email,
                Password = password,
                ConfirmPassword = password
            };
            var response = await _httpClient.PostAsJsonAsync("/api/Account/Register", registerModel);
            return response.StatusCode;
        }

        /// <summary>
        /// Gets the access token.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns>The access token to wrb service.</returns>
        private string GetAccessToken(string userName, string password)
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
            return tokenDictionary["access_token"];
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