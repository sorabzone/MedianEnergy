namespace MedianEnergy.Engine.Model
{
    public class BaseFile
    {
        public string MeterPointCode { get; set; }
        public string SerialNumber { get; set; }
        public string PlantCode { get; set; }
        public string DateAndTime { get; set; }
        public string DataType { get; set; }
        public decimal BaseValue { get; set; }
        public string Units { get; set; }
        public string Status { get; set; }
    }
}
