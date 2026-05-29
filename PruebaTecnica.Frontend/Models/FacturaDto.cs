using System.Text.Json.Serialization;

namespace PruebaTecnica.Frontend.Models
{
    public class FacturaDto
    {
        [JsonPropertyName("idFacturas")]
        public int IdFacturas { get; set; }

        [JsonPropertyName("idUsuarios")]
        public int IdUsuarios { get; set; }

        [JsonPropertyName("idClientes")]
        public int IdClientes { get; set; }

        [JsonPropertyName("fecha")]
        public DateTime Fecha { get; set; }

        [JsonPropertyName("subtotal")]
        public decimal Subtotal { get; set; }

        [JsonPropertyName("isv")]
        public decimal ISV { get; set; }

        [JsonPropertyName("total")]
        public decimal Total { get; set; }
    }
}