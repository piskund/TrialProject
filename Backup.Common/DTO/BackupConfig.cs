using System;
using System.Net;

namespace Backup.Common.DTO
{
    /// <summary>
    ///     Provides configuration info necessary to perform backup.
    /// </summary>
    /// <seealso cref="Backup.Common.Interfaces.IBackupConfig" />
    [Serializable]
    public class BackupConfig
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BackupConfig" /> class.
        /// </summary>
        public BackupConfig()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BackupConfig" /> class.
        /// </summary>
        /// <param name="sourceCredential">The source credential.</param>
        /// <param name="destinationCredential">The destination credential.</param>
        public BackupConfig(CredentialInfo sourceCredential, CredentialInfo destinationCredential)
        {
            SourceCredential = sourceCredential;
            DestinationCredential = destinationCredential;
        }

        /// <summary>
        ///     Gets or sets the client ip address.
        /// </summary>
        /// <value>
        ///     The client ip address.
        /// </value>
        public IPAddress ClientIpAddress { get; set; }

        /// <summary>
        ///     Gets or sets the source folder path.
        /// </summary>
        /// <value>
        ///     The source folder path.
        /// </value>
        public string SourceFolderPath { get; set; }

        /// <summary>
        ///     Gets or sets the source credential.
        /// </summary>
        /// <value>
        ///     The source credential.
        /// </value>
        public CredentialInfo SourceCredential { get; set; }

        /// <summary>
        ///     Gets or sets the destination folder path.
        /// </summary>
        /// <value>
        ///     The destination folder path.
        /// </value>
        public string DestinationFolderPath { get; set; }

        /// <summary>
        ///     Gets or sets the destination credential.
        /// </summary>
        /// <value>
        ///     The destination credential.
        /// </value>
        public CredentialInfo DestinationCredential { get; set; }
    }
}