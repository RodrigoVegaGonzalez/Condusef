using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Clases.Condusef.Redeco
{
    public class ClsTicketRedeco
    {
        public string ticket { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public string data_created { get; set; }
        public ClsTicketRedeco()
        {
            ticket = string.Empty;
            year = 0;
            month = 0;
            data_created = string.Empty;
        }
    }
}
