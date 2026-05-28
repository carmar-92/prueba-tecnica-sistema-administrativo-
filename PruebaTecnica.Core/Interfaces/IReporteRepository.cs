using PruebaTecnica.Core.Entities;

namespace PruebaTecnica.Core.Interfaces
{
    public interface IReporteRepository
    {
        Task<IEnumerable<ReporteProductoMasVendido>> Top5ProductosMasVendidosAsync();
        Task<IEnumerable<ReporteClienteFacturacion>> ClientesMayorFacturacionAsync();
        Task<IEnumerable<Producto>> InventarioBajoAsync();
    }
}