using Outreach.Reporting.Business.BusinessEntities;
using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Outreach.Reporting.Business.Interfaces
{
    public interface IEnrollmentProcessor
    {
        IEnumerable<Enrollment> GetAll(int userId);
        bool SaveEnrollments(IEnumerable<Enrollment> associates);
        IEnumerable<Enrollment> GetEnrolledAssociates(int userId);
        IEnumerable<Associate> GetEnrolledUniqueAssociates(int userId);
        IEnumerable<Associate> GetTopFrequentVolunteers(int count, int userId);
        Dictionary<int, List<int>> GetYearlyVolunteersCount(int yearsCount, int userId);
        IEnumerable<Enrollment> GetAllNewVolunteers(int userId);
        Dictionary<DateTime, List<int>> GetDateWiseVolunteersCount(int userId);
        List<Dictionary<string, int>> GetYearlyBuWiseVolunteersCount(int yearsCount, int userId);
        Dictionary<string, int> GetDesignationWiseVolunteersCount(int userId);
        Dictionary<string, List<decimal>> GetTopVolunteerData(int userId);
        List<NewRepeatedVolunteersByYear> GetDesignationWiseNewRepeatedVolunteersCountByYear(int years, int userId);

        IEnumerable<Enrollment> GetEnrollmentsByFilter(int userId, ReportFilter filters);
        IEnumerable<Enrollment> GetEnrollmentsByFilterId(int filterId);

        IEnumerable<string> GetBusinessUnits();
        IEnumerable<string> GetBaseLocations();
    }
}
