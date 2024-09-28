using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Clases.Admin
{
    public class ClsAdministrador
    {
        public int Id { get; set; }
        public string Username {  get; set; }
        public int Rol { get; set; }
        public ClsAdministrador() { 
            Id = 0;
            Username = string.Empty;
            Rol = 0;
        }
    }
}
