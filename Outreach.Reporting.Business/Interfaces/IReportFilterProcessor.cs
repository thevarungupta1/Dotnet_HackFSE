using Outreach.Reporting.Entity.Entities;
using System.Collections.Generic;

namespace Outreach.Reporting.Business.Interfaces
{
    public interface IReportFilterProcessor 
    {
        IEnumerable<ReportFilter> GetFiltersById(int userId);
        bool SaveFilter(int userId, ReportFilter filter);
    }
}
