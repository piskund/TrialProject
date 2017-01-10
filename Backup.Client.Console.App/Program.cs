using Backup.Common.Logger;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Backup.Client.Console.App
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IUnityContainer unity = new UnityContainer();
            //unity.AddNewExtension<Interception>();
            unity.LoadConfiguration();
            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(unity));
            //var logger1 = unity.Resolve<ILogger>();
            //var logger = ServiceLocator.Current.GetInstance<ILogger>();
            var tmp = new SomeClass();
        }
    }
}