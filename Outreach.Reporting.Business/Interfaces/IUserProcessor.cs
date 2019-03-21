using Outreach.Reporting.Data.Entities;
using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Outreach.Reporting.Business.Interfaces
{
    public interface IUserProcessor
    {
        IEnumerable<ApplicationUser> GetAll();
        bool SaveUser(IEnumerable<ApplicationUser> applicationUsers);
        IEnumerable<UserRole> GetRoles();
        bool SavePOC(IEnumerable<PointOfContact> poc);
    }
}
