using System;
using System.Collections.Generic;
using Backup.Common.Logger;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Backup.Client.Console.App.Unity
{
    public class ExceptionInterceptionBehavior : IInterceptionBehavior
    {
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            var result = getNext()(input, getNext);

            // Log exception if any thrown
            var logger = ServiceLocator.Current.GetInstance<ILogger>();
            if (result.Exception != null)
            {
                logger.LogException(result.Exception);
            }

            return result;
        }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public bool WillExecute => true;
    }
}