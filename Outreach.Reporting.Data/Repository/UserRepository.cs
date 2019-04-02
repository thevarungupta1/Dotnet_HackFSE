using Microsoft.EntityFrameworkCore;
using Outreach.Reporting.Data.Entities;
using Outreach.Reporting.Data.Interfaces;
using Outreach.Reporting.Entity.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Outreach.Reporting.Data.Repository
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        public UserRepository(ReportDBContext context) : base(context)
        {
        }

        public ReportDBContext ReportContext
        {
            get { return Context as ReportDBContext; }
        }
        public IEnumerable<UserRole> GetRoles()
        {
            return ReportContext.UserRoles;
        }

        public string GetUserRoleById(int id)
        {
            return ReportContext.ApplicationUsers.Include(i => i.UserRoles).Where(x => x.AssociateID == id).Select(s => s.UserRoles.Role).FirstOrDefault();
        }
    }
}
