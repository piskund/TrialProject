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
    internal class ScheduledBackupEntityFrameworkRepository : IScheduledBackupRepository, IDisposable
    {
        private readonly ScheduledBackupContext _backupContext = new ScheduledBackupContext();

        public void Add(ScheduledBackup entity)
        {
            _backupContext.Backups.Add(entity);
        }

        public long Count(Expression<Func<ScheduledBackup, bool>> whereCondition)
        {
            return _backupContext.Backups.Where(whereCondition).Count();
        }

        public long Count()
        {
            return _backupContext.Backups.Count();
        }

        public void Delete(ScheduledBackup entity)
        {
            _backupContext.Backups.Remove(entity);
        }

        public IEnumerable<ScheduledBackup> GetAll(Expression<Func<ScheduledBackup, bool>> whereCondition)
        {
            return _backupContext.Backups.Where(whereCondition).AsEnumerable();
        }

        public IEnumerable<ScheduledBackup> GetAll()
        {
            return _backupContext.Backups.AsEnumerable();
        }

        public IQueryable<ScheduledBackup> GetQueryable()
        {
            return _backupContext.Backups.AsQueryable();
        }

        public ScheduledBackup GetSingle(Expression<Func<ScheduledBackup, bool>> whereCondition)
        {
            return _backupContext.Backups.Where(whereCondition).Single();
        }

        public void Update(ScheduledBackup entity)
        {
            _backupContext.Entry(entity).State = EntityState.Modified;
        }

        public IEnumerable<ScheduledBackup> GetAllByIp(string ipAddress)
        {
            return GetAll(backup => backup.BackupConfig.ClientIpAddress == ipAddress);
        }

        public ScheduledBackup GetSingleById(int id)
        {
            return GetSingle(backup => backup.Id == id);
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