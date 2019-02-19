using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Outreach.Reporting.Entity.Entities
{
    public class Enrollments
    {
        [Key]
        public int EnrollmentID { get; set; }
        [ForeignKey("Associates")]
        public int AssociateID { get; set; }
        [ForeignKey("Events")]
        public string EventID { get; set; }
        public decimal VolunteerHours { get; set; }
        public decimal TravelHours { get; set; }
        public string Status { get; set; }
        public string IIEPCategory { get; set; }
        public bool IsPOC { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        //Navigation properties
        public Associates Associates { get; set; }
        public Events Events { get; set; }

    }
}
