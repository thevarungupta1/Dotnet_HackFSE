using Outreach.Reporting.Data.Entities;

namespace Outreach.Reporting.Data.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ReportDBContext context)
        {
            context.Database.EnsureCreated();           
        }
    }
}
