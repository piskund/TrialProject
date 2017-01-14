// -------------------------------------------------------------------------------------------------------------
//  BackupStrategyCopyFiles.cs created by DEP on 2017/01/12
// -------------------------------------------------------------------------------------------------------------

using System.Security.Principal;
using Backup.Client.BL.Helpers;
using Backup.Client.BL.Interfaces;
using Backup.Common.Interfaces;
using Backup.Common.Logger;

namespace Backup.Client.BL.BackupLogic
{
    /// <summary>
    ///  Performs copying files. This is default backup strategy.
    /// </summary>
    /// <seealso cref="Backup.Client.BL.Interfaces.IBackupStrategy" />
    public class BackupStrategyCopyFiles : IBackupStrategy
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BackupStrategyCopyFiles" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public BackupStrategyCopyFiles(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Performs the required work.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public void DoWork(IBackupConfig config)
        {
            _logger.LogInfo(
                $"Copying files started. \n Source: {config.SourceFolderPath} \n Destination: {config.DestinationFolderPath} \n");

            CopyFilesImpersonated(config);

            _logger.LogInfo(
                $"Copying files finished. \n Source: {config.SourceFolderPath} \n Destination: {config.DestinationFolderPath} \n");
        }

        /// <summary>
        /// Backups the file.
        /// </summary>
        /// <param name="backupConfig">The backup configuration.</param>
        private void CopyFilesImpersonated(IBackupConfig backupConfig)
        {
            var sourceImpersonationToken = backupConfig.SourceCredential.GetImpersonationToken(isCurrentSystemLogon: true);
            var destinationImpersonationToken = backupConfig.DestinationCredential.GetImpersonationToken(isCurrentSystemLogon: false);
            using (sourceImpersonationToken)
            {
                // Use the token handle for the user of the source machine.
                using (WindowsIdentity.Impersonate(sourceImpersonationToken.DangerousGetHandle()))
                {
                    using (destinationImpersonationToken)
                    {
                        // Use the token handle for the user of the destination machine.
                        using (WindowsIdentity.Impersonate(destinationImpersonationToken.DangerousGetHandle()))
                        {
                            FilesManagementHelper.DirectoryCopy(backupConfig);
                        }
                    }
                }
            }
        }
    }
}