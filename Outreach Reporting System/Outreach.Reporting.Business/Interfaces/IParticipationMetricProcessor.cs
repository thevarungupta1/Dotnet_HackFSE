using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Outreach.Reporting.Business.Interfaces
{
    public interface IParticipationMetricProcessor
    {
        IEnumerable<Associates> GetAllAssociates();
        IEnumerable<Associates> GetUniqueVolunteers();
        IEnumerable<Enrollments> GetAllEnrollments();
    }
}
