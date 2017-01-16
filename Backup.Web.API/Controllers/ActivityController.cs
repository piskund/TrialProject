// -------------------------------------------------------------------------------------------------------------
//  ActivityController.cs created by DEP on 2017/01/14
// -------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Web.Http;
using Backup.Common.Entities;
using Backup.DAL.Interfaces;
using CodeContracts;

namespace Backup.Web.API.Controllers
{
    /// <summary>
    /// Allows save/read backup activities.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class ActivityController : ApiController
    {
        private readonly IActivityInfoRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientController"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public ActivityController(IActivityInfoRepository repository)
        {
            Requires.NotNull(repository, nameof(repository));
            _repository = repository;
        }

        /// <summary>
        /// Gets the specified ip address.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IEnumerable<ActivityInfo> Get(string ipAddress)
        {
            Requires.NotNullOrEmpty(ipAddress, nameof(ipAddress));
            return _repository.GetAllByIp(ipAddress);
        }

        /// <summary>
        /// Posts the specified activity.
        /// </summary>
        /// <param name="activity">The activity.</param>
        [HttpPost]
        public void SaveActivity([FromBody] ActivityInfo activity)
        {
            Requires.NotNull(activity, nameof(activity));
            _repository.Add(activity);
        }
    }
}