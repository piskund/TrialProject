using Backup.Client.BL.Interfaces;
using Backup.Common.DTO;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Backup.Client.BL.Unity
{
    public class ActivityLogHandler : ICallHandler
    {
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            var result = getNext()(input, getNext);
            if (input.Arguments.ContainsParameter("backupConfig"))
            {
                var config = input.Arguments["backupConfig"] as BackupConfig;
                if (config != null)
                {
                    var activityFacade = ServiceLocator.Current.GetInstance<IActivityFacade>();
                    activityFacade.Save(new ActivityInfo(config));
                }
            }
            return result;
        }

        public int Order { get; set; }
    }
}