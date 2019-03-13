using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Outreach.Reporting.Business.Interfaces
{
    public interface IAuthProcessor
    {
        bool AuthenticateUser(ApplicationUser user);
    }
}
