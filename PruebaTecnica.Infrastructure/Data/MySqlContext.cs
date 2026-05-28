using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

namespace PruebaTecnica.Infrastructure.Data
{
    public class MySqlContext
    {
        private readonly string _connectionString;

        // Inyectamos IConfiguration para leer el appsettings.json
        public MySqlContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                                ?? throw new ArgumentNullException("La cadena de conexión 'DefaultConnection' no fue encontrada.");
        }

        // Este método generará una nueva conexión cada vez que un repositorio la necesite
        public IDbConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}