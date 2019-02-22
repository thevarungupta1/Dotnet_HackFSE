using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Outreach.Reporting.Business.Interfaces
{
    public interface IEnrollmentProcessor
    {
        IEnumerable<Enrollments> GetAll();
        bool SaveEnrollments(IEnumerable<Enrollments> associates);
        IEnumerable<Enrollments> GetEnrollmentsRelatedData();
    }
}
