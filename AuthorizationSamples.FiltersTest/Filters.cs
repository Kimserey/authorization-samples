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
    // Only synchronous functions OR asynchronous function should be implemented (not both at the same time).
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

    // To be used with service filter, allows to resolve dependencies via DI
    public class Hello3Filter : IActionFilter
    {
        private IHelloService _service;

        public Hello3Filter(IHelloService service)
        {
            _service = service;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine(_service.SayHello());
        }

        public void OnActionExecuting(ActionExecutingContext context)
        { }
    }

    // To be used with type filter, allows to resolve dependencies via DI AND pass arguments.
    // Filter does not need to be registered as it is instantiated by type filter.
    //
    // Arguments type are limited by attributes:
    // An attribute argument must be a constant expression, typeof expression or array creation expression of an attribute parameter type.
    public class Hello4Filter : IActionFilter
    {
        private IHelloService _service;
        private string _extraText;

        public Hello4Filter(IHelloService service, string extraText)
        {
            _service = service;
            _extraText = extraText;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine($"{_service.SayHello()} | {_extraText}");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        { }
    }
}
