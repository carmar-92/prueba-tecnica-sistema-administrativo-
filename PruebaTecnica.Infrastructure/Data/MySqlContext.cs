using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

namespace PruebaTecnica.Infrastructure.Data
{
    public class MySqlContext
    {
        private readonly string _connectionString;
        
        public MySqlContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                                ?? throw new ArgumentNullException("La cadena de conexión 'DefaultConnection' no fue encontrada.");
        }
        
        public IDbConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}