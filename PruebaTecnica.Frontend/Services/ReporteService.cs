using System.Net.Http.Headers;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using PruebaTecnica.Frontend.Models;

namespace PruebaTecnica.Frontend.Services
{
    public class ReporteService : IReporteService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public ReporteService(HttpClient httpClient, ILocalStorageService localStorage)
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

        public async Task<List<ReporteProductoMasVendidoDto>> ObtenerTopProductosAsync()
        {
            await PrepararCabeceraAsync();
            var respuesta = await _httpClient.GetFromJsonAsync<List<ReporteProductoMasVendidoDto>>("api/Reportes/top-productos");
            return respuesta ?? new List<ReporteProductoMasVendidoDto>();
        }

        public async Task<List<ReporteClienteFacturacionDto>> ObtenerMejoresClientesAsync()
        {
            await PrepararCabeceraAsync();
            var respuesta = await _httpClient.GetFromJsonAsync<List<ReporteClienteFacturacionDto>>("api/Reportes/mejores-clientes");
            return respuesta ?? new List<ReporteClienteFacturacionDto>();
        }

        public async Task<List<ProductoDto>> ObtenerInventarioBajoAsync()
        {
            await PrepararCabeceraAsync();
            var respuesta = await _httpClient.GetFromJsonAsync<List<ProductoDto>>("api/Reportes/inventario-bajo");
            return respuesta ?? new List<ProductoDto>();
        }
        public async Task<List<ReporteVentasPorMesDto>> ObtenerVentasPorMesAsync(int anio)
        {
            await PrepararCabeceraAsync();
            var respuesta = await _httpClient.GetFromJsonAsync<List<ReporteVentasPorMesDto>>($"api/Reportes/ventas-mes/{anio}");
            return respuesta ?? new List<ReporteVentasPorMesDto>();
        }

    }
}