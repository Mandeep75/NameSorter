using System;
using System.Collections.Generic;

namespace Name_Sorter.IRepository
{
    public interface INamesRepository
    {
        string DataSource { get; set; }

        string Destination { get; set; }

        List<string> RetrieveNames();
        void SaveNames(List<string> names);
    }
}
