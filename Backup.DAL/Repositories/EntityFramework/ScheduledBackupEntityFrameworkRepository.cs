// -------------------------------------------------------------------------------------------------------------
//  ScheduledBackupEntityFrameworkRepository.cs created by DEP on 2017/01/13
// -------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Backup.Common.Entities;
using Backup.DAL.Contexts;
using Backup.DAL.Interfaces;

namespace Backup.DAL.Repositories.EntityFramework
{
    /// <summary>
    /// Works with backup tasks through EF context.
    /// </summary>
    /// <seealso cref="Backup.DAL.Interfaces.IScheduledBackupRepository" />
    /// <seealso cref="System.IDisposable" />
    public class ScheduledBackupEntityFrameworkRepository : IScheduledBackupRepository, IDisposable
    {
        /// <summary>
        /// The backup context
        /// </summary>
        private readonly ScheduledBackupContext _backupContext;

        public ScheduledBackupEntityFrameworkRepository() : this(new ScheduledBackupContext())
        {
        }

        internal ScheduledBackupEntityFrameworkRepository(ScheduledBackupContext backupContext)
        {
            _backupContext = backupContext;
        }

        /// <summary>
        /// Add entity to the repository
        /// </summary>
        /// <param name="entity">the entity to add</param>
        public void Add(ScheduledBackup entity)
        {
            _backupContext.ScheduledBackups.Add(entity);
            _backupContext.SaveChanges();
        }

        /// <summary>
        /// Count using a filer
        /// </summary>
        /// <param name="whereCondition">The where condition.</param>
        /// <returns></returns>
        public long Count(Expression<Func<ScheduledBackup, bool>> whereCondition)
        {
            return _backupContext.ScheduledBackups.Where(whereCondition).Count();
        }

        /// <summary>
        /// All item count
        /// </summary>
        /// <returns></returns>
        public long Count()
        {
            return _backupContext.ScheduledBackups.Count();
        }

        /// <summary>
        /// Mark entity to be deleted within the repository
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        public void Delete(ScheduledBackup entity)
        {
            _backupContext.ScheduledBackups.Remove(entity);
            _backupContext.SaveChanges();
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
            return _backupContext.ScheduledBackups.Where(whereCondition).AsEnumerable();
        }

        /// <summary>
        /// Get all the element of this repository
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ScheduledBackup> GetAll()
        {
            return _backupContext.ScheduledBackups.AsEnumerable();
        }

        /// <summary>
        /// Query entities from the repository that match the linq expression selection criteria
        /// </summary>
        /// <returns>
        /// the loaded entity
        /// </returns>
        public IQueryable<ScheduledBackup> GetQueryable()
        {
            return _backupContext.ScheduledBackups.AsQueryable();
        }

        /// <summary>
        /// Get a selected entity by the expression
        /// </summary>
        /// <param name="whereCondition">The where condition.</param>
        /// <returns></returns>
        public ScheduledBackup GetSingle(Expression<Func<ScheduledBackup, bool>> whereCondition)
        {
            return _backupContext.ScheduledBackups.Where(whereCondition).Single();
        }

        /// <summary>
        /// Updates entity within the the repository
        /// </summary>
        /// <param name="entity">the entity to update</param>
        public void Update(ScheduledBackup entity)
        {
            _backupContext.Entry(entity).State = EntityState.Modified;
            _backupContext.SaveChanges();
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

        #region IDisposable Support

        private bool _disposedValue; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _backupContext.Dispose();
                }

                _disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }

        #endregion
    }
}