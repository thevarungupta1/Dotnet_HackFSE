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
        IEnumerable<Enrollments> GetEnrolledAssociates();
        IEnumerable<Associates> GetTopFrequentVolunteers(int count);
        Dictionary<int, List<int>> GetYearlyVolunteersCount(int yearsCount);
        IEnumerable<Enrollments> GetAllNewVolunteers();
        Dictionary<DateTime, List<int>> GetDateWiseVolunteersCount();
        List<Dictionary<string, int>> GetYearlyBuWiseVolunteersCount(int yearsCount);
        Dictionary<string, int> GetDesignationWiseVolunteersCount();
    }
}
