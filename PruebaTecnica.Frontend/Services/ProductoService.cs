using System.Net.Http.Headers;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using PruebaTecnica.Frontend.Models;

namespace PruebaTecnica.Frontend.Services
{
    public class ProductoService : IProductoService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public ProductoService(HttpClient httpClient, ILocalStorageService localStorage)
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

        public async Task<List<ProductoDto>> ListarAsync()
        {
            await PrepararCabeceraAsync();
            var respuesta = await _httpClient.GetFromJsonAsync<List<ProductoDto>>("api/Productos");
            return respuesta ?? new List<ProductoDto>();
        }

        public async Task<ProductoDto?> ObtenerPorIdAsync(int id)
        {
            await PrepararCabeceraAsync();
            return await _httpClient.GetFromJsonAsync<ProductoDto>($"api/Productos/{id}");
        }

        public async Task<bool> CrearAsync(ProductoDto producto)
        {
            await PrepararCabeceraAsync();
            var respuesta = await _httpClient.PostAsJsonAsync("api/Productos", producto);
            return respuesta.IsSuccessStatusCode;
        }

        public async Task<bool> EditarAsync(ProductoDto producto)
        {
            await PrepararCabeceraAsync();
            var respuesta = await _httpClient.PutAsJsonAsync($"api/Productos/{producto.IdProductos}", producto);
            return respuesta.IsSuccessStatusCode;
        }

        public async Task<bool> DesactivarAsync(int id)
        {
            await PrepararCabeceraAsync();
            var respuesta = await _httpClient.DeleteAsync($"api/Productos/{id}");
            return respuesta.IsSuccessStatusCode;
        }
    }
}