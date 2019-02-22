﻿using Outreach.Reporting.Data.Entities;
using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Outreach.Reporting.Data.Interfaces
{
    public interface IEventRepository : IRepository<Events>
    {
        IEnumerable<Events> GetEventsRelatedData();
    }
}
