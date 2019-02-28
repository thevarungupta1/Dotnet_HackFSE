using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Outreach.Reporting.Business.Interfaces;

namespace Outreach.Reporting.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthProcessor _authProcessor;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthProcessor authProcessor, ILogger<AuthController> logger)
        {
            _authProcessor = authProcessor;
            _logger = logger;
        }

        // POST api/GetToken/{email_id}
        [HttpPost]
        public IActionResult Authenticate([FromBody]string email)
        {
            if (_authProcessor.AuthenticateUser(email))
            {
                IActionResult response = Unauthorized();
               
                    var tokenString = GenerateJSONWebToken();
                    response = Ok(new { token = tokenString });

                return response;

                ////signing credentials
                //var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

              
                ////return token
                //return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            else
                return Unauthorized();
        }
        private string GenerateJSONWebToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this_is_our_supper_long_security_key_for_token_validation_project_2018_09_07$smesk.in"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));

            var token = new JwtSecurityToken(
                issuer: "smesk.in",
              audience: "readers",
              claims: claims,
              expires: DateTime.Now.AddMinutes(10),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        // POST api/auth/Logout
        [HttpPost]
        [Route("Logout")]
        public IActionResult Logout()
        {
            return Ok();
        }

        // POST api/GetToken/{email_id}
        [HttpPost]
        [Route("Refresh")]
        public IActionResult Refresh([FromBody]string email)
        {
            if (_authProcessor.AuthenticateUser(email))
            {
                IActionResult response = Unauthorized();

                var tokenString = GenerateJSONWebToken();
                response = Ok(new { token = tokenString });

                return response;
            }
            else
                return Unauthorized();
        }
    }
}