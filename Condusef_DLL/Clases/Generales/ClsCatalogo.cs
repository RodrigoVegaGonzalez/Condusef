using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Clases.Generales
{
    public class ClsCatalogo
    {
        public string Id { get; set; }
        public string Descripcion { get; set; }
        public string Descripcion_en { get; set; }
        public string Valor { get; set; }
        public int Orden { get; set; }

        public ClsCatalogo()
        {
            Id = string.Empty;
            Descripcion = string.Empty;
            Descripcion_en = string.Empty;
            Valor = string.Empty;
            Orden = 0;
        }
    }
}
