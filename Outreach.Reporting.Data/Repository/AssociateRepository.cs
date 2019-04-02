using Microsoft.EntityFrameworkCore;
using Outreach.Reporting.Data.Entities;
using Outreach.Reporting.Data.Interfaces;
using Outreach.Reporting.Entity.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Outreach.Reporting.Data.Repository
{
    public class AssociateRepository : Repository<Associate>, IAssociateRepository
    {
        public AssociateRepository(ReportDBContext context) : base(context)
        {
        }

        public ReportDBContext ReportContext
        {
            get { return Context as ReportDBContext; }
        }

        public async Task<IEnumerable<Associate>> GetAssociatesRelatedData()
        {
            return await ReportContext.Associates.ToListAsync();
        }
    }
}
