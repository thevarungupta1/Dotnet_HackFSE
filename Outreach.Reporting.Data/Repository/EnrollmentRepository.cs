﻿using Outreach.Reporting.Data.Entities;
using Outreach.Reporting.Data.Interfaces;
using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<Enrollment>> GetEnrolledAssociates()
        {
            var enrollments = await ReportContext.Enrollments
                                            .Include(e => e.Events)
                                            .Include(a => a.Associates).ToListAsync();
            return enrollments;
        }

        public  async Task<IEnumerable<Associate>> GetTopFrequentVolunteers(int count)
        {
            var enrollments = ReportContext.Enrollments
                                    .Include(a => a.Associates);

            var groupedData = enrollments.GroupBy(enroll => enroll.AssociateID)
                                         .Select(group => new
                                         {
                                            enrollments = group
                                         })
                                         .OrderByDescending(x => x.enrollments.Count()).Take(count);
            return await groupedData.SelectMany(group => group.enrollments.Select(s=> s.Associates)).ToListAsync();

        }

        public async Task<IEnumerable<Enrollment>> GetYearlyVolunteersCount(int yearsCount=100)
        {
            var enrollments = await ReportContext.Enrollments
                                    .Include(a => a.Associates).ToListAsync();

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

        public async Task<IEnumerable<Enrollment>> GetEnrollments()
        {
            var enrollments = await ReportContext.Enrollments
                                    .Include(a => a.Associates).ToListAsync();

            return enrollments;//.SelectMany(group => group.enrollments.Select(s=> s));
        }

        public async Task<IEnumerable<Enrollment>> GetEnrollmentsByYears(int yearFrom)
        {
            var enrollments = await ReportContext.Enrollments.Where(x => x.EventDate.Year > yearFrom).ToListAsync();

            return enrollments;
        }

        public async Task<IEnumerable<string>> GetBusinessUnits()
        {
            return await ReportContext.Enrollments.Select(s => s.BusinessUnit).Distinct().ToListAsync();
        }
        public async Task<IEnumerable<string>> GetBaseLocations()
        {
            return await ReportContext.Enrollments.Select(s => s.BaseLocation).Distinct().ToListAsync();
        }

        public async Task<IEnumerable<Enrollment>> GetEnrollmentsWithRelatedTable()
        {
            var enrollments = await ReportContext.Enrollments
                                    .Include(a => a.Associates)
                                    .Include(e => e.Events).ToListAsync();

            return enrollments;//.SelectMany(group => group.enrollments.Select(s=> s));
        }

    }
}
