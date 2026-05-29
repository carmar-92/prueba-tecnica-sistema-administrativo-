using System.Text.Json.Serialization;

namespace PruebaTecnica.Frontend.Models
{
    public class ReporteVentasPorMesDto
    {
        [JsonPropertyName("nombreMes")]
        public string NombreMes { get; set; } = string.Empty;

        [JsonPropertyName("totalVentas")]
        public decimal TotalVentas { get; set; }
    }
}