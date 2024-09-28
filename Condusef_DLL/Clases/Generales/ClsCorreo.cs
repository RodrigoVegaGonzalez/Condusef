using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Clases.Generales
{
    public class ClsCorreo
    {
        public string correo { get; set; }
        public string usuario { get; set; }
        public string Pass { get; set; }
        public List<string> CorreoDestinos { get; set; }
        public List<string> conCopia { get; set; }
        public List<string> copiaOculta { get; set; }
        public int Puerto { get; set; }
        public string Host { get; set; }
        public string Authentificacion { get; set; }
        public string Mensaje { get; set; }
        public string Asunto { get; set; }
        public string RutaDocumento { get; set; }
        public string Resultado { get; set; }
        public bool Autenthicate { get; set; }
        public string correoMostrar { get; set; }
        public string nombreMostrar { get; set; }

        public ClsCorreo()
        {
            correo = "";
            usuario = "";
            Pass = "";
            CorreoDestinos = new List<string>();
            conCopia = new List<string>();
            copiaOculta = new List<string>();
            Puerto = 0;
            Host = "";
            Authentificacion = "";
            Mensaje = "";
            Asunto = "";
            RutaDocumento = "";
            Resultado = "";
            Autenthicate = true;
            correoMostrar = "";
            nombreMostrar = "";
        }
    }
}
