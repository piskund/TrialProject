namespace Backup.Common.DTO
{
    /// <summary>
    /// Represents result of backup activity.
    /// </summary>
    public enum ActivityStatusType
    {
        /// <summary>
        /// Backup not started
        /// </summary>
        NotStarted,

        /// <summary>
        /// The in progress
        /// </summary>
        InProgress,

        /// <summary>
        /// Backup performed
        /// </summary>
        Succeeded,

        /// <summary>
        /// Backup failed
        /// </summary>
        Failed
    }
}