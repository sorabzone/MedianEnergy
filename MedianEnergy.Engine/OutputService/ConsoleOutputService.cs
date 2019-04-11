using MedianEnergy.Engine.Interface;
using MedianEnergy.Engine.Model;
using System;

namespace MedianEnergy.Engine.OutputService
{
    public class ConsoleOutputService : IOutputService
    {
        /// <summary>
        /// Prints output in console
        /// </summary>
        /// <param name="output"></param>
        public void GenerateOutput(OutputRecord output)
        {
            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine($"   Processing File - {output.Filename}");
            Console.WriteLine("-----------------------------------------------------------");
            foreach (var record in output.Records)
            {
                Console.WriteLine($"{output.Filename} {record.DateAndTime} {record.BaseValue.ToString("N6")} {output.Median.ToString("N6")}");
            }
            Console.WriteLine("\n\n");
        }
    }
}
