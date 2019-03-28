using Outreach.Reporting.Business.Interfaces;
using Outreach.Reporting.Data.Interfaces;
using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Outreach.Reporting.Business.Processors
{
    public class AssociateProcessor : IAssociateProcessor
    {
        private readonly IUnitOfWork _unitOfWork;

        public AssociateProcessor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Associate>> GetAll()
        {
            try
            {
                return await _unitOfWork.Associates.GetAllAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<bool> SaveAssociates(IEnumerable<Associate> associates)
        {
            try
            {
                foreach (var row in associates)
                {
                    row.CreatedOn = DateTime.Now;
                }
                await _unitOfWork.Associates.AddRangeAsync(associates);
                 _unitOfWork.Complete();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
