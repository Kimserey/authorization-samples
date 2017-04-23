using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

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

    // Service filter and Type filter are implementation of Filter factory.
    // If we need more control on the arguments and the overall instantiation of the attribute, 
    // we can implement IFilterFactory.
    public class Hello5FilterAttribute: Attribute, IFilterFactory
    {
        private string _extraText;

        // Indicates if the filter created can be reused accross requests.
        public bool IsReusable => false;

        public Hello5FilterAttribute(string extraText)
        {
            _extraText = extraText;
        }

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            // GetRequiredService<>() is available in Microsoft.Extensions.DependencyInjection
            // GetRequiredService VS GetService is that Required will throw exception when service can't be found, while the other will return null.
            return new Hello5FilterImpl(
                serviceProvider.GetRequiredService<IHelloService>(),
                _extraText,
                new HelloOptions { Text = "text from options" });
        }

        private class Hello5FilterImpl : ActionFilterAttribute
        {
            private HelloOptions _testWithObject;
            private string _extraText;
            private IHelloService _service;

            public Hello5FilterImpl(IHelloService service, string extraText, HelloOptions testWithObject)
            {
                _service = service;
                _extraText = extraText;
                _testWithObject = testWithObject;
            }

            public override void OnActionExecuted(ActionExecutedContext context)
            {
                Console.WriteLine($"{_service.SayHello()} | {_extraText} | {_testWithObject.Text}");
            }
        }
    }
}
