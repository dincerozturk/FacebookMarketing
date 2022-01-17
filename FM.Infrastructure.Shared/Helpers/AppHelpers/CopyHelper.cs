using System;
using System.Collections.Generic;
using System.IO;

namespace FM.Infrastructure.Shared.Helpers.AppHelpers
{
    public class CopyHelper
    {
        public static void CopyFirefoxProfile(string basePath, string folderSource, List<string> listUid)
        {
            var sourcePath = basePath + "\\" + folderSource;
            int count = 0;
            foreach (var uid in listUid)
            {
                count++;
                var targetPath = basePath + "\\" + uid;

                CopyFilesRecursively(sourcePath, targetPath);

                Console.WriteLine($"----------------Done {count}/{listUid.Count}: {uid}");
            }
        }

        public static void CopyFilesRecursively(string sourcePath, string targetPath)
        {
            try
            {
                if (!Directory.Exists(targetPath))
                {
                    Directory.CreateDirectory(targetPath);
                }
                else
                {
                    Console.WriteLine($"Existed: {targetPath}");
                    return;
                }

                //Now Create all of the directories
                foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
                    //Console.WriteLine($"Copying {dirPath}");
                }

                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
                {
                    File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
                    //Console.WriteLine($"Copying {newPath}");
                }
            }
            catch (Exception e)
            {
                // ignored
            }
        }
    }
}