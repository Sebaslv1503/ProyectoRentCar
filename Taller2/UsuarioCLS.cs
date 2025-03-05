namespace CapaEntidad
{
    public class UsuarioCLS
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string nombreUsuario { get; set; }
        public string passwordHash { get; set; }
        public string rol { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public string cargo { get; set; }
    }
}
