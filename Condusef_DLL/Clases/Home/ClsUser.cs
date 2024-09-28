using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Clases.Home
{
    public class ClsUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public int Empresa { get; set; }
        public int Rol { get; set; }

        public ClsUser() { 
            Id = 0;
            Username = string.Empty;
            Name = string.Empty;
            Empresa = 0;
            Rol = 0;
        }
    }
}
