using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Net.Client;
using System.Threading;

namespace BrowserSwitcher
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            bool onlyInstance = false;

            var mutex = new Mutex(true, "25CFF1DE-DC44-4DFA-8A3C-422840C6B52C", out onlyInstance);

            if (!onlyInstance)
            {
                Console.WriteLine("There is already another instance that is running!");
                await DoIt(args[0]);
                return;
            }

            CreateHostBuilder(args).Build().Run();
        }

        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseStartup<Startup>()
                    ;
                })
                ;
        public static async Task DoIt(string myVal)
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:5000");
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(
                new HelloRequest
                {
                    Name = myVal
                });
        }
    }




}
