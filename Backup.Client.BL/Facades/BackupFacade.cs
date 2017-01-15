// -------------------------------------------------------------------------------------------------------------
//  BackupFacade.cs created by DEP on 2017/01/15
// -------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using Backup.Client.BL.Interfaces;
using Backup.Common.Entities;

namespace Backup.Client.BL.Facades
{
    public class BackupFacade : IBackupFacade
    {
        public IEnumerable<ScheduledBackup> GetBackups()
        {
            throw new System.NotImplementedException();
        }
    }
}