using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Outreach.Reporting.Data.Entities
{
    public class Users
    {
        [Key]
        public int ID { get; set; }
        public string UserName { get; set; }
        [ForeignKey("UserRoles")]
        public int RoleID { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        //Navigation properties
        public UserRoles UserRoles { get; set; }
    }
}
