using System;
using System.Collections.Generic;
using System.Text;

namespace Name_Sorter.INamesService
{
    public interface INamesMapper
    {
        List<Name> Map(List<string> names);

        List<string> Map(List<Name> names);
    }
}
