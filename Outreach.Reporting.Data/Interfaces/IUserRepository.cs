using Outreach.Reporting.Entity.Entities;
using System.Collections.Generic;

namespace Outreach.Reporting.Data.Interfaces
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        IEnumerable<UserRole> GetRoles();
        string GetUserRoleById(int id);
    }
}
