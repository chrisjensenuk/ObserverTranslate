using System.Threading.Tasks;

namespace ObserverTranslate
{
    public interface ITranslateObservable
    {
        Task TranslateAsync(string textToTranslate);
    }
}
