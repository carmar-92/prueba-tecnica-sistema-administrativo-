using System.Net.Http.Headers;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using PruebaTecnica.Frontend.Models;

namespace PruebaTecnica.Frontend.Services
{
    public class FacturaService : IFacturaService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public FacturaService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        private async Task PrepararCabeceraAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (!string.IsNullOrWhiteSpace(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<List<FacturaDto>> ListarAsync()
        {
            await PrepararCabeceraAsync();
            var respuesta = await _httpClient.GetFromJsonAsync<List<FacturaDto>>("api/Facturas");
            return respuesta ?? new List<FacturaDto>();
        }

        public async Task<bool> CrearFacturaAsync(CrearFacturaDto factura)
        {
            await PrepararCabeceraAsync();
           
            var respuesta = await _httpClient.PostAsJsonAsync("api/Facturas", factura);

            return respuesta.IsSuccessStatusCode;
        }
    }
}