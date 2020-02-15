using ObserverTranslate.Services;
using System.Threading.Tasks;

namespace ObserverTranslate
{
    public class TranslateObserver : ITranslateObserver
    {
        private readonly ITranslator _translator;
        private readonly string _targetLanguage;
        private readonly ITranslateLog _log;

        public TranslateObserver(ITranslator translator, ITranslateLog log, string targetLanguage)
        {
            _translator = translator;
            _log = log;
            _targetLanguage = targetLanguage;
        }

        public async Task TranslateAsync(string sourceLanguage, string textToTranslate)
        {
            var translatedText = await _translator.TranslateAsync(sourceLanguage, _targetLanguage, textToTranslate);

            _log.WriteLine(_targetLanguage, translatedText);
        }
    }
}
