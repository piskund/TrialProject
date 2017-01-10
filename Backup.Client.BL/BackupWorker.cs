using System.IO;
using Backup.Common.Interfaces;

namespace Backup.Client.BL
{
    public class BackupWorker
    {
        private readonly IBackupConfig _backupConfig;

        internal BackupWorker(IBackupConfig backupConfig)
        {
            _backupConfig = backupConfig;
        }

        public void DoBackup()
        {
            var filesList = Directory.GetFiles(_backupConfig.SourceFolderPath);
            foreach (var sourceFullName in filesList)
            {
                var destinationFullName = Path.Combine(_backupConfig.DestinationFolderPath, Path.GetFileName(sourceFullName));
                BackupFile(sourceFullName, destinationFullName);
            }
        }

        internal void BackupFile(string sourceFullName, string destinationFullName)
        {
        }
    }
}