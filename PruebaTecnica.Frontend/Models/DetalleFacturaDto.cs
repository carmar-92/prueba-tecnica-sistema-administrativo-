using System.Text.Json.Serialization;

namespace PruebaTecnica.Frontend.Models
{
    public class DetalleFacturaDto
    {
        [JsonPropertyName("idProductos")]
        public int IdProductos { get; set; }

        [JsonPropertyName("cantidad")]
        public int Cantidad { get; set; }
    }
}