using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Clases.Condusef.API
{
    public class ClsReclamacionesGenerales
    {
        public string RecDenominacion {  get; set; }
        public string RecSector {  get; set; }
        public int RecTrimestre {  get; set; }
        public int RecNumero {  get; set; }
        public string RecFolioAtencion {  get; set; }
        public int RecEstadoConPend {  get; set; }
        public string RecFechaReclamacion {  get; set; }
        public string? RecFechaAtencion {  get; set; }
        public int RecMedioRecepcionCanal {  get; set; }
        public string RecProductoServicio {  get; set; }
        public string RecCausaMotivo {  get; set; }
        public string? RecFechaResolucion {  get; set; }
        public string? RecFechaNotifiUsuario {  get; set; }
        public int RecEntidadFederativa {  get; set; }
        public int? RecCodigoPostal {  get; set; }
        public int RecMunicipioAlcaldia {  get; set; }
        public int? RecLocalidad {  get; set; }
        public int? RecColonia {  get; set; }
        public string RecMonetario {  get; set; }
        public decimal? RecMontoReclamado { get; set; }
        public decimal? RecImporteAbonado { get; set; }
        public string RecFechaAbonoImporte { get; set; }
        public string RecPori { get; set; }
        public int RecTipoPersona { get; set; }
        public string? RecSexo { get; set; }
        public int? RecEdad { get; set; }
        public int? RecSentidoResolucion { get; set; }
        public int? RecNivelAtencion { get; set; }
        public string? RecFolioCondusef { get; set; }
        public int? RecReversa { get; set; }
        public string ColoniaText { get; set; }
        public string MunicipioText { get; set; }
        public DateTime FechaSubida { get; set; }
        public List<string> errors { get; set; }

        public ClsReclamacionesGenerales()
        {
            RecDenominacion = string.Empty;
            RecSector = string.Empty;
            RecTrimestre = 0;
            RecNumero = 1;
            RecFolioAtencion = string.Empty;
            RecEstadoConPend = 1;
            RecFechaReclamacion = string.Empty;
            RecFechaAtencion = string.Empty;
            RecMedioRecepcionCanal = 0;
            RecProductoServicio = string.Empty;
            RecCausaMotivo = string.Empty;
            RecFechaResolucion = string.Empty;
            RecFechaNotifiUsuario = string.Empty;
            RecEntidadFederativa = 0;
            RecCodigoPostal = 0;
            RecMunicipioAlcaldia = 0;
            RecLocalidad = 0;
            RecColonia = 0;
            RecMonetario = string.Empty;
            RecMontoReclamado = 0.0M;
            RecImporteAbonado = 0.0M;
            RecFechaAbonoImporte = string.Empty;
            RecPori = string.Empty;
            RecTipoPersona = 1;
            RecSexo = string.Empty;
            RecEdad = 18;
            RecSentidoResolucion = 0;
            RecNivelAtencion = 0;
            RecFolioCondusef = string.Empty;
            RecReversa = 0;
            ColoniaText = string.Empty;
            MunicipioText = string.Empty;
            errors = new List<string>();
            FechaSubida = new DateTime();
        }

    }

    public class BolsaErroresReclamaciones : ClsReclamacionesGenerales
    {
        public List<string> errors { get; set; }

        public BolsaErroresReclamaciones()
        {
            errors = new List<string>();
        }

        public BolsaErroresReclamaciones(ClsReclamacionesGenerales reclamacion, List<string> errores)
        {
            RecDenominacion = reclamacion.RecDenominacion;
            RecSector = reclamacion.RecSector;
            RecTrimestre = reclamacion.RecTrimestre;
            RecNumero = reclamacion.RecNumero;
            RecFolioAtencion = reclamacion.RecFolioAtencion;
            RecEstadoConPend = reclamacion.RecEstadoConPend;
            RecFechaReclamacion = reclamacion.RecFechaReclamacion;
            RecFechaAtencion = reclamacion.RecFechaAtencion;
            RecMedioRecepcionCanal = reclamacion.RecMedioRecepcionCanal;
            RecProductoServicio = reclamacion.RecProductoServicio;
            RecCausaMotivo = reclamacion.RecCausaMotivo;
            RecFechaResolucion = reclamacion.RecFechaResolucion;
            RecFechaNotifiUsuario = reclamacion.RecFechaNotifiUsuario;
            RecEntidadFederativa = reclamacion.RecEntidadFederativa;
            RecCodigoPostal = reclamacion.RecCodigoPostal;
            RecMunicipioAlcaldia = reclamacion.RecMunicipioAlcaldia;
            RecLocalidad = reclamacion.RecLocalidad;
            RecColonia = reclamacion.RecColonia;
            RecMonetario = reclamacion.RecMonetario;
            RecMontoReclamado = reclamacion.RecMontoReclamado;
            RecImporteAbonado = reclamacion.RecImporteAbonado;
            RecFechaAbonoImporte = reclamacion.RecFechaAbonoImporte;
            RecPori = reclamacion.RecPori;
            RecTipoPersona = reclamacion.RecTipoPersona;
            RecSexo = reclamacion.RecSexo;
            RecEdad = reclamacion.RecEdad;
            RecSentidoResolucion = reclamacion.RecSentidoResolucion;
            RecNivelAtencion = reclamacion.RecNivelAtencion;
            RecFolioCondusef = reclamacion.RecFolioCondusef;
            RecReversa = reclamacion.RecReversa;
            ColoniaText = reclamacion.ColoniaText;
            MunicipioText = reclamacion.MunicipioText;
            FechaSubida = reclamacion.FechaSubida;
            errors = errores;
        }
    }

    public class ClsReclamacionesLayout
    {
        public string RecDenominacion { get; set; }
        public string RecSector { get; set; }
        public int RecTrimestre { get; set; }
        public int RecNumero { get; set; }
        public string RecFolioAtencion { get; set; }
        public int RecEstadoConPend { get; set; }
        public string RecFechaReclamacion { get; set; }
        public string RecFechaAtencion { get; set; }
        public int RecMedioRecepcionCanal { get; set; }
        public string RecProductoServicio { get; set; }
        public string RecCausaMotivo { get; set; }
        public string? RecFechaResolucion { get; set; }
        public string? RecFechaNotifiUsuario { get; set; }
        public int RecEntidadFederativa { get; set; }
        public string RecCodigoPostal { get; set; }
        public int RecMunicipioAlcaldia { get; set; }
        public string RecLocalidad { get; set; }
        public string RecColonia { get; set; }
        public string RecMonetario { get; set; }
        public string RecMontoReclamado { get; set; }
        public string RecImporteAbonado { get; set; }
        public string RecFechaAbonoImporte { get; set; }
        public string RecPori { get; set; }
        public int RecTipoPersona { get; set; }
        public string? RecSexo { get; set; }
        public string RecEdad { get; set; }
        public string RecSentidoResolucion { get; set; }
        public string RecNivelAtencion { get; set; }
        public string? RecFolioCondusef { get; set; }
        public string RecReversa { get; set; }

        public ClsReclamacionesLayout()
        {
            RecDenominacion = string.Empty;
            RecSector = string.Empty;
            RecTrimestre = 0;
            RecNumero = 1;
            RecFolioAtencion = string.Empty;
            RecEstadoConPend = 1;
            RecFechaReclamacion = string.Empty;
            RecFechaAtencion = string.Empty;
            RecMedioRecepcionCanal = 0;
            RecProductoServicio = string.Empty;
            RecCausaMotivo = string.Empty;
            RecFechaResolucion = string.Empty;
            RecFechaNotifiUsuario = string.Empty;
            RecEntidadFederativa = 0;
            RecCodigoPostal = "";
            RecMunicipioAlcaldia = 0;
            RecLocalidad = "";
            RecColonia = "";
            RecMonetario = string.Empty;
            RecMontoReclamado = "";
            RecImporteAbonado = "";
            RecFechaAbonoImporte = string.Empty;
            RecPori = string.Empty;
            RecTipoPersona = 1;
            RecSexo = string.Empty;
            RecEdad = "";
            RecSentidoResolucion = "";
            RecNivelAtencion = "";
            RecFolioCondusef = string.Empty;
            RecReversa = "";
        }

    }
}
