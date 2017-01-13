﻿// -------------------------------------------------------------------------------------------------------------
//  BackupController.cs created by DEP on 2017/01/12
// -------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Web.Http;
using Backup.Common.Entities;

namespace Backup.Web.API.Controllers
{
    /// <summary>
    /// Provides CRUD and enable/disable functionality on backups.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [Authorize]
    public class BackupController : ApiController
    {
        /// <summary>
        /// Gets the list of all backups.
        /// </summary>
        /// <returns>List of backups</returns>
        public IEnumerable<ScheduledBackup> Get()
        {
            return new[] { new ScheduledBackup(DateTime.UtcNow, null), new ScheduledBackup(DateTime.UtcNow, null) };
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
            return new[] {new ScheduledBackup(DateTime.UtcNow, null), new ScheduledBackup(DateTime.UtcNow, null)};
        }

        /// <summary>
        /// Gets the specified backup.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The backup</returns>
        public ScheduledBackup Get(int id)
        {
            return new ScheduledBackup(DateTime.UtcNow, null) {Id = id};
        }

        /// <summary>
        /// Creates the specified backup.
        /// </summary>
        /// <param name="backup">The backup.</param>
        public void Post([FromBody] ScheduledBackup backup)
        {
        }

        /// <summary>
        /// Updates the specified backup.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="backup">The backup.</param>
        public void Put(int id, [FromBody] ScheduledBackup backup)
        {
        }

        /// <summary>
        /// Deletes the specified backup.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
        }
    }
}