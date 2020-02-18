using ObserverTranslate.Services;
using System.Threading.Tasks;

namespace ObserverTranslate
{
    public class TranslateObserver : ITranslateObserver
    {
        private readonly ITranslator _translator;
        private readonly string _targetLanguage;
        private readonly ITranslateOutput _output;

        public TranslateObserver(ITranslator translator, ITranslateOutput output, string targetLanguage)
        {
            _translator = translator;
            _output = output;
            _targetLanguage = targetLanguage;
        }

        public async Task TranslateAsync(string sourceLanguage, string textToTranslate)
        {
            var translatedText = await _translator.TranslateAsync(sourceLanguage, _targetLanguage, textToTranslate);

            _output.WriteLine(_targetLanguage, translatedText);
        }
    }
}
