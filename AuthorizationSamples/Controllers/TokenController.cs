using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;

namespace AuthorizationSamples.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "alice"),
                new Claim("roles", "admin"),
                new Claim("roles", "user")
            };

            var jwt = new JwtSecurityToken(claims: claims);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return Json(encodedJwt);
        }
    }
}