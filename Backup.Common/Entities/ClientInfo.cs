// -------------------------------------------------------------------------------------------------------------
//  ClientInfo.cs created by DEP on 2017/01/15
// -------------------------------------------------------------------------------------------------------------

using Backup.Common.DTO;
using Backup.Common.Interfaces;

namespace Backup.Common.Entities
{
    /// <summary>
    /// Defines info about client machine of the backup system.
    /// </summary>
    /// <seealso cref="Backup.Common.Interfaces.IEntity" />
    public class ClientInfo : IEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the client ip address.
        /// </summary>
        /// <value>
        /// The client ip address.
        /// </value>
        public string ClientIpAddress { get; set; }

        /// <summary>
        /// Gets or sets the credential information.
        /// </summary>
        /// <value>
        /// The credential information.
        /// </value>
        public CredentialInfo CredentialInfo { get; set; }

        /// <summary>
        /// Gets or sets the shared folder path.
        /// </summary>
        /// <value>
        /// The shared folder path.
        /// </value>
        public string SharedFolderPath { get; set; }
    }
}