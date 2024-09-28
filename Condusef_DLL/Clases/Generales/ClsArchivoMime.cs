using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Clases.Generales
{
    public class ClsArchivoMime
    {
        public byte[] Archivo { get; set; }
        public string ArchivoB64 { get; set; }
        public string MimeArchivo { get; set; }
        public string NombreArchivo { get; set; }
        public string Extension { get; set; }

        public ClsArchivoMime()
        {
            Archivo = new byte[] { };
            ArchivoB64 = string.Empty;
            MimeArchivo = string.Empty;
            NombreArchivo = string.Empty;
            Extension = string.Empty;
        }
    }
}
