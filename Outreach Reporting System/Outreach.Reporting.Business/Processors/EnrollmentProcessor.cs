﻿using Outreach.Reporting.Business.Interfaces;
using Outreach.Reporting.Data.Interfaces;
using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Outreach.Reporting.Business.Processors
{
    public class EnrollmentProcessor : IEnrollmentProcessor
    {
        private readonly IUnitOfWork _unitOfWork;

        public EnrollmentProcessor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Enrollments> GetAll()
        {
            try
            {
                return _unitOfWork.Enrollments.GetAll();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<Enrollments> GetEnrollmentsRelatedData()
        {
            try
            {
                var x = _unitOfWork.Enrollments.GetEnrollmentsRelatedData();
                foreach (var item in x)
                {
                    var test = item.Events;
                    if (test != null)
                    {
                        var test1 = test.BaseLocation;
                    }
                }
                return x;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool SaveEnrollments(IEnumerable<Enrollments> enrollments)
        {
            foreach (var row in enrollments)
            {
                row.CreatedOn = DateTime.Now;
            }
            _unitOfWork.Enrollments.AddRange(enrollments);
            _unitOfWork.Complete();
            return true;
        }

    }
}
