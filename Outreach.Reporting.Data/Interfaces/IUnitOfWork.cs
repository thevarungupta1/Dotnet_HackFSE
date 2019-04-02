using System;

namespace Outreach.Reporting.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAssociateRepository Associates { get; }
        IEventRepository Events { get; }
        IEnrollmentRepository Enrollments { get; }
        IUserRepository ApplicationUsers { get; }
        IPocRepository PointOfContacts { get; }
        IFileRepository File { get; }
        IReportFilterRepository ReportFilter { get; }
        int Complete();
    }
}
