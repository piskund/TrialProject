using Backup.Common.Logger;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Backup.Client.BL.Unity
{
    public class ActivityLogHandler : ICallHandler
    {
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            var result = getNext()(input, getNext);
            var logger = ServiceLocator.Current.GetInstance<ILogger>();
            logger.LogActivity(input.MethodBase.Name);
            return result;
        }

        public int Order { get; set; }
    }
}