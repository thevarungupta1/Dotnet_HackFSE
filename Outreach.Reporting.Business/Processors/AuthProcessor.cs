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
        public bool AuthenticateUser(int associateID)
        {
            var user = _unitOfWork.ApplicationUsers.Find(x => x.AssociateID == associateID).FirstOrDefault();
            return user != null;
        }

        public string GetUserRoleById(int id)
        {
            try
            {
                return _unitOfWork.ApplicationUsers.GetUserRoleById(id);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public bool CheckPocById(int userId)
        {
            var poc = _unitOfWork.PointOfContacts.Find(x => x.AssociateID == userId).FirstOrDefault();
            return poc != null;
        }
    }
}
