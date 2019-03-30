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
using System.Net.Mail;
using System.Net;

namespace Outreach.Reporting.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, PMO, POC")]
    public class AssociateController : ControllerBase
    {
        private readonly IAssociateProcessor _associateProcessor;

        public AssociateController(IAssociateProcessor associateProcessor)
        {
            _associateProcessor = associateProcessor;
        }
        // GET api/AllAssociates
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _associateProcessor.GetAll();
            return Ok(result);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] List<Associate> associates)
        {
            if (associates == null)
                return BadRequest();

          var result = await _associateProcessor.SaveAssociates(associates);
          return Ok(result);
        }
        

    }
}