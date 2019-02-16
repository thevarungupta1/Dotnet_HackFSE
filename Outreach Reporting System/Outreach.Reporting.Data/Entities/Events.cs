using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Outreach.Reporting.Data.Entities
{
    public class Events
    {
        [Key]
        public string EventID { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public DateTime? EventDate { get; set; }
        public int? BaseLocationID { get; set; }
        public string BeneficiaryName { get; set; }
        public string CouncilName { get; set; }
        public string Project { get; set; }
        public string Category { get; set; }
        public int LivesImpacted { get; set; }
        public int? ActivityType { get; set; }
        public string Status { get; set; }

        public Locations Locations { get; set; }
        public ICollection<AssociatesEnrolledToEvents> AssociatesEnrolledToEvents { get; set; }
    }
}
