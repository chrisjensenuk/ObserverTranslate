using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ObserverTranslate.Services;
using System;
using System.Threading.Tasks;

namespace ObserverTranslate
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();

            Startup startup = new Startup();
            startup.ConfigureServices(services);

            var serviceProvider = services.BuildServiceProvider();

            await serviceProvider.GetService<App>().RunAsync();
        }
    }
}
