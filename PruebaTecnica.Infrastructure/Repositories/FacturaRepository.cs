using MySql.Data.MySqlClient;
using PruebaTecnica.Core.Entities;
using PruebaTecnica.Core.Interfaces;
using PruebaTecnica.Infrastructure.Data;
using System.Data;

namespace PruebaTecnica.Infrastructure.Repositories
{
    public class FacturaRepository : IFacturaRepository
    {
        private readonly MySqlContext _context;

        public FacturaRepository(MySqlContext context)
        {
            _context = context;
        }

        public async Task<bool> CrearFacturaAsync(Factura factura)
        {
            using var connection = _context.CreateConnection();
            await ((MySqlConnection)connection).OpenAsync();

            // INICIAMOS LA TRANSACCIÓN
            using var transaction = await ((MySqlConnection)connection).BeginTransactionAsync();

            try
            {
                // 1. Insertar el Encabezado de la Factura y obtener el ID generado
                var queryFactura = @"INSERT INTO Facturas (IdUsuarios, IdClientes, Fecha, Subtotal, ISV, Total) 
                                     VALUES (@IdUsuarios, @IdClientes, @Fecha, @Subtotal, @ISV, @Total);
                                     SELECT LAST_INSERT_ID();";

                using var cmdFactura = new MySqlCommand(queryFactura, (MySqlConnection)connection, transaction);
                cmdFactura.Parameters.AddWithValue("@IdUsuarios", factura.IdUsuarios);
                cmdFactura.Parameters.AddWithValue("@IdClientes", factura.IdClientes);
                cmdFactura.Parameters.AddWithValue("@Fecha", factura.Fecha);
                cmdFactura.Parameters.AddWithValue("@Subtotal", factura.Subtotal);
                cmdFactura.Parameters.AddWithValue("@ISV", factura.ISV);
                cmdFactura.Parameters.AddWithValue("@Total", factura.Total);

                // Capturamos el ID de la factura recién creada
                factura.IdFacturas = Convert.ToInt32(await cmdFactura.ExecuteScalarAsync());

                // 2. Insertar los Detalles y descontar el Inventario
                var queryDetalle = @"INSERT INTO FacturaDetalles (IdFacturas, IdProductos, Cantidad, PrecioUnitario, Subtotal) 
                                     VALUES (@IdFacturas, @IdProductos, @Cantidad, @PrecioUnitario, @Subtotal)";

                var queryStock = @"UPDATE Productos SET Stock = Stock - @Cantidad WHERE IdProductos = @IdProductos";

                foreach (var detalle in factura.Detalles)
                {
                    // Guardar el detalle
                    using var cmdDetalle = new MySqlCommand(queryDetalle, (MySqlConnection)connection, transaction);
                    cmdDetalle.Parameters.AddWithValue("@IdFacturas", factura.IdFacturas);
                    cmdDetalle.Parameters.AddWithValue("@IdProductos", detalle.IdProductos);
                    cmdDetalle.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                    cmdDetalle.Parameters.AddWithValue("@PrecioUnitario", detalle.PrecioUnitario);
                    cmdDetalle.Parameters.AddWithValue("@Subtotal", detalle.Subtotal);
                    await cmdDetalle.ExecuteNonQueryAsync();

                    // Descontar el stock automáticamente
                    using var cmdStock = new MySqlCommand(queryStock, (MySqlConnection)connection, transaction);
                    cmdStock.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                    cmdStock.Parameters.AddWithValue("@IdProductos", detalle.IdProductos);
                    await cmdStock.ExecuteNonQueryAsync();
                }

                // SI TODO SALIÓ BIEN, CONFIRMAMOS LOS CAMBIOS EN LA BASE DE DATOS
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                // SI ALGO FALLÓ, REVERTIMOS TODO (No se guarda factura incompleta ni se descuenta stock)
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<Factura>> ListarAsync()
        {
            var facturas = new List<Factura>();
            using var connection = _context.CreateConnection();
            var query = "SELECT * FROM Facturas ORDER BY Fecha DESC";

            using var command = new MySqlCommand(query, (MySqlConnection)connection);
            await ((MySqlConnection)connection).OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                facturas.Add(new Factura
                {
                    IdFacturas = Convert.ToInt32(reader["IdFacturas"]),
                    IdUsuarios = Convert.ToInt32(reader["IdUsuarios"]),
                    IdClientes = Convert.ToInt32(reader["IdClientes"]),
                    Fecha = Convert.ToDateTime(reader["Fecha"]),
                    Subtotal = Convert.ToDecimal(reader["Subtotal"]),
                    ISV = Convert.ToDecimal(reader["ISV"]),
                    Total = Convert.ToDecimal(reader["Total"])
                });
            }
            return facturas;
        }
    }
}