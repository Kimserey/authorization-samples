using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;

namespace AuthorizationSamples.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        [HttpGet]
        [Authorize]
        public string Get()
        {
            var subject = HttpContext.User.Claims.Single(claim => claim.Type == JwtRegisteredClaimNames.Sub);
            return $"{subject.Value} is authorized!";
        }
    }
}
