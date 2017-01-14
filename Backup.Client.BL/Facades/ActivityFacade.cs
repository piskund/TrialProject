// -------------------------------------------------------------------------------------------------------------
//  ActivityFacade.cs created by DEP on 2017/01/12
// -------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net;
using Backup.Client.BL.Interfaces;
using Backup.Common.Entities;

namespace Backup.Client.BL.Facades
{
    public class ActivityFacade : IActivityFacade
    {
        public void Save(ActivityInfo activityInfo)
        {
            throw new NotImplementedException();
        }

        public void SaveBatch(IEnumerable<ActivityInfo> activities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ActivityInfo> GetAllActivities(IPAddress address)
        {
            throw new NotImplementedException();
        }
    }
}