// -------------------------------------------------------------------------------------------------------------
//  BackupControllerTests.cs created by DEP on 2017/01/15
// -------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading;
using Backup.Client.BL.BackupLogic;
using Backup.Client.BL.Helpers;
using Backup.Client.BL.Interfaces;
using Backup.Common.Entities;
using Backup.Common.Logger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace UnitTests.Backup.Client.BL.BackupLogic
{
    [TestClass]
    public class BackupControllerTests
    {
        [TestMethod]
        public void StartListenBackupProviderTest()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            // setup BackupFacade mock
            fixture.Register(() => DateTime.UtcNow);
            var scheduledBackups = fixture.CreateMany<ScheduledBackup>();
            var backupConfigs = scheduledBackups.Select(s => s.BackupConfig);
            var remoteFacadeMock = new Mock<IRemoteRequestsFacade>();
            remoteFacadeMock.Setup(facade => facade.GetBackups()).Returns(scheduledBackups);


            // setup backupStrategy mock
            var backupStrategyMock = new Mock<IBackupStrategy>();
            backupStrategyMock.Setup(strategy => strategy.DoWork(It.IsAny<BackupConfig>()));
            fixture.Register(() => backupStrategyMock.Object);
            fixture.Register(() => remoteFacadeMock.Object);
            fixture.Register<ILogger>(() => new ConsoleLogger());

            const int timerInterval = 30;
            const int repeatNumber = 3;
            var sut = fixture.Create<BackupController>();

            // Act
            sut.StartListen(timerInterval);
            Thread.Sleep(timerInterval * (repeatNumber + 1));

            // Assert
            remoteFacadeMock.Verify(b => b.GetBackups(), Times.AtLeast(repeatNumber));
            backupStrategyMock.Verify(s => s.DoWork(It.IsIn(backupConfigs)), Times.Exactly(scheduledBackups.Count()));
        }
    }
}