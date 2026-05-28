namespace PruebaTecnica.Core.Entities
{
    public class Cliente
    {
        public int IdClientes { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string IdentidadRTN { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Estado { get; set; } = true;
    }
}