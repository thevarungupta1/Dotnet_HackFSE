using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Outreach.Reporting.Business.Processors;
using Outreach.Reporting.Business.Interfaces;
using Microsoft.Extensions.Logging;
using Outreach.Reporting.Entity.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Outreach.Reporting.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, PMO, POC")]
    public class AssociateController : ControllerBase
    {
        private readonly IAssociateProcessor _associateProcessor;
        private readonly ILogger<AssociateController> _logger;

        public AssociateController(IAssociateProcessor associateProcessor, ILogger<AssociateController> logger)
        {
            _associateProcessor = associateProcessor;
            _logger = logger;
        }
        // GET api/AllAssociates
        [HttpGet]
        public ActionResult<IEnumerable<Associates>> Get()
        {
            return Ok(_associateProcessor.GetAll());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] IEnumerable<Associates> associates)
        {
            _associateProcessor.SaveAssociates(associates);
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