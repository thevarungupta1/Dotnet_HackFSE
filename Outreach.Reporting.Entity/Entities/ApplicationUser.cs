using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;
//using Outreach.Reporting.Data.Entities;

namespace Outreach.Reporting.Entity.Entities
{
    public class ApplicationUser
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("Associates")]
        public int AssociateID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        [ForeignKey("UserRoles")]
        public int RoleID { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        //Navigation properties
        public Associate Associates { get; set; }
        public UserRole UserRoles { get; set; }
    }
}
