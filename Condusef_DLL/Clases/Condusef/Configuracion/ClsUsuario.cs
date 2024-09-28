using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Clases.Condusef.Configuracion
{
    public class ClsUsuario
    {
        public string IdUsuario {  get; set; }
        public string Usuario { get; set; }
        public string IdEmpresa { get; set; }
        public string Empresa { get; set; }
        public string Sector { get; set; }
        public string IdSector { get; set; }
        public string NombreCortoEmpresa { get; set; }
        public string CorreoResponsable { get; set; }
        public int IdRol { get; set; }
        public string Rol { get; set; }
        public bool Activo { get; set; }
        public ClsUsuario(string idEmpresa = "", string idUsuario = "", string username = "")
        {
            IdUsuario = idUsuario;
            Usuario = username;
            IdRol = 0;
            IdSector = string.Empty;
            Activo = false;
            IdEmpresa = idEmpresa;
            Rol = string.Empty;
            Empresa = string.Empty;
            NombreCortoEmpresa = string.Empty;
            CorreoResponsable = string.Empty;
        }
    }
}
