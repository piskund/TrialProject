using System.Data.Entity;
using Backup.Common.Entities;

namespace Backup.DAL.Contexts
{
    internal class ScheduledBackupContext : DbContext
    {
        public DbSet<ScheduledBackup> Backups { get; set; }
    }
}