using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Clases.Admin
{
    public class ClsEmpresas
    {
        public string IdEmpresa { get; set; }
        public string NombreEmpresa { get; set; }
        public bool Estatus { get; set; }
        public ClsEmpresas() { 
            IdEmpresa = string.Empty;
            NombreEmpresa = string.Empty;
            Estatus = false;
        }
    }
}
