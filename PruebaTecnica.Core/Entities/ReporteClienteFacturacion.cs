namespace PruebaTecnica.Core.Entities
{
    public class ReporteClienteFacturacion
    {
        public string Nombre { get; set; } = string.Empty;
        public string IdentidadRTN { get; set; } = string.Empty;
        public decimal TotalFacturado { get; set; }
    }
}
