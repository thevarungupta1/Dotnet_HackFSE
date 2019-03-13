using Outreach.Reporting.Business.Interfaces;
using Outreach.Reporting.Data.Interfaces;
using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Outreach.Reporting.Business.Processors
{
    public class AssociateProcessor : IAssociateProcessor
    {
        private readonly IUnitOfWork _unitOfWork;

        public AssociateProcessor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Associate> GetAll()
        {
            try
            {
                return _unitOfWork.Associates.GetAll();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool SaveAssociates(IEnumerable<Associate> associates)
        {
            try
            {
                foreach (var row in associates)
                {
                    row.CreatedOn = DateTime.Now;
                }
                _unitOfWork.Associates.AddRange(associates);
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
