using System;
using System.IO;
using Backup.Client.BL.Interfaces;
using Backup.Client.BL.Unity;
using Backup.Common.DTO;

namespace Backup.Client.BL
{
    /// <summary>
    ///     Performs backup work.
    /// </summary>
    /// <seealso cref="Backup.Client.BL.Interfaces.IWorker" />
    public class BackupWorker : IWorker
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BackupWorker" /> class.
        /// </summary>
        public BackupWorker()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BackupWorker" /> class.
        /// </summary>
        /// <param name="backupConfig">The backup configuration.</param>
        public BackupWorker(BackupConfig backupConfig)
        {
            BackupConfig = backupConfig;
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
            var filesList = Directory.GetFiles(BackupConfig.SourceFolderPath);
            foreach (var sourceFullName in filesList)
            {
                var destinationFullName = Path.Combine(BackupConfig.DestinationFolderPath,
                    Path.GetFileName(sourceFullName));
                BackupFile(sourceFullName, destinationFullName, BackupConfig);
            }
        }

        /// <summary>
        /// Backups the file.
        /// </summary>
        /// <param name="sourceFullName">Full name of the source.</param>
        /// <param name="destinationFullName">Full name of the destination.</param>
        /// <param name="backupConfig">The backup configuration.</param>
        [ActivityLog]
        internal static void BackupFile(string sourceFullName, string destinationFullName, BackupConfig backupConfig)
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
                throw;
            }
        }
    }
}