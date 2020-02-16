using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ObserverTranslate.IoC;
using Serilog;
using Serilog.Exceptions;
using Serilog.Formatting.Compact;
using System;

namespace ObserverTranslate
{
    public class Startup
    {
        IConfigurationRoot Configuration { get; }

        public Startup()
        {
            Log.Logger = new LoggerConfiguration()
               .WriteTo.File(
                formatter: new RenderedCompactJsonFormatter(),
                path: "log.txt", 
                rollingInterval: RollingInterval.Day
                )
               .Enrich.WithExceptionDetails()
               .CreateLogger();

            try
            {
                Log.Information("Application Startup");

                var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

                Configuration = builder.Build();
            }
            catch(Exception ex)
            {
                Log.Fatal(ex, "Exception during startup");
                throw;
            }
        }

        public void ConfigureServices(IServiceCollection services)
        {
            try
            {
                Log.Information("Begin configure services");

                //logging
                services.AddLogging(lb =>
                {
                    lb.AddSerilog(dispose: true);
                });

                services.AddApplicationServices(Configuration);

                Log.Information("End configure services");
            }
            catch(Exception ex)
            {
                Log.Fatal(ex, "Exception during service configuration");
                throw;
            }
         }
    }
}
