using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Outreach.Reporting.Business.Interfaces
{
    public interface IEnrollmentProcessor
    {
        IEnumerable<Enrollment> GetAll();
        bool SaveEnrollments(IEnumerable<Enrollment> associates);
        IEnumerable<Associate> GetEnrolledAssociates();
        IEnumerable<Associate> GetEnrolledUniqueAssociates();
        IEnumerable<Associate> GetTopFrequentVolunteers(int count);
        Dictionary<int, List<int>> GetYearlyVolunteersCount(int yearsCount);
        IEnumerable<Enrollment> GetAllNewVolunteers();
        Dictionary<DateTime, List<int>> GetDateWiseVolunteersCount();
        List<Dictionary<string, int>> GetYearlyBuWiseVolunteersCount(int yearsCount);
        Dictionary<string, int> GetDesignationWiseVolunteersCount();
        Dictionary<string, List<decimal>> GetTopVolunteerData();
        Dictionary<string, int> GetDesignationWiseVolunteersByYear(int years);
    }
}
