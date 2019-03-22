using Outreach.Reporting.Business.BusinessEntities;
using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Outreach.Reporting.Business.Interfaces
{
    public interface IEnrollmentProcessor
    {
        IEnumerable<Enrollment> GetAll(IDictionary<string, string> user);
        bool SaveEnrollments(IEnumerable<Enrollment> associates);
        IEnumerable<Enrollment> GetEnrolledAssociates(IDictionary<string, string> user);
        IEnumerable<Associate> GetEnrolledUniqueAssociates(IDictionary<string, string> user);
        IEnumerable<Associate> GetTopFrequentVolunteers(int count, IDictionary<string, string> user);
        Dictionary<int, List<int>> GetYearlyVolunteersCount(int yearsCount, IDictionary<string, string> user);
        IEnumerable<Enrollment> GetAllNewVolunteers(IDictionary<string, string> user);
        Dictionary<DateTime, List<int>> GetDateWiseVolunteersCount(IDictionary<string, string> user);
        List<Dictionary<string, int>> GetYearlyBuWiseVolunteersCount(int yearsCount, IDictionary<string, string> user);
        Dictionary<string, int> GetDesignationWiseVolunteersCount(IDictionary<string, string> user);
        Dictionary<string, List<decimal>> GetTopVolunteerData(IDictionary<string, string> user);
        List<NewRepeatedVolunteersByYear> GetDesignationWiseNewRepeatedVolunteersCountByYear(int years, IDictionary<string, string> user);

        IEnumerable<Enrollment> GetEnrollmentsByFilter(IDictionary<string, string> user, ReportFilter filters);
        IEnumerable<Enrollment> GetEnrollmentsByFilterId(int filterId);

        IEnumerable<string> GetBusinessUnits();
        IEnumerable<string> GetBaseLocations();
    }
}
