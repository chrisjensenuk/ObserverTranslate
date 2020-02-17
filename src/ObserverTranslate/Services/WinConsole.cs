using System;

namespace ObserverTranslate.Services
{
    public class WinConsole : IConsole
    {
        public ConsoleColor ForegroundColor
        {
            get => Console.ForegroundColor;
            set => Console.ForegroundColor = value;
        }

        public void WriteLine(string value) => Console.WriteLine(value);

        public string ReadLine() => Console.ReadLine();
    }
}
