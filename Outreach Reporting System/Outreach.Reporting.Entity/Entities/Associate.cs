using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Outreach.Reporting.Entity.Entities
{
    public class Associate
    {
        [Key]
       // [MaxLength(6)]
        public int ID { get; set; }//6digit
        [MaxLength(50)]
        public string Name { get; set; }//50 char  null
        [MaxLength(50)]
        public string Designation { get; set; }//50 null
        [MaxLength(20)]
        public string ContactNumber { get; set; }//20 null
        [MaxLength(50)]
        public string BaseLocation { get; set; }//50 null
        [MaxLength(50)]
        public string BusinessUnit { get; set; }// business unit -50 null
        [MaxLength(50)]
        [Required]
        public string CreatedBy { get; set; }//50 not null
        public DateTime CreatedOn { get; set; }
        [MaxLength(50)]
        public string ModifiedBy { get; set; }// null 50
        public DateTime? ModifiedOn { get; set; }

        //Navigation properties
        //public ICollection<Enrollments> Enrollments { get; set; }
    }
}
