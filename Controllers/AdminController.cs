using Condusef.Classes;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Condusef_DLL.Funciones.Administracion;
using Condusef_DLL.Clases.General;
using Microsoft.AspNetCore.Authorization;
using System.Runtime.InteropServices;
using Condusef_DLL.Funciones.Home;
using Condusef_DLL.Funciones.Generales;
using Condusef_DLL.Funciones.Condusef;
using Condusef_DLL.Clases.Generales;

namespace Condusef.Controllers
{
    [Authorize(AuthenticationSchemes = "AdminScheme")]
    public class AdminController : Controller
    {
        public IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public AdminController(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        //[Authorize(AuthenticationSchemes = "AdminScheme", Roles = "10,20")]
        [Route("administracion/", Name = "Index")]
        public IActionResult Index()
        {
            var conexion = _configuration.GetSection("Conexion").Get<Conexion>();
            var variables = _configuration.GetSection("VariablesGlobales").Get<VariablesGlobales>();
            Condusef_DLL.Clases.VariablesGlobales.Url = variables.Url;
            Condusef_DLL.Clases.VariablesGlobales.Llave = variables.Llave;
            Condusef_DLL.Clases.VariablesGlobales.IV = variables.IV;
            Condusef_DLL.Clases.Conexion.CadenaConexion = conexion.DB;

            Condusef_DLL.Clases.Conexion.CadenaConexionSeguridad = conexion.Seguridad;
            Condusef_DLL.Clases.Conexion.Usuario = "Administrador";
            Condusef_DLL.Clases.Conexion.Empresa = conexion.Empresa;
            Condusef_DLL.Clases.Conexion.Idioma = conexion.Idioma;Condusef_DLL.Clases.Conexion.RutaLog = Path.GetFullPath("Log");

            Empresa empresa = new Empresa();
            empresa.CargaCatalogos();
            return View(empresa);
        }

        [HttpPost]
        [Route("administracion/logout", Name = "Logout")]
        public IActionResult Logout()
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            try
            {
                // Cerrar sesión
                HttpContext.SignOutAsync();

                // Eliminar la cookie "AdminAuthCookie"
                if (HttpContext.Request.Cookies.ContainsKey("AdminAuthCookie"))
                {
                    HttpContext.Response.Cookies.Delete("AdminAuthCookie");
                }
                resultado.Estatus = true;
                resultado.Detalle = Url.Action("LoginAdmin", "Home");
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

            // Redireccionar a la página de inicio de sesión u otra página después de cerrar sesión
            return new JsonResult(response);
        }

        //[Authorize(AuthenticationSchemes = "AdminScheme", Roles = "20")]
        [Route("administracion/admin-empresas", Name = "AdministracionEmpresas")]
        public IActionResult AdministracionEmpresas()
        {
            var conexion = _configuration.GetSection("Conexion").Get<Conexion>();
            var variables = _configuration.GetSection("VariablesGlobales").Get<VariablesGlobales>();
            Condusef_DLL.Clases.Conexion.CadenaConexion = conexion.DB;
            Condusef_DLL.Clases.Conexion.CadenaConexionSeguridad = conexion.Seguridad;
            Condusef_DLL.Clases.Conexion.Usuario = conexion.Usuario;
            Condusef_DLL.Clases.Conexion.Empresa = conexion.Empresa;
            Condusef_DLL.Clases.Conexion.Idioma = conexion.Idioma;Condusef_DLL.Clases.Conexion.RutaLog = Path.GetFullPath("Log");
            Condusef_DLL.Clases.VariablesGlobales.Llave = variables.Llave;
            Condusef_DLL.Clases.VariablesGlobales.IV = variables.IV;
            Condusef_DLL.Clases.VariablesGlobales.RutaPlantillasCorreo = Path.GetFullPath("Documentos/Plantillas/Correos");
            Condusef_DLL.Funciones.Condusef.Empresa empresa = new();
            empresa.CargaCatalogos();
            return View(empresa);
            //return View();
        }

        [Route("administracion/bitacora-api", Name = "BitacoraAPI")]
        public IActionResult BitacoraAPI()
        {
            var conexion = _configuration.GetSection("Conexion").Get<Conexion>();
            var variables = _configuration.GetSection("VariablesGlobales").Get<VariablesGlobales>();
            Condusef_DLL.Clases.Conexion.CadenaConexion = conexion.DB;
            Condusef_DLL.Clases.Conexion.CadenaConexionSeguridad = conexion.Seguridad;
            Condusef_DLL.Clases.Conexion.Usuario = conexion.Usuario;
            Condusef_DLL.Clases.Conexion.Empresa = conexion.Empresa;
            Condusef_DLL.Clases.Conexion.Idioma = conexion.Idioma;
            Condusef_DLL.Clases.Conexion.RutaLog = Path.GetFullPath("Log");
            Condusef_DLL.Clases.VariablesGlobales.Llave = variables.Llave;
            Condusef_DLL.Clases.VariablesGlobales.IV = variables.IV;
            Condusef_DLL.Clases.VariablesGlobales.RutaPlantillasCorreo = Path.GetFullPath("Documentos/Plantillas/Correos");
            Condusef_DLL.Funciones.Administracion.Administracion empresa = new();
            empresa.CargaCatalogos();
            return View(empresa);
            //return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Route("administracion/obtener-cp", Name = "Obtener_Catalogo_CP")]
        public async Task<JsonResult> Obtener_Catalogo_CP()
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            Condusef_DLL.Funciones.Condusef.Condusef condusef = new();

            try
            {
                resultado = await condusef.ObtenerCatalogosCP();
            }
            catch (Exception ex)
            {
                //FntLog log = new FntLog();
                //log.Log.nameSpace = "Condusef.AdminController";
                //log.Log.metodo = "SubirXML";
                //log.Log.error = ex.Message;
                //log.insertaLog();
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
            }
            var response = new
            {
                success = resultado.Estatus,
                message = resultado.Detalle,
                code = resultado.Code
            };
            return new JsonResult(response);
        }

        [HttpPost]
        [Route("administracion/cambiar-token", Name = "Cambiar_Token_Usuario")]
        public JsonResult Cambiar_Token_Usuario(string idUsuario, string token, string idServicio)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            Condusef_DLL.Funciones.Condusef.Condusef condusef = new();
            Condusef_DLL.Funciones.Condusef.Empresa empresa = new();

            try
            {
                resultado = condusef.CambiaTokenUsuario(idUsuario, token, idServicio);
            }
            catch (Exception ex)
            {
                //FntLog log = new FntLog();
                //log.Log.nameSpace = "Condusef.AdminController";
                //log.Log.metodo = "SubirXML";
                //log.Log.error = ex.Message;
                //log.insertaLog();
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
            }
            var response = new
            {
                success = resultado.Estatus,
                message = resultado.Detalle
            };
            return new JsonResult(response);
        }

