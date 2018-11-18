using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Name_Sorter.INamesService;
using Name_Sorter.IRepository;
using Name_Sorter.NamesService;
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
            .AddLogging()
            .AddSingleton<INamesRepository, NamesRepository>()
            .AddSingleton<INameSorterService, NameSorterService>()
            .BuildServiceProvider();

            //configure console logging
            //serviceProvider
            //    .GetService<ILoggerFactory>()
            //    .CreateLogger

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();
            logger.LogDebug("Starting application");

            //set repository source and destination..

           var repository= serviceProvider.GetService<INamesRepository>();

            IConfiguration config = new ConfigurationBuilder()
                                    .AddJsonFile("appsettings.json", true, true)
                                    .Build();

            repository.DataSource = config["AppSettings:DataSource"];
            repository.Destination = config["AppSettings:Destination"];
            

            var bar = serviceProvider.GetService<INameSorterService>();
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

        private static async Task<SortResult>   GetSortedNames(INameSorterService bar)
        {
            var backgroundTask = Task.Run(() =>            
            ValidateandGetSortedNames(bar));
            //bar.GetSortedNames());
            // do other work
            var sortResult = await backgroundTask;
            return sortResult;
        }

        private static SortResult ValidateandGetSortedNames(INameSorterService bar)
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
