using System;
using System.Threading;
using System.Threading.Tasks;
using BrowserProfileMediator.Startup;
using BrowserSwitcher;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BrowserProfileMediator
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await new InitialStartupOrchestrator(CreateHostBuilder(args)).StartAsync(args);
        }

        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseStartup<Startup.Startup>()
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
