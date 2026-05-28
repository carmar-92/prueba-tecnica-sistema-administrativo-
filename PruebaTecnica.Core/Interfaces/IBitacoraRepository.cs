using PruebaTecnica.Core.Entities;

namespace PruebaTecnica.Core.Interfaces
{
    public interface IBitacoraRepository
    {
        Task RegistrarAsync(BitacoraError log);
    }
}