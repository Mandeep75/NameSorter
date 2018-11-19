
using NameSorter.Interfaces.IDataReaderWriter;
using NameSorter.Interfaces.ILogger;
using System.Collections.Generic;
using System.IO;

namespace NameSorter.DataReaderWriter
{
    public class DataReaderWriter : IDataReaderWriter
    {
        public string DataSource { get; set; }
        public string Destination { get; set; }

        private List<string> mCache;
        private ILoggerService loggerService;

        public DataReaderWriter(ILoggerService _loggerService)
        {
            loggerService = _loggerService;
        }

        public List<string> RetrieveNames()
        {
            //caching names as IO is expensive and this api gets called multiple times ie ,
            //once for validating data and then for sorting data
            if (mCache != null)
            {
                loggerService.WriteLog("Names Retrieved from Cache");
                return mCache;
            }
            mCache = new List<string>();          
           
            using (var reader = new StreamReader(DataSource))
            {
                while (!reader.EndOfStream)
                {                  
                        var line = reader.ReadLine();
                        mCache.Add(line);          


                }
            }
            loggerService.WriteLog("Names Retrieved fromfile");
            return mCache;
        }

       

        public void SaveNames(List<string> names)
        {
            var destinationFilePath = Destination + "\\sorted-names-list.txt" ;
            var destinationFileInfo = new FileInfo(destinationFilePath);

            var destinationDirInfo = new DirectoryInfo(destinationFileInfo.DirectoryName);
            if (!destinationDirInfo.Exists) destinationDirInfo.Create();
            if (!destinationFileInfo.Exists)
            {
                var stream =destinationFileInfo.Create();
                stream.Close();                
            }
            File.WriteAllLines(destinationFilePath, names);
            loggerService.WriteLog(string.Format("{0} Names Saved to {1} ",names.Count, destinationFilePath));
        }
    }
}
