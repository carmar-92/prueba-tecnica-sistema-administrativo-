using MySql.Data.MySqlClient;
using PruebaTecnica.Core.Entities;
using PruebaTecnica.Core.Interfaces;
using PruebaTecnica.Infrastructure.Data;

namespace PruebaTecnica.Infrastructure.Repositories
{
    public class BitacoraRepository : IBitacoraRepository
    {
        private readonly MySqlContext _context;

        public BitacoraRepository(MySqlContext context)
        {
            _context = context;
        }

        public async Task RegistrarAsync(BitacoraError log)
        {
            using var connection = _context.CreateConnection();
            var query = @"INSERT INTO BitacoraErrores (TipoEvento, Mensaje, DetallesExcepcion) 
                          VALUES (@TipoEvento, @Mensaje, @DetallesExcepcion)";

            using var command = new MySqlCommand(query, (MySqlConnection)connection);
            command.Parameters.AddWithValue("@TipoEvento", log.TipoEvento);
            command.Parameters.AddWithValue("@Mensaje", log.Mensaje);
            command.Parameters.AddWithValue("@DetallesExcepcion", log.DetallesExcepcion ?? (object)DBNull.Value);

            await ((MySqlConnection)connection).OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
    }
}