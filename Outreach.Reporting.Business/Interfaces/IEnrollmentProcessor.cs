using Outreach.Reporting.Business.BusinessEntities;
using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Outreach.Reporting.Business.Interfaces
{
    public interface IEnrollmentProcessor
    {
        Task<IEnumerable<Enrollment>> GetAll(IDictionary<string, string> user);
        Task<bool> SaveEnrollments(IEnumerable<Enrollment> associates);
        Task<IEnumerable<Enrollment>> GetEnrolledAssociates(IDictionary<string, string> user);
        Task<IEnumerable<Associate>> GetEnrolledUniqueAssociates(IDictionary<string, string> user);
        Task<IEnumerable<Enrollment>> GetUniqueVolunteersByDate(string fromDate, string toDate, IDictionary<string, string> user);
       Task<IEnumerable<Associate>> GetTopFrequentVolunteers(int count, IDictionary<string, string> user);
        Task<Dictionary<int, List<int>>> GetYearlyVolunteersCount(int yearsCount, IDictionary<string, string> user);
        Task<IEnumerable<Enrollment>> GetAllNewVolunteers(IDictionary<string, string> user);
        Task<Dictionary<DateTime, List<int>>> GetDateWiseVolunteersCount(IDictionary<string, string> user);
        Task<List<Dictionary<string, int>>> GetYearlyBuWiseVolunteersCount(int yearsCount, IDictionary<string, string> user);
        Task<Dictionary<string, int>> GetDesignationWiseVolunteersCount(IDictionary<string, string> user);
        Task<Dictionary<string, List<decimal>>> GetTopVolunteerData(IDictionary<string, string> user);
        Task<List<NewRepeatedVolunteersByYear>> GetDesignationWiseNewRepeatedVolunteersCountByYear(int years, IDictionary<string, string> user);

        Task<IEnumerable<Enrollment>> GetEnrollmentsByFilter(IDictionary<string, string> user, ReportFilter filters);
        Task<IEnumerable<Enrollment>> GetEnrollmentsByFilterId(int filterId);

        Task<IEnumerable<string>> GetBusinessUnits();
        Task<IEnumerable<string>> GetBaseLocations();
    }
}
