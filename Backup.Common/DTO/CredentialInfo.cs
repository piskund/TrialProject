using System;
using Backup.Common.Interfaces;

namespace Backup.Common.DTO
{
    /// <summary>
    /// Credential info.
    /// </summary>
    [Serializable]
    public class CredentialInfo 
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