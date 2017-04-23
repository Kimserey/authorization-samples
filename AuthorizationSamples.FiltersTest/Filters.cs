using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Filter doc
// https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/filters
//
namespace AuthorizationSamples.FiltersTest
{
    // ActionFilterAttribute hides the empty implementations which would have been needed 
    // when implementing straight IActionFilter
    public class HelloFilterAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("Hello");
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            Console.WriteLine("Bye bye");
        }
    }

    public class Hello2FilterAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("Hello");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        { }
    }


}
