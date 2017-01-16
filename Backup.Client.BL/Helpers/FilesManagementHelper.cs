// -------------------------------------------------------------------------------------------------------------
//  FilesManagementHelper.cs created by DEP on 2017/01/12
// -------------------------------------------------------------------------------------------------------------

using System.IO;
using Backup.Common.Entities;

namespace Backup.Client.BL.Helpers
{
    public static class FilesManagementHelper
    {
        /// <summary>
        ///     Directories the copy.
        /// </summary>
        /// <param name="backupConfig">The backup configuration.</param>
        /// <param name="copySubDirs">if set to <c>true</c> [copy sub dirs].</param>
        public static void DirectoryCopy(BackupConfig backupConfig, bool copySubDirs = false)
        {
            DirectoryCopy(backupConfig.SourceFolderPath, backupConfig.DestinationFolderPath, copySubDirs);
        }

        /// <summary>
        ///     Directories the copy.
        /// </summary>
        /// <param name="sourceDirName">Name of the source dir.</param>
        /// <param name="destDirName">Name of the dest dir.</param>
        /// <param name="copySubDirs">if set to <c>true</c> [copy sub dirs].</param>
        /// <exception cref="System.IO.DirectoryNotFoundException">
        ///     Source directory does not exist or could not be found: "
        ///     + sourceDirName
        /// </exception>
        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs = false)
        {
            // Get the subdirectories for the specified directory.
            var dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);

            var dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
                Directory.CreateDirectory(destDirName);

            // Get the files in the directory and copy them to the new location.
            var files = dir.GetFiles();
            foreach (var file in files)
            {
                var temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, true);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
                foreach (var subdir in dirs)
                {
                    var temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
        }

        /// <summary>
        ///     Directories the copy asynchronous.
        /// </summary>
        /// <param name="sourceDirectory">The source directory.</param>
        /// <param name="destinationDirectory">The destination directory.</param>
        public static async void DirectoryCopyAsync(string sourceDirectory, string destinationDirectory)
        {
            foreach (var filename in Directory.EnumerateFiles(sourceDirectory))
                using (var sourceStream = File.Open(filename, FileMode.Open))
                {
                    using (
                        var destinationStream =
                            File.Create(destinationDirectory + filename.Substring(filename.LastIndexOf('\\'))))
                    {
                        await sourceStream.CopyToAsync(destinationStream);
                    }
                }
        }
    }
}