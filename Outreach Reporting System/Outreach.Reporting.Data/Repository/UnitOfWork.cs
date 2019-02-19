using Outreach.Reporting.Data.Entities;
using Outreach.Reporting.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Outreach.Reporting.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ReportContext _context;

        public IAssociateRepository Associates { get; private set; }
        public IEventRepository Events { get; private set; }
        public IEnrollmentRepository Enrollments { get; private set; }

        public UnitOfWork(ReportContext context)
        {
            _context = context;
            Associates = new AssociateRepository(_context);
            Events = new EventRepository(_context);
            Enrollments = new EnrollmentRepository(_context);
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
