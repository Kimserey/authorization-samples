using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationSamples.FiltersTest
{
    public class HelloOptions
    {
        public string Text { get; set; }
    }

    public interface IHelloService
    {
        string SayHello();
    }

    public class HelloService : IHelloService
    {
        public string SayHello()
        {
            return "Hello from injected service";
        }
    }
}
