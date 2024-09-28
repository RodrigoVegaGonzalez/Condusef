using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Clases.Condusef.Reune
{
    public class ClsTicketData
    {
        public string ticket {  get; set; }
        public string status { get; set; }
        public string date_created { get; set; }
        public int quarter { get; set; }
        public ClsTicketData()
        {
            ticket = string.Empty;
            status = string.Empty;
            date_created = string.Empty;
            quarter = 0;
        }
    }
}
