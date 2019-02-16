﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Outreach.Reporting.Data.Entities
{
    public class Locations
    {
        [Key]
        public int ID { get; set; }
        public string BaseLocation { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PinCode { get; set; }

        public ICollection<Associates> Associates { get; set; }
        public ICollection<Events> Events { get; set; }
    }
}
