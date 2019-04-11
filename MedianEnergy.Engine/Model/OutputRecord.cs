using System.Collections.Generic;

namespace MedianEnergy.Engine.Model
{
    public class OutputRecord
    {
        public string Filename { get; set; }
        public IEnumerable<BaseFile> Records { get; set; }
        public decimal Median { get; set; }
    }
}
