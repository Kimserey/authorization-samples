using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AuthorizationSamples.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        [HttpGet]
        [Authorize]
        public string Get()
        {
            return $"{HttpContext.User.Identity.Name} is authorized!";
        }

        [HttpGet("report")]
        [Authorize(Roles = "user")]
        public string GetReport()
        {
            return $"{HttpContext.User.Identity.Name} is authorized!";
        }
    }
}
