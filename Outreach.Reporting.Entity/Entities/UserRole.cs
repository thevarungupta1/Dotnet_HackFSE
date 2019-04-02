using System;
using System.ComponentModel.DataAnnotations;

namespace Outreach.Reporting.Entity.Entities
{
    public class UserRole
    {
        [Key]
        public int ID { get; set; }
        public string Role { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
