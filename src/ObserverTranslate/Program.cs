using Microsoft.Extensions.DependencyInjection;
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

            var translateObservable = serviceProvider.GetService<ITranslateObservable>();

            //'huh' apparently is the same in 31 different languages so we'll probably never need to translate this so makes a good exit word :) 
            //https://www.bubblestranslation.com/the-one-word-thats-the-same-in-every-language/
            Console.WriteLine("Welcome to Observer Translate! Type a phrase you want to translate. Type 'huh' to quit.");

            while(true)
            {
                var textToTranslate = Console.ReadLine();

                if (textToTranslate == "huh")
                    break;

                await translateObservable.TranslateAsync(textToTranslate);
            }
            
        }
    }
}
