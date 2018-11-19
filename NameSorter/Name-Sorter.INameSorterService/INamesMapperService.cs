using System;
using System.Collections.Generic;
using System.Text;

namespace NameSorter.Interfaces.INameSorterServices
{
    public interface INamesMapperService
    {
        List<Name> Map(List<string> names);

        List<string> Map(List<Name> names);
    }
}
