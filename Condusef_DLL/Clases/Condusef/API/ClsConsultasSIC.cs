using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Clases.Condusef.API
{
    public class ClsConsultaSIC
    {
        public string InstitucionClave { get; set; }
        public string Sector { get; set; }
        public int ConsultasTrim { get; set; }
        public int NumConsultas { get; set; }
        public string ConsultasFolio { get; set; }
        public int ConsultasEstatusCon { get; set; }
        public string ConsultasFecAten { get; set; }
        public int QuejasEstados { get; set; }
        public string ConsultasFecRecepcion  { get; set; }
        public int MediosId { get; set; }
        public int TipoReporte { get; set; }
        public int ClaveSIPRES { get; set; }
        public int TipoPersona { get; set; }
        public int ConsultasCP { get; set; }
        public int ConsultasMpioId { get; set; }
        public int ConsultasLocId { get; set; }
        public int ConsultasColId { get; set; }
        public int ConsultascatnivelatenId { get; set; }

        public ClsConsultaSIC()
        {
            InstitucionClave = string.Empty;
            Sector = string.Empty;
            ConsultasTrim = 0;
            NumConsultas = 0;
            ConsultasFolio = string.Empty;
            ConsultasEstatusCon = 0;
            ConsultasFecAten = string.Empty;
            QuejasEstados = 0;
            ConsultasFecRecepcion = string.Empty;
            MediosId = 0;
            TipoReporte = 0;
            ClaveSIPRES = 0;
            TipoPersona = 0;
            ConsultasCP = 0;
            ConsultasMpioId = 0;
            ConsultasLocId = 0;
            ConsultasColId = 0;
            ConsultascatnivelatenId = 0;
        }
    }
}
