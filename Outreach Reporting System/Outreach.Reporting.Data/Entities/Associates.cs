using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Outreach.Reporting.Data.Entities
{
    public class Associates
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string ContactNumber { get; set; }
        public int BaseLocationID { get; set; }
        public string BU { get; set; }
        public string SBU { get; set; }
        public string Account { get; set; }
        public string Client { get; set; }
        
        public Locations Locations { get; set; }
        public ICollection<AssociatesEnrolledToEvents> AssociatesEnrolledToEvents { get; set; }
    }
}
