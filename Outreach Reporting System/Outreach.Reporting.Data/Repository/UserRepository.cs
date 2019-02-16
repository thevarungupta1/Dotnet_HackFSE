using Outreach.Reporting.Data.Entities;
using Outreach.Reporting.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Outreach.Reporting.Data.Repository
{
    public class UserRepository : Repository<Users>, IUserRepository
    {
        public UserRepository(ReportContext context) : base(context)
        {
        }

        public ReportContext ReportContext
        {
            get { return Context as ReportContext; }
        }
    }
}
