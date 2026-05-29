using PruebaTecnica.Frontend.Models;

namespace PruebaTecnica.Frontend.Services
{
    public interface IProductoService
    {
        Task<List<ProductoDto>> ListarAsync();
        Task<ProductoDto?> ObtenerPorIdAsync(int id);
        Task<bool> CrearAsync(ProductoDto producto);
        Task<bool> EditarAsync(ProductoDto producto);
        Task<bool> DesactivarAsync(int id);
    }
}