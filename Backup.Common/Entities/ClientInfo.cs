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
        public int Id { get; set; }

        public string ClientIpAddress { get; set; }

        public CredentialInfo CredentialInfo { get; set; }

        public string ShareFolderPath { get; set; }
    }
}