﻿using Outreach.Reporting.Data.Entities;
using Outreach.Reporting.Data.Interfaces;
using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Outreach.Reporting.Data.Repository
{
    public class AuthRepository : Repository<ApplicationUser>, IAuthRepository
    {
        public AuthRepository(ReportDBContext context) : base(context)
        {
        }

        public ReportDBContext ReportContext
        {
            get { return Context as ReportDBContext; }
        }

        //public bool Get(ApplicationUser user)
        //{
        //    user = _unitOfWork.Auth.Find(x => x.AssociateID == user.AssociateID && x.Email == user.Email).FirstOrDefault();
        //    return user.ero;
        //}

    }
}
