using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Clases.Condusef.Reune
{
    public class ClsEstatusReune
    {
        public ClsTicketData ticketData {  get; set; }
        public List<ClsReuneFiles> files {  get; set; }
        public ClsEstatusReune()
        {
            ticketData = new ClsTicketData();
            files = new List<ClsReuneFiles>();
        }
    }
}
