using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Outreach.Reporting.Business.Interfaces
{
    public interface IEventProcessor
    {
        bool SaveEvents(IEnumerable<Event> events);
        IEnumerable<Event> GetAll();
        IEnumerable<Event> GetEventsRelatedData();
    }
}
