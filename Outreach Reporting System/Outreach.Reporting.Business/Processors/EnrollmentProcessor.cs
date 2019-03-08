using Outreach.Reporting.Business.Interfaces;
using Outreach.Reporting.Data.Interfaces;
using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Outreach.Reporting.Business.Processors
{
    public class EnrollmentProcessor : IEnrollmentProcessor
    {
        private readonly IUnitOfWork _unitOfWork;

        public EnrollmentProcessor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Enrollments> GetAll()
        {
            try
            {
                return _unitOfWork.Enrollments.GetAll();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<Enrollments> GetEnrolledAssociates()
        {
            try
            {
               return _unitOfWork.Enrollments.GetEnrolledAssociates();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool SaveEnrollments(IEnumerable<Enrollments> enrollments)
        {
            foreach (var row in enrollments)
            {
                row.CreatedOn = DateTime.Now;
            }
            _unitOfWork.Enrollments.AddRange(enrollments);
            _unitOfWork.Complete();
            return true;
        }
        public IEnumerable<Associates> GetTopFrequentVolunteers(int count)
        {
            try
            {
                return _unitOfWork.Enrollments.GetTopFrequentVolunteers(count);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Dictionary<int, List<int>> GetYearlyVolunteersCount(int yearsCount)
        {
            try
            {
                var allEnrollments = _unitOfWork.Enrollments.GetEnrolledAssociates().Select(s => new { s.EventDate.Year, s.AssociateID });
                var enrollments = _unitOfWork.Enrollments.GetYearlyVolunteersCount(yearsCount).Select(s => new { s.EventDate.Year, s.AssociateID, s.EnrollmentID });

                var volunteersCount = new Dictionary<int, List<int>>();
                int prevYear = 0;
                int currYear = 0;
                int recurCount = 0;
                int newCount = 0;

                int enrollId = enrollments.Last().EnrollmentID;
                foreach (var enroll in enrollments)
                {
                    currYear = enroll.Year;

                    if (prevYear == 0)
                        prevYear = currYear;

                    if (prevYear > 0 && prevYear != currYear && (newCount > 0 || recurCount > 0))
                    {
                        volunteersCount.Add(prevYear, new List<int> { newCount, recurCount });
                        prevYear = currYear;
                        newCount = 0;
                        recurCount = 0;
                    }

                    if (allEnrollments.Any(x => x.AssociateID == enroll.AssociateID && x.Year < currYear))
                        recurCount++;
                    else newCount++;

                    if (enroll.EnrollmentID == enrollId)
                    {
                        volunteersCount.Add(currYear, new List<int> { newCount, recurCount });
                    }
                }

                return volunteersCount;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
