using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Clases.General
{
    public class ClsResultadoAccion
    {
        public bool Estatus { get; set; }
        public int Code { get; set; }
        public string Detalle { get; set; }
        public ClsResultadoAccion() { 
            Estatus = false;
            Code = 0;
            Detalle = string.Empty;
        }
    }
}
