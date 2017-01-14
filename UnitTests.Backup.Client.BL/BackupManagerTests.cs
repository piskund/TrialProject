// -------------------------------------------------------------------------------------------------------------
//  BackupManagerTests.cs created by DEP on 2017/01/12
// -------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Backup.Client.BL.BackupLogic;
using Backup.Client.BL.Interfaces;
using Backup.Common.Entities;
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
            var backupJobs = fixture.CreateMany<ScheduledBackupJob>();
            var backupManager = new ScheduledJobManager(backupJobs);

            // Act
            var t = backupManager.StartAsync(new CancellationToken(), false);
            t.Wait();

            // Assert
            foreach (var backupJob in backupJobs)
            {
                backupStrategyMock.Verify(s => s.DoWork(backupJob.BackupConfig), Times.Once);
            }
        }

        [TestMethod]
        public void BackupManager_ExecutesScheduledJobsByDateTime()
        {
            // Arrange
            var backupJobs = new List<IScheduledJob>();

            for (var i = 0; i < 3; i++)
            {
                backupJobs.Add(new DelayedJob());
            }
            var backupManager = new ScheduledJobManager(backupJobs);

            // Act
            var task = backupManager.StartAsync(new CancellationToken(), false);
            // Emulate current thread work.
            Thread.Sleep(300);

            // Assert
            Assert.IsTrue(task.IsCompleted);
        }

        [TestMethod]
        public void BackupManager_SortsScheduledJobsByDateTime()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var resultDates = new List<DateTime>();
            fixture.Register<IScheduledJob>(() => new DateStoringJob(resultDates, fixture.Create<DateTime>()));
            var backupJobs = fixture.CreateMany<IScheduledJob>();
            var initialDates = backupJobs.Select(j => j.ScheduledDateTime).ToList();
            var backupManager = new ScheduledJobManager(backupJobs);

            // Act
            var t = backupManager.StartAsync(new CancellationToken(), false);
            t.Wait();

            // Assert
            Assert.IsTrue(initialDates.OrderBy(d => d).SequenceEqual(resultDates));
        }

        private class DateStoringJob : IScheduledJob
        {
            private readonly List<DateTime> _resultDates;

            public DateStoringJob(List<DateTime> resultDates, DateTime scheduledDateTime)
            {
                _resultDates = resultDates;
                ScheduledDateTime = scheduledDateTime;
            }

            public void Execute()
            {
                _resultDates.Add(ScheduledDateTime);
            }

            public DateTime ScheduledDateTime { get; }
        }

        private class DelayedJob : IScheduledJob
        {
            private static int _delay = 50;

            public DelayedJob()
            {
                ScheduledDateTime = DateTime.UtcNow.AddMilliseconds(_delay);
                _delay += 100;
            }

            public void Execute()
            {
                Console.WriteLine($"Scheduled at {ScheduledDateTime.Second}:{ScheduledDateTime.Millisecond}");
                Console.WriteLine($"Executed at {DateTime.UtcNow.Second}:{DateTime.UtcNow.Millisecond}");
                Console.WriteLine();
            }

            public DateTime ScheduledDateTime { get; }
        }
    }
}