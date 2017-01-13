// -------------------------------------------------------------------------------------------------------------
//  WebApiRequestsManager.cs created by DEP on 2017/01/13
// -------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Backup.Common.Entities;
using Newtonsoft.Json;

namespace Backup.Client.BL
{
    public static class WebApiRequestsManager
    {
        private const string WebServiceUrl = "http://localhost:59901";
        private const string DefaultUserName = "john.dough@fake.com";
        private const string DefaultPassword = "John123#&";
        private static readonly HttpClient HttpClient;

        static WebApiRequestsManager()
        {
            HttpClient = new HttpClient {BaseAddress = new Uri(WebServiceUrl)};
            HttpClient.DefaultRequestHeaders.Accept.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //var status = RegisterNewUserAsync(DefaultUserName, DefaultPassword).Result;

            var token = GetAccessToken(DefaultUserName, DefaultPassword);
            if (!string.IsNullOrWhiteSpace(token))
            {
                HttpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public static async Task<IEnumerable<ScheduledBackup>> GetScheduledBackupsAsync(string ip)
        {
            IEnumerable<ScheduledBackup> backups = null;
            var response = await HttpClient.GetAsync($"/api/backup/?ipAddress={ip}");
            if (response.IsSuccessStatusCode)
            {
                backups = await response.Content.ReadAsAsync<IEnumerable<ScheduledBackup>>();
            }
            return backups;
        }

        private static string GetAccessToken(string userName, string password)
        {
            var tokenDictionary = GetTokenDictionary(userName, password);
            return tokenDictionary["access_token"];
        }

        private static Dictionary<string, string> GetTokenDictionary(string userName, string password)
        {
            var pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", userName),
                new KeyValuePair<string, string>("Password", password)
            };
            var content = new FormUrlEncodedContent(pairs);

            using (var client = new HttpClient())
            {
                var response =
                    client.PostAsync(WebServiceUrl + "/Token", content).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                var tokenDictionary =
                    JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                return tokenDictionary;
            }
        }

        private static async Task<HttpStatusCode> RegisterNewUserAsync(string email, string password)
        {
            var registerModel = new
            {
                Email = email,
                Password = password,
                ConfirmPassword = password
            };
            var response = await HttpClient.PostAsJsonAsync("/api/Account/Register", registerModel);
            return response.StatusCode;
        }
    }
}