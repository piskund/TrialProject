// -------------------------------------------------------------------------------------------------------------
//  BackupStrategyCopyFilesTests.cs created by DEP on 2017/01/12
// -------------------------------------------------------------------------------------------------------------

using System.IO;
using System.Linq;
using Backup.Client.BL.BackupLogic;
using Backup.Common.DTO;
using Backup.Common.Logger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTests.Backup.Client.BL.FunctionalTests
{
    [TestClass]
    public class BackupStrategyCopyFilesTests
    {
        private const int TestFilesNumber = 2;
        private static readonly string SourcePath = Path.GetFullPath("..\\..\\TestData");
        private static readonly string DestinationPath = Path.Combine(Path.GetTempPath(), "TestSharedFolder");

        [TestMethod]
        public void DoBackup_SavesTestDataToTempDestination()
        {
            // Arrange
            var loggerMock = new Mock<ILogger>();
            var backupConfig = new BackupConfig
            {
                SourceFolderPath = SourcePath,
                DestinationFolderPath = DestinationPath,
                DestinationCredential = new CredentialInfo {UserName = "StandardUsr", Password = "123456"}
            };
            var backupStrategy = new BackupStrategyCopyFiles(loggerMock.Object);

            // Act
            backupStrategy.DoWork(backupConfig);

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

        [ClassCleanup]
        public static void TearDownTests()
        {
            // Delete temporary destination directory if exists.
            if (Directory.Exists(DestinationPath))
                Directory.Delete(DestinationPath, true);
        }
    }
}