using System;
using System.Linq;
using System.Linq.Expressions;
using Backup.Common.Entities;
using Backup.DAL.Interfaces;
using Backup.Web.API.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;
using UnitTests.Backup.Web.API.AutoFixture;

namespace UnitTests.Backup.Web.API.Controllers
{
    [TestClass]
    public class BackupControllerTests
    {
        [TestMethod]
        public void GetWithoutParameters_CallsRepositoryGetAll()
        {
            // Arrange
            var fixture = new Fixture();
            var backups = fixture.CreateMany<ScheduledBackup>();
            var repositoryMock = new Mock<IScheduledBackupRepository>();
            repositoryMock.Setup(r => r.GetAll()).Returns(backups);
            fixture.Register(() => repositoryMock.Object);
            var controller = GetBackupController(fixture);

            // Act
            var result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(backups.Count(), result.Count());
            Assert.IsTrue(backups.SequenceEqual(result));
            repositoryMock.Verify(r => r.GetAll(), Times.Once());
        }

        [TestMethod]
        public void Post_CallsRepositoryAdd()
        {
            // Arrange
            var fixture = new Fixture();
            var backup = fixture.Freeze<ScheduledBackup>();
            var repositoryMock = new Mock<IScheduledBackupRepository>();
            fixture.Register(() => repositoryMock.Object);
            var controller = GetBackupController(fixture);

            // Act
            controller.Post(backup);

            // Assert
            repositoryMock.Verify(r => r.Add(backup), Times.Once());
        }

        [TestMethod]
        public void Put_CallsRepositoryUpdate()
        {
            // Arrange
            var fixture = new Fixture();
            var id = fixture.Freeze<int>();
            var backup = fixture.Freeze<ScheduledBackup>();
            var repositoryMock = new Mock<IScheduledBackupRepository>();
            fixture.Register(() => repositoryMock.Object);
            var controller = GetBackupController(fixture);

            // Act
            controller.Put(id, backup);

            // Assert
            repositoryMock.Verify(r => r.Update(backup), Times.Once());
        }

        [TestMethod]
        public void DeleteById_CallsGetSingleAndDeleteByEntity()
        {
            // Arrange
            var fixture = new Fixture();
            var id = fixture.Freeze<int>();
            var backup = fixture.Freeze<ScheduledBackup>();
            var repositoryMock = new Mock<IScheduledBackupRepository>();
            repositoryMock.Setup(r => r.GetSingle(It.IsAny<Expression<Func<ScheduledBackup, bool>>>())).Returns(backup);
            fixture.Register(() => repositoryMock.Object);

            var controller = GetBackupController(fixture);

            // Act
            controller.Delete(id);

            // Assert
            repositoryMock.Verify(r => r.GetSingle(It.IsAny<Expression<Func<ScheduledBackup, bool>>>()), Times.Once());
            repositoryMock.Verify(r => r.Delete(backup), Times.Once());
        }

        private static BackupController GetBackupController(IFixture fixture)
        {
            return AutofixtureHelpers.GetMockedApiController<BackupController>(fixture);
        }
    }
}