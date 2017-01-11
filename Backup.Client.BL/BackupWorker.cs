using System.Security.Principal;
using Backup.Client.BL.Helpers;
using Backup.Client.BL.Interfaces;
using Backup.Client.BL.Unity;
using Backup.Common.DTO;
using Backup.Common.Logger;
using CodeContracts;

namespace Backup.Client.BL
{
    /// <summary>
    ///     Performs backup work.
    /// </summary>
    /// <seealso cref="Backup.Client.BL.Interfaces.IWorker" />
    public class BackupWorker : IWorker
    {
        private readonly ILogger _logger;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BackupWorker" /> class.
        /// </summary>
        public BackupWorker(ILogger logger) : this(logger, new BackupConfig())
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BackupWorker" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="backupConfig">The backup configuration.</param>
        public BackupWorker(ILogger logger, BackupConfig backupConfig)
        {
            Requires.NotNull(logger, nameof(logger));
            Requires.NotNull(backupConfig, nameof(backupConfig));
            BackupConfig = backupConfig;
            _logger = logger;
        }

        /// <summary>
        ///     Gets or sets the backup configuration.
        /// </summary>
        /// <value>
        ///     The backup configuration.
        /// </value>
        public BackupConfig BackupConfig { get; set; }

        /// <summary>
        ///     Performs the required work.
        /// </summary>
        public void DoWork()
        {
            _logger.LogInfo(
                $"Backup work started. \n Source: {BackupConfig.SourceFolderPath} \n Destination: {BackupConfig.DestinationFolderPath} \n");
            PerformBackup(BackupConfig);
            _logger.LogInfo(
                $"Backup work finished. \n Source: {BackupConfig.SourceFolderPath} \n Destination: {BackupConfig.DestinationFolderPath} \n");
        }

        /// <summary>
        ///     Backups the file.
        /// </summary>
        /// <param name="backupConfig">The backup configuration.</param>
        [ActivityLog]
        private void PerformBackup(BackupConfig backupConfig)
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