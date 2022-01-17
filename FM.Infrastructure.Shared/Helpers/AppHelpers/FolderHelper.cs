using System;
using System.IO;

namespace FM.Infrastructure.Shared.Helpers.AppHelpers
{
    public class FolderHelper
    {
        public static string CreateFolderDownload(string folderName)
        {
            string excelExportPath = Environment.CurrentDirectory + $"\\{folderName}\\";

            var logFileInfo = new FileInfo(excelExportPath);
            var logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);

            if (!logDirInfo.Exists) logDirInfo.Create();

            return excelExportPath;
        }
    }
}
