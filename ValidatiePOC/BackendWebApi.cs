using System;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace ValidatiePOC
{
    public interface IBackendWebApi
    {
        Task<T> Get<T>(string requestUri) where T : new();
        void Post<T>(string requestUri, T model);
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
            var response = await http.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseContent);
            }
            else
            {
                return new T();
            }
        }

        public async void Post<T>(string requestUri, T model)
        {
            var http = httpFactory.CreateClient();
            http.BaseAddress = baseAddress;
            await http.PostJsonAsync(requestUri, model);
        }

        public async void Delete(string requestUri)
        {
            var http = httpFactory.CreateClient();
            http.BaseAddress = baseAddress;
            await http.DeleteAsync(requestUri);
        }
    }
}