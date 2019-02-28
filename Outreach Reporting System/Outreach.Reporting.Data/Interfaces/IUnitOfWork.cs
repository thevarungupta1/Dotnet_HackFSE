using System;
using System.Collections.Generic;
using System.Text;

namespace Outreach.Reporting.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAssociateRepository Associates { get; }
        IEventRepository Events { get; }
        IEnrollmentRepository Enrollments { get; }
        IUserRepository ApplicationUsers { get; }
        IAuthRepository Auth { get; }
        int Complete();
    }
}
