using Outreach.Reporting.Data.Entities;
using Outreach.Reporting.Data.Interfaces;

namespace Outreach.Reporting.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ReportDBContext _context;

        public IAssociateRepository Associates { get; private set; }
        public IEventRepository Events { get; private set; }
        public IEnrollmentRepository Enrollments { get; private set; }
        public IUserRepository ApplicationUsers { get; private set; }
        public IPocRepository PointOfContacts { get; private set; }
        public IFileRepository File { get; private set; }
        public IReportFilterRepository ReportFilter { get; private set; }

        public UnitOfWork(ReportDBContext context)
        {
            _context = context;
            Associates = new AssociateRepository(_context);
            Events = new EventRepository(_context);
            Enrollments = new EnrollmentRepository(_context);
            ApplicationUsers = new UserRepository(_context);
            PointOfContacts = new PocRepository(_context);
            File = new FileRepository(_context);
            ReportFilter = new ReportFilterRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
