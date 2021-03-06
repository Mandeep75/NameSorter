using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NameSorter.DataReaderWriter;
using NameSorter.Interfaces.ILogger;
using System.Collections.Generic;
using System.IO;

namespace Name_Sorter.Repository.Tests
{
    [TestClass]
    public class NamesRepositoryTests
    {
        [TestMethod]
        public void RetrieveNames_ValidTextFile_ReturnsListofStrings()
        {
            var loggerMock = new Mock<ILoggerService>();
            var namesRepository = new DataReaderWriter(loggerMock.Object);

            string path = Directory.GetCurrentDirectory();
            namesRepository.DataSource = @".\..\..\..\DataSource\unsorted-names-list.txt";
            var unsortedNames = namesRepository.RetrieveNames();
            Assert.IsTrue(unsortedNames.Count == 11);
        }


        [TestMethod]
        public void SaveNames_ValidListOfStrings_SavesStringsToFile()
        {
            var loggerMock = new Mock<ILoggerService>();
            var namesRepository = new DataReaderWriter(loggerMock.Object);

           

            List<string> names = new List<string>
            {
                "Adonis Julius Archer",
                "Janet Parsons",
                "Vaughn Lewis"
            };


             namesRepository.Destination = @".\..\..\..\Output";
             namesRepository.SaveNames(names);
            
        }

        [TestMethod]
        [ExpectedException(typeof(System.IO.FileNotFoundException))]
        public void RetrieveNames_NonExistingFile_ReturnsListofStrings()
        {
            var loggerMock = new Mock<ILoggerService>();
            var namesRepository = new DataReaderWriter(loggerMock.Object);

            string path = Directory.GetCurrentDirectory();
            namesRepository.DataSource = @".\..\..\..\DataSource\notExisting.txt";
            var unsortedNames = namesRepository.RetrieveNames();
            
        }
    }
}
