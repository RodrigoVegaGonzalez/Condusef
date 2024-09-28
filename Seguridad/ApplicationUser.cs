//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Identity;
//using System.Security.Claims;

//namespace Condusef.Seguridad
//{
//    public class ApplicationUser : IdentityUser
//    {
//        private readonly UserManager<ApplicationUser> _userManager;

//        public ApplicationUser(UserManager<ApplicationUser> userManager)
//        {
//            _userManager = userManager;
//        }
//        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
//        {
//            // Tenga en cuenta que el valor de authenticationType debe coincidir con el definido en CookieAuthenticationOptions.AuthenticationType
//            var user = await _userManager.FindByIdAsync(Username);
//            var claims = await _userManager.GetClaimsAsync(user);

//            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
//            claimsIdentity.AddClaim(new Claim("UsuarioID", Id));
//            claimsIdentity.AddClaim(new Claim("Username", Username));
//            claimsIdentity.AddClaim(new Claim("Empresa", this.Empresa));
//            claimsIdentity.AddClaim(new Claim("Rol", Rol.ToString()));
//            claimsIdentity.AddClaim(new Claim("Name", Name));
//            //claimsIdentity.AddClaim(new Claim("Email", Email));
//            // Agregar aquí notificaciones personalizadas de usuario
//            return claimsIdentity;
//        }
//    }
//}
