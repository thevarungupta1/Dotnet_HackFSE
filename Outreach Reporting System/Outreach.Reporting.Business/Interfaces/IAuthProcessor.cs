using System;
using System.Collections.Generic;
using System.Text;

namespace Outreach.Reporting.Business.Interfaces
{
    public interface IAuthProcessor
    {
        bool AuthenticateUser(string email);
    }
}
