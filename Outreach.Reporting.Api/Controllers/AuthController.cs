using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Outreach.Reporting.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Outreach.Reporting.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IAuthProcessor _authProcessor;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(AuthController));
        public AuthController(IAuthProcessor authProcessor)
        {
            _authProcessor = authProcessor;
        }
       
        // POST api/GetToken/{email_id}
        [HttpPost]
        public IActionResult Authenticate(int id, string email)
        {
            if (id <= 0 || string.IsNullOrEmpty(email))
                return BadRequest();

            if (_authProcessor.AuthenticateUser(id, email) || _authProcessor.CheckPocById(id, email))
            {
                string userRole = _authProcessor.GetUserRoleById(id);
                if (string.IsNullOrEmpty(userRole))
                    userRole = "POC";//if authenticated user role is null then set POC role

                IActionResult response = Unauthorized();
               
                    var tokenString = GenerateJSONWebToken(id, userRole);
                return Ok(new { token = tokenString, role = userRole });
            }
            else
                return Unauthorized("Invalid Credentials");
        }
        private string GenerateJSONWebToken(int userId, string userRole)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this_is_our_super_long_security_key_for_outreach_reporting_system_project_2019_03_10$"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, userRole)); 
            claims.Add(new Claim("UserId", userId.ToString()));
            
            var token = new JwtSecurityToken(
                issuer: "outreachReportingSystem",
              audience: "reportUsers",
              claims: claims,
              expires: DateTime.Now.AddMinutes(60),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}