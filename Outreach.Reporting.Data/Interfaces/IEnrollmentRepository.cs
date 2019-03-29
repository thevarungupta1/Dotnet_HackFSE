using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outreach.Reporting.Data.Interfaces
{
    public interface IEnrollmentRepository : IRepository<Enrollment>
    {
        Task<IEnumerable<Enrollment>> GetEnrolledAssociates();
        Task<IEnumerable<Enrollment>> GetTopFrequentVolunteers(int count);
        Task<IEnumerable<Enrollment>> GetYearlyVolunteersCount(int yearsCount);
        Task<IEnumerable<Enrollment>> GetEnrollments();
        Task<IEnumerable<Enrollment>> GetEnrollmentsByYears(int yearFrom);
        Task<IEnumerable<string>> GetBusinessUnits();
        Task<IEnumerable<string>> GetBaseLocations();
        Task<IEnumerable<Enrollment>> GetEnrollmentsWithRelatedTable();
    }
}
