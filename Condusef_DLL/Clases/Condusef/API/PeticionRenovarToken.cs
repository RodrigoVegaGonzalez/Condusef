using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Clases.Condusef.API
{
    public class PeticionRenovarToken
    {
        public string username {  get; set; }
        public string password { get; set; }
        public PeticionRenovarToken(string _username, string _password)
        {
            username = _username;
            password = _password;
        }
    }
}
