using System.Text.Json.Serialization;

namespace PruebaTecnica.Frontend.Models
{
    public class ClienteDto
    {
        [JsonPropertyName("idClientes")]
        public int Id { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [JsonPropertyName("identidadRTN")]
        public string Identificacion { get; set; } = string.Empty;

        [JsonPropertyName("telefono")]
        public string Telefono { get; set; } = string.Empty;

        [JsonPropertyName("correo")]
        public string Correo { get; set; } = string.Empty;

        [JsonPropertyName("estado")]
        public bool Estado { get; set; } = true;

        [JsonPropertyName("fechaRegistro")]
        public DateTime? FechaRegistro { get; set; }
    }
}