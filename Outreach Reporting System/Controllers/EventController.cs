﻿using System;
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
   // [Authorize(Roles = "Admin, PMO, POC")]
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
        public ActionResult<IEnumerable<Event>> Get()
        {
            return Ok(_eventProcessor.GetAll());
        }

        // GET api/GetWithRelatedData
        [HttpGet]
        [Route("GetWithRelatedData")]
        public ActionResult<IEnumerable<Enrollment>> GetWithRelatedData()
        {
            return Ok(_eventProcessor.GetEventsRelatedData());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] IEnumerable<Event> events)
        {
            _eventProcessor.SaveEvents(events);
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