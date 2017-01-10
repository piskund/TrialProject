using System;
using Backup.Common.Interfaces;

namespace Backup.Common.POCO
{
    /// <summary>
    /// Credential info.
    /// </summary>
    /// <seealso cref="Backup.Common.Interfaces.ICredentialInfo" />
    [Serializable]
    public class CredentialInfo : ICredentialInfo
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }
    }
}