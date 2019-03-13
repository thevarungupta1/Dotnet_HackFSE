using Outreach.Reporting.Business.Interfaces;
using Outreach.Reporting.Data.Interfaces;
using Outreach.Reporting.Entity.Entities;
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
        public bool AuthenticateUser(ApplicationUser user)
        {
            user = _unitOfWork.Auth.Find(x => x.AssociateID == user.AssociateID && x.Email == user.Email).FirstOrDefault();
            return user != null;
        }
    }
}
