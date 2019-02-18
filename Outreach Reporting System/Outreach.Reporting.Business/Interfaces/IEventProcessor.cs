using Outreach.Reporting.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Outreach.Reporting.Business.Interfaces
{
    public interface IEventProcessor
    {
        bool SaveEvents(List<EventModel> events);
    }
}
