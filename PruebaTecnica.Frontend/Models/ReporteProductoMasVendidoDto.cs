using System.Text.Json.Serialization;

namespace PruebaTecnica.Frontend.Models
{
    public class ReporteProductoMasVendidoDto
    {
        [JsonPropertyName("codigo")]
        public string Codigo { get; set; } = string.Empty;

        [JsonPropertyName("nombreProducto")]
        public string NombreProducto { get; set; } = string.Empty;

        [JsonPropertyName("cantidadTotalVendida")]
        public int CantidadTotalVendida { get; set; }

        [JsonPropertyName("totalGenerado")]
        public decimal TotalGenerado { get; set; }
    }
}