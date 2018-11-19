using System;
using System.Collections.Generic;

namespace NameSorter.Interfaces.INameSorterServices
{
    public interface INameSorterService
    {
        List<string> GetSortedNames();

        bool validateNames(out string illegalName);
    }
}
