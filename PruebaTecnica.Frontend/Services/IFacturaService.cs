using PruebaTecnica.Frontend.Models;

namespace PruebaTecnica.Frontend.Services
{
    public interface IFacturaService
    {
        Task<List<FacturaDto>> ListarAsync();
        Task<bool> CrearFacturaAsync(CrearFacturaDto factura);
    }
}