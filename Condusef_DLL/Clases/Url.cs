using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Clases
{
    public class Url
    {
        public string CrearSuperUsuario { get; set; }
        public string CrearUsuario { get; set; }
        public string RenovarToken { get; set; }
        public string ListaUsuarios { get; set; }
        public string StatusUsuario { get; set; }
        public string EnvioQuejas { get; set; }
        public string EliminarQuejas { get; set; }
        public string ConsultaQuejas { get; set; }
        public string ConsultaMedios { get; set; }
        public string ConsultaNivelesAtencion { get; set; }
        public string ConsultaListaProductos { get; set; }
        public string ConsultaListaCausas { get; set; }
        public string ConsultaEstados { get; set; }
        public string ConsultaCP { get; set; }
        public string ConsultaMunicipios { get; set; }
        public string ConsultaColonias { get; set; }

        public string SubirRedeco { get; set; }
        public string ObtenerTicketRedeco { get; set; }
        public string EstatusTicketRedeco { get; set; }
        public string CorregirTicketRedeco { get; set; }
        public string SubirReune { get; set; }
        public string ObtenerTicketReune { get; set; }
        public string EstatusTicketReune { get; set; }
        public string CorregirDocumentoReune { get; set; }
        public string EliminarDocumentoReune { get; set; }


        public Url()
        {
            CrearSuperUsuario = string.Empty;
            CrearUsuario = string.Empty;
            RenovarToken = string.Empty;
            ListaUsuarios = string.Empty;
            StatusUsuario = string.Empty;
            EnvioQuejas = string.Empty;
            EliminarQuejas = string.Empty;
            ConsultaQuejas = string.Empty;
            ConsultaMedios = string.Empty;
            ConsultaNivelesAtencion = string.Empty;
            ConsultaListaProductos = string.Empty;
            ConsultaListaCausas = string.Empty;
            ConsultaEstados = string.Empty;
            ConsultaCP = string.Empty;
            ConsultaMunicipios = string.Empty;
            ConsultaColonias = string.Empty;

            SubirRedeco = string.Empty;
            ObtenerTicketRedeco = string.Empty;
            EstatusTicketRedeco = string.Empty;
            CorregirTicketRedeco = string.Empty;
            SubirReune = string.Empty;
            ObtenerTicketReune = string.Empty;
            EstatusTicketReune = string.Empty;
            CorregirDocumentoReune = string.Empty;
            EliminarDocumentoReune = string.Empty;
        }
    }
}
