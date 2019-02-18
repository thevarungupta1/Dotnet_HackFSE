using System;
using System.Collections.Generic;
using System.Text;

namespace Outreach.Reporting.Model
{
    public class EventModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? Date { get; set; }
       // public int LocationID { get; set; }
        public string Beneficiary { get; set; }
        public string CouncilName { get; set; }
        public string Project { get; set; }
        public string Category { get; set; }
        public int? LivesImpacted { get; set; }
        public int? ActivityType { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

    }
}
