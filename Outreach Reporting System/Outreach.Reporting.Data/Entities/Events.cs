﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Outreach.Reporting.Data.Entities
{
    public class Events
    {
        [Key]
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? Date { get; set; }
        [ForeignKey("Locations")]
        public int LocationID { get; set; }
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

        //Navigation properties
        public Locations Locations { get; set; }
        public ICollection<AssociatesEnrolledToEvents> AssociatesEnrolledToEvents { get; set; }
    }
}
