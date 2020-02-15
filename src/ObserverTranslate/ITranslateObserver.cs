using System.Threading.Tasks;

namespace ObserverTranslate
{
    public interface ITranslateObserver
    {
        Task TranslateAsync(string sourceLanguage, string textToTranslate);
    }
}
