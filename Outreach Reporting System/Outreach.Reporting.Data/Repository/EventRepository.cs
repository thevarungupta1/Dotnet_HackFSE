using Outreach.Reporting.Data.Entities;
using Outreach.Reporting.Data.Interfaces;
using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Outreach.Reporting.Data.Repository
{
    public class EventRepository : Repository<Events>, IEventRepository
    {
        public EventRepository(ReportContext context) : base(context)
        {
        }

        public ReportContext ReportContext
        {
            get { return Context as ReportContext; }
        }
        public IEnumerable<Events> GetEventsRelatedData()
        {
            return ReportContext.Events.ToList();
        }

    }
}
