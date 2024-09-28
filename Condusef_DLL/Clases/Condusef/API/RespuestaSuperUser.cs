using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Clases.Condusef.API
{
    public class RespuestaSuperUser
    {
        public string message { get; set; }
        public ClsSuperUser data { get; set; }
        public RespuestaSuperUser() { 
            message = string.Empty;
            data = new ClsSuperUser();
        }
    }
}
