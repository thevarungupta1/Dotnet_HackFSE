using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Outreach.Reporting.Business.Interfaces
{
    public interface IEventProcessor
    {
        Task<bool> SaveEvents(List<Event> events);
        Task<IEnumerable<Event>> GetAll(IDictionary<string, string> user);
        Task<IEnumerable<Event>> GetEventsRelatedData(IDictionary<string, string> user);
        Task<IEnumerable<string>> GetAllFocusArea();
        Task<IEnumerable<Event>> GetRecentEvents(IDictionary<string, string> user, int recentCount);
    }
}
