using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Clases.Condusef
{
    public class ClsToken
    {
        public string TokenRedeco {  get; set; }
        public string TokenReune {  get; set; }
        public ClsToken() { 
            TokenRedeco = string.Empty;
            TokenReune = string.Empty;
        }
    }
}
