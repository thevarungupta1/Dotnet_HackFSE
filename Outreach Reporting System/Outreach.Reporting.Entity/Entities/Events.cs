using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Outreach.Reporting.Entity.Entities
{
    public class Events
    {
        [Key]
        //[MaxLength(50)]
        public string ID { get; set; }//50
        [MaxLength(100)]
        public string Name { get; set; }//null 100
        public string Description { get; set; }
        public string Date { get; set; }//datetime
                                        //[ForeignKey("Locations")]
                                        //public int LocationID { get; set; }
        public int TotalVolunteers { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalVolunteerHours { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalTravelHours { get; set; }
        public int? LivesImpacted { get; set; }
        [MaxLength(50)]
        [Required]
        public string BaseLocation { get; set; }//not null 50
        [MaxLength(250)]
        public string Address { get; set; }//null 250
        [MaxLength(100)]
        public string City { get; set; }// null 100
        [MaxLength(100)]
        public string State { get; set; }// null 100
        [MaxLength(100)]
        public string Country { get; set; }// null 100
        [MaxLength(20)]
        public string PinCode { get; set; }// null 20
        [MaxLength(100)]
        public string Beneficiary { get; set; }// null 100
        [MaxLength(100)]
        public string CouncilName { get; set; }// null 100
        [MaxLength(100)]
        public string Project { get; set; }// null 100
        [MaxLength(100)]
        public string Category { get; set; }// null 100
        public int? ActivityType { get; set; }
        [MaxLength(50)]
        public string Status { get; set; }// null 50
        [MaxLength(50)]
        [Required]
        public string CreatedBy { get; set; }// not null50
        public DateTime CreatedOn { get; set; }
        [MaxLength(50)]
        public string ModifiedBy { get; set; }//  null50
        public DateTime? ModifiedOn { get; set; }

        //Navigation properties
       // public Locations Locations { get; set; }
        //public ICollection<Enrollments> Enrollments { get; set; }
    }
}
