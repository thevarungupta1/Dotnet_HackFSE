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
        public async Task<IActionResult> GetAsync()
        {
            var user = GetCurrentUser();
            var result = await _enrollmentProcessor.GetAll(user);
          return Ok(result);
        }

        // GET api/GetWithRelatedData
        [HttpGet]
        [Route("GetEnrolledAssociates")]
        public async Task<IActionResult> GetEnrolledAssociates()
        {
            var user = GetCurrentUser();
            var result = await _enrollmentProcessor.GetEnrolledAssociates(user);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetEnrolledUniqueAssociates")]
        public async Task<IActionResult> GetEnrolledUniqueAssociates()
        {
            var user = GetCurrentUser();
            var result = await _enrollmentProcessor.GetEnrolledUniqueAssociates(user);
            return Ok(result);
        }

        [HttpGet]
        [Route("UniqueVolunteersByDate")]
        public async Task<IActionResult> GetUniqueVolunteersByDate(string fromDate, string toDate)
        {
            var user = GetCurrentUser();
            return Ok(await _enrollmentProcessor.GetUniqueVolunteersByDate(fromDate, toDate, user));
        }
        // GET api/GetEnrolledAssociates
        [HttpGet]
        [Route("GetTopFrequentVolunteers")]
        public async Task<IActionResult> GetTopFrequentVolunteers(int count)
        {
            if(count <= 0)
                return BadRequest();

            var user = GetCurrentUser();
            return Ok(await _enrollmentProcessor.GetTopFrequentVolunteers(count, user));
        }

        [HttpGet]
        [Route("GetTopVolunteerData")]
        public async Task<IActionResult> GetTopVolunteerData()
        {
            var user = GetCurrentUser();
            return Ok(await _enrollmentProcessor.GetTopVolunteerData(user));
        }

        [HttpGet]
        [Route("GetYearlyVolunteersCount")]
        public async Task<IActionResult> GetYearlyVolunteersCount(int years)
        {
            if(years <= 0)
                return BadRequest();
            var user = GetCurrentUser();
            return Ok(await _enrollmentProcessor.GetYearlyVolunteersCount(years, user));
        }
        [HttpGet]
        [Route("GetYearlyBuWiseVolunteersCount")]
        public async Task<IActionResult> GetYearlyBuWiseVolunteersCount(int years)
        {
            if(years <= 0)
                return BadRequest();
            var user = GetCurrentUser();
            return Ok( await _enrollmentProcessor.GetYearlyBuWiseVolunteersCount(years, user));
        }

        [HttpGet]
        [Route("GetDesignationWiseVolunteersCount")]
        public async Task<IActionResult> GetDesignationWiseVolunteersCount()
        {
            var user = GetCurrentUser();
            return Ok(await _enrollmentProcessor.GetDesignationWiseVolunteersCount(user));
        }

        [HttpGet]
        [Route("GetDesignationWiseVolunteersByYear")]
        public async Task<IActionResult> GetDesignationWiseVolunteersByYear(int years)
        {
            if(years <= 0)
                return BadRequest();

            var user = GetCurrentUser();
            return Ok(await _enrollmentProcessor.GetDesignationWiseNewRepeatedVolunteersCountByYear(years, user));
        }

        [HttpGet]
        [Route("GetAllNewVolunteers")]
        public async Task<IActionResult> GetAllNewVolunteers()
        {
            var user = GetCurrentUser();
            return Ok(await _enrollmentProcessor.GetAllNewVolunteers(user));
        }

        [HttpGet]
        [Route("GetDateWiseVolunteersCount")]
        public async Task<IActionResult> GetDateWiseVolunteersCount()
        {
            var user = GetCurrentUser();
            return Ok(await _enrollmentProcessor.GetDateWiseVolunteersCount(user));
        }
        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] IEnumerable<Enrollment> enrollments)
        {
            if (enrollments == null)
                return BadRequest();
           return Ok(await _enrollmentProcessor.SaveEnrollments(enrollments));
        }

        [HttpPost]
        [Route("EnrollmentsByFilter")]
        public async Task<IActionResult> GetEnrollmentsByFilter([FromBody] ReportFilter filters)
        {
            if (filters == null)
                return BadRequest();
            var user = GetCurrentUser();
            return Ok(await _enrollmentProcessor.GetEnrollmentsByFilter(user, filters));
        }

        [HttpGet]
        [Route("EnrollmentsByFilterId")]
        public async Task<IActionResult> GetEnrollmentsByFilterId(int filterId)
        {
            if (filterId <= 0)
                return BadRequest();
            return Ok(await _enrollmentProcessor.GetEnrollmentsByFilterId(filterId));
        }

        [HttpGet]
        [Route("GetBusinessUnits")]
        public async Task<IActionResult> GetBusinessUnits()
        {
            return Ok(await _enrollmentProcessor.GetBusinessUnits());
        }

        [HttpGet]
        [Route("GetBaseLocations")]
        public async Task<IActionResult> GetBaseLocations()
        { 
            return Ok(await _enrollmentProcessor.GetBaseLocations());
        }

        private IDictionary<string, string> GetCurrentUser()
        {
            IDictionary<string, string> dict = null;            
            try
            {
                var identity = HttpContext?.User.Identity as ClaimsIdentity;
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
