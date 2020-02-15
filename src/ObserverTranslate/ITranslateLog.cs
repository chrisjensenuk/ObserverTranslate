namespace ObserverTranslate
{
    public interface ITranslateLog
    {
        void WriteLine(string targetLanguage, string translatedText);
    }
}