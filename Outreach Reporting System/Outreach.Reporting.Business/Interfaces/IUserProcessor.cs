using Outreach.Reporting.Data.Entities;
using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Outreach.Reporting.Business.Interfaces
{
    public interface IUserProcessor
    {
        IEnumerable<ApplicationUsers> GetAll();
        bool SaveUser(IEnumerable<ApplicationUsers> applicationUsers);
        IEnumerable<UserRoles> GetRoles();
    }
}
