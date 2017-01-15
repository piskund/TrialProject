﻿// -------------------------------------------------------------------------------------------------------------
//  IBackupFacade.cs created by DEP on 2017/01/14
// -------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using Backup.Common.Entities;

namespace Backup.Client.BL.Interfaces
{
    public interface IBackupFacade
    {
        IEnumerable<ScheduledBackup> GetBackups();
    }
}