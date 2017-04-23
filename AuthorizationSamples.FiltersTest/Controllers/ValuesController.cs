using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationSamples.FiltersTest.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        [HttpGet]
        [HelloFilter]
        public string Get1()
        {
            return "works";
        }

        [HttpGet("2")]
        [Hello2Filter]
        public string Get2()
        {
            return "works";
        }
    }
}
