using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Clases.Condusef.Configuracion
{
    public class ClsTokenUser
    {
        public string token_access {  get; set; }
        public string username { get; set; }
        public ClsTokenUser() { 
            token_access = string.Empty;
            username = string.Empty;
        }
    }
}
