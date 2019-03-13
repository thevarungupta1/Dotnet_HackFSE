using Outreach.Reporting.Data.Entities;
using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Outreach.Reporting.Data.Interfaces
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        IEnumerable<UserRole> GetRoles();
    }
}
