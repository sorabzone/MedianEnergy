using CsvHelper;
using MedianEnergy.Engine.Interface;
using MedianEnergy.Engine.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace MedianEnergy.Engine.Service
{
    public class LPService : BaseService<LPFile>
    {
        public LPService(IOutputService outputService) : base(outputService)
        {
        }
        /// <summary>
        /// This function process LP files
        /// </summary>
        /// <param name="filesPath">directory path that contains LP files</param>
        /// <returns></returns>
        public async Task ProcessFiles(string filesPath)
        {
            #region Reading Files
            TransformBlock<string, OutputRecord> readAction = new TransformBlock<string, OutputRecord>(item =>
            {
                //Read records in as an array of objects.
                //CSVHelper Nuget package is used to read and map csv into object list/array
                List<LPFile> records = GetRecords(item);
                var median = GetMedian(records);

                //Create output object and passed to base class function to print it in console
                return new OutputRecord
                {
                    Filename = item.Substring(item.LastIndexOf('\\') + 1),
                    Records = GetResult(records, median),//only valid values as per business rules
                    Median = median
                };

            }, new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = 5 });
            #endregion

            #region Printing Results
            //Parallelism = 1, so that all records of a file print together
            ActionBlock<OutputRecord> printAction = new ActionBlock<OutputRecord>(item =>
            {
                PrintResult(item);
            }, new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = 1 });
            #endregion

            //Remove trailing \ if exists in directory path
            var path = filesPath[filesPath.Length - 1] == '\\' ? filesPath.Substring(0, filesPath.Length - 1) : filesPath;
            string[] filePaths = Directory.GetFiles($@"{path}\", "*.csv");

            //Dataflow pipeline is created to do the processing in parallel.
            //TransformBlock read and process all files in parallel(max limit is 5). Once processing is complete, it sends results to ActionBlock
            //ActionBlock takes care of printing output in console. MAxDegreeOfParallelism = 1, because we want to print all records of same file together.
            filePaths.ToList().ForEach(filePath =>
            {
                readAction.SendAsync<string>(filePath);
            });

            //Sending result of read files to PrintAction to print output in console 
            readAction.LinkTo(printAction, new DataflowLinkOptions { PropagateCompletion = true });

            //waiting for process to complete without blocking the thread
            readAction.Complete();
            await printAction.Completion;
        }

        /// <summary>
        /// Reads csv and returns array of all records
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public List<LPFile> GetRecords(string file)
        {
            using (var reader = new StreamReader(file))
            {
                using (var csv = new CsvReader(reader))
                {
                    csv.Configuration.RegisterClassMap<LPFileMap>();
                    return csv.GetRecords<LPFile>().OrderBy(r => r.DataValue).ToList();
                }
            }
        }
    }
}
