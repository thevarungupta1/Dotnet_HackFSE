using Outreach.Reporting.Business.Interfaces;
using Outreach.Reporting.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Outreach.Reporting.Business.Processors
{
    public class AuthProcessor : IAuthProcessor
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthProcessor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool AuthenticateUser(string email)
        {
            var user = _unitOfWork.Auth.Find(x => x.Email == email).FirstOrDefault();
            return user != null;
        }
    }
}
