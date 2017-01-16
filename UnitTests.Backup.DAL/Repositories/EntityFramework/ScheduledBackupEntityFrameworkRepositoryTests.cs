// -------------------------------------------------------------------------------------------------------------
//  ScheduledBackupEntityFrameworkRepositoryTests.cs created by DEP on 2017/01/14
// -------------------------------------------------------------------------------------------------------------

using System.Data.Entity;
using Backup.Common.Entities;
using Backup.DAL.EntityFramework;
using Backup.DAL.EntityFramework.Repositories;
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
        public void RepositoryAdd_CreatesScheduledBackupViaContext()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var entity = fixture.Freeze<ScheduledBackup>();
            var mockSet = new Mock<DbSet<ScheduledBackup>>();

            var mockContext = new Mock<BackupContext>();
            mockContext.Setup(m => m.ScheduledBackups).Returns(mockSet.Object);

            var sut = new ScheduledBackupRepository(mockContext.Object);
            sut.Add(entity);

            mockSet.Verify(m => m.Add(entity), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void RepositoryDelete_RemovesScheduledBackupViaContext()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var entity = fixture.Freeze<ScheduledBackup>();
            var mockSet = new Mock<DbSet<ScheduledBackup>>();

            var mockContext = new Mock<BackupContext>();
            mockContext.Setup(m => m.ScheduledBackups).Returns(mockSet.Object);

            var sut = new ScheduledBackupRepository(mockContext.Object);
            sut.Delete(entity);

            mockSet.Verify(m => m.Remove(entity), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}