﻿using System;
using System.IO;
using System.Security.Principal;
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
                BackupFile(sourceFullName, destinationFullName, _backupConfig.SourceCredential);
            }
        }

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