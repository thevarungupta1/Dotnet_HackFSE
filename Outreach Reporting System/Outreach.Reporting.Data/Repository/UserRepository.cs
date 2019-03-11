﻿using Outreach.Reporting.Data.Entities;
using Outreach.Reporting.Data.Interfaces;
using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Outreach.Reporting.Data.Repository
{
    public class UserRepository : Repository<ApplicationUsers>, IUserRepository
    {
        public UserRepository(ReportContext context) : base(context)
        {
        }

        public ReportContext ReportContext
        {
            get { return Context as ReportContext; }
        }
        public IEnumerable<UserRoles> GetRoles()
        {
            return ReportContext.UserRoles;
        }
    }
}
