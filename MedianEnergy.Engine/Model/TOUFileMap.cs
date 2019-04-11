using CsvHelper.Configuration;

namespace MedianEnergy.Engine.Model
{
    public class TOUFileMap : ClassMap<TOUFile>
    {
        public TOUFileMap()
        {
            Map(m => m.MeterPointCode).Name("MeterPoint Code");
            Map(m => m.SerialNumber).Name("Serial Number");
            Map(m => m.PlantCode).Name("Plant Code");
            Map(m => m.DateAndTime).Name("Date/Time");
            Map(m => m.DataType).Name("Data Type");
            Map(m => m.BaseValue).Name("Energy");
            Map(m => m.Units).Name("Units");
            Map(m => m.Status).Name("Status");
            Map(m => m.MaximumDemand).Name("Maximum Demand");
            Map(m => m.TimeofMaxDemand).Name("Time of Max Demand");
            Map(m => m.Period).Name("Period");
            Map(m => m.DLSActive).Name("DLS Active");
            Map(m => m.BillingResetCount).Name("Billing Reset Count");
            Map(m => m.BillingResetDateTime).Name("Billing Reset Date/Time");
            Map(m => m.Rate).Name("Rate");
        }
    }
}
