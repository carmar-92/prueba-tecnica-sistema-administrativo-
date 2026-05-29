using System.Text.Json.Serialization;

namespace PruebaTecnica.Frontend.Models
{
    public class ReporteClienteFacturacionDto
    {
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [JsonPropertyName("identidadRTN")]
        public string IdentidadRTN { get; set; } = string.Empty;

        [JsonPropertyName("totalFacturado")]
        public decimal TotalFacturado { get; set; }
    }
}