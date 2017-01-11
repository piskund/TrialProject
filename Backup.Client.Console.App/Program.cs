using Backup.Client.BL;
using Backup.Client.BL.Interfaces;
using Backup.Common.DTO;
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
            //var worker = ServiceLocator.Current.GetInstance<IWorker>(); // as BackupWorker;
            var worker = new BackupWorker(new BackupConfig() { DestinationFolderPath = "D:\\temp\\tempShare", SourceFolderPath = "D:\\TrialProject\\UnitTests.Backup.Client.BL\\TestData" });
            //worker.BackupConfig.SourceFolderPath = "D:\\TrialProject\\UnitTests.Backup.Client.BL\\TestData";
            //worker.BackupConfig.DestinationFolderPath = "D:\\temp\\tempShare";
            worker.DoWork();
        }
    }
}