using System.Net.Http.Headers;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using PruebaTecnica.Frontend.Models;

namespace PruebaTecnica.Frontend.Services
{
    public class ClienteService : IClienteService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public ClienteService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public async Task<List<ClienteDto>> ObtenerClientesAsync()
        {            
            var token = await _localStorage.GetItemAsync<string>("authToken");
            
            if (!string.IsNullOrWhiteSpace(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            
            var respuesta = await _httpClient.GetFromJsonAsync<List<ClienteDto>>("api/Clientes");

            return respuesta ?? new List<ClienteDto>();
        }
        public async Task<bool> CrearClienteAsync(ClienteDto cliente)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (!string.IsNullOrWhiteSpace(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            
            var respuesta = await _httpClient.PostAsJsonAsync("api/Clientes", cliente);

            return respuesta.IsSuccessStatusCode;
        }
        public async Task<ClienteDto> ObtenerClientePorIdAsync(int id)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (!string.IsNullOrWhiteSpace(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            var respuesta = await _httpClient.GetFromJsonAsync<ClienteDto>($"api/Clientes/{id}");
            return respuesta ?? new ClienteDto();
        }

        public async Task<bool> ActualizarClienteAsync(ClienteDto cliente)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (!string.IsNullOrWhiteSpace(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
           
            var respuesta = await _httpClient.PutAsJsonAsync($"api/Clientes/{cliente.Id}", cliente);

            return respuesta.IsSuccessStatusCode;
        }
        public async Task<bool> EliminarClienteAsync(int id)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (!string.IsNullOrWhiteSpace(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
                        
            var respuesta = await _httpClient.DeleteAsync($"api/Clientes/{id}");
            return respuesta.IsSuccessStatusCode;
        }
    }
}