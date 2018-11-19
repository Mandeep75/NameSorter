using System;

namespace NameSorter.Interfaces.ILogger
{
    public interface ILoggerService
    {
        string LogFilePath { get; set; }
        void WriteLog(string strLog);
    }
}
