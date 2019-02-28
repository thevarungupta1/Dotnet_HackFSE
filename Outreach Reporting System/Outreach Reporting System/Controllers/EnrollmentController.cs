using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<EnrollmentController> _logger;

        public EnrollmentController(IEnrollmentProcessor enrollmentProcessor, ILogger<EnrollmentController> logger)
        {
            _enrollmentProcessor = enrollmentProcessor;
            _logger = logger;
        }
        // GET api/AllEnrollments
        [HttpGet]
        public ActionResult<IEnumerable<Enrollments>> Get()
        {
            return Ok(_enrollmentProcessor.GetAll());
        }

        // GET api/GetWithRelatedData
        [HttpGet]
        [Route("GetWithRelatedData")]
        public ActionResult<IEnumerable<Enrollments>> GetWithRelatedData()
        {
            return Ok(_enrollmentProcessor.GetEnrollmentsRelatedData());
        }
        // GET api/GetEnrolledAssociates
        [HttpGet]
        [Route("GetEnrolledAssociates")]
        public ActionResult<IEnumerable<Enrollments>> GetEnrolledAssociates()
        {
            return Ok(_enrollmentProcessor.GetEnrolledAssociates());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] IEnumerable<Enrollments> enrollments)
        {
            _enrollmentProcessor.SaveEnrollments(enrollments);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}