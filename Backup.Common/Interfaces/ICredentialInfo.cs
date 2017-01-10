namespace Backup.Common.Interfaces
{
    /// <summary>
    /// Contains credential info.
    /// </summary>
    public interface ICredentialInfo
    {
        /// <summary>
        ///     Gets or sets the name of the user.
        /// </summary>
        /// <value>
        ///     The name of the user.
        /// </value>
        string UserName { get; set; }

        /// <summary>
        ///     Gets or sets the password.
        /// </summary>
        /// <value>
        ///     The password.
        /// </value>
        string Password { get; set; }
    }
}