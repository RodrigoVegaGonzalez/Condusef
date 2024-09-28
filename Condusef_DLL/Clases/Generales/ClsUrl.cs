using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Clases.Generales
{
    public class ClsUrl
    {
        public string Redeco_CrearSuperUsuario { get; set; }
        public string Redeco_CrearUsuario { get; set; }
        public string Redeco_RenovarToken { get; set; }
        public string Redeco_ListaUsuarios { get; set; }
        public string Redeco_StatusUsuario { get; set; }
        public string Redeco_EnvioQuejas { get; set; }
        public string Redeco_EliminarQuejas { get; set; }
        public string Redeco_ConsultaQuejas { get; set; }

        public string Reune_CrearSuperUsuario { get; set; }
        public string Reune_CrearUsuario { get; set; }
        public string Reune_RenovarToken { get; set; }
        public string Reune_ListaUsuarios { get; set; }
        public string Reune_StatusUsuario { get; set; }
        public string Reune_EnvioConsultasGenerales { get; set; }
        public string Reune_EnvioConsultasSeguros { get; set; }
        public string Reune_EnvioConsultasSic { get; set; }

        public string Reune_EnvioReclamacionesGenerales { get; set; }

        public string Reune_EnvioAclaracionesGenerales { get; set; }


        public string ConsultaMedios { get; set; }
        public string ConsultaNivelesAtencion { get; set; }
        public string ConsultaListaProductos { get; set; }
        public string ConsultaListaCausas { get; set; }
        public string ConsultaEstados { get; set; }
        public string ConsultaCP { get; set; }
        public string ConsultaMunicipios { get; set; }
        public string ConsultaColonias { get; set; }

        public ClsUrl()
        {
            Redeco_CrearSuperUsuario = string.Empty;
            Redeco_CrearUsuario = string.Empty;
            Redeco_RenovarToken = string.Empty;
            Redeco_ListaUsuarios = string.Empty;
            Redeco_StatusUsuario = string.Empty;
            Redeco_EnvioQuejas = string.Empty;
            Redeco_EliminarQuejas = string.Empty;
            Redeco_ConsultaQuejas = string.Empty;

            Reune_CrearSuperUsuario = string.Empty;
            Reune_CrearUsuario = string.Empty;
            Reune_RenovarToken = string.Empty;
            Reune_ListaUsuarios = string.Empty;
            Reune_StatusUsuario = string.Empty;
            Reune_EnvioConsultasGenerales = string.Empty;
            Reune_EnvioConsultasSeguros = string.Empty;
            Reune_EnvioConsultasSic = string.Empty;
            Reune_EnvioReclamacionesGenerales = string.Empty;
            Reune_EnvioAclaracionesGenerales = string.Empty;

            ConsultaMedios = string.Empty;
            ConsultaNivelesAtencion = string.Empty;
            ConsultaListaProductos = string.Empty;
            ConsultaListaCausas = string.Empty;
            ConsultaEstados = string.Empty;
            ConsultaCP = string.Empty;
            ConsultaMunicipios = string.Empty;
            ConsultaColonias = string.Empty;
        }
    }
}
