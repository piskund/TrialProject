// -------------------------------------------------------------------------------------------------------------
//  ClientController.cs created by DEP on 2017/01/15
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
        /// Gets the specified ip.
        /// </summary>
        /// <param name="ip">The ip.</param>
        /// <returns></returns>
        public ClientInfo Get(string ip)
        {
            Requires.NotNullOrEmpty(ip, nameof(ip));
            return _repository.GetSingle(c => c.ClientIpAddress == ip);
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

            IdentityResult result;
            var user = new ApplicationUser { UserName = clientInfo.CredentialInfo.UserName, Email = clientInfo.CredentialInfo.UserName };

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