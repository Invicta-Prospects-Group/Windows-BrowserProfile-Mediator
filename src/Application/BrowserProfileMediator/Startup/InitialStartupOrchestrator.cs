using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace BrowserProfileMediator.Startup
{
    public class InitialStartupOrchestrator : IInitialStartupOrchestrator
    {
        private readonly IHostBuilder _hostBuilder;

        public InitialStartupOrchestrator(IHostBuilder hostBuilder)
        {
            _hostBuilder = hostBuilder;
        }

        public async Task StartAsync(string[] args)
        {
            _ = new Mutex(true, "25CFF1DE-DC44-4DFA-8A3C-422840C6B52C", out var onlyInstance);

            if (!onlyInstance)
            {
                Console.WriteLine("There is already another instance that is running!");
                //await DoIt(args[0]);
                return;
            }

            await _hostBuilder.Build().RunAsync();

        }
    }
}
