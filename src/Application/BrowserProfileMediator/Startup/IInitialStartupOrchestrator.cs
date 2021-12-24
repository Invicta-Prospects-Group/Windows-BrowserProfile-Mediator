using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace BrowserProfileMediator.Startup
{
    public interface IInitialStartupOrchestrator
    {
        Task StartAsync(string[] args);
    }
}
