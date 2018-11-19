using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NameSorter.Interfaces.INameSorterServices;
using Name_Sorter.NamesService;
using System.Collections.Generic;
using Name_Sorter.IRepository;
using NameSorter.Interfaces.ILogger;

namespace NameSorter.NamesService.Tests
{
    [TestClass]
    public class NameSorterServiceTest
    {
        [TestMethod]
        public void GetSortedNames_ValidListOf11unsortedStrings_ReturnsValidListOf11sortedStrings()
        {
            var namesRepositoryMock = new Mock<INamesRepository>();
            namesRepositoryMock.Setup(x => x.RetrieveNames())
                .Returns(new List<string> { "Janet Parsons",
                "Vaughn Lewis",
                "Adonis Julius Archer",
                "Shelby Nathan Yoder",
                "Marin Alvarez",
                "London Lindsey",
                "Beau Tristan Bentley",
                "Leo Gardner",
                "Hunter Uriah Mathew Clarke",
                "Mikayla Lopez",
                "Frankie Conner Ritter" });

            var loggerMock = new Mock<ILoggerService>();
            INameSorterService objNameSorterService = new NameSorterService(namesRepositoryMock.Object, loggerMock.Object);
            var sortedNames = objNameSorterService.GetSortedNames();
            Assert.AreEqual(sortedNames.Count,11);
            Assert.AreEqual(sortedNames[0], "Marin Alvarez");
            Assert.AreEqual(sortedNames[1], "Adonis Julius Archer");
            Assert.AreEqual(sortedNames[2], "Beau Tristan Bentley");
            Assert.AreEqual(sortedNames[3], "Hunter Uriah Mathew Clarke");
            Assert.AreEqual(sortedNames[4], "Leo Gardner");
            Assert.AreEqual(sortedNames[5], "Vaughn Lewis");
            Assert.AreEqual(sortedNames[6], "London Lindsey");
            Assert.AreEqual(sortedNames[7], "Mikayla Lopez");
            Assert.AreEqual(sortedNames[8], "Janet Parsons");
            Assert.AreEqual(sortedNames[9], "Frankie Conner Ritter");
            Assert.AreEqual(sortedNames[10], "Shelby Nathan Yoder");

        }

        [TestMethod]
        public void validateNames_ValidListOf2Strings_ReturnsTrue()
        {
            var namesRepositoryMock = new Mock<INamesRepository>();
            namesRepositoryMock.Setup(x => x.RetrieveNames())
                .Returns(new List<string> { "Janet",                
                "Adonis Julius Archer sainsbury",
                 });


            var loggerMock = new Mock<ILoggerService>();
            INameSorterService objNameSorterService = new NameSorterService(namesRepositoryMock.Object, loggerMock.Object);
            string illegalName;
            var result = objNameSorterService.validateNames(out illegalName);

            Assert.IsNull(illegalName);
            Assert.IsTrue(result);
            

        }

        [TestMethod]
        public void validateNames_EmptytringInAListOf2Strings_Returnsfalse()
        {
            var namesRepositoryMock = new Mock<INamesRepository>();
            namesRepositoryMock.Setup(x => x.RetrieveNames())
                .Returns(new List<string> { "Janet Parsons",
                "",
                 });

            var loggerMock = new Mock<ILoggerService>();
            INameSorterService objNameSorterService = new NameSorterService(namesRepositoryMock.Object,loggerMock.Object);
            string illegalName;
            var result = objNameSorterService.validateNames(out illegalName);

            Assert.IsTrue(illegalName == string.Empty);
            Assert.IsFalse(result);

        }

        [TestMethod]
        public void validateNames_ANameHavingMoreThan3GivenNamesInAListOf2Strings_Returnsfalse()
        {
            var namesRepositoryMock = new Mock<INamesRepository>();
            namesRepositoryMock.Setup(x => x.RetrieveNames())
                .Returns(new List<string> { "Janet Parsons",
                "Janet Parsons jackson brown whittle",
                 });

            var loggerMock = new Mock<ILoggerService>();
            INameSorterService objNameSorterService = new NameSorterService(namesRepositoryMock.Object, loggerMock.Object);
            string illegalName;
            var result = objNameSorterService.validateNames(out illegalName);
            Assert.AreEqual(illegalName, "Janet Parsons jackson brown whittle");
            Assert.IsFalse(result);

        }



    }
}
