using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Outreach.Reporting.Data.Entities
{
    public class Users
    {
        [Key]
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
    }
}
