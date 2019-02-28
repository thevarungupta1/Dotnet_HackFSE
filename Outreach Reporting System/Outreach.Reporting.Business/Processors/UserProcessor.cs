﻿using Microsoft.IdentityModel.Tokens;
using Outreach.Reporting.Business.Interfaces;
using Outreach.Reporting.Data.Entities;
using Outreach.Reporting.Data.Interfaces;
using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Outreach.Reporting.Business.Processors
{
    public class UserProcessor : IUserProcessor
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserProcessor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<ApplicationUsers> GetAll()
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
        public bool SaveUser(IEnumerable<ApplicationUsers> users)
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
    }
}
