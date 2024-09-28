using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Clases.Condusef.API
{
    public class ClsSuperUser
    {
        public string userid { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string institucionid { get; set; }
        public bool is_active { get; set; }
        public string profileid { get; set; }
        public string date { get; set; }
        public string token_access { get; set; }
        public ClsSuperUser() { 
            userid = string.Empty;
            username = string.Empty;
            password = string.Empty;
            institucionid = string.Empty;
            profileid = string.Empty;
            token_access = string.Empty;
            date = string.Empty;
            is_active = false;
        }
    }
}
