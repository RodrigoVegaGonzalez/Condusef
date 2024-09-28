using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Clases.Condusef.Reune
{
    public class ClsReuneTickets
    {
        public List<ClsTicketReune> tickets { get; set; }
        public ClsReuneTickets() { 
            tickets = new List<ClsTicketReune>();
        }
    }
}
