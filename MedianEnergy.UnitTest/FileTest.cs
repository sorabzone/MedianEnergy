using MedianEnergy.Engine.OutputService;
using MedianEnergy.Engine.Service;
using NUnit.Framework;
using System;
using System.Linq;

namespace MedianEnergy.UnitTest
{
    [TestFixture(@"TestData\LP", @"TestData\TOU", TypeArgs = new Type[] { typeof(string), typeof(string) })]
    public class FileTest
    {
        private string _lpPath;
        private string _touPath;
        private LPService _lpService;
        private TOUService _touService;

        public FileTest(string lpPath, string touPath)
        {
            ConsoleOutputService svc = new ConsoleOutputService();
            this._lpPath = lpPath;
            this._touPath = touPath;
            _lpService = new LPService(svc);
            _touService = new TOUService(svc);
        }

        /// <summary>
        /// IF there are invaid column header
        /// </summary>
        [TestCase]
        public void When_Invalid_ColumnHeader()
        {
            Assert.Throws<CsvHelper.HeaderValidationException>(delegate ()
            {
                var result = _lpService.GetRecords($@"{_lpPath}\LP_Invalid.csv");
            });
        }

        /// <summary>
        /// Check if function read all records
        /// </summary>
        [TestCase]
        public void When_Read_All_LP_Records()
        {
            var result = _lpService.GetRecords($@"{_lpPath}\LP_Even.csv");
            Assert.AreEqual(6, result.Count);
        }

        /// <summary>
        /// Even number of records
        /// </summary>
        [TestCase]
        public void When_Calculate_LP_Median_Even()
        {
            var result = _lpService.GetRecords($@"{_lpPath}\LP_Even.csv");
            var median = _lpService.GetMedian(result);

            Assert.AreEqual(4.050000, median);
        }

        /// <summary>
        /// Odd number of records
        /// </summary>
        [TestCase]
        public void When_Calculate_LP_Median_Odd()
        {
            var result = _lpService.GetRecords($@"{_lpPath}\LP_Odd.csv");
            var median = _lpService.GetMedian(result);

            Assert.AreEqual(4.000000, median);
        }

        /// <summary>
        /// Check the result
        /// </summary>
        [TestCase]
        public void When_Check_LP_Result()
        {
            var records = _lpService.GetRecords($@"{_lpPath}\LP_Odd.csv");
            var median = _lpService.GetMedian(records);
            var result = _lpService.GetResult(records, median);

            //As per test data, there are 5 valid records
            Assert.AreEqual(5, result.Count());
        }

        /// <summary>
        /// Check if function read all records
        /// </summary>
        [TestCase]
        public void When_Read_All_TOU_Records()
        {
            var result = _touService.GetRecords($@"{_touPath}\TOU_Even.csv");
            Assert.AreEqual(8, result.Count);
        }

        /// <summary>
        /// Even number of records
        /// </summary>
        [TestCase]
        public void When_Calculate_TOU_Median_Even()
        {
            var result = _touService.GetRecords($@"{_touPath}\TOU_Even.csv");
            var median = _touService.GetMedian(result);

            Assert.AreEqual(0.147000, median);
        }

        /// <summary>
        /// Odd number of records
        /// </summary>
        [TestCase]
        public void When_Calculate_TOU_Median_Odd()
        {
            var result = _touService.GetRecords($@"{_touPath}\TOU_Odd.csv");
            var median = _touService.GetMedian(result);

            Assert.AreEqual(0.148000, median);
        }

        /// <summary>
        /// Check Result
        /// </summary>
        [TestCase]
        public void When_Check_TOU_Result()
        {
            var records = _touService.GetRecords($@"{_touPath}\TOU_Odd.csv");
            var median = _touService.GetMedian(records);
            var result = _touService.GetResult(records, median);

            //As per test data, there are 4 valid records
            Assert.AreEqual(4, result.Count());
        }

        /// <summary>
        /// Check empty result
        /// </summary>
        [TestCase]
        public void When_Check_Zero_Result()
        {
            var records = _touService.GetRecords($@"{_touPath}\TOU_Zero.csv");
            var median = _touService.GetMedian(records);
            var result = _touService.GetResult(records, median);

            //As per test data, there are 0 valid records
            Assert.AreEqual(0, result.Count());
        }
    }
}
