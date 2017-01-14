// -------------------------------------------------------------------------------------------------------------
//  ActivityController.cs created by DEP on 2017/01/14
// -------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Web.Http;
using Backup.Common.Entities;

namespace Backup.Web.API.Controllers
{
    /// <summary>
    /// Allows save/read backup activities.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class ActivityController : ApiController
    {
        /// <summary>
        /// Gets the specified ip address.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IEnumerable<ActivityInfo> Get(string ipAddress)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Posts the specified activity.
        /// </summary>
        /// <param name="activity">The activity.</param>
        public void Post([FromBody] ActivityInfo activity)
        {
        }

        /// <summary>
        /// Posts the specified activities.
        /// </summary>
        /// <param name="activities">The activities.</param>
        public void Post([FromBody] IEnumerable<ActivityInfo> activities)
        {
        }
    }
}