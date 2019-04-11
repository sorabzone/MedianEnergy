using CsvHelper.Configuration;

namespace MedianEnergy.Engine.Model
{
    public class LPFileMap : ClassMap<LPFile>
    {
        public LPFileMap()
        {
            Map(m => m.MeterPointCode).Name("MeterPoint Code");
            Map(m => m.SerialNumber).Name("Serial Number");
            Map(m => m.PlantCode).Name("Plant Code");
            Map(m => m.DateAndTime).Name("Date/Time");
            Map(m => m.DataType).Name("Data Type");
            Map(m => m.BaseValue).Name("Data Value");
            Map(m => m.Units).Name("Units");
            Map(m => m.Status).Name("Status");
        }
    }
}
