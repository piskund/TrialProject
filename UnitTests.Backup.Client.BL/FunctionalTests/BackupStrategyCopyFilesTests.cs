﻿// -------------------------------------------------------------------------------------------------------------
//  BackupStrategyCopyFilesTests.cs created by DEP on 2017/01/16
// -------------------------------------------------------------------------------------------------------------

using System.IO;
using System.Linq;
using Backup.Client.BL.BackupLogic;
using Backup.Client.BL.Helpers;
using Backup.Common.Entities;
using Backup.Common.Helpers;
using Backup.Common.Logger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Backup.Common.DTO;

namespace UnitTests.Backup.Client.BL.FunctionalTests
{
    [TestClass]
    [Ignore]
    // Please make sure you have shared  shareFolder for ShareUser with password 1q2w3e before enable this test.
    public class BackupStrategyCopyFilesTests
    {
        private static readonly string DestinationPath = Path.Combine(Path.GetTempPath(), "TestSharedFolder");

        [TestMethod]
        public void DoBackup_SavesTestDataToTempDestination()
        {
            // Arrange
            var crh = new ClientRegistrationHelper("ShareUser", "1q2w3e", "sharedFolder");
            var logger = new ConsoleLogger();
            var backupConfig = new BackupConfig
            {
                SourceFolderPath = crh.ClientInfo.SharedFolderPath,
                SourceCredential = crh.ClientInfo.CredentialInfo,
                DestinationFolderPath = DestinationPath,
                DestinationCredential = crh.ClientInfo.CredentialInfo
            };
            var backupStrategy = new BackupStrategyCopyFiles(logger);

            // Act
            backupStrategy.DoWork(backupConfig);

            // Assert
            var filesInSource = Directory.EnumerateFiles(backupConfig.SourceFolderPath);
            Assert.IsNotNull(filesInSource);
            var filesInDestination = Directory.EnumerateFiles(backupConfig.DestinationFolderPath);
            Assert.IsNotNull(filesInDestination);
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