using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Condusef_DLL.Clases.Condusef.Configuracion;
using Condusef_DLL.Clases.General;

namespace Condusef_DLL.Clases.Condusef.API
{
    public class ClsRenovarToken
    {
        public string mesg { get; set; }
        public ClsTokenUser user { get; set; }
        public ClsResultadoAccion resultado { get; set; }
        public ClsRenovarToken()
        {
            mesg = string.Empty;
            user = new ClsTokenUser();
            resultado = new ClsResultadoAccion();
        }
    }
}
