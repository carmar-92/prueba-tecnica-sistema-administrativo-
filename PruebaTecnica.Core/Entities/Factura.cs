namespace PruebaTecnica.Core.Entities
{
    public class Factura
    {
        public int IdFacturas { get; set; }
        public int IdUsuarios { get; set; }
        public int IdClientes { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public decimal Subtotal { get; set; }
        public decimal ISV { get; set; }
        public decimal Total { get; set; }

        // Relación con el detalle de la factura
        public List<FacturaDetalle> Detalles { get; set; } = new List<FacturaDetalle>();
    }
}