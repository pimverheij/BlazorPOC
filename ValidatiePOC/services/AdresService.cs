using System;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Components;
using SharedModels;
using Newtonsoft.Json;

namespace ValidatiePOC.services
{
    public interface IAdresService
    {
        Task<AdresModel> GetAdres(int id);
        void PostAdres(AdresModel model);
    }

    public class AdresService : IAdresService
    {
        private readonly IHttpClientFactory httpFactory;

        public AdresService(IHttpClientFactory clientFactory)
        {
            httpFactory = clientFactory;
        }

        public async Task<AdresModel> GetAdres(int id)
        {
            using var http = httpFactory.CreateClient();
            http.BaseAddress = new Uri("http://localhost:65157/");

            var response = await http.GetAsync($"Adres/{id}");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var adres = JsonConvert.DeserializeObject<AdresModel>(responseContent);
                return adres;
            }
            else
            {
                return new AdresModel();
            }
        }

        public async void PostAdres(AdresModel model)
        {
            var http = httpFactory.CreateClient();
            http.BaseAddress = new Uri("http://localhost:65157/");
            await http.PostJsonAsync("Adres", model);
        }
    }
}