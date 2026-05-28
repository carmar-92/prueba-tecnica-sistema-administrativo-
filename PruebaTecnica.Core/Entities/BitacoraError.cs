namespace PruebaTecnica.Core.Entities
{
    public class BitacoraError
    {
        public int IdBitacoraErrores { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoEvento { get; set; } = string.Empty;
        public string Mensaje { get; set; } = string.Empty;
        public string? DetallesExcepcion { get; set; }
    }
}
