using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Clases.Condusef.API
{
    public class ClsConsultasSeguros
    {
        public string InstitucionClave { get; set; }
        public string Sector { get; set; }
        public int Ramo { get; set; }
        public int ConsultasTrim { get; set; }
        public int NumConsultas { get; set; }
        public string ConsultasFolio { get; set; }
        public int ConsultasEstatusCon { get; set; }
        public string ConsultasFecAten { get; set; }
        public int QuejasEstados { get; set; }
        public string ConsultasFecRecepcion { get; set; }
        public int MediosId { get; set; }
        public string Producto { get; set; }
        public string CausaId { get; set; }
        public int ConsultasCP { get; set; }
        public int ConsultasMpioId { get; set; }
        public int ConsultasLocId { get; set; }
        public int ConsultasColId { get; set; }
        public int ConsultascatnivelatenId { get; set; }
        public string ConsultasPori { get; set; }
        public string FolioCONDUSEF { get; set; }

        public ClsConsultasSeguros()
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
            Producto = string.Empty;
            CausaId = string.Empty;
            ConsultasCP = 0;
            ConsultasMpioId = 0;
            ConsultasLocId = 0;
            ConsultasColId = 0;
            ConsultascatnivelatenId = 0;
            ConsultasPori = string.Empty;
            Ramo = 0;
            FolioCONDUSEF = string.Empty;
        }
    }
}
