using MedianEnergy.Engine.Interface;
using MedianEnergy.Engine.Model;
using System.Collections.Generic;
using System.Linq;

namespace MedianEnergy.Engine.Service
{
    public abstract class BaseService<T> where T : BaseFile
    {
        private readonly IOutputService _outputService;
        public BaseService(IOutputService outputService)
        {
            _outputService = outputService;
        }
        /// <summary>
        /// Calculates the median value
        /// </summary>
        /// <param name="records"></param>
        /// <returns></returns>
        public decimal GetMedian(List<T> records)
        {
            int length = records.Count;

            if (length % 2 != 0)//Odd number of records
            {
                return records[length / 2].BaseValue;
            }
            else//Even number of records
            {
                return (records[length / 2].BaseValue + records[(length / 2) - 1].BaseValue) / 2;
            }
        }

        /// <summary>
        /// Prints the result in Console
        /// </summary>
        /// <param name="output"></param>
        public void PrintResult(OutputRecord output)
        {
            _outputService.GenerateOutput(output);
            //Console.WriteLine("-----------------------------------------------------------");
            //Console.WriteLine($"   Processing File - {output.Filename}");
            //Console.WriteLine("-----------------------------------------------------------");
            //foreach (var record in output.Records)
            //{
            //    Console.WriteLine($"{output.Filename} {record.DateAndTime} {record.BaseValue.ToString("N6")} {output.Median.ToString("N6")}");
            //}
            //Console.WriteLine("\n\n");
        }

        public IEnumerable<BaseFile> GetResult(List<T> records, decimal median)
        {
            //Calculate -20% and +20% values, so that we don't have to calculate for comparing each record.
            var lowerLimit = median * 80 / 100;
            var upperLimit = median * 120 / 100;

            return records.Where(r => r.BaseValue >= lowerLimit && r.BaseValue <= upperLimit);
        }
    }
}
