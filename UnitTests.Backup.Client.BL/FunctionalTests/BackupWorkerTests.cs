using System.IO;
using System.Linq;
using Backup.Client.BL;
using Backup.Common.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Backup.Client.BL.FunctionalTests
{
    [TestClass]
    public class BackupWorkerTests
    {
        private const int TestFilesNumber = 2;
        private static readonly string SourcePath = Path.GetFullPath("..\\..\\TestData");
        private static readonly string DestinationPath = Path.Combine(Path.GetTempPath(), "TestSharedFolder");

        [ClassInitialize]
        public static void SetUpTests(TestContext context)
        {
            // Create temporary destination directory if doesn't exist.
            Directory.CreateDirectory(DestinationPath);
        }

        [ClassCleanup]
        public static void TearDownTests()
        {
            // Delete temporary destination directory if exists.
            if (Directory.Exists(DestinationPath))
                Directory.Delete(DestinationPath, true);
        }

        [TestMethod]
        public void DoBackup_SavesTestDataToTempDestination()
        {
            // Arrange
            var backupConfig = new BackupConfig
            {
                SourceFolderPath = SourcePath,
                DestinationFolderPath = DestinationPath
            };
            var backupWorker = new BackupWorker(backupConfig);

            // Act
            backupWorker.DoWork();

            // Assert
            var filesInSource = Directory.EnumerateFiles(SourcePath);
            Assert.IsNotNull(filesInSource);
            Assert.AreEqual(TestFilesNumber, filesInSource.Count());
            var filesInDestination = Directory.EnumerateFiles(DestinationPath);
            Assert.IsNotNull(filesInDestination);
            Assert.AreEqual(TestFilesNumber, filesInDestination.Count());
            foreach (var fileName in filesInSource.Select(Path.GetFileName))
                Assert.IsTrue(filesInDestination.Any(f => f.Contains(fileName)));
        }
    }
}