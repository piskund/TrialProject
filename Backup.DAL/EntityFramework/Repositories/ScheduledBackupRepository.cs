// -------------------------------------------------------------------------------------------------------------
//  ScheduledBackupEntityFrameworkRepository.cs created by DEP on 2017/01/13
// -------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Backup.Common.Entities;
using Backup.DAL.Interfaces;

namespace Backup.DAL.EntityFramework.Repositories
{
    /// <summary>
    /// Works with backup tasks through EF context.
    /// </summary>
    /// <seealso cref="Backup.DAL.EntityFramework.Repositories.BaseEntityFrameworkRepository" />
    /// <seealso cref="Backup.DAL.Interfaces.IScheduledBackupRepository" />
    /// <seealso cref="System.IDisposable" />
    public class ScheduledBackupRepository : BaseEntityFrameworkRepository, IScheduledBackupRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduledBackupRepository"/> class.
        /// </summary>
        public ScheduledBackupRepository()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduledBackupRepository"/> class.
        /// </summary>
        /// <param name="backupContext">The backup context.</param>
        internal ScheduledBackupRepository(BackupContext backupContext) : base(backupContext)
        { }

        /// <summary>
        /// Add entity to the repository
        /// </summary>
        /// <param name="entity">the entity to add</param>
        public void Add(ScheduledBackup entity)
        {
            BackupContext.ScheduledBackups.Add(entity);
            BackupContext.SaveChanges();
        }

        /// <summary>
        /// Count using a filer
        /// </summary>
        /// <param name="whereCondition">The where condition.</param>
        /// <returns></returns>
        public long Count(Expression<Func<ScheduledBackup, bool>> whereCondition)
        {
            return BackupContext.ScheduledBackups.Where(whereCondition).Count();
        }

        /// <summary>
        /// All item count
        /// </summary>
        /// <returns></returns>
        public long Count()
        {
            return BackupContext.ScheduledBackups.Count();
        }

        /// <summary>
        /// Mark entity to be deleted within the repository
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        public void Delete(ScheduledBackup entity)
        {
            BackupContext.ScheduledBackups.Remove(entity);
            BackupContext.SaveChanges();
        }

        /// <summary>
        /// Load the entities using a linq expression filter
        /// </summary>
        /// <param name="whereCondition">The where condition.</param>
        /// <returns>
        /// the loaded entity
        /// </returns>
        public IEnumerable<ScheduledBackup> GetAll(Expression<Func<ScheduledBackup, bool>> whereCondition)
        {
            return BackupContext.ScheduledBackups.Where(whereCondition).AsEnumerable();
        }

        /// <summary>
        /// Get all the element of this repository
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ScheduledBackup> GetAll()
        {
            return BackupContext.ScheduledBackups.AsEnumerable();
        }

        /// <summary>
        /// Query entities from the repository that match the linq expression selection criteria
        /// </summary>
        /// <returns>
        /// the loaded entity
        /// </returns>
        public IQueryable<ScheduledBackup> GetQueryable()
        {
            return BackupContext.ScheduledBackups.AsQueryable();
        }

        /// <summary>
        /// Get a selected entity by the expression
        /// </summary>
        /// <param name="whereCondition">The where condition.</param>
        /// <returns></returns>
        public ScheduledBackup GetSingle(Expression<Func<ScheduledBackup, bool>> whereCondition)
        {
            return BackupContext.ScheduledBackups.Where(whereCondition).SingleOrDefault();
        }

        /// <summary>
        /// Updates entity within the the repository
        /// </summary>
        /// <param name="entity">the entity to update</param>
        public void Update(ScheduledBackup entity)
        {
            BackupContext.Entry(entity).State = EntityState.Modified;
            BackupContext.SaveChanges();
        }

        /// <summary>
        /// Gets all by ip.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns></returns>
        public IEnumerable<ScheduledBackup> GetAllByIp(string ipAddress)
        {
            return GetAll(backup => backup.BackupConfig.ClientIpAddress == ipAddress);
        }
    }
}