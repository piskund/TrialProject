using Backup.Client.BL.Unity;
using Backup.Common.DTO;

namespace Backup.Client.BL.Interfaces
{
    public interface IBackupWorker
    {
        [ActivityLog]
        void PerformBackup();
    }
}