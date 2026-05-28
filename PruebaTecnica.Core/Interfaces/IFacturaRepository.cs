using PruebaTecnica.Core.Entities;

namespace PruebaTecnica.Core.Interfaces
{
    public interface IFacturaRepository
    {
        Task<bool> CrearFacturaAsync(Factura factura);
        Task<IEnumerable<Factura>> ListarAsync();
    }
}