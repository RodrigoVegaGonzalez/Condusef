using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Clases.Condusef.API
{
    public class ClsAclaracionesGenerales
    {
        public string AclaracionDenominacion { get; set; }
        public string AclaracionSector { get; set; }
        public int AclaracionTrimestre { get; set; }
        public int AclaracionNumero { get; set; }
        public string AclaracionFolioAtencion { get; set; }
        public int AclaracionEstadoConPend { get; set; }
        public string AclaracionFechaAclaracion { get; set; }
        public string? AclaracionFechaAtencion { get; set; }
        public int AclaracionMedioRecepcionCanal { get; set; }
        public string AclaracionProductoServicio { get; set; }
        public string AclaracionCausaMotivo { get; set; }
        public string? AclaracionFechaResolucion { get; set; }
        public string? AclaracionFechaNotifiUsuario { get; set; }
        public int AclaracionEntidadFederativa { get; set; }
        public int AclaracionCodigoPostal { get; set; }
        public int AclaracionMunicipioAlcaldia { get; set; }
        public int? AclaracionLocalidad { get; set; }
        public int? AclaracionColonia { get; set; }
        public string AclaracionMonetario { get; set; }
        public decimal? AclaracionMontoReclamado { get; set; }
        public string AclaracionPori { get; set; }
        public int AclaracionTipoPersona { get; set; }
        public string AclaracionSexo { get; set; }
        public int? AclaracionEdad { get; set; }
        public int? AclaracionNivelAtencion { get; set; }
        public string? AclaracionFolioCondusef { get; set; }
        public int? AclaracionReversa { get; set; }
        public string AclaracionOperacionExtranjero { get; set; }
        public string ColoniaText { get; set; }
        public string MunicipioText { get; set; }
        public DateTime FechaSubida { get; set; }
        public List<string> errors { get; set; }
        public ClsAclaracionesGenerales() {
            AclaracionDenominacion =  string.Empty;
            AclaracionSector = string.Empty;
            AclaracionTrimestre =  0;
            AclaracionNumero =  1;
            AclaracionFolioAtencion = string.Empty;
            AclaracionEstadoConPend =  1;
            AclaracionFechaAclaracion = string.Empty;
            AclaracionFechaAtencion = string.Empty;
            AclaracionMedioRecepcionCanal =  0;
            AclaracionProductoServicio = string.Empty;
            AclaracionCausaMotivo = string.Empty;
            AclaracionFechaResolucion = string.Empty;
            AclaracionFechaNotifiUsuario = string.Empty;
            AclaracionEntidadFederativa =  0;
            AclaracionCodigoPostal = 0;
            AclaracionMunicipioAlcaldia =  0;
            AclaracionLocalidad =  0;
            AclaracionColonia =  0;
            AclaracionMonetario = string.Empty;
            AclaracionMontoReclamado =  0;
            AclaracionPori = string.Empty;
            AclaracionTipoPersona =  1;
            AclaracionSexo = string.Empty;
            AclaracionEdad =  18;
            AclaracionNivelAtencion =  0;
            AclaracionFolioCondusef = string.Empty;
            AclaracionReversa = 0;
            AclaracionOperacionExtranjero = string.Empty;
            ColoniaText = string.Empty;
            MunicipioText = string.Empty;
            FechaSubida = new DateTime();
            errors = new List<string>();
        }
    }

    public class BolsaErroresAclaraciones : ClsAclaracionesGenerales
    {
        public List<string> errors { get; set; }

        public BolsaErroresAclaraciones()
        {
            errors = new List<string>();
        }

        public BolsaErroresAclaraciones(ClsAclaracionesGenerales aclaracion, List<string> errores)
        {
            AclaracionDenominacion = aclaracion.AclaracionDenominacion;
            AclaracionSector = aclaracion.AclaracionSector;
            AclaracionTrimestre = aclaracion.AclaracionTrimestre;
            AclaracionNumero = aclaracion.AclaracionNumero;
            AclaracionFolioAtencion = aclaracion.AclaracionFolioAtencion;
            AclaracionEstadoConPend = aclaracion.AclaracionEstadoConPend;
            AclaracionFechaAclaracion = aclaracion.AclaracionFechaAclaracion;
            AclaracionFechaAtencion = aclaracion.AclaracionFechaAtencion;
            AclaracionMedioRecepcionCanal = aclaracion.AclaracionMedioRecepcionCanal;
            AclaracionProductoServicio = aclaracion.AclaracionProductoServicio;
            AclaracionCausaMotivo = aclaracion.AclaracionCausaMotivo;
            AclaracionFechaResolucion = aclaracion.AclaracionFechaResolucion;
            AclaracionFechaNotifiUsuario = aclaracion.AclaracionFechaNotifiUsuario;
            AclaracionEntidadFederativa = aclaracion.AclaracionEntidadFederativa;
            AclaracionCodigoPostal = aclaracion.AclaracionCodigoPostal;
            AclaracionMunicipioAlcaldia = aclaracion.AclaracionMunicipioAlcaldia;
            AclaracionLocalidad = aclaracion.AclaracionLocalidad;
            AclaracionColonia = aclaracion.AclaracionColonia;
            AclaracionMonetario = aclaracion.AclaracionMonetario;
            AclaracionMontoReclamado = aclaracion.AclaracionMontoReclamado;
            AclaracionPori = aclaracion.AclaracionPori;
            AclaracionTipoPersona = aclaracion.AclaracionTipoPersona;
            AclaracionSexo = aclaracion.AclaracionSexo;
            AclaracionEdad = aclaracion.AclaracionEdad;
            AclaracionNivelAtencion = aclaracion.AclaracionNivelAtencion;
            AclaracionFolioCondusef = aclaracion.AclaracionFolioCondusef;
            AclaracionReversa = aclaracion.AclaracionReversa;
            AclaracionOperacionExtranjero = aclaracion.AclaracionOperacionExtranjero;
            ColoniaText = aclaracion.ColoniaText;
            MunicipioText = aclaracion.MunicipioText;
            FechaSubida = aclaracion.FechaSubida;
            errors = errores;
        }
    }

    public class ClsAclaracionesLayout
    {
        public string AclaracionDenominacion { get; set; }
        public string AclaracionSector { get; set; }
        public int AclaracionTrimestre { get; set; }
        public int AclaracionNumero { get; set; }
        public string AclaracionFolioAtencion { get; set; }
        public int AclaracionEstadoConPend { get; set; }
        public string AclaracionFechaAclaracion { get; set; }
        public string? AclaracionFechaAtencion { get; set; }
        public int AclaracionMedioRecepcionCanal { get; set; }
        public string AclaracionProductoServicio { get; set; }
        public string AclaracionCausaMotivo { get; set; }
        public string? AclaracionFechaResolucion { get; set; }
        public string? AclaracionFechaNotifiUsuario { get; set; }
        public int AclaracionEntidadFederativa { get; set; }
        public string AclaracionCodigoPostal { get; set; }
        public int AclaracionMunicipioAlcaldia { get; set; }
        public string AclaracionLocalidad { get; set; }
        public string AclaracionColonia { get; set; }
        public string AclaracionMonetario { get; set; }
        public string AclaracionMontoReclamado { get; set; }
        public string AclaracionPori { get; set; }
        public int AclaracionTipoPersona { get; set; }
        public string AclaracionSexo { get; set; }
        public string AclaracionEdad { get; set; }
        public string AclaracionNivelAtencion { get; set; }
        public string? AclaracionFolioCondusef { get; set; }
        public string AclaracionReversa { get; set; }
        public string AclaracionOperacionExtranjero { get; set; }

        public ClsAclaracionesLayout()
        {
            AclaracionDenominacion = string.Empty;
            AclaracionSector = string.Empty;
            AclaracionTrimestre = 0;
            AclaracionNumero = 1;
            AclaracionFolioAtencion = string.Empty;
            AclaracionEstadoConPend = 1;
            AclaracionFechaAclaracion = string.Empty;
            AclaracionFechaAtencion = string.Empty;
            AclaracionMedioRecepcionCanal = 0;
            AclaracionProductoServicio = string.Empty;
            AclaracionCausaMotivo = string.Empty;
            AclaracionFechaResolucion = string.Empty;
            AclaracionFechaNotifiUsuario = string.Empty;
            AclaracionEntidadFederativa = 0;
            AclaracionCodigoPostal = "";
            AclaracionMunicipioAlcaldia = 0;
            AclaracionLocalidad = "";
            AclaracionColonia = "";
            AclaracionMonetario = string.Empty;
            AclaracionMontoReclamado = "";
            AclaracionPori = string.Empty;
            AclaracionTipoPersona = 1;
            AclaracionSexo = string.Empty;
            AclaracionEdad = "";
            AclaracionNivelAtencion = "";
            AclaracionFolioCondusef = string.Empty;
            AclaracionReversa = "";
            AclaracionOperacionExtranjero = string.Empty;
        }
    }
}
