namespace PruebaTecnica.Core.Entities
{
    public class ReporteProductoMasVendido
    {
        public string Codigo { get; set; } = string.Empty;
        public string NombreProducto { get; set; } = string.Empty;
        public int CantidadTotalVendida { get; set; }
        public decimal TotalGenerado { get; set; }
    }
}
