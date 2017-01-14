// -------------------------------------------------------------------------------------------------------------
//  EntityRepositoryExtension.cs created by DEP on 2017/01/14
// -------------------------------------------------------------------------------------------------------------

using System;
using Backup.Common.Interfaces;
using Backup.DAL.Interfaces;

namespace Backup.DAL.Repositories
{
    /// <summary>
    /// Provides some useful extensions allow to operate with entities by integer id.
    /// </summary>
    public static class EntityRepositoryExtension
    {
        /// <summary>
        /// Gets the single by identifier.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="id">The identifier.</param>
        /// <exception cref="System.ArgumentException">Throws if entity with given id doesn't exist.</exception>
        /// <returns>The entity, if any found.</returns>
        public static T GetSingleById<T>(this IRepository<T> repository, int id) where T : IEntity
        {
            var entity = repository.GetSingle(e => e.Id == id);
            if (entity == null)
            {
                throw new ArgumentException($"Entity with id {id} not found");
            }

            return entity;
        }

        /// <summary>
        /// Deletes the by identifier.
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="id">The identifier.</param>
        /// <exception cref="System.ArgumentException">Throws if entity with given id doesn't exist.</exception>
        public static void DeleteById<T>(this IRepository<T> repository, int id) where T : IEntity
        {
            var entity = repository.GetSingleById(id);

            repository.Delete(entity);
        }
    }
}