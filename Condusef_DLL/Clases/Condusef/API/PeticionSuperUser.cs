using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Clases.Condusef.API
{
    public class PeticionSuperUser
    {
        public string key {  get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string confirm_password { get; set; }
        public PeticionSuperUser(string _key, string _username, string _password, string _confirm_password) { 
            key = _key;
            username = _username;
            password = _password;
            confirm_password = _confirm_password;
        }
    }

    public class PeticionUsuario
    {
        public string username { get; set; }
        public string password { get; set; }
        public string confirm_password { get; set; }
        public PeticionUsuario(string _username, string _password, string _confirm_password)
        {
            username = _username;
            password = _password;
            confirm_password = _confirm_password;
        }
    }
}
