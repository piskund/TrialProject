using Backup.Common.Logger;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Backup.Client.Console.App.Unity
{
    public class ExceptionHandler : ICallHandler
    {
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            var result = getNext()(input, getNext);
            var logger = ServiceLocator.Current.GetInstance<ILogger>();
            if (result.Exception != null)
            {
                logger.LogException(result.Exception);
            }
            return result;
        }

        public int Order { get; set; }
    }
}