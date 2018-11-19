using System;
using System.Collections.Generic;
using System.Text;

namespace NameSorter.Interfaces.INameSorterServices
{
    public class Name
    {
        public string GivenName_1 { get; set; }
        public string GivenName_2 { get; set; }
        public string GivenName_3 { get; set; }
        public string LastName { get; set; }
    }

    public interface INamesMapperService
    {
        List<Name> Map(List<string> names);

        List<string> Map(List<Name> names);
    }
}
