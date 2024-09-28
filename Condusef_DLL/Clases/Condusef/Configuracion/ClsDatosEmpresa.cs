using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Clases.Condusef.Configuracion
{
    public class ClsDatosEmpresa
    {
        public string RFC { get; set; }
        public string Nombre { get; set; }
        public string NomCorto { get; set; }
        public string Sector { get; set; }
        public string DirCalle { get; set; }
        public string DirColonia { get; set; }
        public string DirNumInt { get; set; }
        public string DirNumExt { get; set; }
        public string DirCP { get; set; }
        public string DirMunicipio { get; set; }
        public string DirCiudad { get; set; }
        public string DirEstado { get; set; }
        public bool Estatus {  get; set; }
        public bool Registrado { get; set; }
        public string Plan {  get; set; }
        public byte[] Logo { get; set; }

        public string CorreoContacto { get; set; }
        public string TelefonoContacto { get; set; }
        public string PersonaContacto { get; set; }
        public int idTipoEmpresa { get; set; }

        public string TokenRedeco { get; set; }
        public string TokenReune { get; set; }

        public int Servicios { get; set; }

        public ClsDatosEmpresa()
        {
            RFC = string.Empty;
            Nombre = string.Empty;
            NomCorto = string.Empty;
            Plan = string.Empty;
            Sector = string.Empty;

            DirCalle = string.Empty;
            DirColonia = string.Empty;
            DirNumInt = string.Empty;
            DirNumExt = string.Empty;
            DirCP = string.Empty;
            DirMunicipio = string.Empty;
            DirCiudad = string.Empty;
            DirEstado = string.Empty;

            Logo = new byte[] { };

            CorreoContacto = string.Empty;
            TelefonoContacto = string.Empty;
            PersonaContacto = string.Empty;

            idTipoEmpresa = 0;
            TokenRedeco = string.Empty;
            TokenReune = string.Empty;
            Servicios = 0;
        }
    }
}
