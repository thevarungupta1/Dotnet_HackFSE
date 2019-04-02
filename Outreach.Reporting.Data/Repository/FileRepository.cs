using Outreach.Reporting.Data.Entities;
using Outreach.Reporting.Data.Interfaces;
using Outreach.Reporting.Entity.Entities;

namespace Outreach.Reporting.Data.Repository
{
    public class FileRepository : Repository<File>, IFileRepository
    {
        public FileRepository(ReportDBContext context) : base(context)
        {
        }

        public ReportDBContext ReportContext
        {
            get { return Context as ReportDBContext; }
        }
    }
}
