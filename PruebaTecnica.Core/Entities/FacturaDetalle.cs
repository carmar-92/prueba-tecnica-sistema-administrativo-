namespace PruebaTecnica.Core.Entities
{
    public class FacturaDetalle
    {
        public int IdFacturaDetalles { get; set; }
        public int IdFacturas { get; set; }
        public int IdProductos { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }
}