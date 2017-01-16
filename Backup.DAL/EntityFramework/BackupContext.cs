// -------------------------------------------------------------------------------------------------------------
//  ScheduledBackupContext.cs created by DEP on 2017/01/14
// -------------------------------------------------------------------------------------------------------------

using System.Data.Entity;
using Backup.Common.Entities;

namespace Backup.DAL.EntityFramework
{
    /// <summary>
    /// EF context to manipulate with backup info and related.
    /// </summary>
    /// <seealso cref="System.Data.Entity.DbContext" />
    public class BackupContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BackupContext"/> class.
        /// </summary>
        public BackupContext() : base("BackupConnection")
        {
        }

        /// <summary>
        /// Gets or sets the scheduled backups.
        /// </summary>
        /// <value>
        /// The scheduled backups.
        /// </value>
        public virtual DbSet<ScheduledBackup> ScheduledBackups { get; set; }

        /// <summary>
        /// Gets or sets the activity infos.
        /// </summary>
        /// <value>
        /// The activity infos.
        /// </value>
        public virtual DbSet<ActivityInfo> ActivityInfos { get; set; }

        /// <summary>
        /// Gets or sets the client infos.
        /// </summary>
        /// <value>
        /// The client infos.
        /// </value>
        public virtual DbSet<ClientInfo> ClientInfos { get; set; }
    }
}