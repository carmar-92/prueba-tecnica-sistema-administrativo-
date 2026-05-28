namespace PruebaTecnica.Core.Entities
{
    public class Producto
    {
        public int IdProductos { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public bool Estado { get; set; } = true;
    }
}