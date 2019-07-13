using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Outreach.Reporting.Business.Interfaces;
using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Outreach.Reporting.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, PMO, POC")]
    public class EventController : ControllerBase
    {
        private readonly IEventProcessor _eventProcessor;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(EventController));

        public EventController(IEventProcessor eventProcessor)
        {
            _eventProcessor = eventProcessor;
        }
        // GET api/AllEvents
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var user = GetCurrentUser();
            return Ok(await _eventProcessor.GetAll(user));
        }

        // GET api/GetWithRelatedData
        [HttpGet]
        [Route("GetWithRelatedData")]
        public async Task<IActionResult> GetWithRelatedData()
        {
            var user = GetCurrentUser();
            return Ok(await _eventProcessor.GetEventsRelatedData(user));
        }
        
        // GET api/GetWithRelatedData
        [HttpGet]
        [Route("RecentEvents")]
        public async Task<IActionResult> GetRecentEvents(int recentCount)
        {
            var user = GetCurrentUser();
            return Ok(await _eventProcessor.GetRecentEvents(user, recentCount));
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] List<Event> events)
        {
            if(events == null)
                return BadRequest();
            return Ok(await _eventProcessor.SaveEvents(events));
        }
               
        [HttpGet]
        [Route("GetAllFocusArea")]
        public async Task<IActionResult> GetAllFocusArea()
        {
            return Ok(await _eventProcessor.GetAllFocusArea());
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