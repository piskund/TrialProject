// -------------------------------------------------------------------------------------------------------------
//  ClientRegistrationHelper.cs created by DEP on 2017/01/15
// -------------------------------------------------------------------------------------------------------------

using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Backup.Common.DTO;
using Backup.Common.Entities;

namespace Backup.Client.BL.Helpers
{
    public class ClientRegistrationHelper
    { 
        public ClientInfo ClientInfo { get; }

        public ClientRegistrationHelper(string userName, string password, string folderPath)
        {
            var ipAddress = GetIpAddress();
            var localPath = @"\\" + ipAddress;
            ClientInfo = new ClientInfo()
            {
                CredentialInfo = new CredentialInfo {UserName = userName, Password = password},
                ClientIpAddress = ipAddress,
                SharedFolderPath = Path.Combine(localPath, folderPath)
            };
        }

        /// <summary>
        /// Gets the ip address.
        /// </summary>
        /// <returns>The ip address of this machine</returns>
        private static string GetIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());

            var address = host.AddressList.FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork);

            // Return 
            return address.ToString();
        }
    }
}