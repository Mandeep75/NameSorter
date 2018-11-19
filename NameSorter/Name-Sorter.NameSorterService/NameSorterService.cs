using System;
using System.Collections.Generic;
using INameSorterServices=NameSorter.Interfaces.INameSorterServices;
using Name_Sorter.IRepository;
using System.Linq;

namespace Name_Sorter.NamesService
{
    public class NameSorterService : INameSorterServices.INameSorterService
    {
        
        private INamesRepository namesRepository;
        private INameSorterServices.ILoggerService loggerService;
        public NameSorterService(INamesRepository _repository, INameSorterServices.ILoggerService _loggerService)
        {
            namesRepository = _repository;
            loggerService = _loggerService;
        }
             
        public List<string> GetSortedNames()
        {
            var unsortedNames = namesRepository.RetrieveNames();
            INameSorterServices.INamesMapperService namesMapper = NamesMapperService.Instance;
            var names=namesMapper.Map(unsortedNames);
            var sortedNames = names.OrderBy(n => n.LastName).ThenBy(n => n.GivenName_1).
                            ThenBy(n => n.GivenName_2).ThenBy(n => n.GivenName_3).ToList();

            var sortedNamestrings = namesMapper.Map(sortedNames);

            namesRepository.SaveNames(sortedNamestrings);
            loggerService.WriteLog(string.Format("{0} Names sorted and successfully written to output file", sortedNames.Count));

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
