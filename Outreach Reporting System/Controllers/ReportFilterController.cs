using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Outreach.Reporting.Business.Interfaces;
using Outreach.Reporting.Entity.Entities;

namespace Outreach_Reporting_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize(Roles = "Administrator")]
    public class ReportFilterController : ControllerBase
    {
        private readonly IReportFilterProcessor _reportFilterProcessor;
        private readonly ILogger<ReportFilterController> _logger;
        public ReportFilterController(IReportFilterProcessor reportFilterProcessor, ILogger<ReportFilterController> logger)
        {
            _reportFilterProcessor = reportFilterProcessor;
            _logger = logger;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            int userId = GetCurrentUserId();
            if (userId <= 0)
                return BadRequest();

            return await Task.FromResult(Ok(_reportFilterProcessor.GetFiltersById(userId)));
        }

        // POST api/ReportFilter
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ReportFilter filter)
        {
            int userId = GetCurrentUserId();
            if (userId <= 0)
                return BadRequest();
            return await Task.FromResult(Ok(_reportFilterProcessor.SaveFilter(userId, filter)));
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
        private int GetCurrentUserId()
        {
            int userId = 0;
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    //if (identity.FindFirst(ClaimTypes.Role).Value == "POC")
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
