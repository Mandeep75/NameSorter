using NameSorter.Interfaces.ILogger;
using System;
using System.IO;

namespace NameSorter.LoggerService
{
    public class LoggerService : ILoggerService
    {
        public string LogFilePath { get; set; }

        public void WriteLog(string strLog)
        {
            StreamWriter log;
            FileStream fileStream = null;
            DirectoryInfo logDirInfo = null;
            FileInfo logFileInfo;
            var logFile = LogFilePath + "\\Log-" + System.DateTime.Today.ToString("MM-dd-yyyy") + "." + "txt";
            logFileInfo = new FileInfo(logFile);
            logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
            if (!logDirInfo.Exists) logDirInfo.Create();
            if (!logFileInfo.Exists)
            {
                fileStream = logFileInfo.Create();
            }
            else
            {
                fileStream = new FileStream(logFile, FileMode.Append);
            }
            log = new StreamWriter(fileStream);
            log.WriteLine(strLog);
            log.Close();
        }
    }
}
