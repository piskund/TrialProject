using System;
using System.Collections.Generic;
using System.Linq;
using Backup.Client.BL;
using Backup.Common.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace UnitTests.Backup.Client.BL
{
    [TestClass]
    public class BackupManagerTests
    {
        [TestMethod]
        public void BackupManagerQueueProducer_SortsScheduledJobsByDateTime()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
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