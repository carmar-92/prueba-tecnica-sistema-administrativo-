using MySql.Data.MySqlClient;
using PruebaTecnica.Core.Entities;
using PruebaTecnica.Core.Interfaces;
using PruebaTecnica.Infrastructure.Data;
using System.Data;

namespace PruebaTecnica.Infrastructure.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly MySqlContext _context;

        public ProductoRepository(MySqlContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Producto>> ListarAsync()
        {
            var productos = new List<Producto>();
            using var connection = _context.CreateConnection();
            var query = "SELECT * FROM Productos WHERE Estado = 1";

            using var command = new MySqlCommand(query, (MySqlConnection)connection);
            await ((MySqlConnection)connection).OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {                
                productos.Add(MapearProducto(reader));
            }
            return productos;
        }

        public async Task<Producto?> ObtenerPorIdAsync(int id)
        {
            using var connection = _context.CreateConnection();
            var query = "SELECT * FROM Productos WHERE IdProductos = @Id AND Estado = 1";

            using var command = new MySqlCommand(query, (MySqlConnection)connection);
            command.Parameters.AddWithValue("@Id", id);

            await ((MySqlConnection)connection).OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return MapearProducto(reader);
            }
            return null;
        }

        public async Task<bool> CrearAsync(Producto producto)
        {
            using var connection = _context.CreateConnection();
            var query = @"INSERT INTO Productos (Codigo, Nombre, Precio, Stock, Estado) 
                          VALUES (@Codigo, @Nombre, @Precio, @Stock, @Estado)";

            using var command = new MySqlCommand(query, (MySqlConnection)connection);
            command.Parameters.AddWithValue("@Codigo", producto.Codigo);
            command.Parameters.AddWithValue("@Nombre", producto.Nombre);
            command.Parameters.AddWithValue("@Precio", producto.Precio);
            command.Parameters.AddWithValue("@Stock", producto.Stock);
            command.Parameters.AddWithValue("@Estado", producto.Estado);

            await ((MySqlConnection)connection).OpenAsync();
            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> EditarAsync(Producto producto)
        {
            using var connection = _context.CreateConnection();
            var query = @"UPDATE Productos 
                          SET Codigo = @Codigo, Nombre = @Nombre, Precio = @Precio, Stock = @Stock 
                          WHERE IdProductos = @IdProductos";

            using var command = new MySqlCommand(query, (MySqlConnection)connection);
            command.Parameters.AddWithValue("@IdProductos", producto.IdProductos);
            command.Parameters.AddWithValue("@Codigo", producto.Codigo);
            command.Parameters.AddWithValue("@Nombre", producto.Nombre);
            command.Parameters.AddWithValue("@Precio", producto.Precio);
            command.Parameters.AddWithValue("@Stock", producto.Stock);

            await ((MySqlConnection)connection).OpenAsync();
            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> DesactivarAsync(int id)
        {
            using var connection = _context.CreateConnection();

            var query = "UPDATE Productos SET Estado = 0 WHERE IdProductos = @Id";

            using var command = new MySqlCommand(query, (MySqlConnection)connection);
            command.Parameters.AddWithValue("@Id", id);

            await ((MySqlConnection)connection).OpenAsync();
            return await command.ExecuteNonQueryAsync() > 0;
        }
       
        private Producto MapearProducto(IDataReader reader)
        {
            return new Producto
            {
                IdProductos = Convert.ToInt32(reader["IdProductos"]),
                Codigo = reader["Codigo"].ToString()!,
                Nombre = reader["Nombre"].ToString()!,
                Precio = Convert.ToDecimal(reader["Precio"]),
                Stock = Convert.ToInt32(reader["Stock"]),
                Estado = Convert.ToBoolean(reader["Estado"])
            };
        }
    }
}