using System;
using System.Collections.Generic;
using System.Text;

namespace Outreach.Reporting.Model
{
    public class AssociateModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string ContactNumber { get; set; }
        public string BaseLocation { get; set; }
        public string BU { get; set; }
        public string SBU { get; set; }
        public string Account { get; set; }
        public string Client { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
