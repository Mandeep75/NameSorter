using System;
using System.Collections.Generic;

namespace Name_Sorter.INamesService
{
    public interface INameSorterService
    {
        List<string> GetSortedNames();

        bool validateNames(out string illegalName);
    }
}
