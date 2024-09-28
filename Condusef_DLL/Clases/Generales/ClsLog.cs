using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Clases.Generales
{
    public class ClsLog
    {
        public string empresa { get; set; }
        public string usuario { get; set; }
        public string nameSpace { get; set; }
        public string metodo { get; set; }
        public string error { get; set; }

        public ClsLog()
        {
            empresa = Conexion.Empresa;
            usuario = Conexion.Usuario;
            nameSpace = "";
            metodo = "";
            error = "";
        }
    }
}
