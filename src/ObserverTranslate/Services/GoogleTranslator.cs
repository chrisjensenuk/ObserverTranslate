using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ObserverTranslate.Services
{
    public class GoogleTranslator : ITranslator
    {
        private HttpClient _httpClient;
        private Microsoft.Extensions.Logging.ILogger _logger;

        public GoogleTranslator(HttpClient httpClient, ILogger<GoogleTranslator> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<string> TranslateAsync(string sourceLangauge, string targetLangauge, string textToTranslate)
        {
            var url = QueryHelpers.AddQueryString("translate_a/single", new Dictionary<string, string>
            {
                { "client", "gtx" },
                { "sl", sourceLangauge },
                { "tl", targetLangauge },
                { "dt", "t" },
                { "q", textToTranslate }
            });

            string translation = "";
            try
            {
                var response = await _httpClient.GetStringAsync(url);
                var doc = JsonDocument.Parse(response);

                translation = doc.RootElement[0][0][0].ToString();
            }
            catch(Exception ex)
            {
                //The translate.googleapis.com site use is very limited. It only allows about 100 requests per one hour period and there after returns a 429 error (Too many requests).
                _logger.LogError(ex, "Error translating '{sourceLangauge}' to '{targetLanguage}' for phrase '{textToTranslate}'", sourceLangauge, targetLangauge, textToTranslate);

                translation = "Whoops! There has been an error whilst trying to translate";
            }

            _logger.LogInformation("The phrase '{textToTranslate}' translated from '{sourceLangauge}' to '{targetLangauge}' is {translation}", textToTranslate, sourceLangauge, targetLangauge, translation);

            return translation;
        }
    }
}
