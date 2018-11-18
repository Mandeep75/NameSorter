using Name_Sorter.IRepository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Configuration;
namespace Name_Sorter.Repository
{
    public class NamesRepository : INamesRepository
    {
        public string DataSource { get; set; }
        public string Destination { get; set; }

        private List<string> mCache;

        public List<string> RetrieveNames()
        {
            //caching names as IO is expensive and this api gets called multiple times ie ,
            //once for validating data and then for sorting data
            if (mCache != null)
                return mCache;

            mCache = new List<string>();
           
           
            using (var reader = new StreamReader(DataSource))
            {
                while (!reader.EndOfStream)
                {
                   
                    try
                    {
                        var line = reader.ReadLine();
                        mCache.Add(line);

                    }
                    catch 
                    {
                        //Utilities.Utilities.WriteLog(string.Format("Invalid Data. TeamName: {0}, For : {1} , Against : {2}", values[0], values[5], values[7]));
                        //Utilities.Utilities.WriteLog(ex.Message);
                    }


                }
            }
            return mCache;
        }

       

        public void SaveNames(List<string> names)
        {
           File.WriteAllLines(Destination, names);
        }
    }
}
