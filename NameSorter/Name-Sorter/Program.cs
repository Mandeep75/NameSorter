using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using INameSorterServices=NameSorter.Interfaces.INameSorterServices;
using Name_Sorter.IRepository;
using namesService = Name_Sorter.NamesService;
using Name_Sorter.Repository;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Name_Sorter
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()            
            .AddSingleton<INamesRepository, NamesRepository>()
            .AddSingleton<INameSorterServices.INameSorterService, namesService.NameSorterService>()
            .AddSingleton<INameSorterServices.ILoggerService, namesService.LoggerService>()
            .BuildServiceProvider();
           

            //var logger = serviceProvider.GetService<ILoggerFactory>()
            //    .CreateLogger<Program>();
            //logger.LogDebug("Starting application");

          

            IConfiguration config = new ConfigurationBuilder()
                                    .AddJsonFile("appsettings.json", true, true)
                                    .Build();
            //set repository source and destination..

            var repository = serviceProvider.GetService<INamesRepository>();
            repository.DataSource = config["AppSettings:DataSource"];
            repository.Destination = config["AppSettings:Destination"];

            //set Log file location

            var loggerService = serviceProvider.GetService<INameSorterServices.ILoggerService>();
            loggerService.LogFilePath= config["AppSettings:LogFilePath"];

            var bar = serviceProvider.GetService<INameSorterServices.INameSorterService>();
            Console.WriteLine(string.Format("Main Thread id {0}", Thread.CurrentThread.ManagedThreadId));
            Console.WriteLine("Begining sort.......");
            Console.WriteLine();

            var sortedNames =  GetSortedNames(bar);

            var result = sortedNames.Result;
            Console.WriteLine();
            Console.WriteLine(string.Format("Displaying Results in Main Thread id :{0}", Thread.CurrentThread.ManagedThreadId));
            Console.WriteLine();
            if (!result.IsUnsortedDataValid)
            {
                if (result.illegalName==null|| result.illegalName.Length==0)
                    Console.WriteLine("Invalid Name in Data.A person must have at least 1 given name");
                else
                    Console.WriteLine(string.Format("Invalid Name in Data. A person cant have more than 3 given names. Invalid Name: {0}", result.illegalName));
            }
            else
            {
                foreach (var sortedName in result.sortedNames)
                {
                    Console.WriteLine(sortedName);
                }

            }           
            Console.ReadLine();
            Console.WriteLine("All done!");
        }

        private static async Task<SortResult>   GetSortedNames(INameSorterServices.INameSorterService bar)
        {
            var backgroundTask = Task.Run(() =>            
            ValidateandGetSortedNames(bar));
            //bar.GetSortedNames());
            // do other work
            var sortResult = await backgroundTask;
            return sortResult;
        }

        private static SortResult ValidateandGetSortedNames(INameSorterServices.INameSorterService bar)
        {
            string illegalName;
            var result = new SortResult();
            Console.WriteLine(string.Format("Running sort Op in Background Thread , id {0}", Thread.CurrentThread.ManagedThreadId));
            bool isDataValid=bar.validateNames(out illegalName);
            if (!isDataValid)
            {
                result.IsUnsortedDataValid = false;
                result.illegalName = illegalName;
                return result;
            }            
            result.sortedNames = bar.GetSortedNames();
            result.IsUnsortedDataValid = true;
            return result;
        }
    }

    class SortResult
    {
        internal bool IsUnsortedDataValid { get; set;}
        internal string illegalName { get; set; }
        internal List<string> sortedNames { get; set; }
    }
}
