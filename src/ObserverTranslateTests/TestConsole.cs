using ObserverTranslate.Services;
using System;
using System.Collections.Generic;

namespace ObserverTranslateTests
{
    public class TestConsole : IConsole
    {
        public List<WriteLineParams> WriteLineHistory = new List<WriteLineParams>();
        
        public ConsoleColor ForegroundColor { get; set; }

        public string ReadLine()
        {
            throw new NotImplementedException();
        }

        public void WriteLine(string value)
        {
            WriteLineHistory.Add(new WriteLineParams 
            {
                ForegroundColor = this.ForegroundColor,
                Text = value
            });
        }
    }

    public class WriteLineParams
    {
        public ConsoleColor ForegroundColor { get; set; }
        public string Text { get; set; }
    }
}
