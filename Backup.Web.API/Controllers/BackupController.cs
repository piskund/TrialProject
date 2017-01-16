// -------------------------------------------------------------------------------------------------------------
//  BackupController.cs created by DEP on 2017/01/12
// -------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Backup.Common.Entities;
using Backup.DAL.Interfaces;
using Backup.DAL.Repositories;
using CodeContracts;

namespace Backup.Web.API.Controllers
{
    /// <summary>
    /// Provides CRUD and enable/disable functionality on backups.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [Authorize]
    public class BackupController : ApiController
    {
        private readonly IScheduledBackupRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BackupController"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public BackupController(IScheduledBackupRepository repository)
        {
            Requires.NotNull(repository, nameof(repository));
            _repository = repository;
        }

        /// <summary>
        /// Gets the list of all backups.
        /// </summary>
        /// <returns>List of backups</returns>
        public IEnumerable<ScheduledBackup> Get()
        {
            return _repository.GetAll();
        }

        /// <summary>
        /// Gets the list of backups.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns>
        /// List of backups by Ip
        /// </returns>
        public IEnumerable<ScheduledBackup> Get(string ipAddress)
        {
            Requires.NotNullOrEmpty(ipAddress, nameof(ipAddress));
            var result = _repository.GetAllByIp(ipAddress).ToList();
            var bcs = result.Select(s => s.BackupConfig).ToList();
            return result;
        }

        /// <summary>
        /// Gets the specified backup.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The backup</returns>
        public ScheduledBackup Get(int id)
        {
            return _repository.GetSingleById(id);
        }

        /// <summary>
        /// Creates the specified backup.
        /// </summary>
        /// <param name="backup">The backup.</param>
        public void Post([FromBody] ScheduledBackup backup)
        {
            Requires.NotNull(backup, nameof(backup));
            _repository.Add(backup);
        }

        /// <summary>
        /// Updates the specified backup.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="backup">The backup.</param>
        public void Put(int id, [FromBody] ScheduledBackup backup)
        {
            Requires.NotNull(backup, nameof(backup));
            Requires.ValidState(id == backup.Id, $"The id : {id} doesn't match to the ScheduledBackup.id : {backup.Id} ");
            _repository.Update(backup);
        }

        /// <summary>
        /// Deletes the specified backup.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            _repository.DeleteById(id);
        }
    }
}