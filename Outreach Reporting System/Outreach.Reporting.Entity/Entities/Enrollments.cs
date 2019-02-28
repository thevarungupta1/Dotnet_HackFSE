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
        [Required]
        public string EventID { get; set; }//not null 
        [Column(TypeName = "decimal(18,2)")]
        public decimal VolunteerHours { get; set; }//not null
        [Column(TypeName = "decimal(18,2)")]
        public decimal TravelHours { get; set; }//not null
        [MaxLength(50)]
        public string Status { get; set; }//50 null
        [MaxLength(100)]
        public string IIEPCategory { get; set; }//null 100
        //[Required]
        public bool IsPOC { get; set; }//not null
        [MaxLength(50)]
        [Required]
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        [MaxLength(50)]
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        //Navigation properties
        public Associates Associates { get; set; }
        public Events Events { get; set; }

    }
}
