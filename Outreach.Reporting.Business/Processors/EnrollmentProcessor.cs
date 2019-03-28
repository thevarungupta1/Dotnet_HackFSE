using Outreach.Reporting.Business.BusinessEntities;
using Outreach.Reporting.Business.Interfaces;
using Outreach.Reporting.Data.Interfaces;
using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outreach.Reporting.Business.Processors
{
    public class EnrollmentProcessor : IEnrollmentProcessor
    {
        private readonly IUnitOfWork _unitOfWork;

        public EnrollmentProcessor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Enrollment>> GetAll(IDictionary<string, string> user)
        {
            try
            {
                List<string> eventIds = null;
                if (user != null && user["role"] == "POC")
                {
                    int userId = Convert.ToInt32(user["userId"]);
                    eventIds = await GetEventIdsByUserId(userId);
                }
                var result = await _unitOfWork.Enrollments.GetAllAsync();
                return result.Where(x => eventIds == null || eventIds.Contains(x.EventID));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Enrollment>> GetEnrolledAssociates(IDictionary<string, string> user)
        {
            try
            {
                List<string> eventIds = null;
                if (user != null && user["role"] == "POC")
                {
                    int userId = Convert.ToInt32(user["userId"]);
                    eventIds = await GetEventIdsByUserId(userId);
                }
               var result = await _unitOfWork.Enrollments.GetEnrolledAssociates();
                return result.Where(x => eventIds == null || eventIds.Contains(x.EventID)).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Associate>> GetEnrolledUniqueAssociates(IDictionary<string, string> user)
        {
            try
            {
                List<string> eventIds = null;
                if (user != null && user["role"] == "POC")
                {
                    int userId = Convert.ToInt32(user["userId"]);
                    eventIds = await GetEventIdsByUserId(userId);
                }
                var result = await _unitOfWork.Enrollments.GetEnrolledAssociates();
                return result.Where(x => eventIds == null || eventIds.Contains(x.EventID)).Select(s => s.Associates).Distinct().ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Enrollment>> GetUniqueVolunteersByDate(string fromDate, string toDate, IDictionary<string, string> user)
        {
            try
            {
                List<string> eventIds = null;
                if (user != null && user["role"] == "POC")
                {
                    int userId = Convert.ToInt32(user["userId"]);
                    eventIds = await GetEventIdsByUserId(userId);
                }
                DateTime? dtFromDate = null;
                DateTime? dtToDate = null;
                if (!string.IsNullOrEmpty(fromDate))
                    dtFromDate = Convert.ToDateTime(fromDate);
                if (!string.IsNullOrEmpty(toDate))
                    dtToDate = Convert.ToDateTime(toDate);
                var result = await _unitOfWork.Enrollments.GetEnrolledAssociates();
                    return result.Where(x => (eventIds == null || eventIds.Contains(x.EventID))
                                                                                && (fromDate == null || x.EventDate >= dtFromDate)
                                                                                && (toDate == null || x.EventDate <= dtToDate)).Distinct().ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> SaveEnrollments(IEnumerable<Enrollment> enrollments)
        {
            try
            {
                foreach (var row in enrollments)
                {
                    row.CreatedOn = DateTime.Now;
                }
                await _unitOfWork.Enrollments.AddRangeAsync(enrollments);
                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                return false;
            }
            
            return true;
        }
        public async Task<IEnumerable<Associate>> GetTopFrequentVolunteers(int count, IDictionary<string, string> user)
        {
            try
            {
                List<string> eventIds = null;
                if (user != null && user["role"] == "POC")
                {
                    int userId = Convert.ToInt32(user["userId"]);
                    eventIds = await GetEventIdsByUserId(userId);
                }
                return await _unitOfWork.Enrollments.GetTopFrequentVolunteers(count);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Dictionary<int, List<int>>> GetYearlyVolunteersCount(int yearsCount, IDictionary<string, string> user)
        {
            try
            {
                List<string> eventIds = null;
                if (user != null && user["role"] == "POC")
                {
                    int userId = Convert.ToInt32(user["userId"]);
                    eventIds = await GetEventIdsByUserId(userId);
                }

                var tmpEnrollments = await _unitOfWork.Enrollments.GetEnrolledAssociates();
                   var allEnrollments = tmpEnrollments.Select(s => new { s.EventDate.Year, s.AssociateID });

                var tmpenrollments = await _unitOfWork.Enrollments.GetYearlyVolunteersCount(yearsCount);
                var enrollments = tmpenrollments.Where(x => eventIds == null || eventIds.Contains(x.EventID)).Select(s => new { s.EventDate.Year, s.AssociateID, s.EnrollmentID });

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

        public async Task<IEnumerable<Enrollment>> GetAllNewVolunteers(IDictionary<string, string> user)
        {
            try
            {
                List<string> eventIds = null;
                if (user != null && user["role"] == "POC")
                {
                    int userId = Convert.ToInt32(user["userId"]);
                    eventIds = await GetEventIdsByUserId(userId);
                }

                var tmpVolunteers = await _unitOfWork.Enrollments.GetEnrolledAssociates();
                var allVolunteers = tmpVolunteers.Where(x => eventIds == null || eventIds.Contains(x.EventID)).OrderBy(o => o.EventDate);

                var newVolunteers = new List<Enrollment>();
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

        public async Task<Dictionary<DateTime, List<int>>> GetDateWiseVolunteersCount(IDictionary<string, string> user)
        {
            try
            {
                List<string> eventIds = null;
                if (user != null && user["role"] == "POC")
                {
                    int userId = Convert.ToInt32(user["userId"]);
                    eventIds = await GetEventIdsByUserId(userId);
                }

                var tmpenrollments = await _unitOfWork.Enrollments.GetEnrolledAssociates();
                var enrollments = tmpenrollments.Where(x => eventIds == null || eventIds.Contains(x.EventID));

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
                var recurVolunteers = new List<Enrollment>();
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
                return volunteersCount;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Dictionary<string, int>>> GetYearlyBuWiseVolunteersCount(int yearsCount, IDictionary<string, string> user)
        {
            try
            {
                List<string> eventIds = null;
                if (user != null && user["role"] == "POC")
                {
                    int userId = Convert.ToInt32(user["userId"]);
                    eventIds = await GetEventIdsByUserId(userId);
                }
                var result = await _unitOfWork.Associates.GetAllAsync();
                var businessUnits = result.Select(s => s.BusinessUnit).Distinct();// .GroupBy(g=> g.BusinessUnit)
                var data= await _unitOfWork.Enrollments.GetEnrollments();
                var enrollments = data.Where(x => eventIds == null || eventIds.Contains(x.EventID));

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

        public async Task<Dictionary<string, int>> GetDesignationWiseVolunteersCount(IDictionary<string, string> user)
        {
            try
            {
                List<string> eventIds = null;
                if (user != null && user["role"] == "POC")
                {
                    int userId = Convert.ToInt32(user["userId"]);
                    eventIds = await GetEventIdsByUserId(userId);
                }
                var data = await _unitOfWork.Enrollments.GetEnrollments();
                var enrolledAssociates = data.Where(x => eventIds == null || eventIds.Contains(x.EventID)).Select(s => s.Associates).Distinct();

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

        public async Task<Dictionary<string, List<decimal>>> GetTopVolunteerData(IDictionary<string, string> user)
        {
            try
            {
                List<string> eventIds = null;
                if (user != null && user["role"] == "POC")
                {
                    int userId = Convert.ToInt32(user["userId"]);
                    eventIds = await GetEventIdsByUserId(userId);
                }

                List<decimal> decimalList;
                decimal count = 0;
                decimal percentage = 0;
                Dictionary<string, List<decimal>> data = new Dictionary<string, List<decimal>>();
                var enrollments = await _unitOfWork.Enrollments.GetEnrollments();
                decimal enrollmentsCount =  enrollments.Where(x => eventIds == null || eventIds.Contains(x.EventID)).Count();

                if (enrollments != null)
                {
                    var buGroup = enrollments.GroupBy(enroll => enroll.Associates.BusinessUnit)
                                                   .Select(group => new { bu = group.Key, count = group.Count() })
                                                   .OrderByDescending(o => o.count).Take(1).ToList();
                    if (buGroup != null)
                    {
                        decimalList = new List<decimal>();
                        var buData = buGroup.First();
                        count = buData.count;
                        percentage = (count / enrollmentsCount)*100;
                        decimalList.Add(count);
                        decimalList.Add(percentage);
                        data.Add(buData.bu, decimalList);
                    }

                    var designGroup = enrollments.GroupBy(enroll => enroll.Associates.Designation)
                                                  .Select(group => new { design = group.Key, count = group.Count() })
                                                  .OrderByDescending(o => o.count).Take(1).ToList();
                    if (designGroup != null)
                    {
                        decimalList = new List<decimal>();
                        var designData = designGroup.First();
                        count = designData.count;
                        percentage = (count / enrollmentsCount) * 100;
                        decimalList.Add(count);
                        decimalList.Add(percentage);
                        data.Add(designData.design, decimalList);
                    }

                    var locationGroup = enrollments.GroupBy(enroll => enroll.Associates.BaseLocation)
                                                 .Select(group => new { location = group.Key, count = group.Count() })
                                                 .OrderByDescending(o => o.count).Take(1).ToList();
                    if (locationGroup != null)
                    {
                        decimalList = new List<decimal>();
                        var locationData = locationGroup.First();
                        count = locationData.count;
                        percentage = (count / enrollmentsCount) * 100;
                        decimalList.Add(count);
                        decimalList.Add(percentage);
                        data.Add(locationData.location, decimalList);
                    }

                    var yearlyGroup = enrollments.GroupBy(enroll => enroll.EventDate.Year)
                                                .Select(group => new { year = group.Key, count = group.Count() })
                                                .OrderByDescending(o => o.count).Take(1).ToList();
                    if (yearlyGroup != null)
                    {
                        decimalList = new List<decimal>();
                        var yearlyData = yearlyGroup.First();
                        count = yearlyData.count;
                        percentage = (count / enrollmentsCount) * 100;
                        decimalList.Add(count);
                        decimalList.Add(percentage);
                        data.Add(yearlyData.year.ToString(), decimalList);
                    }
                }
                return  data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<Dictionary<DateTime, List<int>>> GetMonthWiseVolunteersCount(IDictionary<string, string> user)
        {
            try
            {
                List<string> eventIds = null;
                if (user != null && user["role"] == "POC")
                {
                    int userId = Convert.ToInt32(user["userId"]);
                    eventIds = await GetEventIdsByUserId(userId);
                }

                var tmpenrollments = await _unitOfWork.Enrollments.GetEnrolledAssociates();
                var enrollments = tmpenrollments.Where(x => eventIds == null || eventIds.Contains(x.EventID));

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
                var recurVolunteers = new List<Enrollment>();
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
                return volunteersCount;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //public Dictionary<string, List<int>> GetDesignationWiseVolunteersAndAssociateCount()
        //{
        //    try
        //    {
        //        var associates = _unitOfWork.Associates.GetAll();
        //        var enrollments = _unitOfWork.Enrollments.GetEnrollments();

        //        List<Associates>

        //        foreach (var enrollment in enrollments)
        //        {
        //            if (enrollment.EventDate.Month <= 3)
        //            {
        //                //q1
        //            }
        //            else if (enrollment.EventDate.Month <= 6)
        //            {
        //                //q2
        //            }
        //            else if (enrollment.EventDate.Month <= 9)
        //            {
        //                //q3
        //            }
        //            else
        //            {
        //                //q4
        //            }
        //        }

        //        var groupedData = enrolledAssociates.GroupBy(enroll => enroll.Designation)
        //                                       .Select(group => group);

        //        Dictionary<string, int> designationCount = new Dictionary<string, int>();

        //        foreach (var enroll in groupedData)
        //        {
        //            designationCount.Add(enroll.Key, enroll.Count());
        //        }

        //        return designationCount;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        public async Task<List<NewRepeatedVolunteersByYear>> GetDesignationWiseNewRepeatedVolunteersCountByYear(int years, IDictionary<string, string> user)
        {
            try
            {
                List<string> eventIds = null;
                if (user != null && user["role"] == "POC")
                {
                    int userId = Convert.ToInt32(user["userId"]);
                    eventIds = await GetEventIdsByUserId(userId);
                }

                var newRepeatedVolunteersByYear = new List<NewRepeatedVolunteersByYear>();
                int lastFiveYear = DateTime.Now.AddYears(-years).Year;
                var data = await _unitOfWork.Enrollments.GetAllAsync();
                var allEnrollments = data.Where(x => eventIds == null || eventIds.Contains(x.EventID));
                var yearlyFilteredEnrollments = allEnrollments.Where(x => x.EventDate.Year > lastFiveYear).OrderBy(o => o.EventDate.Year);

                var repeatedVolunteers = new List<Enrollment>();
                int year = 0;
                int newVolunteersCount = 0;
                int repeatedVolunteersCount = 0;
                               
                foreach(var enroll in yearlyFilteredEnrollments)
                {
                    if (year == 0)
                        year = enroll.EventDate.Year;

                    if(year != enroll.EventDate.Year)
                    {
                        newRepeatedVolunteersByYear.Add(new NewRepeatedVolunteersByYear { Year = year, NewVolunteers = newVolunteersCount, RepeatedVolunteers = repeatedVolunteersCount });
                        year = enroll.EventDate.Year;
                        newVolunteersCount = 0;
                        repeatedVolunteersCount = 0;
                    }

                   if(!allEnrollments.Any(x=> x.AssociateID == enroll.AssociateID && x.EventDate < enroll.EventDate))
                    {
                        newVolunteersCount++;
                    }
                    else
                    {
                        if (!repeatedVolunteers.Any(x => x.AssociateID == enroll.AssociateID && x.EventDate.Year == enroll.EventDate.Year))
                        {
                            repeatedVolunteersCount++;
                            repeatedVolunteers.Add(enroll);
                        }
                    }                   
                }
                newRepeatedVolunteersByYear.Add(new NewRepeatedVolunteersByYear { Year = year, NewVolunteers = newVolunteersCount, RepeatedVolunteers = repeatedVolunteersCount });


                return newRepeatedVolunteersByYear;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<NewRepeatedVolunteersByYear>> GetBusinessUnitWiseNewRepeatedVolunteersCountByYear(int years, IDictionary<string, string> user)
        {
            try
            {
                List<string> eventIds = null;
                if (user != null && user["role"] == "POC")
                {
                    int userId = Convert.ToInt32(user["userId"]);
                    eventIds = await GetEventIdsByUserId(userId);
                }

                var newRepeatedVolunteersByYear = new List<NewRepeatedVolunteersByYear>();
                int lastFiveYear = DateTime.Now.AddYears(-years).Year;
                var data = await _unitOfWork.Enrollments.GetAllAsync();
                var allEnrollments = data.Where(x => eventIds == null || eventIds.Contains(x.EventID));
                var yearlyFilteredEnrollments = allEnrollments.Where(x => x.EventDate.Year > lastFiveYear).OrderBy(o => o.EventDate.Year);

                var repeatedVolunteers = new List<Enrollment>();
                int year = 0;
                int newVolunteersCount = 0;
                int repeatedVolunteersCount = 0;



                foreach (var enroll in yearlyFilteredEnrollments)
                {
                    if (year == 0)
                        year = enroll.EventDate.Year;

                    if (year != enroll.EventDate.Year)
                    {
                        newRepeatedVolunteersByYear.Add(new NewRepeatedVolunteersByYear { Year = year, NewVolunteers = newVolunteersCount, RepeatedVolunteers = repeatedVolunteersCount });
                        year = enroll.EventDate.Year;
                        newVolunteersCount = 0;
                        repeatedVolunteersCount = 0;
                    }

                    if (!allEnrollments.Any(x => x.AssociateID == enroll.AssociateID && x.EventDate < enroll.EventDate))
                    {
                        newVolunteersCount++;
                    }
                    else
                    {
                        if (!repeatedVolunteers.Any(x => x.AssociateID == enroll.AssociateID && x.EventDate.Year == enroll.EventDate.Year))
                        {
                            repeatedVolunteersCount++;
                            repeatedVolunteers.Add(enroll);
                        }
                    }
                }
                newRepeatedVolunteersByYear.Add(new NewRepeatedVolunteersByYear { Year = year, NewVolunteers = newVolunteersCount, RepeatedVolunteers = repeatedVolunteersCount });


                return newRepeatedVolunteersByYear;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Enrollment>> GetEnrollmentsByFilter(IDictionary<string, string> user, ReportFilter filters)
        {
            try
            {
                var bus = filters.BusinessUnits?.Split(',');
                var bl = filters.BaseLocations?.Split(',');
                string[] focusAreas;

                List<string> projects = null;
                List<string> categories = null;
                //get focus area by splitting project and category
                if (!string.IsNullOrEmpty(filters.FocusAreas))
                {
                    projects = new List<string>();
                    categories = new List<string>();
                    focusAreas = filters.FocusAreas.Split(',');                   
                    string[] splittedValues;
                    foreach (var item in focusAreas)
                    {
                        splittedValues = item.Split('-');
                        projects.Add(splittedValues[0].Trim());
                        categories.Add(splittedValues[1].Trim());
                    }
                }
                var data = await _unitOfWork.Enrollments.GetEnrollmentsWithRelatedTable();
                var res = data  .Where(x =>
                                (bus == null || bus.Contains(x.BusinessUnit)) 
                                && (bl == null || bl.Contains(x.BaseLocation))
                                && (projects == null || projects.Contains(x.Events.Project))
                                && (categories == null || categories.Contains(x.Events.Category))
                                && ((filters.FromDate == null || x.EventDate >= filters.FromDate)
                                && (filters.ToDate == null || x.EventDate <= filters.ToDate))).ToList();
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<string>> GetBusinessUnits()
        {
            IEnumerable<string> result = null;
            try
            {
                result = await _unitOfWork.Enrollments.GetBusinessUnits();
            }
            catch(Exception ex)
            {
                errorhandle(ex);
            }
            return result;
        }
        public async Task<IEnumerable<string>> GetBaseLocations()
        {
            return await _unitOfWork.Enrollments.GetBaseLocations();
        }

        private async Task<List<string>> GetEventIdsByUserId(int userId)
        {
            List<string> eventIds = null;
            try
            {
                if (userId == 0)
                    return null;
                var data = await _unitOfWork.PointOfContacts.GetAllAsync();
                var result = data.Where(x => x.AssociateID == userId).Select(s => s.EventIDs).ToList();
                if (result != null && result.Any())
                {
                    eventIds = new List<string>();
                    foreach (var eventid in result)
                    {
                        eventIds.AddRange(eventid.Split(','));
                    }
                }
            }catch(Exception ex)
            {

            }
            return eventIds;
        }

        public async Task<IEnumerable<Enrollment>> GetEnrollmentsByFilterId(int filterId)
        {
            IEnumerable<Enrollment> enrollments = null;
            try
            {
                ReportFilter filters = _unitOfWork.ReportFilter.Get(filterId);
                if (filters == null)
                    return enrollments;

                var bus = filters.BusinessUnits?.Split(',');
                var bl = filters.BaseLocations?.Split(',');
                string[] focusAreas;

                List<string> projects = null;
                List<string> categories = null;
                //get focus area by splitting project and category
                if (!string.IsNullOrEmpty(filters.FocusAreas))
                {
                    projects = new List<string>();
                    categories = new List<string>();
                    focusAreas = filters.FocusAreas.Split(',');
                    string[] splittedValues;
                    foreach (var item in focusAreas)
                    {
                        splittedValues = item.Split('-');
                        projects.Add(splittedValues[0].Trim());
                        categories.Add(splittedValues[1].Trim());
                    }
                }
                var data = await _unitOfWork.Enrollments.GetEnrollmentsWithRelatedTable();
                
                enrollments = data
                            .Where(x =>
                            (bus == null || bus.Contains(x.BusinessUnit))
                            && (bl == null || bl.Contains(x.BaseLocation))
                            && (projects == null || projects.Contains(x.Events.Project))
                            && (categories == null || categories.Contains(x.Events.Category))
                            && ((filters.FromDate == null || x.EventDate >= filters.FromDate)
                            && (filters.ToDate == null || x.EventDate <= filters.ToDate))).ToList();
                
            }
            catch (Exception ex)
            {
                return null;
            }
            return enrollments;
        }

        private void errorhandle(Exception ex)
        {
            string filePath = @"C:\Error.txt";


            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("-----------------------------------------------------------------------------");
                writer.WriteLine("Date : " + DateTime.Now.ToString());
                writer.WriteLine();

                while (ex != null)
                {
                    writer.WriteLine(ex.GetType().FullName);
                    writer.WriteLine("Message : " + ex.Message);
                    writer.WriteLine("StackTrace : " + ex.StackTrace);

                    ex = ex.InnerException;
                }
            }
        }
    }
}
