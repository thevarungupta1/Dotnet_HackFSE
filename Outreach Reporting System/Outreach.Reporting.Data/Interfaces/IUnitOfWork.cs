using System;
using System.Collections.Generic;
using System.Text;

namespace Outreach.Reporting.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAssociateRepository Associates { get; }
        IEventRepository Events { get; }
        IUserRepository Users { get; }
        int Complete();
    }
}
