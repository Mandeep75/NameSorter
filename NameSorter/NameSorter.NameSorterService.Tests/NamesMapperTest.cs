using Microsoft.VisualStudio.TestTools.UnitTesting;

using NameSorter.Interfaces.INameSorterServices;
using Name_Sorter.NamesService;
using System.Collections.Generic;

namespace NameSorter.NamesService.Tests
{
    [TestClass]
    public class NamesMapperTest
    {
        [TestMethod]
        public void Map_ValidListOf11Strings_Returns11ListOfNamesDto()
        {
            List<string> namestrings = new List<string>
            {
                "Janet Parsons",
                "Vaughn Lewis",
                "Adonis Julius Archer",
                "Shelby Nathan Yoder",
                "Marin Alvarez",
                "London Lindsey",
                "Beau Tristan Bentley",
                "Leo Gardner",
                "Hunter Uriah Mathew Clarke",
                "Mikayla Lopez",
                "Frankie Conner Ritter"
            };
            var mapper = NamesMapperService.Instance;
            var names=mapper.Map(namestrings);
            Assert.AreEqual(namestrings.Count, names.Count);

        }

        [TestMethod]
        public void Map_Namewith3GivenNamesandLastName_ReturnsCorrectlypopulatedNamesDto()
        {
            List<string> namestrings = new List<string>
            {
               
                "Hunter  Uriah Mathew  Clarke  ",
                
            };
            var mapper = NamesMapperService.Instance;
            var names = mapper.Map(namestrings);
            Assert.AreEqual(names[0].GivenName_1, "Hunter");
            Assert.AreEqual(names[0].GivenName_2, "Uriah");
            Assert.AreEqual(names[0].GivenName_3, "Mathew");
            Assert.AreEqual(names[0].LastName, "Clarke");


        }


        [TestMethod]
        public void Map_11ListOfNamesDto_ReturnsValidListOf3Strings()
        {
            List<Name> names = new List<Name>
            {
                new Name {GivenName_1="Janet",LastName="Parsons"},
                new Name {GivenName_1="Adonis",GivenName_2="Julius",LastName="Archer"},
                new Name {GivenName_1="Hunter",GivenName_2="Uriah",GivenName_3="Mathew",LastName="Clarke"},
                
            };
            var mapper = NamesMapperService.Instance;
            var namestrings = mapper.Map(names);
            Assert.AreEqual(names.Count,namestrings.Count );
            Assert.AreEqual(namestrings[0], "Janet Parsons");
            Assert.AreEqual(namestrings[1], "Adonis Julius Archer");
            Assert.AreEqual(namestrings[2], "Hunter Uriah Mathew Clarke");


        }

    }
}
