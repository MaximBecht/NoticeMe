using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Protection.PlayReady;

namespace NoticeMe.Services.Particle
{
    public class RestApiTests
    {
        private static HttpClient _httpClient;

        public void CreateHttpClient()
        {
#if __WASM__
            var innerHandler = new Uno.UI.Wasm.WasmHttpHandler();
#else
            var innerHandler = new HttpClientHandler();
#endif
            // please ensure that a single instance of _httpClient is used
            // between all requests.
            // https://aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/
            _httpClient = new HttpClient(innerHandler);
        }

        public async Task ProcessRepositories()
        {
            if (_httpClient == null)
                CreateHttpClient();

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            _httpClient.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var stringTask = _httpClient.GetStringAsync("https://api.github.com/orgs/dotnet/repos");

            var msg = await stringTask;
            Debug.WriteLine(msg);
        }
    }
}
