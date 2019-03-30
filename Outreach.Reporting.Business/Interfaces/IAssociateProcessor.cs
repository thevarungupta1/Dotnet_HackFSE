using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Outreach.Reporting.Business.Interfaces
{
    public interface IAssociateProcessor
    {
        Task<bool> SaveAssociates(List<Associate> associates);
        Task<IEnumerable<Associate>> GetAll();
    }
}
