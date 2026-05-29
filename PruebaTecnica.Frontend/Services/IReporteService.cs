using PruebaTecnica.Frontend.Models;

namespace PruebaTecnica.Frontend.Services
{
    public interface IReporteService
    {
        Task<List<ReporteProductoMasVendidoDto>> ObtenerTopProductosAsync();
        Task<List<ReporteClienteFacturacionDto>> ObtenerMejoresClientesAsync();
        Task<List<ProductoDto>> ObtenerInventarioBajoAsync();
        Task<List<ReporteVentasPorMesDto>> ObtenerVentasPorMesAsync(int anio);
    }
}