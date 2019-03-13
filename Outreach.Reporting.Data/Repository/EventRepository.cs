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
    public class EventRepository : Repository<Event>, IEventRepository
    {
        public EventRepository(ReportDBContext context) : base(context)
        {
        }

        public ReportDBContext ReportContext
        {
            get { return Context as ReportDBContext; }
        }
        public IEnumerable<Event> GetEventsRelatedData()
        {
            return ReportContext.Events.ToList();
        }

    }
}
