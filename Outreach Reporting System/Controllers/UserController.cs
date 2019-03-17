using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
    //[Authorize(Roles = "Admin")]
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
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> Get()
        {
           
            return await Task.FromResult(Ok(_userProcessor.GetAll()));
        }
        [HttpGet]
        [Route("GetRoles")]
        public async Task<ActionResult<IEnumerable<UserRole>>> GetRoles()
        {
            
            return await Task.FromResult(Ok(_userProcessor.GetRoles()));
        }

        // POST api/User
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] IEnumerable<ApplicationUser> applicationUsers)
        {
           return await Task.FromResult(Ok( _userProcessor.SaveUser(applicationUsers)));
        }
    }
}