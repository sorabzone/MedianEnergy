namespace MedianEnergy.Engine.Model
{
    public class TOUFile : BaseFile
    {
        public decimal Energy { get { return base.BaseValue; } }
        public string MaximumDemand { get; set; }
        public string TimeofMaxDemand { get; set; }
        public string Period { get; set; }
        public string DLSActive { get; set; }
        public string BillingResetCount { get; set; }
        public string BillingResetDateTime { get; set; }
        public string Rate { get; set; }
    }
}
