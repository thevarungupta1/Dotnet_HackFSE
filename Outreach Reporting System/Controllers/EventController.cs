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
        private readonly ILogger<EventController> _logger;

        public EventController(IEventProcessor eventProcessor, ILogger<EventController> logger)
        {
            _eventProcessor = eventProcessor;
            _logger = logger;
        }
        // GET api/AllEvents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> Get()
        {
            int userId = GetCurrentUserId();
            return await Task.FromResult(Ok(_eventProcessor.GetAll(userId)));
        }

        // GET api/GetWithRelatedData
        [HttpGet]
        [Route("GetWithRelatedData")]
        public async Task<ActionResult<IEnumerable<Enrollment>>> GetWithRelatedData()
        {
            int userId = GetCurrentUserId();
            return await Task.FromResult(Ok(_eventProcessor.GetEventsRelatedData(userId)));
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] IEnumerable<Event> events)
        {
           return await Task.FromResult(Ok(_eventProcessor.SaveEvents(events)));
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
                    if (identity.FindFirst(ClaimTypes.Role).Value == "POC")
                        int.TryParse(identity.FindFirst("UserId").Value, out userId);
                }
            }
            catch (Exception ex)
            {

            }
            return userId;
        }

        [HttpGet]
        [Route("GetAllFocusArea")]
        public async Task<ActionResult<IEnumerable<string>>> GetAllFocusArea()
        {
            return await Task.FromResult(Ok(_eventProcessor.GetAllFocusArea()));
        }
    }
}