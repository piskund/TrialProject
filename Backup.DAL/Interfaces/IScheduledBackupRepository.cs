// -------------------------------------------------------------------------------------------------------------
//  IScheduledBackupRepository.cs created by DEP on 2017/01/13
// -------------------------------------------------------------------------------------------------------------

using Backup.Common.Entities;

namespace Backup.DAL.Interfaces
{
    /// <summary>
    /// Defines repository contract for ScheduledBackup entity.
    /// </summary>
    public interface IScheduledBackupRepository : IRepository<ScheduledBackup>
    {
    }
}