// -------------------------------------------------------------------------------------------------------------
//  BackupManagerTests.cs created by DEP on 2017/01/12
// -------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Backup.Client.BL;
using Backup.Client.BL.BackupLogic;
using Backup.Client.BL.Interfaces;
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
        public void BackupManager_CallsOnce_DoWorkOfAllBackupStrategies()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var backupStrategyMock = new Mock<IBackupStrategy>();
            backupStrategyMock.Setup(worker => worker.DoWork(It.IsAny<IBackupConfig>()));
            fixture.Register(() => backupStrategyMock.Object);
            var backupJobs = fixture.CreateMany<ScheduledBackupJob>();
            var backupManager = new BackupManager(backupJobs);

            // Act
            backupManager.ExecuteJobs(false);

            // Assert
            foreach (var backupJob in backupJobs)
            {
                backupStrategyMock.Verify(s => s.DoWork(backupJob.BackupConfig), Times.Once);
            }
        }

        [TestMethod]
        public void BackupManager_SortsScheduledJobsByDateTime()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var resultDates = new List<DateTime>();
            fixture.Register<IScheduledBackupJob>(() => new FakeJob(resultDates, fixture.Create<DateTime>()));
            var backupJobs = fixture.CreateMany<IScheduledBackupJob>();
            var initialDates = backupJobs.Select(j => j.ScheduledDateTime).ToList();
            var backupManager = new BackupManager(backupJobs);

            // Act
            backupManager.ExecuteJobs(false);

            // Assert
            Assert.IsTrue(initialDates.OrderBy(d => d).SequenceEqual(resultDates));
        }

        private class FakeJob : IScheduledBackupJob
        {
            private readonly List<DateTime> _resultDates;

            public FakeJob(List<DateTime> resultDates, DateTime scheduledDateTime)
            {
                _resultDates = resultDates;
                ScheduledDateTime = scheduledDateTime;
            }

            public void Execute()
            {
                _resultDates.Add(ScheduledDateTime);
            }

            public DateTime ScheduledDateTime { get; }

            public IBackupConfig BackupConfig { get; }
        }
    }
}