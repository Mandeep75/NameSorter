using System;
using System.Collections.Generic;
using Name_Sorter.INamesService;
using Name_Sorter.IRepository;
using System.Linq;
using System.Threading;

namespace Name_Sorter.NamesService
{
    public class NameSorterService : INameSorterService
    {

        private INamesRepository namesRepository;
        public NameSorterService(INamesRepository _repository)
        {
            namesRepository = _repository;
        }
             
        public List<string> GetSortedNames()
        {
            var unsortedNames = namesRepository.RetrieveNames();
            INamesMapper namesMapper = NamesMapper.Instance;
            var names=namesMapper.Map(unsortedNames);
            var sortedNames = names.OrderBy(n => n.LastName).ThenBy(n => n.GivenName_1).
                            ThenBy(n => n.GivenName_2).ThenBy(n => n.GivenName_3).ToList();

            var sortedNamestrings = namesMapper.Map(sortedNames);

            namesRepository.SaveNames(sortedNamestrings);
            //Thread.Sleep(50000);
            return sortedNamestrings;
        }

        public bool validateNames(out string illegalName)
        {
            illegalName = null;
            var unsortedNames = namesRepository.RetrieveNames();
            foreach (var name in unsortedNames)
            {
                var nameparts = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (nameparts==null || nameparts.Length==0 || nameparts.Length> 4)
                {
                    illegalName = name;
                    return false;
                }
            }
            return true;
              


          
        }
    }
}
