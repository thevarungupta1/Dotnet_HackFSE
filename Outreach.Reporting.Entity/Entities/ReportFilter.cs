using System;

namespace Outreach.Reporting.Entity.Entities
{
    public class ReportFilter
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int AssociateID { get; set; }
        public string BaseLocations { get; set; }
        public string BusinessUnits { get; set; }
        public string FocusAreas { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        //public DateTime CreatedOn { get; set; }
    }
}
