using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Outreach.Reporting.Business.Interfaces;

namespace Outreach.Reporting.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, PMO, POC")]
    public class ParticipationMetricController : ControllerBase
    {
        private readonly IParticipationMetricProcessor _participationMetricProcessor;
        private readonly ILogger<ParticipationMetricController> _logger;

        public ParticipationMetricController(IParticipationMetricProcessor participationMetricProcessor, ILogger<ParticipationMetricController> logger)
        {
            _participationMetricProcessor = participationMetricProcessor;
            _logger = logger;
        }
        // GET api/GetAllAssociates
        [HttpGet]
        [Route("GetAllAssociates")]
        public ActionResult GetAllAssociates()
        {
            return Ok(_participationMetricProcessor.GetAllAssociates());
        } 
        
        // GET api/GetAllAssociates
        [HttpGet]
        [Route("GetUniqueVolunteers")]
        public ActionResult GetUniqueVolunteers()
        {
            return Ok(_participationMetricProcessor.GetUniqueVolunteers());
        } 
        
        // GET api/GetAllAssociates
        [HttpGet]
        [Route("GetAllEnrollments")]
        public ActionResult GetAllEnrollments()
        {
            return Ok(_participationMetricProcessor.GetAllEnrollments());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody] IEnumerable<Associates> associates)
        //{
        //    _associateProcessor.SaveAssociates(associates);
        //}

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