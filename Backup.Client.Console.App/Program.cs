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

            //var worker = ServiceLocator.Current.GetInstance<IWorker>() as BackupWorker;
            //worker.BackupConfig.SourceFolderPath = @"D:\TrialProject\UnitTests.Backup.Client.BL\TestData";
            //worker.BackupConfig.DestinationFolderPath = @"\\192.168.0.100\share1";
            //worker.BackupConfig.DestinationCredential = new CredentialInfo {UserName = "StandardUsr", Password = "123456"};
            //worker.DoWork();

            //var bm = new BackupManager();
            //bm.Execute();
            //bm.Execute();
            //bm.Execute();

            System.Console.ReadKey();
        }
    }
}