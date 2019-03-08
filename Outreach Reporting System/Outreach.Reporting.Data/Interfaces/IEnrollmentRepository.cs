using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Outreach.Reporting.Data.Interfaces
{
    public interface IEnrollmentRepository : IRepository<Enrollments>
    {
        IEnumerable<Enrollments> GetEnrolledAssociates();
        IEnumerable<Associates> GetTopFrequentVolunteers(int count);
        IEnumerable<Enrollments> GetYearlyVolunteersCount(int yearsCount);
    }
}
