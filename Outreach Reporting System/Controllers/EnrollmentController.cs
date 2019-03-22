﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OfficeOpenXml;
using Outreach.Reporting.Business.Interfaces;
using Outreach.Reporting.Entity.Entities;

namespace Outreach.Reporting.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, PMO, POC")]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentProcessor _enrollmentProcessor;
        public EnrollmentController(IEnrollmentProcessor enrollmentProcessor)
        {
            _enrollmentProcessor = enrollmentProcessor;
        }     
       
        // GET api/AllEnrollments
        [HttpGet]
        public IActionResult GetAsync()
        {
            int userId = 0;// GetCurrentUserId();
          return Ok(_enrollmentProcessor.GetAll(userId));
        }

        // GET api/GetWithRelatedData
        [HttpGet]
        [Route("GetEnrolledAssociates")]
        public async Task<ActionResult<IEnumerable<Enrollment>>> GetEnrolledAssociates()
        {
            int userId = GetCurrentUserId();
            return await Task.FromResult(Ok(_enrollmentProcessor.GetEnrolledAssociates(userId)));
        }

        [HttpGet]
        [Route("GetEnrolledUniqueAssociates")]
        public async Task<ActionResult<IEnumerable<Associate>>> GetEnrolledUniqueAssociates()
        {
            int userId = GetCurrentUserId();
            return await Task.FromResult(Ok(_enrollmentProcessor.GetEnrolledUniqueAssociates(userId)));
        }
        // GET api/GetEnrolledAssociates
        [HttpGet]
        [Route("GetTopFrequentVolunteers")]
        public async Task<IActionResult> GetTopFrequentVolunteers(int count)
        {
            int userId = GetCurrentUserId();
            return await Task.FromResult(Ok(_enrollmentProcessor.GetTopFrequentVolunteers(count, userId)));
        }

        [HttpGet]
        [Route("GetTopVolunteerData")]
        public async Task<IActionResult> GetTopVolunteerData()
        {
            int userId = GetCurrentUserId();
            return await Task.FromResult(Ok(_enrollmentProcessor.GetTopVolunteerData(userId)));
        }

        [HttpGet]
        [Route("GetYearlyVolunteersCount")]
        public async Task<IActionResult> GetYearlyVolunteersCount(int years)
        {
            int userId = GetCurrentUserId();
            return await Task.FromResult(Ok(_enrollmentProcessor.GetYearlyVolunteersCount(years, userId)));
        }
        [HttpGet]
        [Route("GetYearlyBuWiseVolunteersCount")]
        public async Task<IActionResult> GetYearlyBuWiseVolunteersCount(int years)
        {
            int userId = GetCurrentUserId();
            return await Task.FromResult(Ok(_enrollmentProcessor.GetYearlyBuWiseVolunteersCount(years, userId)));
        }

        [HttpGet]
        [Route("GetDesignationWiseVolunteersCount")]
        public async Task<IActionResult> GetDesignationWiseVolunteersCount()
        {
            int userId = GetCurrentUserId();
            return await Task.FromResult(Ok(_enrollmentProcessor.GetDesignationWiseVolunteersCount(userId)));
        }

        [HttpGet]
        [Route("GetDesignationWiseVolunteersByYear")]
        public async Task<IActionResult> GetDesignationWiseVolunteersByYear(int years)
        {
            int userId = GetCurrentUserId();
            return await Task.FromResult(Ok(_enrollmentProcessor.GetDesignationWiseNewRepeatedVolunteersCountByYear(years, userId)));
        }

        [HttpGet]
        [Route("GetAllNewVolunteers")]
        public async Task<IActionResult> GetAllNewVolunteers()
        {
            int userId = GetCurrentUserId();
            return await Task.FromResult(Ok(_enrollmentProcessor.GetAllNewVolunteers(userId)));
        }

        [HttpGet]
        [Route("GetDateWiseVolunteersCount")]
        public async Task<IActionResult> GetDateWiseVolunteersCount()
        {
            int userId = GetCurrentUserId();
            return await Task.FromResult(Ok(_enrollmentProcessor.GetDateWiseVolunteersCount(userId)));
        }
        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] IEnumerable<Enrollment> enrollments)
        {
           return await Task.FromResult(Ok( _enrollmentProcessor.SaveEnrollments(enrollments)));
        }

        [HttpPost]
        [Route("EnrollmentsByFilter")]
        public async Task<IActionResult> GetEnrollmentsByFilter([FromBody] ReportFilter filters)
        {
            int userId = GetCurrentUserId();
            return await Task.FromResult(Ok(_enrollmentProcessor.GetEnrollmentsByFilter(userId, filters)));
        }

        [HttpGet]
        [Route("EnrollmentsByFilterId")]
        public async Task<IActionResult> GetEnrollmentsByFilterId(int filterId)
        {
            return await Task.FromResult(Ok(_enrollmentProcessor.GetEnrollmentsByFilterId(filterId)));
        }

        [HttpGet]
        [Route("GetBusinessUnits")]
        public IActionResult GetBusinessUnits()
        {
            return Ok(_enrollmentProcessor.GetBusinessUnits());
        }

        [HttpGet]
        [Route("GetBaseLocations")]
        public IActionResult GetBaseLocations()
        {
            return Ok(_enrollmentProcessor.GetBaseLocations());
        }    
       
        private int GetCurrentUserId()
        {
            int userId = 0;
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    if (identity.FindFirst(ClaimTypes.Role).Value == "POC")
                        int.TryParse(identity.FindFirst("UserId").Value, out userId);
                }
            }
            catch (Exception ex)
            {

            }
            return userId;
        }
    }
}
