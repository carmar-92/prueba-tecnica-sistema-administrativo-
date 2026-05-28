namespace PruebaTecnica.Core.Entities
{
    public class Usuario
    {
        public int IdUsuarios { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
        public bool Estado { get; set; } = true;
    }
}
