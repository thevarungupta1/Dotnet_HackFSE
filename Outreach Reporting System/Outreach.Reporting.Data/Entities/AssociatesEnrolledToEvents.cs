using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Outreach.Reporting.Data.Entities
{
    public class AssociatesEnrolledToEvents
    {
        [Key]
        public int EnrollmentID { get; set; }
        public int AssociateID { get; set; }
        public string EventID { get; set; }
        public decimal VolunteerHours { get; set; }
        public decimal TravelHours { get; set; }
        public string Status { get; set; }
        public string IIEPCategory { get; set; }
        public bool IsPOC { get; set; }

        public Associates Associates { get; set; }
        public Events Events { get; set; }

    }
}
