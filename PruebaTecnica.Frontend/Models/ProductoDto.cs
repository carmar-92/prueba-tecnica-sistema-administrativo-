using System.Text.Json.Serialization;

namespace PruebaTecnica.Frontend.Models
{
    public class ProductoDto
    {
        [JsonPropertyName("idProductos")]
        public int IdProductos { get; set; }

        [JsonPropertyName("codigo")]
        public string Codigo { get; set; } = string.Empty;

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [JsonPropertyName("precio")]
        public decimal Precio { get; set; }

        [JsonPropertyName("stock")]
        public int Stock { get; set; }

        [JsonPropertyName("estado")]
        public bool Estado { get; set; } = true;
    }
}