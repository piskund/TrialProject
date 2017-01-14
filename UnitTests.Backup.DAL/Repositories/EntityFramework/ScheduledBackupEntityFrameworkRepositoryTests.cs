// -------------------------------------------------------------------------------------------------------------
//  ScheduledBackupEntityFrameworkRepositoryTests.cs created by DEP on 2017/01/14
// -------------------------------------------------------------------------------------------------------------

using Backup.Common.Entities;
using Backup.DAL.Contexts;
using Backup.DAL.Repositories.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace UnitTests.Backup.DAL.Repositories.EntityFramework
{
    [TestClass]
    public class ScheduledBackupEntityFrameworkRepositoryTests
    {
        [TestMethod]
        public void AddTest()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var contextMock = new Mock<ScheduledBackupContext>();
            var entity = fixture.Freeze<ScheduledBackup>();
            var sut = new ScheduledBackupEntityFrameworkRepository();

            // Act
            sut.Add(entity);

            // Assert
            contextMock.Verify(c => c.Backups.Add(entity), Times.Once);
        }

        [TestMethod]
        public void CountTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void CountTest1()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void DeleteTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void DisposeTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GetAllByIpTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GetAllTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GetAllTest1()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GetQueryableTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GetSingleTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void UpdateTest()
        {
            Assert.Fail();
        }
    }
}