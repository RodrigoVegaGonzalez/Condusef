using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Clases.Generales
{
    public class ClsStreamFile
    {

        public string FileName { get; set; }

        public string Path { get; set; }

        public Stream File { get; set; }

        public ClsStreamFile()
        {
            FileName = string.Empty;
            Path = string.Empty;
            File = null;
        }

        public ClsStreamFile(Stream file, string path, string fileName) { 
            FileName = fileName;
            File = file;
            Path = path;
        }
    }
}
