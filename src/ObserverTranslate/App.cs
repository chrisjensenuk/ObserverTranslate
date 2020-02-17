using Microsoft.Extensions.Logging;
using ObserverTranslate.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ObserverTranslate
{
    public class App
    {
        private readonly IConsole _console;
        private readonly ITranslateObservable _observable;
        private readonly ILogger _logger;

        public App(IConsole console, ITranslateObservable observable, ILogger<App> logger)
        {
            _console = console;
            _observable = observable;
            _logger = logger;
        }

        public async Task RunAsync()
        {
            try
            {
                //'huh' apparently is the same in 31 different languages so we'll probably never need to translate this so makes a good exit word :) 
                //https://www.bubblestranslation.com/the-one-word-thats-the-same-in-every-language/
                _console.WriteLine("Welcome to Observer Translate! Type a phrase you want to translate. Type 'huh' to quit.");

                while (true)
                {
                    var textToTranslate = _console.ReadLine();

                    if (textToTranslate == "huh")
                        break;

                    await _observable.TranslateAsync(textToTranslate);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error in Main");
                _console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
