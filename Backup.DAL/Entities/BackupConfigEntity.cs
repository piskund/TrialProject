// -------------------------------------------------------------------------------------------------------------
//  BackupConfig.cs created by DEP on 2017/01/12
// -------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations.Schema;
using Backup.Common.Entities;

namespace Backup.DAL.Entities
{
    /// <summary>
    /// Provides configuration info necessary to perform backup.
    /// </summary>
    [Serializable]
    internal class BackupConfigEntity : BackupConfig
    {
        /// <summary>
        /// Gets or sets the scheduled backup identifier.
        /// </summary>
        /// <value>
        /// The scheduled backup identifier.
        /// </value>
        [ForeignKey("ScheduledBackup")]
        public int ScheduledBackupId { get; set; }

        /// <summary>
        /// Gets or sets the scheduled backup.
        /// </summary>
        /// <value>
        /// The scheduled backup.
        /// </value>
        public virtual ScheduledBackup ScheduledBackup { get; set; }
    }
}