using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Outreach.Reporting.Entity.Entities
{
    public class File
    {
        [Key]
        public int ID { get; set; }
        public string FileName { get; set; }
        [ForeignKey("Associates")]
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public Associate Associates { get; set; }
    }
}
