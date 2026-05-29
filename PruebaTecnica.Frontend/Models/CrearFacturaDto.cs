using System.Text.Json.Serialization;

namespace PruebaTecnica.Frontend.Models
{
    public class CrearFacturaDto
    {
        [JsonPropertyName("idClientes")]
        public int IdClientes { get; set; }

        [JsonPropertyName("detalles")]
        public List<DetalleFacturaDto> Detalles { get; set; } = new List<DetalleFacturaDto>();
    }
}