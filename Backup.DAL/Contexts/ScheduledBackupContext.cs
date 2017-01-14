// -------------------------------------------------------------------------------------------------------------
//  ScheduledBackupContext.cs created by DEP on 2017/01/14
// -------------------------------------------------------------------------------------------------------------

using System.Data.Entity;
using Backup.Common.Entities;

namespace Backup.DAL.Contexts
{
    public class ScheduledBackupContext : DbContext
    {
        public ScheduledBackupContext() : base("BackupConnection")
        {
        }

        /// <summary>
        /// Gets or sets the scheduled backups.
        /// </summary>
        /// <value>
        /// The scheduled backups.
        /// </value>
        public DbSet<ScheduledBackup> ScheduledBackups { get; set; }
    }
}