using Grpc.Net.Client;
using System;
using System.Threading.Tasks;

namespace BrowserSwitcher.Mediator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if(args.Length > 0)
            {
                await DoIt(args[0]);
            }
        }

        public static async Task DoIt(string myVal)
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:5000");
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(
                new HelloRequest { Name = myVal 
            });
        }
    }
}
