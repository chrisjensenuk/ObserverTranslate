using Microsoft.AspNetCore.WebUtilities;
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

        public GoogleTranslator(HttpClient httpClient)
        {
            _httpClient = httpClient;
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
                //log exceptions to private log but for now just outputting exception message
                //The translate.googleapis.com site use is very limited. It only allows about 100 requests per one hour period and there after returns a 429 error (Too many requests).
                translation = $"ERROR!!! {ex.Message}";
            }
            
            return translation;
        }
    }
}
