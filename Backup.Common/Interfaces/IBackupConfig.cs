using System.Net;

namespace Backup.Common.Interfaces
{
    /// <summary>
    /// Provides configuration info necessary to perform backup.
    /// </summary>
    public interface IBackupConfig
    {
        /// <summary>
        /// Gets or sets the client ip address.
        /// </summary>
        /// <value>
        /// The client ip address.
        /// </value>
        string ClientIpAddress { get; }

        /// <summary>
        /// Gets or sets the source folder path.
        /// </summary>
        /// <value>
        /// The source folder path.
        /// </value>
        string SourceFolderPath { get; }

        /// <summary>
        /// Gets or sets the source credential.
        /// </summary>
        /// <value>
        /// The source credential.
        /// </value>
        ICredentialInfo SourceCredential { get; }

        /// <summary>
        /// Gets or sets the destination folder path.
        /// </summary>
        /// <value>
        /// The destination folder path.
        /// </value>
        string DestinationFolderPath { get; }

        /// <summary>
        /// Gets or sets the destination credential.
        /// </summary>
        /// <value>
        /// The destination credential.
        /// </value>
        ICredentialInfo DestinationCredential { get; }
    }
}