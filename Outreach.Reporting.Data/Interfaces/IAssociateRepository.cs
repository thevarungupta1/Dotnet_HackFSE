using Outreach.Reporting.Entity.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Outreach.Reporting.Data.Interfaces
{
    public interface IAssociateRepository : IRepository<Associate>
    {
        Task<IEnumerable<Associate>> GetAssociatesRelatedData();
    }
}
