using Outreach.Reporting.Data.Entities;
using Outreach.Reporting.Data.Interfaces;
using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Outreach.Reporting.Data.Repository
{
    public class EnrollmentRepository : Repository<Enrollments>, IEnrollmentRepository
    {
        public EnrollmentRepository(ReportContext context) : base(context)
        {
        }

        public ReportContext ReportContext
        {
            get { return Context as ReportContext; }
        }

        public IEnumerable<Enrollments> GetEnrollmentsRelatedData()
        {
            return ReportContext.Enrollments.Include(e=> e.Events).Include(a=> a.Associates).ToList();
        }

    }
}
