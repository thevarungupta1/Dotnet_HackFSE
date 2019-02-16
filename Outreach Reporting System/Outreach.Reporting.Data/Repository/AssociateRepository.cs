using Outreach.Reporting.Data.Entities;
using Outreach.Reporting.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Outreach.Reporting.Data.Repository
{
    public class AssociateRepository : Repository<Associates>, IAssociateRepository
    {
        public AssociateRepository(ReportContext context) : base(context)
        {
        }

        public ReportContext ReportContext
        {
            get { return Context as ReportContext; }
        }
    }
}
