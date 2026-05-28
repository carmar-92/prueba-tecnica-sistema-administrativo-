using PruebaTecnica.Core.Entities;

namespace PruebaTecnica.Core.Interfaces
{
    public interface IProductoRepository
    {
        Task<IEnumerable<Producto>> ListarAsync();
        Task<bool> CrearAsync(Producto producto);
        Task<bool> EditarAsync(Producto producto);
        Task<bool> DesactivarAsync(int id);
        Task<Producto?> ObtenerPorIdAsync(int id);
    }
}
