// -------------------------------------------------------------------------------------------------------------
//  ActivityInfoRepository.cs created by DEP on 2017/01/15
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
    /// Works with activity info through EF context.
    /// </summary>
    /// <seealso cref="Backup.DAL.EntityFramework.Repositories.BaseEntityFrameworkRepository" />
    /// <seealso cref="Backup.DAL.Interfaces.IActivityInfoRepository" />
    public class ActivityInfoRepository : BaseEntityFrameworkRepository, IActivityInfoRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityInfoRepository"/> class.
        /// </summary>
        public ActivityInfoRepository()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityInfoRepository"/> class.
        /// </summary>
        /// <param name="backupContext">The backup context.</param>
        internal ActivityInfoRepository(BackupContext backupContext) : base(backupContext)
        { }

        /// <summary>
        /// Add entity to the repository
        /// </summary>
        /// <param name="entity">the entity to add</param>
        public void Add(ActivityInfo entity)
        {
            BackupContext.ActivityInfos.Add(entity);
            BackupContext.SaveChanges();
        }

        /// <summary>
        /// Count using a filer
        /// </summary>
        /// <param name="whereCondition">The where condition.</param>
        /// <returns></returns>
        public long Count(Expression<Func<ActivityInfo, bool>> whereCondition)
        {
            return BackupContext.ActivityInfos.Where(whereCondition).Count();
        }

        /// <summary>
        /// All item count
        /// </summary>
        /// <returns></returns>
        public long Count()
        {
            return BackupContext.ActivityInfos.Count();
        }

        /// <summary>
        /// Mark entity to be deleted within the repository
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        public void Delete(ActivityInfo entity)
        {
            BackupContext.ActivityInfos.Remove(entity);
            BackupContext.SaveChanges();
        }

        /// <summary>
        /// Load the entities using a linq expression filter
        /// </summary>
        /// <param name="whereCondition">The where condition.</param>
        /// <returns>
        /// the loaded entity
        /// </returns>
        public IEnumerable<ActivityInfo> GetAll(Expression<Func<ActivityInfo, bool>> whereCondition)
        {
            return BackupContext.ActivityInfos.Where(whereCondition).AsEnumerable();
        }

        /// <summary>
        /// Get all the element of this repository
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ActivityInfo> GetAll()
        {
            return BackupContext.ActivityInfos.AsEnumerable();
        }

        /// <summary>
        /// Query entities from the repository that match the linq expression selection criteria
        /// </summary>
        /// <returns>
        /// the loaded entity
        /// </returns>
        public IQueryable<ActivityInfo> GetQueryable()
        {
            return BackupContext.ActivityInfos.AsQueryable();
        }

        /// <summary>
        /// Get a selected entity by the expression
        /// </summary>
        /// <param name="whereCondition">The where condition.</param>
        /// <returns></returns>
        public ActivityInfo GetSingle(Expression<Func<ActivityInfo, bool>> whereCondition)
        {
            return BackupContext.ActivityInfos.Where(whereCondition).Single();
        }

        /// <summary>
        /// Updates entity within the the repository
        /// </summary>
        /// <param name="entity">the entity to update</param>
        public void Update(ActivityInfo entity)
        {
            BackupContext.Entry(entity).State = EntityState.Modified;
            BackupContext.SaveChanges();
        }

        /// <summary>
        /// Gets all by ip.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns></returns>
        public IEnumerable<ActivityInfo> GetAllByIp(string ipAddress)
        {
            return GetAll(activityInfo => activityInfo.ScheduledBackup.BackupConfig.ClientIpAddress == ipAddress);
        }
    }
}