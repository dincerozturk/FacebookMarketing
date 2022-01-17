using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FM.Infrastructure.Shared.Helpers.AppHelpers
{
    public class LogHelper
    {
        static ReaderWriterLock locker = new ReaderWriterLock();
        
        public static void WriteDebug(string text)
        {
            try
            {
                locker.AcquireWriterLock(int.MaxValue); //You might wanna change timeout value 
                StreamWriter log;
                FileStream fileStream = null;
                DirectoryInfo logDirInfo = null;
                FileInfo logFileInfo;

                string logFilePath = Environment.CurrentDirectory + "\\Logs\\";
                logFilePath = logFilePath + DateTime.Today.ToString("dd-MM-yyyy") + "-debug." + "txt";
                logFileInfo = new FileInfo(logFilePath);
                logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);

                if (!logDirInfo.Exists) logDirInfo.Create();

                if (!logFileInfo.Exists)
                {
                    fileStream = logFileInfo.Create();
                }
                else
                {
                    fileStream = new FileStream(logFilePath, FileMode.Append);
                }

                log = new StreamWriter(fileStream);
                log.WriteLine(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff tt\t") + text);

                log.Close();
            }
            finally
            {
                locker.ReleaseWriterLock();
            }
        }
        public static async Task WriteLogAsync(string strLog)
        {
            StreamWriter log;
            FileStream fileStream = null;
            DirectoryInfo logDirInfo = null;
            FileInfo logFileInfo;

            string logFilePath = Environment.CurrentDirectory + "\\Logs\\";
            logFilePath = logFilePath + DateTime.Today.ToString("dd-MM-yyyy") + "." + "txt";
            logFileInfo = new FileInfo(logFilePath);
            logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);

            if (!logDirInfo.Exists) logDirInfo.Create();

            if (!logFileInfo.Exists)
            {
                fileStream = logFileInfo.Create();
            }
            else
            {
                fileStream = new FileStream(logFilePath, FileMode.Append);
            }

            log = new StreamWriter(fileStream);
            await log.WriteLineAsync(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff tt\t") + strLog);

            log.Close();
        }

        public static void WriteLog(string strLog)
        {
            StreamWriter log;
            FileStream fileStream = null;
            DirectoryInfo logDirInfo = null;
            FileInfo logFileInfo;

            string logFilePath = Environment.CurrentDirectory + "\\Logs\\";
            logFilePath = logFilePath + DateTime.Today.ToString("dd-MM-yyyy") + "." + "txt";
            logFileInfo = new FileInfo(logFilePath);
            logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
            if (!logDirInfo.Exists) logDirInfo.Create();

            if (!logFileInfo.Exists)
            {
                fileStream = logFileInfo.Create();
            }
            else
            {
                fileStream = new FileStream(logFilePath, FileMode.Append);
            }

            log = new StreamWriter(fileStream);
            log.WriteLine(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff tt\t") + strLog);
            log.Close();
        }

        public static void WriteLogIntoCommonFile(string strLog)
        {
            StreamWriter log;
            FileStream fileStream = null;
            DirectoryInfo logDirInfo = null;
            FileInfo logFileInfo;

            string logFilePath = Environment.CurrentDirectory + "\\Logs\\";
            logFilePath = logFilePath + "commons.txt";
            logFileInfo = new FileInfo(logFilePath);
            logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
            if (!logDirInfo.Exists) logDirInfo.Create();
            if (!logFileInfo.Exists)
            {
                fileStream = logFileInfo.Create();
            }
            else
            {
                fileStream = new FileStream(logFilePath, FileMode.Append);
            }
            log = new StreamWriter(fileStream);
            log.WriteLine(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff tt\t") + strLog);

            log.Close();
        }
        public static void WriteLogWithFolderNameAndFileName(string folderName, string fileName, string content, bool isAppendFile)
        {
            try
            {
                string path = Environment.CurrentDirectory + "\\Logs\\" + folderName;
                Directory.CreateDirectory(path);
                FileInfo fileInfo = new FileInfo(path + "\\" + fileName + ".txt");
                if (!fileInfo.Directory.Exists)
                    fileInfo.Directory.Create();
                var filename = path + "\\" + fileName + ".txt";
                var sw = new System.IO.StreamWriter(filename, isAppendFile);
                sw.WriteLine(DateTime.Now.ToString() + " " + content);
                sw.Close();
            }
            catch (Exception e)
            {
                // ignored
            }
        }
        public static void WriteLogListNeedProcess(string folderName, string fileName, List<string> listContent)
        {
            try
            {
                if (listContent == null)
                {
                    listContent = new List<string>();
                }

                listContent.Add(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));

                string path = Environment.CurrentDirectory + "\\Logs\\" + folderName;

                Directory.CreateDirectory(path);

                FileInfo fileInfo = new FileInfo(path + "\\" + fileName + ".txt");
                if (!fileInfo.Directory.Exists) fileInfo.Directory.Create();

                var filename = path + "\\" + fileName + ".txt";

                File.WriteAllLines(filename, listContent);
            }
            catch (Exception e)
            {
                // ignored
            }
        }
    }
}