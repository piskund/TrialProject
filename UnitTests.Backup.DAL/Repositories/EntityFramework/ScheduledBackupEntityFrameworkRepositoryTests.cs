// -------------------------------------------------------------------------------------------------------------
//  ScheduledBackupEntityFrameworkRepositoryTests.cs created by DEP on 2017/01/14
// -------------------------------------------------------------------------------------------------------------

using System.Data.Entity;
using System.Linq;
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
            var dbSet = new Mock<DbSet<ScheduledBackup>>();
            fixture.Register(() => dbSet.Object);
            var context = fixture.Create<ScheduledBackupContext>();
            var entity = fixture.Freeze<ScheduledBackup>();
            var sut = new ScheduledBackupEntityFrameworkRepository(context);

            // Act
            sut.Add(entity);

            // Assert
            dbSet.Verify(d => d.Add(entity), Times.Once);
        }

        [TestMethod]
        public void DeleteTest()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var dbSet = new Mock<DbSet<ScheduledBackup>>();
            fixture.Register(() => dbSet.Object);
            var context = fixture.Create<ScheduledBackupContext>();
            var entity = fixture.Freeze<ScheduledBackup>();
            var sut = new ScheduledBackupEntityFrameworkRepository(context);

            // Act
            sut.Delete(entity);

            // Assert
            dbSet.Verify(d => d.Remove(entity), Times.Once);
        }
    }
}