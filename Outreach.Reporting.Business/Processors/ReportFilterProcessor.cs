using Outreach.Reporting.Business.Interfaces;
using Outreach.Reporting.Data.Interfaces;
using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Outreach.Reporting.Business.Processors
{
    public class ReportFilterProcessor : IReportFilterProcessor
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportFilterProcessor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<ReportFilter> GetFiltersById(int userId)
        {
            try
            {
                return _unitOfWork.ReportFilter.Find(x => x.AssociateID == userId).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool SaveFilter(int userId, ReportFilter filter)
        {
            try
            {
                // filter.CreatedOn = DateTime.Now;

                _unitOfWork.ReportFilter.Add(filter);
                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}