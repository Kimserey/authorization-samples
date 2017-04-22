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
        private IAuthorizationService _authorizationService;

        public ValuesController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpGet]
        [Authorize]
        public string Get()
        {
            return $"{HttpContext.User.Identity.Name} is authorized!";
        }

        [HttpGet("profile")]
        [Authorize(Roles = "user")]
        public string GetProfile()
        {
            return $"{HttpContext.User.Identity.Name} is authorized!";
        }

        [HttpGet("report")]
        [Authorize(Policy = "hasReportAccess")]
        public string GetReport()
        {
            return $"{HttpContext.User.Identity.Name} is authorized!";
        }

        [HttpPut("report/{id}")]
        public async Task<IActionResult> PutReport(string id)
        {
            var report = new Report { Author = "alice", Content = "" }; // Here we would get the resource from somewhere
            if (await _authorizationService.AuthorizeAsync(HttpContext.User, report, new AuthorRequirement()))
            {
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet("financereport")]
        [Authorize(Policy = "accessibleOnlyDuringOfficeHours")]
        public string GetFinanceReport()
        {
            return $"{HttpContext.User.Identity.Name} is authorized!";
        }
    }
}
