using ObserverTranslate;
using System;
using Xunit;

namespace ObserverTranslateTests
{
    public class TranslateOutputTests
    {
        [Fact]
        public void WriteLine_ShouldWriteUsingNextColour()
        {
            //Arrange
            var testConsole = new TestConsole();
            var translateOutputter = new TranslateOutput(testConsole);

            //Act
            for(var language = 0; language <= 20; language++)
            {
                translateOutputter.WriteLine(language.ToString(), "s");
            }
            translateOutputter.WriteLine("5", "s");
            translateOutputter.WriteLine("10", "s");

            //Assert
            for (var language = 0; language < 15; language++)
            {
                //Ignore black (0)
                var expectedColour = (ConsoleColor)(language + 1);
                AssertColour(testConsole, language, expectedColour);
            }

            //once we've exhausted all 15 colours then should start again at 1
            AssertColour(testConsole, 15, (ConsoleColor)1);
            AssertColour(testConsole, 16, (ConsoleColor)2);
            AssertColour(testConsole, 17, (ConsoleColor)3);
            AssertColour(testConsole, 18, (ConsoleColor)4);
            AssertColour(testConsole, 19, (ConsoleColor)5);
            AssertColour(testConsole, 20, (ConsoleColor)6);

            //should reuse the same colours for each language
            AssertColour(testConsole, 5, (ConsoleColor)6);
            AssertColour(testConsole, 10, (ConsoleColor)11);

        }

        private void AssertColour(TestConsole testConsole, int targetLangauge, ConsoleColor expectedColour)
        {
            Assert.Equal($"{targetLangauge.ToString()}: s", testConsole.WriteLineHistory[targetLangauge].Text);
            Assert.Equal(expectedColour, testConsole.WriteLineHistory[targetLangauge].ForegroundColor);
        }
    }
}
