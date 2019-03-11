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

        public IEnumerable<Enrollments> GetAllNewVolunteers()
        {
            try
            {
                var allVolunteers = _unitOfWork.Enrollments.GetEnrolledAssociates();

                var newVolunteers = new List<Enrollments>();
                foreach(var volunteer in allVolunteers)
                {
                    if (!newVolunteers.Any(x => x.AssociateID == volunteer.AssociateID))
                        newVolunteers.Add(volunteer);
                }
                return newVolunteers.OrderBy(o => o.EventDate);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Dictionary<DateTime, List<int>> GetDateWiseVolunteersCount()
        {
            try
            {
                var enrollments = _unitOfWork.Enrollments.GetEnrolledAssociates();

                var groupedData = enrollments.GroupBy(enroll => enroll.EventDate)
                //.Select(group => group);
                .Select(group => new
                {
                    eventyear = group.Select(s => s.EventDate).FirstOrDefault(),
                    enrollments = group
                })
                .OrderBy(x => x.eventyear);

                var volunteersCount = new Dictionary<DateTime, List<int>>();
                int recurCount = 0;
                int newCount = 0;
                var tmpVolunteerIds = new List<int>();
                var recurVolunteers = new List<Enrollments>();
                foreach (var enrollment in groupedData)
                {                    
                    recurCount = 0;
                    newCount = 0;
                    foreach (var enroll in enrollment.enrollments)
                    {
                        if (tmpVolunteerIds.Any(a => a == enroll.AssociateID))
                            recurCount++;
                        else newCount++;

                        tmpVolunteerIds.Add(enroll.AssociateID);
                    }
                    volunteersCount.Add(enrollment.enrollments.Select(s => s.EventDate).First(), new List<int> { newCount, recurCount });
                }
               // groupedData.SelectMany(group => group.enrollments.Select(s => s));

                //var allEnrollments = _unitOfWork.Enrollments.GetEnrolledAssociates().Select(s => new { s.EventDate.Year, s.AssociateID });
                //var enrollments = _unitOfWork.Enrollments.GetYearlyVolunteersCount(yearsCount).Select(s => new { s.EventDate.Year, s.AssociateID, s.EnrollmentID });

                //var volunteersCount = new Dictionary<int, List<int>>();
                //int prevYear = 0;
                //int currYear = 0;
                //int recurCount = 0;
                //int newCount = 0;

                //int enrollId = enrollments.Last().EnrollmentID;
                //foreach (var enroll in enrollments)
                //{
                //    currYear = enroll.Year;

                //    if (prevYear == 0)
                //        prevYear = currYear;

                //    if (prevYear > 0 && prevYear != currYear && (newCount > 0 || recurCount > 0))
                //    {
                //        volunteersCount.Add(prevYear, new List<int> { newCount, recurCount });
                //        prevYear = currYear;
                //        newCount = 0;
                //        recurCount = 0;
                //    }

                //    if (allEnrollments.Any(x => x.AssociateID == enroll.AssociateID && x.Year < currYear))
                //        recurCount++;
                //    else newCount++;

                //    if (enroll.EnrollmentID == enrollId)
                //    {
                //        volunteersCount.Add(currYear, new List<int> { newCount, recurCount });
                //    }
                //}

                return volunteersCount;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Dictionary<string, int>> GetYearlyBuWiseVolunteersCount(int yearsCount)
        {
            try
            {
                var businessUnits = _unitOfWork.Associates.GetAll().Select(s => s.BusinessUnit).Distinct();// .GroupBy(g=> g.BusinessUnit)
                var enrollments = _unitOfWork.Enrollments.GetEnrollments();

                var groupedData = enrollments.GroupBy(enroll => enroll.EventDate.Year)
                                               .Select(group => new
                                               {
                                                   //eventyear = group.Select(s => s.EventDate).FirstOrDefault(),
                                                   enrollments = group
                                               })
                                               .OrderBy(x => x.enrollments.FirstOrDefault().EventDate).Take(yearsCount);
                var enrollmentData = groupedData.Select(s => s.enrollments);

                List<Dictionary<string, int>> yearlyBuData = new List<Dictionary<string, int>>();
                Dictionary<string, int> buCount;

                foreach (var yearlyGroup in enrollmentData)
                {
                    buCount = new Dictionary<string, int>();
                    buCount.Add("year", yearlyGroup.Key);
                    foreach (var bu in businessUnits)
                    {
                        buCount.Add(bu, yearlyGroup.Where(x => x.Associates.BusinessUnit == bu).Count());
                    }
                    yearlyBuData.Add(buCount);
                }

                return yearlyBuData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Dictionary<string, int> GetDesignationWiseVolunteersCount()
        {
            try
            {
                var enrolledAssociates = _unitOfWork.Enrollments.GetEnrollments().Select(s => s.Associates).Distinct();

                var groupedData = enrolledAssociates.GroupBy(enroll => enroll.Designation)
                                               .Select(group => group);

                Dictionary<string, int> designationCount = new Dictionary<string, int>();

                foreach (var enroll in groupedData)
                {                    
                    designationCount.Add(enroll.Key, enroll.Count());
                }

                return designationCount;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
