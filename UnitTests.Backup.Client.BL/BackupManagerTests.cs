using System;
using System.Collections.Generic;
using System.Linq;
using Backup.Client.BL;
using Backup.Client.BL.Interfaces;
using Backup.Common.DTO;
using Backup.Common.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace UnitTests.Backup.Client.BL
{
    [TestClass]
    public class BackupManagerTests
    {
        [TestMethod]
        public void BackupManager_CallsOnce_DoWorkOfAllBackupWorkers()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var backupWorkerMock = new Mock<IBackupWorker>();
            backupWorkerMock.Setup(worker => worker.DoWork(It.IsAny<IBackupConfig>()));

            var backupJobs = fixture.CreateMany<ScheduledBackup>();
            var backupManager = new BackupManager(backupJobs, backupWorkerMock.Object);

            // Act
            backupManager.ExecuteAll();

            // Assert
            foreach (var backupJob in backupJobs)
            {
                backupWorkerMock.Verify(w => w.DoWork(backupJob.BackupConfig),Times.Once);
            }
        }

        [TestMethod]
        public void BackupManager_SortsScheduledJobsByDateTime()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var backupWorkerMock = new Mock<IBackupWorker>();
            backupWorkerMock.Setup(worker => worker.DoWork(It.IsAny<IBackupConfig>()));

            var backupJobs = fixture.CreateMany<ScheduledBackup>();
            var initialDates = backupJobs.Select(j => j.ScheduledDateTime).ToList();
            var resultDates = new List<DateTime>();
            var backupManager = new BackupManager(backupJobs, job => resultDates.Add(job.ScheduledDateTime));

            // Act
            backupManager.ExecuteAll();

            // Assert
            Assert.IsTrue(initialDates.OrderBy(d => d).SequenceEqual(resultDates));
        }
    }
}