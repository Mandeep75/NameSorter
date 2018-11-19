using System;
using System.Collections.Generic;

namespace NameSorter.Interfaces.IDataReaderWriter
{
    public interface IDataReaderWriter
    {
        string DataSource { get; set; }

        string Destination { get; set; }

        List<string> RetrieveNames();
        void SaveNames(List<string> names);
    }
}
