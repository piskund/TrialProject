using System.Linq;
using Backup.Common.Entities;
using Backup.Web.API.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using UnitTests.Backup.Web.API.AutoFixture;

namespace UnitTests.Backup.Web.API.Controllers
{
    [TestClass]
    public class BackupControllerTests
    {
        private const string LocalHost = "127.0.0.1";

        [TestMethod]
        public void Get()
        {
            // Arrange
            var controller = GetBackupController();

            // Act
            var result = controller.Get(LocalHost);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.IsInstanceOfType(result.ElementAt(0), typeof(ScheduledBackup));
            Assert.IsInstanceOfType(result.ElementAt(0), typeof(ScheduledBackup));
        }

        [TestMethod]
        public void Post()
        {
            // Arrange
            var fixture = new Fixture();
            var controller = GetBackupController(fixture);
            var backup = fixture.Freeze<ScheduledBackup>();

            // Act
            controller.Post(backup);

            // Assert
        }

        [TestMethod]
        public void Put()
        {
            // Arrange
            var fixture = new Fixture();
            var controller = GetBackupController(fixture);
            var backup = fixture.Freeze<ScheduledBackup>();

            // Act
            controller.Put(backup.Id, backup);

            // Assert
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            var fixture = new Fixture();
            var controller = GetBackupController(fixture);
            var id = fixture.Freeze<int>();

            // Act
            controller.Delete(id);

            // Assert
        }

        private static BackupController GetBackupController(Fixture fixture = null)
        {
            return AutofixtureHelpers.GetMockedApiController<BackupController>(fixture);
        }
    }
}