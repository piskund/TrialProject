using System;
using System.Collections.Generic;
using System.Net;
using Backup.Client.BL.Interfaces;
using Backup.Common.DTO;

namespace Backup.Client.BL
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