        [HttpPost]
        [Route("administracion/obtener-token", Name = "Obtener_Token_Usuario")]
        public async Task<JsonResult> Obtener_Token_Usuario(string idUsuario, string idServicio)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            Condusef_DLL.Funciones.Condusef.Condusef condusef = new();
            Condusef_DLL.Funciones.Condusef.Empresa empresa = new();

            try
            {
                resultado = await condusef.ConsultaTokenUsuario(idUsuario, idServicio);
            }
            catch (Exception ex)
            {
                //FntLog log = new FntLog();
                //log.Log.nameSpace = "Condusef.AdminController";
                //log.Log.metodo = "SubirXML";
                //log.Log.error = ex.Message;
                //log.insertaLog();
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
            }
            var response = new
            {
                success = resultado.Estatus,
                message = resultado.Detalle
            };
            return new JsonResult(response);
        }

        [HttpPost]
        [Route("administracion/consultar-empresas-no-registradas", Name = "Consultar_Empresas_No_Registradas")]
        public async Task<JsonResult> Consultar_Empresas_No_Registradas()
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            Condusef_DLL.Funciones.Administracion.Administracion administracion = new();
            try
            {
                resultado = administracion.ConsultarEmpresasNoRegistradas();
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef.AdminController";
                log.Log.metodo = "Consultar_Empresas_No_Registradas";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
            }
            var response = new
            {
                success = resultado.Estatus,
                message = resultado.Detalle,
                data = administracion.EmpresasNoRegistradas
            };
            return new JsonResult(response);
        }

        [HttpPost]
        [Route("administracion/consultar-empresas-activas", Name = "Consultar_Empresas_Activas")]
        public async Task<JsonResult> Consultar_Empresas_Activas()
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            Condusef_DLL.Funciones.Administracion.Administracion administracion = new();
            try
            {
                resultado = administracion.ConsultarEmpresasActivas();
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef.AdminController";
                log.Log.metodo = "Consultar_Empresas_Activas";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
            }
            var response = new
            {
                success = resultado.Estatus,
                message = resultado.Detalle,
                data = administracion.EmpresasActivas
            };
            return new JsonResult(response);
        }

        [HttpPost]
        [Route("administracion/mostrar-info-empresa", Name = "Mostrar_Informacion_Empresa")]
        public async Task<JsonResult> Mostrar_Informacion_Empresa(string idEmpresa)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            Condusef_DLL.Funciones.Condusef.Empresa empresa = new();

            try
            {
                empresa.ConsultaDatosGeneralesPorEmpresa(idEmpresa);
                if (empresa.DatosGeneralesPorEmpresa.Nombre != "") resultado.Estatus = true; 
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef.AdminControllers";
                log.Log.metodo = "Mostrar_Informacion_Empresa_Registrada";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
            }
            var response = new
            {
                success = resultado.Estatus,
                message = resultado.Detalle,
                data = empresa.DatosGeneralesPorEmpresa
            };
            return new JsonResult(response);
        }

        [HttpPost]
        [Route("administracion/habilitar-empresa", Name = "Habilitar_Empresa")]
        public async Task<JsonResult> Habilitar_Empresa(string idEmpresa)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            Condusef_DLL.Funciones.Condusef.Empresa empresa = new();

            try
            {
                resultado = empresa.HabilitarEmpresa(idEmpresa);
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef.AdminControllers";
                log.Log.metodo = "Mostrar_Informacion_Empresa_Registrada";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
            }
            var response = new
            {
                success = resultado.Estatus,
                message = resultado.Detalle
            };
            return new JsonResult(response);
        }

        [HttpPost]
        [Route("administracion/catalogo-prods", Name = "Subir_Catalogo_Productos_Reune")]
        public async Task<JsonResult> Subir_Catalogo_Productos_Reune([FromForm] IFormFile layout)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            Condusef_DLL.Funciones.Administracion.Administracion admin = new();

            try
            {
                string path = Path.GetFullPath("Documentos/Temp");
                path = Path.Combine(path, layout.FileName);
                ClsStreamFile file = new(layout.OpenReadStream(), path, layout.FileName);
                resultado = await admin.CargarCatalogoProductosReune(file);
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef.AdminControllers";
                log.Log.metodo = "Subir_Catalogo_Productos_Reune";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
            }
            var response = new
            {
                success = resultado.Estatus,
                message = resultado.Detalle
            };
            return new JsonResult(response);
        }

        [HttpPost]
        [Route("administracion/consultar-log-empresa", Name = "Consultar_Log_Empresa")]
        public async Task<JsonResult> Consultar_Log_Empresa(string idEmpresa, int idServicio)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            Condusef_DLL.Funciones.Administracion.Administracion administracion = new();
            try
            {
                resultado = administracion.ConsultarLogAPI(idEmpresa, idServicio);
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef.AdminController";
                log.Log.metodo = "Consultar_Log_Empresa";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
            }
            var response = new
            {
                success = resultado.Estatus,
                message = resultado.Detalle,
                code = resultado.Code,
                data = administracion.ListadoLogAPI
            };
            return new JsonResult(response);
        }

        //[HttpPost]
        ////[ValidateAntiForgeryToken]
        //[Route("administracion/agregar-empresa", Name = "Agregar_Empresa")]
        //public async Task<JsonResult> Agregar_Empresa(string nombre, string nomCorto, string rfc, string casfim, string tokenRedeco)
        //{
        //    ClsResultadoAccion resultado = new ClsResultadoAccion();
        //    Condusef_DLL.Funciones.Condusef.Condusef condusef = new();
        //    Condusef_DLL.Funciones.Condusef.Empresa empresa = new();

        //    try
        //    {
        //        resultado = await empresa.AgregarEmpresa(nombre, nomCorto, rfc, casfim, tokenRedeco);
        //    }
        //    catch (Exception ex)
        //    {
        //        //FntLog log = new FntLog();
        //        //log.Log.nameSpace = "Condusef.AdminController";
        //        //log.Log.metodo = "SubirXML";
        //        //log.Log.error = ex.Message;
        //        //log.insertaLog();
        //        resultado.Estatus = false;
        //        resultado.Detalle = ex.Message;
        //    }
        //    var response = new
        //    {
        //        success = resultado.Estatus,
        //        message = resultado.Detalle,
        //        usuario = empresa.usuario
        //    };
        //    return new JsonResult(response);
        //}
    }
}
