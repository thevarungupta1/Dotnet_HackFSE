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
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly IUserProcessor _userProcessor;

        public UserController(IUserProcessor userProcessor)
        {
            _userProcessor = userProcessor;
        }
        // GET api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> Get()
        {           
            return await Task.FromResult(Ok(_userProcessor.GetAll()));
        }
        //GET api/user/Roles
        [HttpGet]
        [Route("Roles")]
        public IActionResult GetRoles()
        {            
            return Ok(_userProcessor.GetRoles());
        }

        // POST api/User
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] IEnumerable<ApplicationUser> applicationUsers)
        {
            if (applicationUsers == null)
                return BadRequest();
            return await Task.FromResult(Ok( _userProcessor.SaveUser(applicationUsers)));
        }
        //POST api/SavePOC
        [HttpPost]
        [Route("SavePOC")]
        public async Task<IActionResult> SavePOC([FromBody] IEnumerable<PointOfContact> pocUsers)
        {
            if (pocUsers == null)
                return BadRequest();
           return await Task.FromResult(Ok( _userProcessor.SavePOC(pocUsers)));
        }

    }
}