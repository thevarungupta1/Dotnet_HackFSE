﻿using Outreach.Reporting.Data.Entities;
using Outreach.Reporting.Data.Interfaces;
using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Outreach.Reporting.Data.Repository
{
    public class EnrollmentRepository : Repository<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(ReportDBContext context) : base(context)
        {
        }

        public ReportDBContext ReportContext
        {
            get { return Context as ReportDBContext; }
        }

        public IEnumerable<Enrollment> GetEnrolledAssociates()
        {
            return ReportContext.Enrollments
                                            //.Include(e => e.Events)
                                            .Include(a => a.Associates).ToList();
        }

        public IEnumerable<Associate> GetTopFrequentVolunteers(int count)
        {
            var enrollments = ReportContext.Enrollments
                                    .Include(a => a.Associates);

            var groupedData = enrollments.GroupBy(enroll => enroll.AssociateID)
                                         .Select(group => new
                                         {
                                            enrollments = group
                                         })
                                         .OrderByDescending(x => x.enrollments.Count()).Take(count);

            return groupedData.SelectMany(group => group.enrollments.Select(s=> s.Associates)).ToList();

        }

        public IEnumerable<Enrollment> GetYearlyVolunteersCount(int yearsCount=100)
        {
            var enrollments = ReportContext.Enrollments
                                    .Include(a => a.Associates);

            var groupedData = enrollments.GroupBy(enroll => enroll.EventDate)
            //.Select(group => group);
            .Select(group => new
            {
                eventyear = group.Select(s => s.EventDate).FirstOrDefault(),
                enrollments = group
            })
            .OrderByDescending(x => x.eventyear).Take(yearsCount);

            return groupedData.SelectMany(group => group.enrollments.Select(s=> s));
        }

        public IQueryable<Enrollment> GetEnrollments()
        {
            var enrollments = ReportContext.Enrollments
                                    .Include(a => a.Associates);

            return enrollments;//.SelectMany(group => group.enrollments.Select(s=> s));
        }

    }
}