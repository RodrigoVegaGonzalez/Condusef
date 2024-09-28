using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Clases.Condusef.Redeco
{
    public class ClsEstatusTicket
    {
        public string ticket { get; set; }
        public bool processed { get; set; }
        public int totalErrors { get; set; }
        public string linesWithErrors { get; set; }
        public ClsEstatusTicket()
        {
            ticket = string.Empty;
            processed = false;
            totalErrors = 0;
            linesWithErrors = string.Empty;
        }
    }
}
