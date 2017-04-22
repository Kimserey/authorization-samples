using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

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
            };

            var jwt = new JwtSecurityToken(claims: claims);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return Json(encodedJwt);
        }
    }
}
