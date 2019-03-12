using Outreach.Reporting.Business.Interfaces;
using Outreach.Reporting.Data.Interfaces;
using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
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
        public IEnumerable<Event> GetAll()
        {
            try
            {
                var events = _unitOfWork.Events.GetAll();
                foreach(var even in events)
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
        public IEnumerable<Event> GetEventsRelatedData()
        {
            try
            {
                var x = _unitOfWork.Events.GetEventsRelatedData();
                return x;
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
    }
}
