using PruebaTecnica.Core.Entities;

namespace PruebaTecnica.Core.Interfaces
{
    public interface IUsuarioRepository
    {
        // Métodos asíncronos para buenas prácticas de rendimiento
        Task<Usuario?> ObtenerPorCorreoAsync(string correo);
        Task<bool> CrearAsync(Usuario usuario);
    }
}