using System.Collections.Generic;
using System.Web.Http;

namespace Backup.REST.Web.API.Controllers
{
    /// <summary>
    ///     Provides CRUD and enable/disable functionality on backups.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class BackupController : ApiController
    {
        /// <summary>
        ///     Gets list of backups.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> Get()
        {
            return new[] {"value1", "value2"};
        }

        /// <summary>
        ///     Gets the specified backup.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>backup</returns>
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        ///     Creates the specified backup.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Post([FromBody] string value)
        {
        }

        /// <summary>
        ///     Updates the specified backup.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        public void Put(int id, [FromBody] string value)
        {
        }

        /// <summary>
        ///     Deletes the specified backup.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
        }
    }
}