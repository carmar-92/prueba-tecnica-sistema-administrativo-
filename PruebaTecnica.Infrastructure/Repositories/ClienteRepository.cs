using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using PruebaTecnica.Core.Entities;
using PruebaTecnica.Core.Interfaces;
using PruebaTecnica.Infrastructure.Data;
using System.Data;

namespace PruebaTecnica.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly MySqlContext _context;

        public ClienteRepository(MySqlContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> ListarAsync()
        {
            var clientes = new List<Cliente>();
            using var connection = _context.CreateConnection();
            var query = "SELECT * FROM Clientes WHERE Estado = 1";

            using var command = new MySqlCommand(query, (MySqlConnection)connection);
            await ((MySqlConnection)connection).OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                clientes.Add(MapearCliente(reader));
            }
            return clientes;
        }

        public async Task<IEnumerable<Cliente>> BuscarAsync(string termino)
        {
            var clientes = new List<Cliente>();
            using var connection = _context.CreateConnection();            
            var query = @"SELECT * FROM Clientes 
                          WHERE Estado = 1 AND 
                          (Nombre LIKE @Termino OR IdentidadRTN LIKE @Termino OR Correo LIKE @Termino)";

            using var command = new MySqlCommand(query, (MySqlConnection)connection);
            command.Parameters.AddWithValue("@Termino", $"%{termino}%");

            await ((MySqlConnection)connection).OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                clientes.Add(MapearCliente(reader));
            }
            return clientes;
        }

        public async Task<bool> CrearAsync(Cliente cliente)
        {
            using var connection = _context.CreateConnection();
            var query = @"INSERT INTO Clientes (Nombre, IdentidadRTN, Telefono, Correo, Estado) 
                          VALUES (@Nombre, @IdentidadRTN, @Telefono, @Correo, @Estado)";

            using var command = new MySqlCommand(query, (MySqlConnection)connection);
            command.Parameters.AddWithValue("@Nombre", cliente.Nombre);
            command.Parameters.AddWithValue("@IdentidadRTN", cliente.IdentidadRTN);
            command.Parameters.AddWithValue("@Telefono", cliente.Telefono ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Correo", cliente.Correo ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Estado", cliente.Estado);

            await ((MySqlConnection)connection).OpenAsync();
            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> EditarAsync(Cliente cliente)
        {
            using var connection = _context.CreateConnection();
            var query = @"UPDATE Clientes 
                          SET Nombre = @Nombre, IdentidadRTN = @IdentidadRTN, Telefono = @Telefono, Correo = @Correo, Estado = @Estado 
                          WHERE IdClientes = @IdClientes";

            using var command = new MySqlCommand(query, (MySqlConnection)connection);
            command.Parameters.AddWithValue("@IdClientes", cliente.IdClientes);
            command.Parameters.AddWithValue("@Nombre", cliente.Nombre);
            command.Parameters.AddWithValue("@IdentidadRTN", cliente.IdentidadRTN);
            command.Parameters.AddWithValue("@Telefono", cliente.Telefono ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Correo", cliente.Correo ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Estado", cliente.Estado);

            await ((MySqlConnection)connection).OpenAsync();
            return await command.ExecuteNonQueryAsync() > 0;
        }
                
        private Cliente MapearCliente(IDataReader reader)
        {
            return new Cliente
            {
                IdClientes = Convert.ToInt32(reader["IdClientes"]),
                Nombre = reader["Nombre"].ToString()!,
                IdentidadRTN = reader["IdentidadRTN"].ToString()!,
                Telefono = reader["Telefono"] != DBNull.Value ? reader["Telefono"].ToString() : null,
                Correo = reader["Correo"] != DBNull.Value ? reader["Correo"].ToString() : null,
                FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"]),
                Estado = Convert.ToBoolean(reader["Estado"])
            };
        }

        public async Task<Cliente> ObtenerPorIdAsync(int id)
        {
            using var connection = _context.CreateConnection();
            var query = "SELECT * FROM Clientes WHERE IdClientes = @Id";

            using var command = new MySqlCommand(query, (MySqlConnection)connection);
            command.Parameters.AddWithValue("@Id", id);

            await ((MySqlConnection)connection).OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            
            if (await reader.ReadAsync())
            {
                return MapearCliente(reader);
            }
            
            return null!;
        }
        public async Task<bool> EliminarLogicoAsync(int id)
        {
            using var connection = _context.CreateConnection();
            
            var query = "UPDATE Clientes SET Estado = 0 WHERE IdClientes = @Id";

            using var command = new MySqlCommand(query, (MySqlConnection)connection);
            command.Parameters.AddWithValue("@Id", id);

            await ((MySqlConnection)connection).OpenAsync();
            return await command.ExecuteNonQueryAsync() > 0;
        }

    }
}