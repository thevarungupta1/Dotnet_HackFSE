using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Outreach.Reporting.Entity.Entities
{
    public class Associates
    {
        [Key]
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

        //Navigation properties
        //public ICollection<Enrollments> Enrollments { get; set; }
    }
}
