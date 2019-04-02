using Outreach.Reporting.Business.Interfaces;
using Outreach.Reporting.Data.Interfaces;
using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.IO;

namespace Outreach.Reporting.Business.Processors
{
    public class UserProcessor : IUserProcessor
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserProcessor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<ApplicationUser> GetAll()
        {
            try
            {
                return _unitOfWork.ApplicationUsers.GetAll();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool SaveUser(IEnumerable<ApplicationUser> users)
        {
            try
            {
                foreach (var row in users)
                {
                    row.CreatedOn = DateTime.Now;
                }
                _unitOfWork.ApplicationUsers.AddRange(users);
                _unitOfWork.Complete();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool SavePOC(IEnumerable<PointOfContact> poc)
        {
            try
            {
                foreach (var row in poc)
                {
                    row.CreatedOn = DateTime.Now;
                }
                _unitOfWork.PointOfContacts.AddRange(poc);
                _unitOfWork.Complete();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public IEnumerable<UserRole> GetRoles()
        {
            try
            {
                return _unitOfWork.ApplicationUsers.GetRoles();
            }
            catch (Exception ex)
            {
                errorhandle(ex);
                return null;
            }
        }

        private void errorhandle(Exception ex)
        {
            string filePath = @"C:\Error.txt";


            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("-----------------------------------------------------------------------------");
                writer.WriteLine("Date : " + DateTime.Now.ToString());
                writer.WriteLine();

                while (ex != null)
                {
                    writer.WriteLine(ex.GetType().FullName);
                    writer.WriteLine("Message : " + ex.Message);
                    writer.WriteLine("StackTrace : " + ex.StackTrace);

                    ex = ex.InnerException;
                }
            }
        }

    }
}
