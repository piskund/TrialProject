using Backup.Client.BL;
using Backup.Common.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTests.Backup.Client.BL
{
    [TestClass]
    public class BackupWorkerTests
    {
        [TestMethod]
        public void DoBackupTest()
        {
            // Arrange
            var configMock = new Mock<IBackupConfig>();
            var backupWorker = new BackupWorker(configMock.Object);

            // Act
            backupWorker.DoBackup();

            // Assert
            configMock.VerifyGet(c => c.SourceFolderPath, Times.AtLeastOnce());
        }
    }
}