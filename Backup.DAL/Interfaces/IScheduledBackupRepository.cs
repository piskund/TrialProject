// -------------------------------------------------------------------------------------------------------------
//  IScheduledBackupRepository.cs created by DEP on 2017/01/13
// -------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using Backup.Common.Entities;

namespace Backup.DAL.Interfaces
{
    /// <summary>
    /// Defines repository contract for ScheduledBackup entity.
    /// </summary>
    /// <seealso cref="ScheduledBackup" />
    public interface IScheduledBackupRepository : IRepository<ScheduledBackup>
    {
        IEnumerable<ScheduledBackup> GetAllByIp(string ipAddress);
    }
}