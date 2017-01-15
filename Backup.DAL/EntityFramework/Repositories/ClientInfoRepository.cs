// -------------------------------------------------------------------------------------------------------------
//  ClientInfoRepository.cs created by DEP on 2017/01/15
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
    public class ClientInfoRepository : BaseEntityFrameworkRepository, IClientInfoRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientInfoRepository"/> class.
        /// </summary>
        public ClientInfoRepository()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientInfoRepository"/> class.
        /// </summary>
        /// <param name="backupContext">The backup context.</param>
        internal ClientInfoRepository(BackupContext backupContext) : base(backupContext)
        {
        }

        /// <summary>
        /// Add entity to the repository
        /// </summary>
        /// <param name="entity">the entity to add</param>
        public void Add(ClientInfo entity)
        {
            BackupContext.ClientInfos.Add(entity);
            BackupContext.SaveChanges();
        }

        /// <summary>
        /// Count using a filer
        /// </summary>
        /// <param name="whereCondition">The where condition.</param>
        /// <returns></returns>
        public long Count(Expression<Func<ClientInfo, bool>> whereCondition)
        {
            return BackupContext.ClientInfos.Where(whereCondition).Count();
        }

        /// <summary>
        /// All item count
        /// </summary>
        /// <returns></returns>
        public long Count()
        {
            return BackupContext.ClientInfos.Count();
        }

        /// <summary>
        /// Mark entity to be deleted within the repository
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        public void Delete(ClientInfo entity)
        {
            BackupContext.ClientInfos.Remove(entity);
            BackupContext.SaveChanges();
        }

        /// <summary>
        /// Load the entities using a linq expression filter
        /// </summary>
        /// <param name="whereCondition">The where condition.</param>
        /// <returns>
        /// the loaded entity
        /// </returns>
        public IEnumerable<ClientInfo> GetAll(Expression<Func<ClientInfo, bool>> whereCondition)
        {
            return BackupContext.ClientInfos.Where(whereCondition).AsEnumerable();
        }

        /// <summary>
        /// Get all the element of this repository
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ClientInfo> GetAll()
        {
            return BackupContext.ClientInfos.AsEnumerable();
        }

        /// <summary>
        /// Query entities from the repository that match the linq expression selection criteria
        /// </summary>
        /// <returns>
        /// the loaded entity
        /// </returns>
        public IQueryable<ClientInfo> GetQueryable()
        {
            return BackupContext.ClientInfos.AsQueryable();
        }

        /// <summary>
        /// Get a selected entity by the expression
        /// </summary>
        /// <param name="whereCondition">The where condition.</param>
        /// <returns></returns>
        public ClientInfo GetSingle(Expression<Func<ClientInfo, bool>> whereCondition)
        {
            return BackupContext.ClientInfos.Where(whereCondition).Single();
        }

        /// <summary>
        /// Updates entity within the the repository
        /// </summary>
        /// <param name="entity">the entity to update</param>
        public void Update(ClientInfo entity)
        {
            BackupContext.Entry(entity).State = EntityState.Modified;
            BackupContext.SaveChanges();
        }

        /// <summary>
        /// Gets all by ip.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns></returns>
        public IEnumerable<ClientInfo> GetAllByIp(string ipAddress)
        {
            return GetAll(clientInfo => clientInfo.ClientIpAddress == ipAddress);
        }
    }
}