using PruebaTecnica.Core.Entities;

namespace PruebaTecnica.Core.Interfaces
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> ListarAsync();
        Task<IEnumerable<Cliente>> BuscarAsync(string termino);
        Task<bool> CrearAsync(Cliente cliente);
        Task<bool> EditarAsync(Cliente cliente);
        Task<Cliente> ObtenerPorIdAsync(int id);
        Task<bool> EliminarLogicoAsync(int id);
    }
}
