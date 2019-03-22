using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    //[Authorize(Roles = "Admin, PMO, POC")]
    public class EventController : ControllerBase
    {
        private readonly IEventProcessor _eventProcessor;

        public EventController(IEventProcessor eventProcessor)
        {
            _eventProcessor = eventProcessor;
        }
        // GET api/AllEvents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> Get()
        {
            var user = GetCurrentUser();
            return await Task.FromResult(Ok(_eventProcessor.GetAll(user)));
        }

        // GET api/GetWithRelatedData
        [HttpGet]
        [Route("GetWithRelatedData")]
        public async Task<ActionResult<IEnumerable<Enrollment>>> GetWithRelatedData()
        {
            var user = GetCurrentUser();
            return await Task.FromResult(Ok(_eventProcessor.GetEventsRelatedData(user)));
        }
        
        // GET api/GetWithRelatedData
        [HttpGet]
        [Route("RecentEvents")]
        public async Task<IActionResult> GetRecentEvents(int recentCount)
        {
            var user = GetCurrentUser();
            return await Task.FromResult(Ok(_eventProcessor.GetRecentEvents(user, recentCount)));
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] IEnumerable<Event> events)
        {
            if(events == null)
                return BadRequest();
            return await Task.FromResult(Ok(_eventProcessor.SaveEvents(events)));
        }
               
        [HttpGet]
        [Route("GetAllFocusArea")]
        public async Task<ActionResult<IEnumerable<string>>> GetAllFocusArea()
        {
            return await Task.FromResult(Ok(_eventProcessor.GetAllFocusArea()));
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