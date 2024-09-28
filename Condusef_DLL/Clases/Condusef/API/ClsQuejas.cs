using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Clases.Condusef.API
{
    public class ClsQuejas
    {
        public string QuejasDenominacion { get; set; }
        public string QuejasSector { get; set; }
        public int QuejasNoMes { get; set; }
        public int QuejasNum {  get; set; }
        public string QuejasFolio { get; set; }
        public string QuejasFecRecepcion { get; set; }
        public int QuejasMedio { get; set; }
        public int QuejasNivelAT { get; set; }
        public string QuejasProducto { get; set; }
        public string QuejasCausa { get; set; }
        public string QuejasPORI { get; set; }
        public int QuejasEstatus { get; set; }
        public int QuejasEstados { get; set; }
        public int QuejasMunId { get; set; }
        public int? QuejasLocId { get; set; }
        public int QuejasColId { get; set; }
        public int QuejasCP { get; set; }
        public int QuejasTipoPersona { get; set; }
        public string? QuejasSexo { get; set; }
        public int? QuejasEdad {  get; set; }
        public string? QuejasFecResolucion { get; set; }
        public string? QuejasFecNotificacion { get; set; }
        public int? QuejasRespuesta { get; set; }
        public int? QuejasNumPenal {  get; set; }
        public int? QuejasPenalizacion { get; set; }
        public string CausaText { get; set; }
        public string ColoniaText { get; set; }
        public string MunicipioText { get; set; }
        public DateTime FechaSubida { get; set; }
        //public string CausaId {  get; set; }
        public ClsQuejas()
        {
            QuejasDenominacion = string.Empty;
            QuejasSector = string.Empty;
            QuejasNoMes = 0;
            QuejasNum = 1;
            QuejasFolio = string.Empty;
            QuejasFecRecepcion = string.Empty;
            QuejasMedio = 0;
            QuejasNivelAT = 0;
            QuejasProducto = string.Empty;
            QuejasCausa = string.Empty;
            QuejasPORI = string.Empty;
            QuejasEstatus = 0;
            QuejasEstados = 0;
            QuejasMunId = 0;
            QuejasLocId = 0;
            QuejasColId = 0;
            QuejasCP = 0;
            QuejasTipoPersona = 0;
            QuejasSexo = string.Empty;
            QuejasEdad = 18;
            QuejasFecResolucion = string.Empty;
            QuejasFecNotificacion = string.Empty;
            QuejasRespuesta = 0;
            QuejasPenalizacion = 0;
            QuejasNumPenal = 0;
            ColoniaText = string.Empty;
            MunicipioText = string.Empty;
            CausaText = string.Empty;
            FechaSubida = new DateTime();
            //CausaId = string.Empty;
        }
    }

    public class BolsaErroresQuejas : ClsQuejas
    {
        public List<string> errors { get; set; }

        public BolsaErroresQuejas()
        {
            errors = new List<string>();
        }

        public BolsaErroresQuejas(ClsQuejas queja, List<string> errores)
        {
            QuejasDenominacion = queja.QuejasDenominacion;
            QuejasSector = queja.QuejasSector;
            QuejasNoMes = queja.QuejasNoMes;
            QuejasNum = queja.QuejasNum;
            QuejasFolio = queja.QuejasFolio;
            QuejasFecRecepcion = queja.QuejasFecRecepcion;
            QuejasMedio = queja.QuejasMedio;
            QuejasNivelAT = queja.QuejasNivelAT;
            QuejasProducto = queja.QuejasProducto;
            QuejasCausa = queja.QuejasCausa;
            QuejasPORI = queja.QuejasPORI;
            QuejasEstatus = queja.QuejasEstatus;
            QuejasEstados = queja.QuejasEstados;
            QuejasMunId = queja.QuejasMunId;
            QuejasLocId = queja.QuejasLocId;
            QuejasColId = queja.QuejasColId;
            QuejasCP = queja.QuejasCP;
            QuejasTipoPersona = queja.QuejasTipoPersona;
            QuejasSexo = queja.QuejasSexo;
            QuejasEdad = queja.QuejasEdad;
            QuejasFecResolucion = queja.QuejasFecResolucion;
            QuejasFecNotificacion = queja.QuejasFecNotificacion;
            QuejasRespuesta = queja.QuejasRespuesta;
            QuejasNumPenal = queja.QuejasNumPenal;
            QuejasPenalizacion = queja.QuejasPenalizacion;
            CausaText = queja.CausaText;
            ColoniaText = queja.ColoniaText;
            MunicipioText = queja.MunicipioText;
            FechaSubida = queja.FechaSubida;
            errors = errores;
        }
    }

    public class ClsQuejasLayout
    {
        public string QuejasDenominacion { get; set; }
        public string QuejasSector { get; set; }
        public int QuejasNoMes { get; set; }
        public int QuejasNum { get; set; }
        public string QuejasFolio { get; set; }
        public string QuejasFecRecepcion { get; set; }
        public int QuejasMedio { get; set; }
        public int QuejasNivelAT { get; set; }
        public string QuejasProducto { get; set; }
        public string QuejasCausa { get; set; }
        public string QuejasPORI { get; set; }
        public int QuejasEstatus { get; set; }
        public int QuejasEstados { get; set; }
        public int QuejasMunId { get; set; }
        public int? QuejasLocId { get; set; }
        public int QuejasColId { get; set; }
        public string QuejasCP { get; set; }
        public int QuejasTipoPersona { get; set; }
        public string QuejasSexo { get; set; }
        public string QuejasEdad { get; set; }
        public string QuejasFecResolucion { get; set; }
        public string QuejasFecNotificacion { get; set; }
        public string QuejasRespuesta { get; set; }
        public string QuejasNumPenal { get; set; }
        public string QuejasPenalizacion { get; set; }
        public ClsQuejasLayout()
        {
            QuejasDenominacion = string.Empty;
            QuejasSector = string.Empty;
            QuejasNoMes = 0;
            QuejasNum = 1;
            QuejasFolio = string.Empty;
            QuejasFecRecepcion = string.Empty;
            QuejasMedio = 0;
            QuejasNivelAT = 0;
            QuejasProducto = string.Empty;
            QuejasCausa = string.Empty;
            QuejasPORI = string.Empty;
            QuejasEstatus = 0;
            QuejasEstados = 0;
            QuejasMunId = 0;
            QuejasLocId = 0;
            QuejasColId = 0;
            QuejasCP = "";
            QuejasTipoPersona = 0;
            QuejasSexo = string.Empty;
            QuejasEdad = string.Empty;
            QuejasFecResolucion = string.Empty;
            QuejasFecNotificacion = string.Empty;
            QuejasRespuesta = string.Empty;
            QuejasPenalizacion = string.Empty;
            QuejasNumPenal = string.Empty;
        }
    }
}
