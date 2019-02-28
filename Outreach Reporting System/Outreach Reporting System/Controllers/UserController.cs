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
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly IUserProcessor _userProcessor;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserProcessor userProcessor, ILogger<UserController> logger)
        {
            _userProcessor = userProcessor;
            _logger = logger;
        }
        // GET api/User
        [HttpGet]
        public ActionResult<IEnumerable<ApplicationUsers>> Get()
        {
            return Ok(_userProcessor.GetAll());
        }

        // POST api/User
        [HttpPost]
        public void Post([FromBody] IEnumerable<ApplicationUsers> applicationUsers)
        {
            _userProcessor.SaveUser(applicationUsers);
        }
    }
}