using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Clases.Condusef.API
{
    public class ClsErrorPeticion
    {
        public string error { get; set; }
        public string msg { get; set; }
        public ClsErrorPeticion()
        {
            error = string.Empty;
            msg = string.Empty;
        }
    }
}
