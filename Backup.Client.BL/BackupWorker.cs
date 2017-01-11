using System;
using System.IO;
using System.Security.Principal;
using Backup.Client.BL.Interfaces;
using Backup.Client.BL.Unity;
using Backup.Common.Interfaces;

namespace Backup.Client.BL
{
    public class BackupWorker : IBackupWorker
    {
        private readonly IBackupConfig _backupConfig;

        internal BackupWorker(IBackupConfig backupConfig)
        {
            _backupConfig = backupConfig;
        }

        public void PerformBackup()
        {
            var filesList = Directory.GetFiles(_backupConfig.SourceFolderPath);
            foreach (var sourceFullName in filesList)
            {
                var destinationFullName = Path.Combine(_backupConfig.DestinationFolderPath, Path.GetFileName(sourceFullName));
                BackupFile(sourceFullName, destinationFullName, _backupConfig.SourceCredential);
            }
        }

        [ActivityLog]
        internal void BackupFile(string sourceFullName, string destinationFullName, ICredentialInfo credential)
        {
            //AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
            //var identity = new WindowsIdentity(credential);
            //var context = identity.Impersonate();

            try
            {
                File.Copy(sourceFullName, destinationFullName, true);
            }
            catch (Exception e)
            {
                var tmp = e.Message;
                //context.Undo();
            }
        }
    }
}