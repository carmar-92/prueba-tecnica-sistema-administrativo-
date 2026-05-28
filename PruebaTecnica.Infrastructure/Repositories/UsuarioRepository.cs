using MySql.Data.MySqlClient;
using PruebaTecnica.Core.Entities;
using PruebaTecnica.Core.Interfaces;
using PruebaTecnica.Infrastructure.Data;
using System.Data;

namespace PruebaTecnica.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly MySqlContext _context;

        public UsuarioRepository(MySqlContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> ObtenerPorCorreoAsync(string correo)
        {
            using var connection = _context.CreateConnection();
            var query = "SELECT IdUsuarios, Nombre, Correo, Contrasena, Estado FROM Usuarios WHERE Correo = @Correo AND Estado = 1";

            using var command = new MySqlCommand(query, (MySqlConnection)connection);
            command.Parameters.AddWithValue("@Correo", correo);

            await ((MySqlConnection)connection).OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new Usuario
                {
                    IdUsuarios = reader.GetInt32("IdUsuarios"),
                    Nombre = reader.GetString("Nombre"),
                    Correo = reader.GetString("Correo"),
                    Contrasena = reader.GetString("Contrasena"),
                    Estado = reader.GetBoolean("Estado")
                };
            }

            return null;
        }

        public async Task<bool> CrearAsync(Usuario usuario)
        {
            using var connection = _context.CreateConnection();
            var query = "INSERT INTO Usuarios (Nombre, Correo, Contrasena, Estado) VALUES (@Nombre, @Correo, @Contrasena, @Estado)";

            using var command = new MySqlCommand(query, (MySqlConnection)connection);
            command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
            command.Parameters.AddWithValue("@Correo", usuario.Correo);
            command.Parameters.AddWithValue("@Contrasena", usuario.Contrasena);
            command.Parameters.AddWithValue("@Estado", usuario.Estado);

            await ((MySqlConnection)connection).OpenAsync();
            var result = await command.ExecuteNonQueryAsync();

            return result > 0;
        }
    }
}