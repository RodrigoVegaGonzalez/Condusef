namespace Condusef.Classes
{
    public class Conexion
    {
        public string DB { get; set; }
        public string Seguridad { get; set; }
        public string Usuario { get; set; }
        public string Empresa { get; set; }
        public string Idioma { get; set; }
        public Conexion()
        {
            DB = string.Empty;
            Seguridad = string.Empty;
            Usuario = string.Empty;
            Empresa = string.Empty;
            Idioma = string.Empty;
        }
    }
}
