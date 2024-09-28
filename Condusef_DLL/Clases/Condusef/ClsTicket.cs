using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Clases.Condusef
{
    public class ClsTicket
    {
        public int Id { get; set; }
        public string Ticket { get; set; }
        public string Archivo { get; set; }
        public int Año { get; set; }
        public int Periodo { get; set; }
        public int Estatus  { get; set; }
        public string FechaEnvio { get; set; }
        public int TipoDocumento { get; set; }
        public ClsTicket() { 
            Id = 0;
            Ticket = string.Empty;
            Archivo = string.Empty;
            Año = 0;
            Periodo = 0;
            Estatus = 0;
            FechaEnvio = string.Empty;
            TipoDocumento = 0;
        }

        public ClsTicket(int id, string ticket, string archivo, int año, int periodo, int estatus, string fecha, int tipoDocumento)
        {
            Id = id;
            Ticket = ticket;
            Archivo = archivo;
            Año = año;
            Periodo = periodo;
            Estatus = estatus;
            FechaEnvio = fecha;
            TipoDocumento = tipoDocumento;
        }
    }
}
