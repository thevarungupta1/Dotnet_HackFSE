using Outreach.Reporting.Business.Interfaces;
using Outreach.Reporting.Data.Interfaces;
using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Outreach.Reporting.Business.Processors
{
    public class ParticipationMetricProcessor : IParticipationMetricProcessor
    {
        private readonly IUnitOfWork _unitOfWork;

        public ParticipationMetricProcessor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IEnumerable<Associates> GetAllAssociates()
        {
            try
            {
                return _unitOfWork.Associates.GetAll();
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        public IEnumerable<Associates> GetUniqueVolunteers()
        {
            try
            {
                return _unitOfWork.Associates.GetAll();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<Enrollments> GetAllEnrollments()
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
    }
}
