using Outreach.Reporting.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Outreach.Reporting.Data.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ReportContext context)
        {
            context.Database.EnsureCreated();           
        }
    }
}
