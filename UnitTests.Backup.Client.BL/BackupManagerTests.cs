// -------------------------------------------------------------------------------------------------------------
//  BackupManagerTests.cs created by DEP on 2017/01/12
// -------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Backup.Client.BL.BackupLogic;
using Backup.Client.BL.Helpers;
using Backup.Client.BL.Interfaces;
using Backup.Common.Entities;
using Backup.Common.Logger;
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
        public void BackupManager_CallsDoWorkOfAllBackupStrategies()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var backupStrategyMock = new Mock<IBackupStrategy>();
            backupStrategyMock.Setup(strategy => strategy.DoWork(It.IsAny<BackupConfig>()));
            fixture.Register(() => backupStrategyMock.Object);
            fixture.Register(() => DateTime.UtcNow);
            fixture.Register<ILogger>(() => new ConsoleLogger());
            var backupJobs = fixture.CreateMany<ScheduledBackupJob>();
            var backupManager = fixture.Create<ScheduledJobsManager>();

            // Act
            var t = backupManager.ProceedScheduledJobsAsync(backupJobs);
            t.Wait();

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
            fixture.Register<ILogger>(() => new ConsoleLogger());
            var logger = fixture.Create<ILogger>();
            fixture.Register<IScheduledJob>(() => new DateStoringJob(logger, resultDates));
            var backupJobs = fixture.CreateMany<IScheduledJob>();
            var initialDates = backupJobs.Select(j => j.ScheduledDateTime).ToList();

            var backupManager = fixture.Create<ScheduledJobsManager>();

            // Act
            var t = backupManager.ProceedScheduledJobsAsync(backupJobs);
            t.Wait();

            // Assert
            Assert.IsTrue(initialDates.OrderBy(d => d).SequenceEqual(resultDates));
        }

        private class DateStoringJob : IScheduledJob
        {
            private static int _delay = 50;
            private readonly ILogger _logger;
            private readonly List<DateTime> _resultDates;

            public DateStoringJob(ILogger logger, List<DateTime> resultDates)
            {
                _logger = logger;
                ScheduledDateTime = DateTime.UtcNow.AddMilliseconds(_delay);
                _delay += 100;
                _resultDates = resultDates;
            }

            public void Execute()
            {
                _logger.LogInfo($"Scheduled at {ScheduledDateTime.Second}:{ScheduledDateTime.Millisecond}");
                _logger.LogInfo($"Executed at {DateTime.UtcNow.Second}:{DateTime.UtcNow.Millisecond} \n");
                _resultDates.Add(ScheduledDateTime);
            }

            public DateTime ScheduledDateTime { get; }
        }
    }
}