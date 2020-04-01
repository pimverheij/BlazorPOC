using System;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace ValidatiePOC
{
    public interface IBackendWebApi
    {
        Task<T> Get<T>(string requestUri) where T : new();
        void Post<T>(string requestUri, T model);
        void Put<T>(string requestUri, T model);
        void Delete(string requestUri);
    }

    public class BackendWebApi : IBackendWebApi
    {
        private readonly IHttpClientFactory httpFactory;
        private readonly Uri baseAddress;

        public BackendWebApi(IHttpClientFactory clientFactory)
        {
            httpFactory = clientFactory;
            baseAddress = new Uri("http://localhost:65157/");
        }

        public async Task<T> Get<T>(string requestUri) where T : new()
        {
            using var http = httpFactory.CreateClient();
            http.BaseAddress = baseAddress;
            using var response = await http.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStreamAsync();
                var result = DeserializeJsonFromStream<T>(responseContent);
                return result;
        }


        public async void Post<T>(string requestUri, T model)
        {
            using var client = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
            using var httpContent = CreateHttpContent(model);
            request.Content = httpContent;

            using var response = await client
                .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }

        public async void Put<T>(string requestUri, T model)
        {
            using var client = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Put, requestUri);
            using var httpContent = CreateHttpContent(model);
            request.Content = httpContent;

            using var response = await client
                .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }

        public async void Delete(string requestUri)
        {
            var http = httpFactory.CreateClient();
            http.BaseAddress = baseAddress;
            using var response = await http.DeleteAsync(requestUri);
            response.EnsureSuccessStatusCode();
        }

        private static void SerializeJsonIntoStream(object value, Stream stream)
        {
            using var sw = new StreamWriter(stream, new UTF8Encoding(false), 1024, true);
            using var jtw = new JsonTextWriter(sw) {Formatting = Formatting.None};
            var js = new JsonSerializer();
            js.Serialize(jtw, value);
            jtw.Flush();
        }

        private static T DeserializeJsonFromStream<T>(Stream stream)
        {
            if (stream == null || stream.CanRead == false)
                return default;

            using var sr = new StreamReader(stream);
            using var jtr = new JsonTextReader(sr);
            var js = new JsonSerializer();
            var searchResult = js.Deserialize<T>(jtr);
            return searchResult;
        }

        private static HttpContent CreateHttpContent(object content)
        {
            if (content == null) return null;

            var ms = new MemoryStream();
            SerializeJsonIntoStream(content, ms);
            ms.Seek(0, SeekOrigin.Begin);
            HttpContent httpContent = new StreamContent(ms);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return httpContent;
        }
    }
}