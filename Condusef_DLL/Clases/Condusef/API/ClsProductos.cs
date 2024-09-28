using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Clases.Condusef.API
{
    public class RespuestaProductos
    {
        public List<ClsProductos> products { get; set; }
        public RespuestaProductos()
        {
            products = new List<ClsProductos>();
        }
    }
    public class ClsProductos
    {
        public string productId {  get; set; }
        public string product {  get; set; }
        public string institucion { get; set; }
        public ClsProductos() { 
            productId = string.Empty;
            product = string.Empty;
            institucion = string.Empty;
        }
    }
}
