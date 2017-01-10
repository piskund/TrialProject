using System.IO;
using System.Linq;
using Backup.Client.BL;
using Backup.Common.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace UnitTests.Backup.Client.BL.FunctionalTests
{
    [TestClass]
    public class BackupWorkerTests
    {
        private readonly string _sourcePath = Path.GetFullPath("..\\..\\TestData");
        private readonly string _destinationPath = "D:\\temp\\testShare";
        private const int TestFilesNumber = 2;
        //private readonly string _destinationPath = Path.GetTempPath();

        [TestMethod]
        public void DoBackup_SavesToDestination()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var backupConfig = fixture.Create<BackupConfig>();
            backupConfig.SourceFolderPath = _sourcePath;
            backupConfig.DestinationFolderPath = _destinationPath;
            var backupWorker = new BackupWorker(backupConfig);

            // Act
            backupWorker.DoBackup();

            // Assert
            var filesInSource = Directory.EnumerateFiles(_sourcePath);
            Assert.IsNotNull(filesInSource);
            Assert.AreEqual(TestFilesNumber, filesInSource.Count());
            var filesInDestination = Directory.EnumerateFiles(_destinationPath);
            Assert.IsNotNull(filesInDestination);
            Assert.AreEqual(TestFilesNumber, filesInDestination.Count());
            foreach (var fileName in filesInSource.Select(Path.GetFileName))
            {
                Assert.IsTrue(filesInDestination.Contains(fileName));
            }
        }
    }
}