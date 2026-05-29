using PruebaTecnica.Core.Entities;

namespace PruebaTecnica.Core.Interfaces
{
    public interface IUsuarioRepository
    {       
        Task<Usuario?> ObtenerPorCorreoAsync(string correo);
        Task<bool> CrearAsync(Usuario usuario);
    }
}