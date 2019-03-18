using Outreach.Reporting.Data.Interfaces;
using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Outreach.Reporting.Business.Interfaces
{
    public interface IReportFilterProcessor 
    {
        IEnumerable<ReportFilter> GetFiltersById(int userId);
        bool SaveFilter(int userId, ReportFilter filter);
    }
}
