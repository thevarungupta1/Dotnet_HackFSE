using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Outreach.Reporting.Entity.Entities
{
    public class Enrollment
    {
        [Key]
        public int EnrollmentID { get; set; }
        [ForeignKey("Associates")]
        public int AssociateID { get; set; }
        [ForeignKey("Events")]
        [Required]
        public string EventID { get; set; }//not null 
        public string BaseLocation { get; set; }
        public string BusinessUnit { get; set; }

        [Required]
        public DateTime EventDate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal VolunteerHours { get; set; }//not null
        [Column(TypeName = "decimal(18,2)")]
        public decimal TravelHours { get; set; }//not null
        [MaxLength(50)]
        public string Status { get; set; }//50 null
        [MaxLength(100)]
        public string IIEPCategory { get; set; }//null 100
        [MaxLength(50)]
        [Required]
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        [MaxLength(50)]
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        //Navigation properties
        public Associate Associates { get; set; }
        public Event Events { get; set; }

    }
}
