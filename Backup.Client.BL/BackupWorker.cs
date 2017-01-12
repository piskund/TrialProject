// -------------------------------------------------------------------------------------------------------------
//  BackupWorker.cs created by DEP on 2017/01/12
// -------------------------------------------------------------------------------------------------------------

using System.Security.Principal;
using Backup.Client.BL.Helpers;
using Backup.Client.BL.Interfaces;
using Backup.Common.Interfaces;
using Backup.Common.Logger;

namespace Backup.Client.BL
{
    /// <summary>
    ///     Performs backup work.
    /// </summary>
    /// <seealso cref="Backup.Client.BL.Interfaces.IBackupWorker" />
    public class BackupWorker : IBackupWorker
    {
        private readonly ILogger _logger;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BackupWorker" /> class.
        /// </summary>
        public BackupWorker(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        ///     Performs the required work.
        /// </summary>
        public void DoWork(IBackupConfig config)
        {
            _logger.LogInfo(
                $"Backup work started. \n Source: {config.SourceFolderPath} \n Destination: {config.DestinationFolderPath} \n");

            PerformBackup(config);

            _logger.LogInfo(
                $"Backup work finished. \n Source: {config.SourceFolderPath} \n Destination: {config.DestinationFolderPath} \n");
        }

        /// <summary>
        ///     Backups the file.
        /// </summary>
        /// <param name="backupConfig">The backup configuration.</param>
        private void PerformBackup(IBackupConfig backupConfig)
        {
            var destinationImpersonationToken = backupConfig.DestinationCredential.GetImpersonationToken();
            using (destinationImpersonationToken)
            {
                // Use the token handle returned by LogonUser.
                using (WindowsIdentity.Impersonate(destinationImpersonationToken.DangerousGetHandle()))
                {
                    FilesManagementHelper.DirectoryCopy(backupConfig);
                }
            }
        }
    }
}