using System.Web.Mvc;
using Backup.Admin.WebApp.Controllers;
using Backup.DAL.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTests.Backup.Admin.WebApp.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            var repositoryMock = new Mock<IScheduledBackupRepository>();
            var controller = new HomeController(repositoryMock.Object);

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            repositoryMock.Verify(r => r.GetAll(), Times.Once);
        }
    }
}