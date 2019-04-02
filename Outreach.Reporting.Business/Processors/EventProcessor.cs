using Outreach.Reporting.Business.Interfaces;
using Outreach.Reporting.Data.Interfaces;
using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Outreach.Reporting.Business.Processors
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IUnitOfWork _unitOfWork;

        public EventProcessor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Event>> GetAll(IDictionary<string, string> user)
        {
            try
            {
                List<string> eventIds = null;
                if (user != null && user["role"] == "POC")
                {
                    int userId = Convert.ToInt32(user["userId"]);
                    eventIds = await GetEventIdsByUserId(userId);
                }
                var tmpevents = await _unitOfWork.Events.GetAllAsync();
                 var events = tmpevents.Where(x => eventIds == null || eventIds.Contains(x.ID));
                foreach (var even in events)
                {
                    even.Date = even.Date.Date;
                }
                return events;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<IEnumerable<Event>> GetEventsRelatedData(IDictionary<string, string> user)
        {
            try
            {
                List<string> eventIds = null;
                if (user != null && user["role"] == "POC")
                {
                    int userId = Convert.ToInt32(user["userId"]);
                    eventIds = await GetEventIdsByUserId(userId);
                }
                var result = await _unitOfWork.Events.GetEventsRelatedData();
                return result.Where(x => eventIds == null || eventIds.Contains(x.ID));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Event>> GetRecentEvents(IDictionary<string, string> user, int recentCount)
        {
            try
            {
                List<string> eventIds = null;
                if (user != null && user["role"] == "POC")
                {
                    int userId = Convert.ToInt32(user["userId"]);
                    eventIds = await GetEventIdsByUserId(userId);
                }
                var result = await _unitOfWork.Events.GetAllAsync();
                return result.Where(x => eventIds == null || eventIds.Contains(x.ID)).OrderByDescending(o => o.Date).Take(recentCount);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> SaveEvents(List<Event> events)
        {
            var data = _unitOfWork.Events.GetAll();
            foreach (var ev in data)
            {
                events.RemoveAll(a => a.ID == ev.ID);
            }
            foreach (var row in events)
            {
                row.CreatedOn = DateTime.Now;
            }
            await _unitOfWork.Events.AddRangeAsync(events);
            _unitOfWork.Complete();
            return true;
        }

        private async Task<List<string>> GetEventIdsByUserId(int userId)
        {
            List<string> eventIds = null;
            try
            {
                if (userId == 0)
                    return null;
                var tmpresult = await _unitOfWork.PointOfContacts.GetAllAsync();
                var result = tmpresult.Where(x => x.AssociateID == userId).Select(s => s.EventIDs);
                if (result != null && result.Any())
                {
                    eventIds = new List<string>();
                    foreach (var eventid in result)
                    {
                        eventIds.AddRange(eventid.Split(','));
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return eventIds;
        }

        public async Task<IEnumerable<string>> GetAllFocusArea()
        {
            try
            {
                var result = await _unitOfWork.Events.GetAllAsync();
                return result.Select(s => s.Project + " - " + s.Category).Distinct();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
