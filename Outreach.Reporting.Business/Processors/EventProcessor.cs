using Outreach.Reporting.Business.Interfaces;
using Outreach.Reporting.Data.Interfaces;
using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Outreach.Reporting.Business.Processors
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IUnitOfWork _unitOfWork;

        public EventProcessor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Event> GetAll(int userId)
        {
            try
            {
                List<string> eventIds = GetEventIdsByUserId(userId);
                var events = _unitOfWork.Events.GetAll().Where(x => eventIds == null || eventIds.Contains(x.ID));
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
        public IEnumerable<Event> GetEventsRelatedData(int userId)
        {
            try
            {
                List<string> eventIds = GetEventIdsByUserId(userId);
                return _unitOfWork.Events.GetEventsRelatedData().Where(x => eventIds == null || eventIds.Contains(x.ID));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool SaveEvents(IEnumerable<Event> events)
        {
            foreach (var row in events)
            {
                row.CreatedOn = DateTime.Now;
            }
            _unitOfWork.Events.AddRange(events);
            _unitOfWork.Complete();
            return true;
        }

        private List<string> GetEventIdsByUserId(int userId)
        {
            if (userId == 0)
                return null;
            return _unitOfWork.PointOfContacts.GetAll().Where(x => x.AssociateID == userId).Select(s => s.EventID).ToList();
        }
    }
}
