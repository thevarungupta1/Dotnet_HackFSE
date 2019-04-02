using Outreach.Reporting.Entity.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Outreach.Reporting.Data.Interfaces
{
    public interface IEventRepository : IRepository<Event>
    {
        Task<IEnumerable<Event>> GetEventsRelatedData();
    }
}
