using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Outreach.Reporting.Business.Interfaces
{
    public interface IEventProcessor
    {
        bool SaveEvents(IEnumerable<Event> events);
        IEnumerable<Event> GetAll(IDictionary<string, string> user);
        IEnumerable<Event> GetEventsRelatedData(IDictionary<string, string> user);
        IEnumerable<string> GetAllFocusArea();
        IEnumerable<Event> GetRecentEvents(IDictionary<string, string> user, int recentCount);
    }
}
