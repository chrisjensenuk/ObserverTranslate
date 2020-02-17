using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObserverTranslate
{
    public class TranslateObservable : ITranslateObservable
    {
        private const string SourceLanguage = "en";
        private readonly IEnumerable<ITranslateObserver> _subscribers;


        public TranslateObservable(IEnumerable<ITranslateObserver> subscribers)
        {
            _subscribers = subscribers;
        }

        public Task TranslateAsync(string textToTranslate)
        {
            if (_subscribers?.Any() != true)
                return Task.CompletedTask;

            var tasks = new List<Task>();

            foreach(var subscriber in _subscribers)
            {
                tasks.Add(subscriber.TranslateAsync(SourceLanguage, textToTranslate));
            }

            return Task.WhenAll(tasks);
        }
    }
}
