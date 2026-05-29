namespace PruebaTecnica.Core.Entities
{
    public class ReporteVentasPorMes
    {
        public int Mes { get; set; }
        public string NombreMes { get; set; } = string.Empty;
        public decimal TotalVentas { get; set; }
    }
}