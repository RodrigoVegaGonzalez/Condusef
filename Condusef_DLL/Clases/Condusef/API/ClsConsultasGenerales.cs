using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Clases.Condusef.API
{
    public class ClsConsultasGenerales
    {
        public string InstitucionClave { get; set; }
        public string Sector { get; set; }
        public int ConsultasTrim { get; set; }
        public int NumConsultas { get; set; }
        public string ConsultasFolio { get; set; }
        public int ConsultasEstatusCon { get; set; }
        public string ConsultasFecAten { get; set; }
        public int EstadosId { get; set; }
        public string ConsultasFecRecepcion { get; set; }
        public int MediosId { get; set; }
        public string Producto { get; set; }
        public string CausaId { get; set; }
        public int? ConsultasCP { get; set; }
        public int ConsultasMpioId { get; set; }
        public int? ConsultasLocId { get; set; }
        public int? ConsultasColId { get; set; }
        public string ColoniaText { get; set; }
        public string MunicipioText { get; set; }
        public int? ConsultascatnivelatenId { get; set; }
        public string ConsultasPori { get; set; }
        public DateTime FechaSubida { get; set; }
        public List<string> errors { get; set; }
        public ClsConsultasGenerales()
        {
            InstitucionClave = string.Empty;
            Sector = string.Empty;
            ConsultasTrim = 0;
            NumConsultas = 1;
            ConsultasFolio = string.Empty;
            ConsultasEstatusCon = 0;
            ConsultasFecAten = string.Empty;
            EstadosId = 0;
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
            ColoniaText = string.Empty;
            MunicipioText = string.Empty;
            FechaSubida = new DateTime();
            errors = new List<string>();
        }
    }

    public class BolsaErroresConsultas : ClsConsultasGenerales
    {
        public List<string> errors { get; set; }

        public BolsaErroresConsultas()
        {
            errors = new List<string>();
        }

        public BolsaErroresConsultas(ClsConsultasGenerales consulta, List<string> errores)
        {
            InstitucionClave = consulta.InstitucionClave;
            Sector = consulta.Sector;
            ConsultasTrim = consulta.ConsultasTrim;
            NumConsultas = consulta.NumConsultas;
            ConsultasFolio = consulta.ConsultasFolio;
            ConsultasEstatusCon = consulta.ConsultasEstatusCon;
            ConsultasFecAten = consulta.ConsultasFecAten;
            EstadosId = consulta.EstadosId;
            ConsultasFecRecepcion = consulta.ConsultasFecRecepcion;
            MediosId = consulta.MediosId;
            Producto = consulta.Producto;
            CausaId = consulta.CausaId;
            ConsultasCP = consulta.ConsultasCP;
            ConsultasMpioId = consulta.ConsultasMpioId;
            ConsultasLocId = consulta.ConsultasLocId;
            ConsultasColId = consulta.ConsultasColId;
            ConsultascatnivelatenId = consulta.ConsultascatnivelatenId;
            ConsultasPori = consulta.ConsultasPori;
            ColoniaText = consulta.ColoniaText;
            MunicipioText = consulta.MunicipioText;
            FechaSubida = consulta.FechaSubida;
            errors = errores;
        }
    }

    public class ClsConsultasLayout
    {
        public string InstitucionClave { get; set; }
        public string Sector { get; set; }
        public int ConsultasTrim { get; set; }
        public int NumConsultas { get; set; }
        public string ConsultasFolio { get; set; }
        public int ConsultasEstatusCon { get; set; }
        public string ConsultasFecAten { get; set; }
        public int EstadosId { get; set; }
        public string ConsultasFecRecepcion { get; set; }
        public int MediosId { get; set; }
        public string Producto { get; set; }
        public string CausaId { get; set; }
        public string ConsultasCP { get; set; }
        public int ConsultasMpioId { get; set; }
        public string ConsultasLocId { get; set; }
        public string ConsultasColId { get; set; }
        public string ConsultascatnivelatenId { get; set; }
        public string ConsultasPori { get; set; }
        public ClsConsultasLayout()
        {
            InstitucionClave = string.Empty;
            Sector = string.Empty;
            ConsultasTrim = 0;
            NumConsultas = 1;
            ConsultasFolio = string.Empty;
            ConsultasEstatusCon = 0;
            ConsultasFecAten = string.Empty;
            EstadosId = 0;
            ConsultasFecRecepcion = string.Empty;
            MediosId = 0;
            Producto = string.Empty;
            CausaId = string.Empty;
            ConsultasCP = "";
            ConsultasMpioId = 0;
            ConsultasLocId = "";
            ConsultasColId = "";
            ConsultascatnivelatenId = "";
            ConsultasPori = string.Empty;
        }
    }
}
