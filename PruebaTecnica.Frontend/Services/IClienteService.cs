using PruebaTecnica.Frontend.Models;

namespace PruebaTecnica.Frontend.Services
{
    public interface IClienteService
    {
        Task<List<ClienteDto>> ObtenerClientesAsync();
        Task<bool> CrearClienteAsync(ClienteDto cliente);
        Task<ClienteDto> ObtenerClientePorIdAsync(int id);
        Task<bool> ActualizarClienteAsync(ClienteDto cliente);
        Task<bool> EliminarClienteAsync(int id);
    }
}