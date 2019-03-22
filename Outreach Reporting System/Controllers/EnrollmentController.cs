using System;
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
            var user = GetCurrentUser();
          return Ok(_enrollmentProcessor.GetAll(user));
        }

        // GET api/GetWithRelatedData
        [HttpGet]
        [Route("GetEnrolledAssociates")]
        public async Task<ActionResult<IEnumerable<Enrollment>>> GetEnrolledAssociates()
        {
            var user = GetCurrentUser();
            return await Task.FromResult(Ok(_enrollmentProcessor.GetEnrolledAssociates(user)));
        }

        [HttpGet]
        [Route("GetEnrolledUniqueAssociates")]
        public async Task<ActionResult<IEnumerable<Associate>>> GetEnrolledUniqueAssociates()
        {
            var user = GetCurrentUser();
            return await Task.FromResult(Ok(_enrollmentProcessor.GetEnrolledUniqueAssociates(user)));
        }
        // GET api/GetEnrolledAssociates
        [HttpGet]
        [Route("GetTopFrequentVolunteers")]
        public async Task<IActionResult> GetTopFrequentVolunteers(int count)
        {
            if(count <= 0)
                return BadRequest();

            var user = GetCurrentUser();
            return await Task.FromResult(Ok(_enrollmentProcessor.GetTopFrequentVolunteers(count, user)));
        }

        [HttpGet]
        [Route("GetTopVolunteerData")]
        public async Task<IActionResult> GetTopVolunteerData()
        {
            var user = GetCurrentUser();
            return await Task.FromResult(Ok(_enrollmentProcessor.GetTopVolunteerData(user)));
        }

        [HttpGet]
        [Route("GetYearlyVolunteersCount")]
        public async Task<IActionResult> GetYearlyVolunteersCount(int years)
        {
            if(years <= 0)
                return BadRequest();
            var user = GetCurrentUser();
            return await Task.FromResult(Ok(_enrollmentProcessor.GetYearlyVolunteersCount(years, user)));
        }
        [HttpGet]
        [Route("GetYearlyBuWiseVolunteersCount")]
        public async Task<IActionResult> GetYearlyBuWiseVolunteersCount(int years)
        {
            if(years <= 0)
                return BadRequest();
            var user = GetCurrentUser();
            return await Task.FromResult(Ok(_enrollmentProcessor.GetYearlyBuWiseVolunteersCount(years, user)));
        }

        [HttpGet]
        [Route("GetDesignationWiseVolunteersCount")]
        public async Task<IActionResult> GetDesignationWiseVolunteersCount()
        {
            var user = GetCurrentUser();
            return await Task.FromResult(Ok(_enrollmentProcessor.GetDesignationWiseVolunteersCount(user)));
        }

        [HttpGet]
        [Route("GetDesignationWiseVolunteersByYear")]
        public async Task<IActionResult> GetDesignationWiseVolunteersByYear(int years)
        {
            if(years <= 0)
                return BadRequest();

            var user = GetCurrentUser();
            return await Task.FromResult(Ok(_enrollmentProcessor.GetDesignationWiseNewRepeatedVolunteersCountByYear(years, user)));
        }

        [HttpGet]
        [Route("GetAllNewVolunteers")]
        public async Task<IActionResult> GetAllNewVolunteers()
        {
            var user = GetCurrentUser();
            return await Task.FromResult(Ok(_enrollmentProcessor.GetAllNewVolunteers(user)));
        }

        [HttpGet]
        [Route("GetDateWiseVolunteersCount")]
        public async Task<IActionResult> GetDateWiseVolunteersCount()
        {
            var user = GetCurrentUser();
            return await Task.FromResult(Ok(_enrollmentProcessor.GetDateWiseVolunteersCount(user)));
        }
        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] IEnumerable<Enrollment> enrollments)
        {
            if (enrollments == null)
                return BadRequest();
           return await Task.FromResult(Ok( _enrollmentProcessor.SaveEnrollments(enrollments)));
        }

        [HttpPost]
        [Route("EnrollmentsByFilter")]
        public async Task<IActionResult> GetEnrollmentsByFilter([FromBody] ReportFilter filters)
        {
            if (filters == null)
                return BadRequest();
            var user = GetCurrentUser();
            return await Task.FromResult(Ok(_enrollmentProcessor.GetEnrollmentsByFilter(user, filters)));
        }

        [HttpGet]
        [Route("EnrollmentsByFilterId")]
        public async Task<IActionResult> GetEnrollmentsByFilterId(int filterId)
        {
            if (filterId <= 0)
                return BadRequest();
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

        private IDictionary<string, string> GetCurrentUser()
        {
            IDictionary<string, string> dict = null;            
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    dict = new Dictionary<string, string>();
                    dict.Add("UserId", identity.FindFirst("UserId").Value);
                    dict.Add("role", identity.FindFirst(ClaimTypes.Role).Value);
                }                
            }
            catch (Exception ex)
            {

            }
            return dict;
        }
    }
}
