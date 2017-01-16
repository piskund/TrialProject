// -------------------------------------------------------------------------------------------------------------
//  Program.cs created by DEP on 2017/01/15
// -------------------------------------------------------------------------------------------------------------

using Backup.Client.BL.Interfaces;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Backup.Client.Console.App
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Unity IoC setup.
            IUnityContainer container = new UnityContainer();
            container.LoadConfiguration();
            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(container));

            // Start listen of web service.
            var listener = ServiceLocator.Current.GetInstance<IListener>();
            listener.StartListen();

            System.Console.ReadKey();
        }
    }
}