using System;

namespace ObserverTranslate.Services
{
    public interface IConsole
    {
        ConsoleColor ForegroundColor { get; set; }

        void WriteLine(string value);
        string ReadLine();
    }
}