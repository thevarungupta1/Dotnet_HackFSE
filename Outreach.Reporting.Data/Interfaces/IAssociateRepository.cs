using Outreach.Reporting.Data.Entities;
using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Outreach.Reporting.Data.Interfaces
{
    public interface IAssociateRepository : IRepository<Associate>
    {
        Task<IEnumerable<Associate>> GetAssociatesRelatedData();
    }
}
