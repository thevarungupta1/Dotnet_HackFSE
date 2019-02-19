﻿using Outreach.Reporting.Business.Interfaces;
using Outreach.Reporting.Data.Interfaces;
using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Outreach.Reporting.Business.Processors
{
    public class AssociateProcessor : IAssociateProcessor
    {
        private readonly IUnitOfWork _unitOfWork;

        public AssociateProcessor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool SaveAssociates(IEnumerable<Associates> associates)
        {
            foreach(var row in associates)
            {
                row.CreatedOn = DateTime.Now;
            }
            _unitOfWork.Associates.AddRange(associates);
            _unitOfWork.Complete();
            return true;
        }
    }
}
