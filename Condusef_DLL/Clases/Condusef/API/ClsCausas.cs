using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Clases.Condusef.API
{
    public class RespuestaCausas
    {
        public List<ClsCausas> causas { get; set; }
        public RespuestaCausas()
        {
            causas = new List<ClsCausas>();
        }
    }
    public class ClsCausas
    {
        public string causaId {  get; set; }
        public string causa { get; set;}
        public string institucion { get; set;}
        public ClsCausas()
        {
            causaId = string.Empty;
            causa = string.Empty;
            institucion = string.Empty;
        }
    }
}
