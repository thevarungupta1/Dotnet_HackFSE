using Outreach.Reporting.Entity.Entities;
using System.Collections.Generic;

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
