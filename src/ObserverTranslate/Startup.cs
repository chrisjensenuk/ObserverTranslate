using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ObserverTranslate.Services;
using System;
using System.Collections.Generic;

namespace ObserverTranslate
{
    public class Startup
    {
        IConfigurationRoot Configuration { get; }

        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }


        public void ConfigureServices(IServiceCollection services)
        {
            //HttpClients
            services.AddHttpClient<ITranslator, GoogleTranslator>(client =>
            {
                //https://translate.googleapis.com/translate_a/single?client=gtx&sl=auto&tl=de&dt=t&q=cat
                client.BaseAddress = new Uri(Configuration.GetSection("googleTranslator").Get<GoogleTranslatorConfig>().BaseAddress);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            services.AddSingleton<ITranslateLog, TranslateLog>();
            services.AddSingleton<ITranslateObservable, TranslateObservable>();

            var targetLanguages = Configuration.GetSection("targetLanguages").Get<IEnumerable<string>>();

            foreach(var targetLanguage in targetLanguages)
            {
                services.AddSingleton<ITranslateObserver>(s => new TranslateObserver(
                    s.GetRequiredService<ITranslator>(),
                    s.GetRequiredService<ITranslateLog>(),
                    targetLanguage));
            }
         }
    }
}
