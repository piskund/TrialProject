// -------------------------------------------------------------------------------------------------------------
//  ClientController.cs created by DEP on 2017/01/15
// -------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Backup.DAL.Interfaces;
using CodeContracts;

namespace Backup.Web.API.Controllers
{
    /// <summary>
    /// Manages clients of backups
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class ClientController : ApiController
    {
        private readonly IScheduledBackupRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BackupController"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public ClientController(IScheduledBackupRepository repository)
        {
            Requires.NotNull(repository, nameof(repository));
            _repository = repository;
        }

        /// <summary>
        /// Gets all registered clients' ips.
        /// </summary>
        /// <returns>Collection of ips.</returns>
        [HttpGet]
        public IEnumerable<string> GetAllRegisteredClients()
        {
            return _repository.GetAll().Select(b => b.BackupConfig.ClientIpAddress).Distinct();
        }

        /// <summary>
        /// Determines whether [is client registered] [the specified ip].
        /// </summary>
        /// <param name="ip">The ip.</param>
        /// <returns>
        ///   <c>true</c> if [is client registered] [the specified ip]; otherwise, <c>false</c>.
        /// </returns>
        [HttpGet]
        public bool IsClientRegistered(string ip)
        {
            Requires.NotNullOrEmpty(ip, nameof(ip));
            return _repository.GetAll(s => s.BackupConfig.ClientIpAddress.Contains(ip)).Any();
        }
    }
}