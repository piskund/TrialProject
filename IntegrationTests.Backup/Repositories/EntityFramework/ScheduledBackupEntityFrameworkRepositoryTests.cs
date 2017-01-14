// -------------------------------------------------------------------------------------------------------------
//  ScheduledBackupEntityFrameworkRepositoryTests.cs created by DEP on 2017/01/14
// -------------------------------------------------------------------------------------------------------------

using Backup.Common.Entities;
using Backup.DAL.Repositories;
using Backup.DAL.Repositories.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace IntegrationTests.Backup.Repositories.EntityFramework
{
    [TestClass]
    public class ScheduledBackupEntityFrameworkRepositoryTests
    {
        [TestMethod]
        [Ignore]
        public void AddTest()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var entity = fixture.Create<ScheduledBackup>();
            var sut = new ScheduledBackupEntityFrameworkRepository();

            // Act
            sut.Add(entity);
            var newEntity = sut.GetSingleById(entity.Id);

            // Assert
            Assert.AreEqual(entity, newEntity);
        }
    }
}