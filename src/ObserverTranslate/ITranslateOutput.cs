namespace ObserverTranslate
{
    public interface ITranslateOutput
    {
        void WriteLine(string targetLanguage, string translatedText);
    }
}