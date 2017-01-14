// -------------------------------------------------------------------------------------------------------------
//  BackupConfig.cs created by DEP on 2017/01/12
// -------------------------------------------------------------------------------------------------------------

using System;
using Backup.Common.DTO;
using Backup.Common.Interfaces;

namespace Backup.Common.Entities
{
    /// <summary>
    /// Provides configuration info necessary to perform backup.
    /// </summary>
    /// <seealso cref="Backup.Common.Interfaces.IBackupConfig" />
    [Serializable]
    public class BackupConfig : IBackupConfig
    {
        /// <summary>
        /// Gets or sets the client ip address.
        /// </summary>
        /// <value>
        /// The client ip address.
        /// </value>
        public string ClientIpAddress { get; set; }

        /// <summary>
        /// Gets or sets the source credential.
        /// </summary>
        /// <value>
        /// The source credential.
        /// </value>
        public CredentialInfo SourceCredential { get; set; }

        /// <summary>
        /// Gets or sets the destination credential.
        /// </summary>
        /// <value>
        /// The destination credential.
        /// </value>
        public CredentialInfo DestinationCredential { get; set; }

        /// <summary>
        /// Gets or sets the source folder path.
        /// </summary>
        /// <value>
        /// The source folder path.
        /// </value>
        public string SourceFolderPath { get; set; }

        /// <summary>
        /// Gets the source credential (explicit interface implementation).
        /// </summary>
        /// <value>
        /// The source credential.
        /// </value>
        ICredentialInfo IBackupConfig.SourceCredential => SourceCredential;

        /// <summary>
        /// Gets or sets the destination folder path.
        /// </summary>
        /// <value>
        /// The destination folder path.
        /// </value>
        public string DestinationFolderPath { get; set; }

        /// <summary>
        /// Gets the destination credential (explicit interface implementation).
        /// </summary>
        /// <value>
        /// The destination credential.
        /// </value>
        ICredentialInfo IBackupConfig.DestinationCredential => DestinationCredential;
    }
}