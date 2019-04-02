using Microsoft.AspNetCore.Mvc;
using Outreach.Reporting.Business.Interfaces;
using Outreach.Reporting.Entity.Entities;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Outreach_Reporting_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize(Roles = "Administrator")]
    public class ReportFilterController : ControllerBase
    {
        private readonly IReportFilterProcessor _reportFilterProcessor;
        public ReportFilterController(IReportFilterProcessor reportFilterProcessor)
        {
            _reportFilterProcessor = reportFilterProcessor;
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
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if(id <= 0)
                return BadRequest();
            return await Task.FromResult(Ok(_reportFilterProcessor.GetFiltersById(id)));
        }        // POST api/ReportFilter
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ReportFilter filter)
        {
            int userId = GetCurrentUserId();
            if (userId <= 0)
                return BadRequest();
            return await Task.FromResult(Ok(_reportFilterProcessor.SaveFilter(userId, filter)));
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
