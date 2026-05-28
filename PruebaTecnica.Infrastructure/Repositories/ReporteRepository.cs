using MySql.Data.MySqlClient;
using PruebaTecnica.Core.Entities;
using PruebaTecnica.Core.Interfaces;
using PruebaTecnica.Infrastructure.Data;

namespace PruebaTecnica.Infrastructure.Repositories
{
    public class ReporteRepository : IReporteRepository
    {
        private readonly MySqlContext _context;

        public ReporteRepository(MySqlContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ReporteProductoMasVendido>> Top5ProductosMasVendidosAsync()
        {
            var reporte = new List<ReporteProductoMasVendido>();
            using var connection = _context.CreateConnection();

            // JOIN entre Detalles y Productos, agrupado y ordenado con LIMIT 5
            var query = @"SELECT p.Codigo, p.Nombre AS NombreProducto, 
                                 SUM(fd.Cantidad) AS CantidadTotalVendida, 
                                 SUM(fd.Subtotal) AS TotalGenerado 
                          FROM FacturaDetalles fd 
                          INNER JOIN Productos p ON fd.IdProductos = p.IdProductos 
                          GROUP BY p.IdProductos, p.Codigo, p.Nombre 
                          ORDER BY CantidadTotalVendida DESC 
                          LIMIT 5";

            using var command = new MySqlCommand(query, (MySqlConnection)connection);
            await ((MySqlConnection)connection).OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                reporte.Add(new ReporteProductoMasVendido
                {
                    Codigo = reader["Codigo"].ToString()!,
                    NombreProducto = reader["NombreProducto"].ToString()!,
                    CantidadTotalVendida = Convert.ToInt32(reader["CantidadTotalVendida"]),
                    TotalGenerado = Convert.ToDecimal(reader["TotalGenerado"])
                });
            }
            return reporte;
        }

        public async Task<IEnumerable<ReporteClienteFacturacion>> ClientesMayorFacturacionAsync()
        {
            var reporte = new List<ReporteClienteFacturacion>();
            using var connection = _context.CreateConnection();

            // JOIN entre Facturas y Clientes, sumando el Total
            var query = @"SELECT c.Nombre, c.IdentidadRTN, SUM(f.Total) AS TotalFacturado 
                          FROM Facturas f 
                          INNER JOIN Clientes c ON f.IdClientes = c.IdClientes 
                          GROUP BY c.IdClientes, c.Nombre, c.IdentidadRTN 
                          ORDER BY TotalFacturado DESC 
                          LIMIT 10"; // Mostramos el Top 10 de mejores clientes

            using var command = new MySqlCommand(query, (MySqlConnection)connection);
            await ((MySqlConnection)connection).OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                reporte.Add(new ReporteClienteFacturacion
                {
                    Nombre = reader["Nombre"].ToString()!,
                    IdentidadRTN = reader["IdentidadRTN"].ToString()!,
                    TotalFacturado = Convert.ToDecimal(reader["TotalFacturado"])
                });
            }
            return reporte;
        }

        public async Task<IEnumerable<Producto>> InventarioBajoAsync()
        {
            var productos = new List<Producto>();
            using var connection = _context.CreateConnection();

            // Filtro simple para stock menor a 5
            var query = "SELECT * FROM Productos WHERE Stock < 5 AND Estado = 1 ORDER BY Stock ASC";

            using var command = new MySqlCommand(query, (MySqlConnection)connection);
            await ((MySqlConnection)connection).OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                productos.Add(new Producto
                {
                    IdProductos = Convert.ToInt32(reader["IdProductos"]),
                    Codigo = reader["Codigo"].ToString()!,
                    Nombre = reader["Nombre"].ToString()!,
                    Precio = Convert.ToDecimal(reader["Precio"]),
                    Stock = Convert.ToInt32(reader["Stock"]),
                    Estado = Convert.ToBoolean(reader["Estado"])
                });
            }
            return productos;
        }
    }
}