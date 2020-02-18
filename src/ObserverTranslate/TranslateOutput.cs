using ObserverTranslate.Services;
using System;
using System.Collections.Generic;

namespace ObserverTranslate
{
    public class TranslateOutput : ITranslateOutput
    {
        private ConsoleColor _defaultConsoleForegroundColour;

        private readonly IConsole _console;
        private readonly IDictionary<string, ConsoleColor> _languageColours = new Dictionary<string, ConsoleColor>();

        //don't include black (0) so -1
        private readonly int totalColoursAvailable = Enum.GetNames(typeof(ConsoleColor)).Length - 1;

        public TranslateOutput(IConsole console)
        {
            _console = console;
            _defaultConsoleForegroundColour = _console.ForegroundColor;
        }

        public void WriteLine(string targetLanguage, string translatedText)
        {
            lock (_languageColours)
            {
                if (!_languageColours.ContainsKey(targetLanguage))
                {
                    _languageColours.Add(targetLanguage, GetNextColour());
                }

                _console.ForegroundColor = _languageColours[targetLanguage];
                _console.WriteLine($"{targetLanguage.ToUpper()}: {translatedText}");

                _console.ForegroundColor = _defaultConsoleForegroundColour;
            }
        }

        private ConsoleColor GetNextColour()
        {
            var colourPosition = _languageColours.Count;
            var remainder = (colourPosition) % totalColoursAvailable;

            //ConsoleColor 0 is black so don't use it
            remainder++;

            return (ConsoleColor)remainder;
        }
    }
}
