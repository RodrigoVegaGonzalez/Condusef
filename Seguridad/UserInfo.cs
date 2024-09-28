using Microsoft.AspNetCore.Identity;

namespace Condusef.Seguridad
{
    public class UserInfo
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Empresa { get; set; }
        public int Rol { get; set; }
        public string Password { get; set; }

    }
}
