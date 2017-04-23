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

        // When filter is not registered:
        // System.InvalidOperationException: No service for type 'AuthorizationSamples.FiltersTest.Hello3Filter' has been registered.
        [HttpGet("3")]
        [ServiceFilter(typeof(Hello3Filter))]
        public string Get3()
        {
            return "works";
        }

        [HttpGet("4")]
        [TypeFilter(typeof(Hello4Filter), Arguments = new[] { "some arguments" })]
        public string Get4()
        {
            return "works";
        }
    }
}
