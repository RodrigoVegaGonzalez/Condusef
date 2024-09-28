using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Clases.Condusef.Reune
{
    public class ClsReuneFiles
    {
        public int id {  get; set; }
        public string file_id {  get; set; }
        public string status { get; set; }
        public string file {  get; set; }
        public string date_added { get; set; }
        public string type { get; set; }
        public bool file_with_error { get; set; }
        public ClsReuneFiles()
        {
            id = 0;
            file_id = string.Empty;
            status = string.Empty;
            file = string.Empty;
            date_added = string.Empty;
            type = string.Empty;
            file_with_error = false;
        }
    }
}
