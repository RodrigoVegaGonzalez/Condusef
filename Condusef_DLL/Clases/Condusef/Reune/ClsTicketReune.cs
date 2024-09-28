using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Clases.Condusef.Reune
{
    public class ClsTicketReune
    {
        public string ticket { get; set; }
        public int year { get; set; }
        public int quarter { get; set; }
        public string data_created { get; set; }
        public ClsTicketReune()
        {
            ticket = string.Empty;
            year = 0;
            quarter = 0;
            data_created = string.Empty;
        }
    }
}
