using Condusef.Models;
using Condusef_DLL.Clases.General;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using Condusef.Seguridad;
using Condusef_DLL.Funciones.Administracion;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Condusef_DLL.Funciones.Home;
using Microsoft.Extensions.Configuration;
using Condusef.Classes;
using System.Text.Json;
using Condusef_DLL.Clases.Condusef.Configuracion;

namespace Condusef.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public IConfiguration _configuration;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            Condusef_DLL.Clases.Conexion.Usuario = "Login condusef";
            return View();
        }

        [Route("/home", Name = "Home")]
        public IActionResult Home()
        {
            ViewData["Lang"] = Request.Cookies["lang"] ?? "es";
            return View();
        }
        
        public IActionResult SuccessOrRedirection()
        {
            // Realizar lógica específica para códigos de estado 2xx y 3xx, si es necesario
            // Puedes retornar una vista específica o realizar alguna otra lógica
            // Aquí no hay redirección explícita
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region Login

        [Route("/", Name = "Login")]
        public async Task<IActionResult> Login()
        {
            // Recuperar la cookie de autenticación personalizada
            var authResult = await HttpContext.AuthenticateAsync("UserScheme");

            // Verificar si la autenticación fue exitosa
            if (authResult.Succeeded)
            {
                // Recuperar la identidad del usuario autenticado
                //var userIdentity = authResult.Principal.Identity as ClaimsIdentity;

                //// Recuperar los claims de la identidad del usuario autenticado
                //var username = userIdentity.FindFirst(ClaimTypes.Name)?.Value;
                //var rol = userIdentity.FindFirst(ClaimTypes.Role)?.Value;
                //var id = userIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                //var empresa = userIdentity.FindFirst(ClaimTypes.GroupSid)?.Value;

                // Hacer algo con los claims recuperados
                return RedirectToAction("Condusef_Home", "Condusef");
            }


            ViewData["Lang"] = Request.Cookies["lang"] ?? "es";
            var conexion = _configuration.GetSection("Conexion").Get<Conexion>();
            Condusef_DLL.Clases.Conexion.CadenaConexion = conexion.DB;
            Condusef_DLL.Clases.Conexion.CadenaConexionSeguridad = conexion.Seguridad;
            Condusef_DLL.Clases.Conexion.Usuario = conexion.Usuario;
            Condusef_DLL.Clases.Conexion.Idioma = conexion.Idioma;Condusef_DLL.Clases.Conexion.RutaLog = Path.GetFullPath("Log");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Ingresar(string username, string password, bool remember)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            Home home = new Home();
            try
            {
                // Validar el usuario y la contraseña aquí
                
                resultado = home.IniciarSesion(username, password);
                if (resultado.Estatus)
                {
                    // Crear una lista de reclamaciones para el usuario autenticado
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, home.User.Usuario),
                        new Claim(ClaimTypes.Role, home.User.IdRol.ToString()),
                        new Claim(ClaimTypes.NameIdentifier, home.User.IdUsuario),
                        new Claim(ClaimTypes.GroupSid, home.User.IdEmpresa),
                        new Claim("Empresa", home.User.Empresa.ToString()),
                        new Claim("EmpresaCorto", home.User.NombreCortoEmpresa),
                        new Claim("Sector", home.User.Sector),
                        new Claim("IdSector", home.User.IdSector),
                    };

                    // Crear una identidad para el usuario autenticado
                    var userIdentity = new ClaimsIdentity(claims, "Login");

                    // Generar una cookie de autenticación personalizada
                    await HttpContext.SignInAsync("UserScheme", new ClaimsPrincipal(userIdentity), new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1), // Duración limitada de la cookie de autenticación
                    });

                    home.User = new ClsUsuario();
                    
                    resultado.Detalle = Url.Action("Condusef_Home", "Condusef");
                }
            }
            catch (Exception ex)
            {
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
                resultado.Code = 0;
            }
            var response = new
            {
                success = resultado.Estatus,
                message = resultado.Detalle,
                code = resultado.Code
            };
            return new JsonResult(response);
        }

        #endregion

        #region Login Administracion
        [AllowAnonymous]
        [Route("/login-admin", Name = "LoginAdmin")]
        public IActionResult LoginAdmin()
        {
            ViewData["Lang"] = "es";

            // Verificar si la cookie "AdminAuthCookie" existe y es válida
            //if (Request.Cookies.ContainsKey("AdminAuthCookie"))
            //{
            //    var adminAuthCookie = Request.Cookies["AdminAuthCookie"];
            //    // Realizar la validación de la cookie aquí, por ejemplo, verificar si es válida o contiene los datos correctos
            //    // Si la cookie es válida, redirigir a "/administracion"
            //    return RedirectToAction("Index", "Admin");
            //}

            var conexion = _configuration.GetSection("Conexion").Get<Conexion>();
            Condusef_DLL.Clases.Conexion.CadenaConexion = conexion.DB;
            Condusef_DLL.Clases.Conexion.CadenaConexionSeguridad = conexion.Seguridad;
            Condusef_DLL.Clases.Conexion.Usuario = conexion.Usuario;
            Condusef_DLL.Clases.Conexion.Idioma = conexion.Idioma;Condusef_DLL.Clases.Conexion.RutaLog = Path.GetFullPath("Log");
            return View();
        }

        [HttpPost]
        [Route("/admin-login", Name = "AdminLogin")]
        public async Task<IActionResult> AdminLogin(string username, string password)
        {
            // Validar el usuario y la contraseña aquí
            Administracion administracion = new Administracion();
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            try
            {
                resultado = administracion.AdminLogin(username, password);
                if (resultado.Estatus)
                {
                    // Crear una lista de reclamaciones para el usuario autenticado
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, administracion.Administrador.Username),
                        new Claim(ClaimTypes.Role, administracion.Administrador.Rol.ToString()),
                        new Claim(ClaimTypes.NameIdentifier, administracion.Administrador.Id.ToString()),
                        //new Claim("RutaAutorizada", "/administracion")
                    };
                    // Crear una identidad para el usuario autenticado
                    var userIdentity = new ClaimsIdentity(claims, "AdminScheme");
                    // Generar una cookie de autenticación personalizada
                    await HttpContext.SignInAsync("AdminScheme", new ClaimsPrincipal(userIdentity), new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30) // Duración limitada de la cookie de autenticación
                    });
                    administracion.Administrador = new Condusef_DLL.Clases.Admin.ClsAdministrador();
                    resultado.Detalle = Url.Action("Index", "Admin");
                }
            }
            catch (Exception ex)
            {
                resultado.Estatus = false;
            }
            var response = new
            {
                success = resultado.Estatus,
                message = resultado.Detalle,
                code = resultado.Code
            };
            return new JsonResult(response);

        }

        #endregion

        [AllowAnonymous]
        public JsonResult Cambiar_Idioma(string idioma)
        {
            bool estatus = false;
            try
            {
                Actualizar_Cookie_Idioma(idioma);
                estatus = true;
            }
            catch (Exception ex)
            {
                estatus = false;
            }
            var response = new
            {
                success = estatus
            };
            return new JsonResult(response);
        }

        #region Prerregistro

        [Route("/prerregistro", Name = "Prerregistro")]
        public IActionResult Prerregistro()
        {
            ViewData["Lang"] = Request.Cookies["lang"] ?? "es";
            ViewData["Lang"] = Request.Cookies["lang"] ?? "es";
            var conexion = _configuration.GetSection("Conexion").Get<Conexion>();
            Condusef_DLL.Clases.Conexion.CadenaConexion = conexion.DB;
            Condusef_DLL.Clases.Conexion.CadenaConexionSeguridad = conexion.Seguridad;
            Condusef_DLL.Clases.Conexion.Usuario = "Prerregistro";
            Condusef_DLL.Clases.Conexion.Username = "Prerregistro";
            var variables = _configuration.GetSection("VariablesGlobales").Get<VariablesGlobales>();
            Condusef_DLL.Clases.VariablesGlobales.Llave = variables.Llave;
            Condusef_DLL.Clases.VariablesGlobales.IV = variables.IV;
            Condusef_DLL.Clases.VariablesGlobales.Url = variables.Url;
            Condusef_DLL.Clases.VariablesGlobales.RutaPlantillasCorreo = Path.GetFullPath("Documentos/Plantillas/Correos");
            Condusef_DLL.Funciones.Condusef.Empresa empresa = new();
            empresa.CargaCatalogos();
            return View(empresa);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("/prerregistro/enviar", Name = "Enviar_Prerregistro")]
        public async Task<ActionResult> Enviar_Prerregistro(ClsDatosEmpresa datosEmpresa)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            Condusef_DLL.Funciones.Condusef.Empresa empresa = new();
            try
            {
                resultado = empresa.RegistrarNuevaEmpresa(datosEmpresa);
            }
            catch (Exception ex)
            {
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
                resultado.Code = 0;
            }
            var response = new
            {
                success = resultado.Estatus,
                message = resultado.Detalle,
                code = resultado.Code
            };
            return new JsonResult(response);
        }

        #endregion

        #region Forgot password
        [Route("/forgot-password", Name = "ForgotPassword")]
        public IActionResult ForgotPassword()
        {
            ViewData["Lang"] = Request.Cookies["lang"] ?? "es";
            ViewData["Lang"] = Request.Cookies["lang"] ?? "es";
            var conexion = _configuration.GetSection("Conexion").Get<Conexion>();
            Condusef_DLL.Clases.Conexion.CadenaConexion = conexion.DB;
            Condusef_DLL.Clases.Conexion.CadenaConexionSeguridad = conexion.Seguridad;
            Condusef_DLL.Clases.Conexion.Usuario = "ForgotPassword";
            Condusef_DLL.Clases.Conexion.Username = "ForgotPassword";
            var variables = _configuration.GetSection("VariablesGlobales").Get<VariablesGlobales>();
            Condusef_DLL.Clases.VariablesGlobales.Llave = variables.Llave;
            Condusef_DLL.Clases.VariablesGlobales.IV = variables.IV;
            Condusef_DLL.Clases.VariablesGlobales.Url = variables.Url;
            Condusef_DLL.Clases.VariablesGlobales.RutaPlantillasCorreo = Path.GetFullPath("Documentos/Plantillas/Correos");
            Condusef_DLL.Funciones.Condusef.Empresa empresa = new();
            empresa.CargaCatalogos();
            return View(empresa);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("/forgot-password/enviar-solicitud", Name = "Solicitar_Cambio_Password")]
        public ActionResult Solicitar_Cambio_Password(string username)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            Condusef_DLL.Funciones.Condusef.Empresa empresa = new();
            try
            {
                resultado = empresa.SolicitarCambioPassword(username);
            }
            catch (Exception ex)
            {
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
                resultado.Code = 0;
            }
            var response = new
            {
                success = resultado.Estatus,
                message = resultado.Detalle,
                code = resultado.Code
            };
            return new JsonResult(response);
        }

        [Route("/cambiar-password", Name = "CambiarPassword")]
        public IActionResult CambiarPassword()
        {
            ViewData["Lang"] = Request.Cookies["lang"] ?? "es";
            ViewData["Lang"] = Request.Cookies["lang"] ?? "es";
            var conexion = _configuration.GetSection("Conexion").Get<Conexion>();
            Condusef_DLL.Clases.Conexion.CadenaConexion = conexion.DB;
            Condusef_DLL.Clases.Conexion.CadenaConexionSeguridad = conexion.Seguridad;
            Condusef_DLL.Clases.Conexion.Usuario = "CambiarPassword";
            Condusef_DLL.Clases.Conexion.Username = "CambiarPassword";
            var variables = _configuration.GetSection("VariablesGlobales").Get<VariablesGlobales>();
            Condusef_DLL.Clases.VariablesGlobales.Llave = variables.Llave;
            Condusef_DLL.Clases.VariablesGlobales.IV = variables.IV;
            Condusef_DLL.Clases.VariablesGlobales.Url = variables.Url;
            Condusef_DLL.Clases.VariablesGlobales.RutaPlantillasCorreo = Path.GetFullPath("Documentos/Plantillas/Correos");
            Condusef_DLL.Funciones.Condusef.Empresa empresa = new();
            empresa.CargaCatalogos();
            return View(empresa);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("/cambiar-password/enviar-solicitud", Name = "Cambiar_Password_Usuario")]
        public ActionResult Cambiar_Password_Usuario(string otp, string password)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            Condusef_DLL.Funciones.Condusef.Empresa empresa = new();
            try
            {
                resultado = empresa.CambiarPasswordUsuario(otp, password);
            }
            catch (Exception ex)
            {
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
                resultado.Code = 0;
            }
            var response = new
            {
                success = resultado.Estatus,
                message = resultado.Detalle,
                code = resultado.Code
            };
            return new JsonResult(response);
        }

        #endregion


        #region Private functions
        private void Actualizar_Cookie_Idioma(string idIdioma)
		{
			// Crear una nueva cookie
			var cookieOptions = new CookieOptions
			{
				Expires = DateTime.Now.AddDays(7),
				IsEssential = true,
				SameSite = SameSiteMode.Strict,
				Secure = true
			};

			Response.Cookies.Append("lang", idIdioma, cookieOptions);
		}

        private bool EsCookieValida(string cookieValue)
        {
            // Decodificar la cadena base64
            var decodedCookieValue = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(cookieValue));

            // Deserializar el contenido de la cookie
            var claims = JsonSerializer.Deserialize<List<Claim>>(decodedCookieValue);

            // Realizar la validación de las claims
            var nombreClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var rolClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            var idClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var empresaClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.GroupSid)?.Value;

            // Validar las claims según tus requisitos específicos
            // Aquí puedes comparar los valores de las claims con los valores esperados
            // y realizar otras comprobaciones necesarias

            // Por ejemplo, si esperas un valor específico para el rol "Admin"
            bool valido = !string.IsNullOrWhiteSpace(nombreClaim) &&
               !string.IsNullOrWhiteSpace(rolClaim) &&
               !string.IsNullOrWhiteSpace(idClaim) &&
               !string.IsNullOrWhiteSpace(empresaClaim);

            return valido;

        }


        #endregion

    }
}