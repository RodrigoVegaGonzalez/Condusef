using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Clases.Admin
{
    public class ClsLogAPI
    {
        public string Metodo {  get; set; }
        public int CodigoError { get; set; }
        public string Peticion { get; set; }
        public string Error { get; set; }
        public DateTime Fecha { get; set; }
        public ClsLogAPI()
        {
            Metodo = string.Empty;
            CodigoError = 0;
            Peticion = string.Empty;
            Error = string.Empty;
            Fecha = new DateTime();
        }
    }
}
