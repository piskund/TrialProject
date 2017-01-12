// -------------------------------------------------------------------------------------------------------------
//  Program.cs created by DEP on 2017/01/12
// -------------------------------------------------------------------------------------------------------------

using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Backup.Client.Console.App
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IUnityContainer container = new UnityContainer();
            container.LoadConfiguration();
            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(container));

            System.Console.ReadKey();
        }
    }
}