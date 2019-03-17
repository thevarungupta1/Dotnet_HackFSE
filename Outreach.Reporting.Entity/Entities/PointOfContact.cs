using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Outreach.Reporting.Entity.Entities
{
    public class PointOfContact
    {
        [Key]
        public int ID { get; set; }
        public int AssociateID { get; set; }        
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string EventID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
