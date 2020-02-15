using System;
using System.Collections.Generic;

namespace ObserverTranslate
{
    public class TranslateLog : ITranslateLog
    {
        private static ConsoleColor DefaultConsoleForegroundColour;

        private readonly object locker = new object();
        private readonly IDictionary<string, ConsoleColor> _languageColours = new Dictionary<string, ConsoleColor>();
        private readonly int totalColoursAvailable = Enum.GetNames(typeof(ConsoleColor)).Length - 1; //don't include black (0)

        public TranslateLog()
        {
            DefaultConsoleForegroundColour = Console.ForegroundColor;
        }

        public void WriteLine(string targetLanguage, string translatedText)
        {
            lock (locker)
            {
                if (!_languageColours.ContainsKey(targetLanguage))
                {
                    _languageColours.Add(targetLanguage, GetNextColour());
                }

                Console.ForegroundColor = _languageColours[targetLanguage];
                Console.WriteLine($"{targetLanguage.ToUpper()}: {translatedText}");

                Console.ForegroundColor = DefaultConsoleForegroundColour;
            }
        }

        private ConsoleColor GetNextColour()
        {
            var colourPosition = _languageColours.Count;
            var remainder = colourPosition % totalColoursAvailable;
            colourPosition = remainder > 0 ? remainder : colourPosition;

            //ConsoleColor 0 is black so don't use it
            colourPosition++;

            return (ConsoleColor)colourPosition;
        }
    }
}
