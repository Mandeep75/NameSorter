using System;
using System.Collections.Generic;
using System.Text;

namespace NameSorter.Interfaces.INameSorterServices
{
    public interface ILoggerService
    {
        string LogFilePath { get; set; }
        void WriteLog(string strLog);
    }
}
