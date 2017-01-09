using Microsoft.VisualStudio.TestTools.UnitTesting;
using Backup.REST.Web.API.Controllers;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests.Backup.REST.Web.API.Controllers
{
    [TestClass()]
    public class BackupControllerTests
    {
        [TestMethod]
        public void Get()
        {
            // Arrange
            BackupController controller = new BackupController();

            // Act
            IEnumerable<string> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("value1", result.ElementAt(0));
            Assert.AreEqual("value2", result.ElementAt(1));
        }

        [TestMethod]
        public void GetById()
        {
            // Arrange
            BackupController controller = new BackupController();

            // Act
            string result = controller.Get(5);

            // Assert
            Assert.AreEqual("value", result);
        }

        [TestMethod]
        public void Post()
        {
            // Arrange
            BackupController controller = new BackupController();

            // Act
            controller.Post("value");

            // Assert
        }

        [TestMethod]
        public void Put()
        {
            // Arrange
            BackupController controller = new BackupController();

            // Act
            controller.Put(5, "value");

            // Assert
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            BackupController controller = new BackupController();

            // Act
            controller.Delete(5);

            // Assert
        }
    }
}
