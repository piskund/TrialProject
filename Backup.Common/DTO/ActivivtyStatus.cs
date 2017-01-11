namespace Backup.Common.DTO
{
    /// <summary>
    ///     Represents result of backup activity.
    /// </summary>
    public enum ActivityStatusType
    {
        /// <summary>
        ///     Backup not started
        /// </summary>
        NotStarted,

        /// <summary>
        ///     Backup performed
        /// </summary>
        Succeeded,

        /// <summary>
        ///     Backup failed
        /// </summary>
        Failed
    }
}