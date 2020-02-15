using System.Threading.Tasks;

namespace ObserverTranslate.Services
{
    public interface ITranslator
    {
        Task<string> TranslateAsync(string sourceLangauge, string targetLangauge, string textToTranslate);
    }
}