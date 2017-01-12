using System.Linq;
using Backup.Common.Entities;
using Backup.REST.Web.API.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

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
            var controller = new BackupController();

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
            var controller = new BackupController();
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var backup = fixture.Freeze<ScheduledBackup>();

            // Act
            controller.Post(backup);

            // Assert
        }

        [TestMethod]
        public void Put()
        {
            // Arrange
            var controller = new BackupController();
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var backup = fixture.Freeze<ScheduledBackup>();

            // Act
            controller.Put(backup.Id, backup);

            // Assert
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            var controller = new BackupController();
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var id = fixture.Freeze<int>();

            // Act
            controller.Delete(id);

            // Assert
        }
    }
}