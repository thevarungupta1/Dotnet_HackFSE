using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Outreach.Reporting.Data.Interfaces
{
    public interface IEnrollmentRepository : IRepository<Enrollment>
    {
        IEnumerable<Enrollment> GetEnrolledAssociates();
        IEnumerable<Associate> GetTopFrequentVolunteers(int count);
        IEnumerable<Enrollment> GetYearlyVolunteersCount(int yearsCount);
        IQueryable<Enrollment> GetEnrollments();
        IQueryable<Enrollment> GetEnrollmentsByYears(int yearFrom);
        IEnumerable<string> GetBusinessUnits();
        IEnumerable<string> GetBaseLocations();
    }
}
