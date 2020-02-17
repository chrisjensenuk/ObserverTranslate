using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ObserverTranslate.Services;
using System;
using System.Collections.Generic;

namespace ObserverTranslate.IoC
{
    public static class ApplicationIoC
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            //HttpClients
            services.AddHttpClient<ITranslator, GoogleTranslator>(client =>
            {
                //https://translate.googleapis.com/translate_a/single?client=gtx&sl=auto&tl=de&dt=t&q=cat
                client.BaseAddress = new Uri(configuration.GetSection("googleTranslator").Get<GoogleTranslatorConfig>().BaseAddress);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            //Add application IoC
            services.AddSingleton<ITranslateLog, TranslateOutputter>();
            services.AddSingleton<ITranslateObservable, TranslateObservable>();

            var targetLanguages = configuration.GetSection("targetLanguages").Get<IEnumerable<string>>();

            foreach (var targetLanguage in targetLanguages)
            {
                services.AddSingleton<ITranslateObserver>(s => new TranslateObserver(
                    s.GetRequiredService<ITranslator>(),
                    s.GetRequiredService<ITranslateLog>(),
                    targetLanguage));
            }

            services.AddSingleton<IConsole, WinConsole>();

            services.AddSingleton<App>();
        }
    }
}
