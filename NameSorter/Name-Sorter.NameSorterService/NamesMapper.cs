using System;
using System.Collections.Generic;
using System.Text;
using Name_Sorter.INamesService;

namespace Name_Sorter.NamesService
{
    //singleton class....
    public class NamesMapper : INamesMapper
    {

        private static NamesMapper instance = null;

        private NamesMapper()
        {
        }

        public static NamesMapper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NamesMapper();
                }
                return instance;
            }
        }

        public List<Name> Map(List<string> names)
        {
            var namesList = new List<Name>();
            foreach (var name in names)
            {
                var nameparts = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);                
                if (nameparts.Length < 1)
                {
                    //log error
                    continue;
                }

                var structuredName = new Name();                
                structuredName.GivenName_1 = nameparts[0];

                if (nameparts.Length>1)
                    structuredName.LastName = nameparts[nameparts.Length - 1];
                if (nameparts.Length == 3)
                {
                    structuredName.GivenName_2 = nameparts[1];
                }
                else if (nameparts.Length == 4)
                {
                    structuredName.GivenName_2 = nameparts[1];
                    structuredName.GivenName_3 = nameparts[2];
                }
                else if (nameparts.Length > 4)
                {
                    //log error
                    continue;
                }
                namesList.Add(structuredName);
            }

            return namesList;

        }


        public List<string> Map(List<Name> structuredNames)
        {
            var names = new List<string>();
            string name = string.Empty;
            foreach (var structuredName in structuredNames)
            {
                name = structuredName.GivenName_1 + (structuredName.GivenName_2?.Length > 0 ? (" " + structuredName.GivenName_2) : string.Empty) +
                      " " + (structuredName.GivenName_3?.Length > 0 ? structuredName.GivenName_3 + " " : string.Empty) + structuredName.LastName;

                names.Add(name);
            }

            return names;
        }
    }
}

