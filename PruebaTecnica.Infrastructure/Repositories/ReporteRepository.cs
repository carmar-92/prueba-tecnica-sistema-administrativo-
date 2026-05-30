using MySql.Data.MySqlClient;
using PruebaTecnica.Core.Entities;
using PruebaTecnica.Core.Interfaces;
using PruebaTecnica.Infrastructure.Data;
using System.Data;

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

            // Utilización de Procedimiento Almacenado para obtener los 5 productos más vendidos
            using var command = new MySqlCommand("sp_TopProductosVendidos", (MySqlConnection)connection);
            command.CommandType = CommandType.StoredProcedure;

            await ((MySqlConnection)connection).OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                reporte.Add(new ReporteProductoMasVendido
                {
                    Codigo = reader["Codigo"].ToString()!,
                    NombreProducto = reader["Nombre"].ToString()!,
                    CantidadTotalVendida = Convert.ToInt32(reader["TotalVendido"]),
                    TotalGenerado = Convert.ToDecimal(reader["TotalGenerado"])
                });
            }
            return reporte;
        }

        public async Task<IEnumerable<ReporteClienteFacturacion>> ClientesMayorFacturacionAsync()
        {
            var reporte = new List<ReporteClienteFacturacion>();
            using var connection = _context.CreateConnection();

            // Utilización de Procedimiento Almacenado para obtener clientes con mayor facturación
            using var command = new MySqlCommand("sp_MejoresClientes", (MySqlConnection)connection);
            command.CommandType = CommandType.StoredProcedure;

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

            // Utilización de Procedimiento Almacenado para obtener productos con stock bajo
            using var command = new MySqlCommand("sp_InventarioBajo", (MySqlConnection)connection);
            command.CommandType = CommandType.StoredProcedure;

            await ((MySqlConnection)connection).OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                productos.Add(new Producto
                {
                    IdProductos = 0,
                    Codigo = reader["Codigo"].ToString()!,
                    Nombre = reader["Nombre"].ToString()!,
                    Precio = 0,
                    Stock = Convert.ToInt32(reader["Stock"]),
                    Estado = true
                });
            }
            return productos;
        }

        public async Task<IEnumerable<ReporteVentasPorMes>> VentasPorMesAsync(int anio)
        {
            var reporteFinal = new List<ReporteVentasPorMes>();
            using var connection = _context.CreateConnection();

            var query = @"SELECT MONTH(Fecha) AS Mes, SUM(Total) AS TotalVentas 
                  FROM Facturas 
                  WHERE YEAR(Fecha) = @Anio 
                  GROUP BY MONTH(Fecha)";

            using var command = new MySqlCommand(query, (MySqlConnection)connection);
            command.Parameters.AddWithValue("@Anio", anio);
            await ((MySqlConnection)connection).OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            var resultadosBD = new Dictionary<int, decimal>();
            while (await reader.ReadAsync())
            {
                resultadosBD.Add(Convert.ToInt32(reader["Mes"]), Convert.ToDecimal(reader["TotalVentas"]));
            }

            string[] nombresMeses = { "", "Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic" };

            for (int i = 1; i <= 12; i++)
            {
                reporteFinal.Add(new ReporteVentasPorMes
                {
                    Mes = i,
                    NombreMes = nombresMeses[i],
                    TotalVentas = resultadosBD.ContainsKey(i) ? resultadosBD[i] : 0
                });
            }

            return reporteFinal;
        }
    }
}