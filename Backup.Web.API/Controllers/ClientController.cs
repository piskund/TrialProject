// -------------------------------------------------------------------------------------------------------------
//  ClientController.cs created by DEP on 2017/01/16
// -------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Backup.Common.Entities;
using Backup.DAL.Interfaces;
using Backup.DAL.Models;
using CodeContracts;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Backup.Common.DTO;

namespace Backup.Web.API.Controllers
{
    /// <summary>
    /// Manages clients of backups
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class ClientController : ApiController
    {
        private readonly IClientInfoRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientController"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public ClientController(IClientInfoRepository repository)
        {
            Requires.NotNull(repository, nameof(repository));
            _repository = repository;
        }

        /// <summary>
        /// Gets client info by the specified ip.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns></returns>
        public ClientInfo Get(string ipAddress)
        {
            Requires.NotNullOrEmpty(ipAddress, nameof(ipAddress));
            return _repository.GetSingle(c => c.ClientIpAddress == ipAddress);
        }

        /// <summary>
        /// Gets all registered clients' ips.
        /// </summary>
        /// <returns>Collection of ips.</returns>
        public IEnumerable<ClientInfo> GetAllRegisteredClients()
        {
            return _repository.GetAll();
        }

        /// <summary>
        /// Registers the client.
        /// </summary>
        /// <param name="clientInfo">The client information.</param>
        [HttpPost]
        public async Task<IHttpActionResult> RegisterClient([FromBody] ClientInfo clientInfo)
        {
            Requires.NotNull(clientInfo, nameof(clientInfo));

            var userName = clientInfo.CredentialInfo.UserName;

            IdentityResult result;
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = userName + "@fake.com"
            };

            using (var userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>())
            {
                result = await userManager.CreateAsync(user, clientInfo.CredentialInfo.Password);
            }

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            _repository.Add(clientInfo);

            return Ok();
        }
    }
}