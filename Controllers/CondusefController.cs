using Condusef.Classes;
using Condusef_DLL.Clases.Condusef;
using Condusef_DLL.Clases.Condusef.API;
using Condusef_DLL.Clases.Condusef.Configuracion;
using Condusef_DLL.Clases.General;
using Condusef_DLL.Clases.Generales;
using Condusef_DLL.Funciones.Generales;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.NetworkInformation;
using System.Security.Claims;

namespace Condusef.Controllers
{
    [Authorize(AuthenticationSchemes = "UserScheme")]
    public class CondusefController : Controller
    {
        public IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public CondusefController(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        [Route("condusef/", Name = "Condusef_Home")]
        public IActionResult Condusef_Home()
        {
            ViewData["Lang"] = Request.Cookies["lang"] ?? "es";
            return View();
        }

        [HttpPost]
        [Route("condusef/logout", Name = "CondusefLogout")]
        public async Task<IActionResult> CondusefLogout()
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            try
            {
                // Eliminar la cookie "UserAuthCookie" (antes de cerrar la sesión)
                if (HttpContext.Request.Cookies.ContainsKey("UserAuthCookie"))
                {
                    HttpContext.Response.Cookies.Delete("UserAuthCookie");
                }

                // Cerrar sesión
                await HttpContext.SignOutAsync("UserScheme");
                resultado.Estatus = true;
                resultado.Detalle = Url.Action("Login", "Home");
            }
            catch (Exception ex)
            {
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
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

        #region Configuracion

        [Route("condusef/configuracion", Name = "Configuracion")]
        public IActionResult Configuracion()
        {

            ViewData["Lang"] = Request.Cookies["lang"] ?? "es";
            var conexion = _configuration.GetSection("Conexion").Get<Conexion>();
            Condusef_DLL.Clases.Conexion.CadenaConexion = conexion.DB;
            Condusef_DLL.Clases.Conexion.CadenaConexionSeguridad = conexion.Seguridad;
            Condusef_DLL.Clases.Conexion.Usuario = HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;Condusef_DLL.Clases.Conexion.Empresa = HttpContext.User?.FindFirst(ClaimTypes.GroupSid)?.Value;
            Condusef_DLL.Clases.Conexion.Username = HttpContext.User?.FindFirst(ClaimTypes.Name)?.Value;
            var variables = _configuration.GetSection("VariablesGlobales").Get<VariablesGlobales>();
            Condusef_DLL.Clases.VariablesGlobales.Llave = variables.Llave;
            Condusef_DLL.Clases.VariablesGlobales.IV = variables.IV;
            Condusef_DLL.Clases.VariablesGlobales.Url = variables.Url;
            Condusef_DLL.Funciones.Condusef.Empresa empresa = new();
            var user = HttpContext.User; // Obtiene el principal del usuario actual
            var idEmpresa = user?.FindFirst(ClaimTypes.GroupSid);
            empresa.ConsultaDatosGeneralesPorEmpresa(idEmpresa.Value);
            empresa.CargaCatalogos();
            return View(empresa);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("condusef/configuracion/actualizar-tokens", Name = "Actualizar_Token")]
        public JsonResult Actualizar_Token(string tokenRedeco, string tokenReune)
        {
            ClsResultadoAccion resultado = new();
            Condusef_DLL.Funciones.Condusef.Condusef condusef = new();
            try
            {
                var user = HttpContext.User; // Obtiene el principal del usuario actual
                var groupSidClaim = user?.FindFirst(ClaimTypes.GroupSid);

                if (groupSidClaim != null)
                {
                    var idEmpresa = groupSidClaim.Value;
                    resultado = condusef.ActualizarToken(idEmpresa, tokenRedeco, tokenReune);
                }
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef.CondusefController";
                log.Log.metodo = "Actualizar_Token";
                log.Log.error = ex.Message;
                log.insertaLog();
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
        [ValidateAntiForgeryToken]
        [Route("condusef/configuracion/generar-super-usuario", Name = "Generar_Super_Usuario")]
        public async Task<JsonResult> Generar_Super_Usuario()
        {
            ClsResultadoAccion resultado = new();
            Condusef_DLL.Funciones.Condusef.Empresa empresa = new();
            try
            {
                var user = HttpContext.User; // Obtiene el principal del usuario actual
                var idEmpresa = user?.FindFirst(ClaimTypes.GroupSid).Value;
                var idUsuario = user?.FindFirst(ClaimTypes.NameIdentifier).Value;
                var username = user?.FindFirst(ClaimTypes.Name).Value;

                resultado = await empresa.DarAltaSuperUsuarioCondusef(idEmpresa, idUsuario, username);
                
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef.Controllers";
                log.Log.metodo = "Generar_Super_Usuario";
                log.Log.error = ex.Message;
                log.insertaLog();
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
        [ValidateAntiForgeryToken]
        [Route("condusef/configuracion/consulta-usuarios", Name = "Consultar_Usuarios")]
        public JsonResult Consultar_Usuarios()
        {
            List<ClsUsuario> resultado = new();
            Condusef_DLL.Funciones.Condusef.Empresa condusef = new();
            try
            {
                var user = HttpContext.User; // Obtiene el principal del usuario actual
                var groupSidClaim = user?.FindFirst(ClaimTypes.GroupSid);

                if (groupSidClaim != null)
                {
                    var idEmpresa = groupSidClaim.Value;
                    resultado = condusef.ConsultaUsuariosEmpresa(idEmpresa);
                }
            }
            catch (Exception ex)
            {
                //FntLog log = new FntLog();
                //log.Log.nameSpace = "Condusef.CondusefController";
                //log.Log.metodo = "Consulta_Aqlist";
                //log.Log.error = ex.Message;
                //log.insertaLog();
                //resultado.Estatus = false;
                //resultado.Detalle = ex.Message;
            }
            var response = new
            {
                data = resultado
            };
            return new JsonResult(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("condusef/configuracion/guardar-usuario", Name = "Guardar_Usuario")]
        public async Task<JsonResult> Guardar_Usuario(string usuario, string password, int idPerfil, bool activo, string correoResponsable)
        {
            ClsResultadoAccion resultado = new();
            Condusef_DLL.Funciones.Condusef.Empresa condusef = new();
            try
            {
                var user = HttpContext.User; // Obtiene el principal del usuario actual
                var groupSidClaim = user?.FindFirst(ClaimTypes.GroupSid);
                var idClaim = user?.FindFirst(ClaimTypes.NameIdentifier);

                if (groupSidClaim != null || idClaim != null)
                {
                    string idEmpresa = groupSidClaim.Value;
                    string idUsuario = idClaim.Value;
                    resultado = await condusef.CrearNuevoUsuario(idEmpresa,idUsuario,usuario,password,idPerfil,activo, correoResponsable);
                }
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef.CondusefController";
                log.Log.metodo = "Guardar_Usuario";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
            }
            var response = new
            {
                success = resultado.Estatus,
                mensaje = resultado.Detalle,
                code = resultado.Code
            };
            return new JsonResult(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("condusef/configuracion/eliminar-usuario", Name = "Eliminar_Usuario")]
        public JsonResult Eliminar_Usuario(string idUsuario)
        {
            ClsResultadoAccion resultado = new();
            Condusef_DLL.Funciones.Condusef.Empresa condusef = new();
            try
            {
                var user = HttpContext.User; // Obtiene el principal del usuario actual
                var groupSidClaim = user?.FindFirst(ClaimTypes.GroupSid);

                if (groupSidClaim != null)
                {
                    string idEmpresa = groupSidClaim.Value;
                    resultado = condusef.EliminarUsuario(idEmpresa, idUsuario);
                }
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef.CondusefController";
                log.Log.metodo = "Eliminar_Usuario";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
            }
            var response = new
            {
                success = resultado.Estatus,
                mensaje = resultado.Detalle
            };
            return new JsonResult(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("condusef/configuracion/desactivar-usuario", Name = "Desactivar_Usuario")]
        public JsonResult Desactivar_Usuario(string idUsuario, bool estaActivado)
        {
            ClsResultadoAccion resultado = new();
            Condusef_DLL.Funciones.Condusef.Empresa condusef = new();
            try
            {
                var user = HttpContext.User; // Obtiene el principal del usuario actual
                var groupSidClaim = user?.FindFirst(ClaimTypes.GroupSid);

                if (groupSidClaim != null)
                {
                    string idEmpresa = groupSidClaim.Value;
                    resultado = condusef.DesactivarUsuario(idEmpresa, idUsuario, estaActivado);
                }
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef.CondusefController";
                log.Log.metodo = "Desactivar_Usuario";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
            }
            var response = new
            {
                success = resultado.Estatus,
                mensaje = resultado.Detalle
            };
            return new JsonResult(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("condusef/configuracion/cambiar-correo-responsable", Name = "Cambiar_Correo_Responsable")]
        public JsonResult Cambiar_Correo_Responsable(string idUsuario, string correo)
        {
            ClsResultadoAccion resultado = new();
            Condusef_DLL.Funciones.Condusef.Empresa condusef = new();
            try
            {
                var user = HttpContext.User; // Obtiene el principal del usuario actual
                var groupSidClaim = user?.FindFirst(ClaimTypes.GroupSid);

                if (groupSidClaim != null)
                {
                    string idEmpresa = groupSidClaim.Value;
                    resultado = condusef.CambiarCorreoResponsable(idEmpresa, idUsuario, correo);
                }
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef.CondusefController";
                log.Log.metodo = "Cambiar_Correo_Responsable";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
            }
            var response = new
            {
                success = resultado.Estatus,
                mensaje = resultado.Detalle
            };
            return new JsonResult(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("condusef/configuracion/cambiar-password-usuario", Name = "Cambiar_Password_Usuario_Config")]
        public JsonResult Cambiar_Password_Usuario_Config(string idUsuario, string password)
        {
            ClsResultadoAccion resultado = new();
            Condusef_DLL.Funciones.Condusef.Empresa condusef = new();
            try
            {
                resultado = condusef.CambiarPasswordUsuarioConfiguracion(idUsuario, password);
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef.CondusefController";
                log.Log.metodo = "Cambiar_Password_Usuario";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
            }
            var response = new
            {
                success = resultado.Estatus,
                mensaje = resultado.Detalle
            };
            return new JsonResult(response);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("condusef/configuracion/descargar-productos", Name = "Descargar_Productos")]
        public async Task<JsonResult> Descargar_Productos()
        {
            ClsResultadoAccion resultado = new();
            Condusef_DLL.Funciones.Condusef.Empresa condusef = new();
            try
            {
                var user = HttpContext.User; // Obtiene el principal del usuario actual
                var groupSidClaim = user?.FindFirst(ClaimTypes.GroupSid);
                var idClaim = user?.FindFirst(ClaimTypes.NameIdentifier);

                if (groupSidClaim != null || idClaim != null)
                {
                    string idEmpresa = groupSidClaim.Value;
                    string idUsuario = idClaim.Value;
                    resultado = await condusef.DescargarCatalogoProductos(idEmpresa, idUsuario);
                }
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef.CondusefController";
                log.Log.metodo = "Descargar_Productos";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
            }
            var response = new
            {
                success = resultado.Estatus,
                mensaje = resultado.Detalle
            };
            return new JsonResult(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("condusef/configuracion/actualizar-datos-empresa", Name = "Actualizar_Datos_Empresa")]
        public JsonResult Actualizar_Datos_Empresa(ClsDatosEmpresa datosEmpresa)
        {
            ClsResultadoAccion resultado = new();
            Condusef_DLL.Funciones.Condusef.Empresa condusef = new();
            try
            {
                var user = HttpContext.User; // Obtiene el principal del usuario actual
                var idEmpresa = user?.FindFirst(ClaimTypes.GroupSid).Value;
                resultado = condusef.ActualizarDatosEmpresa(idEmpresa, datosEmpresa);
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef.CondusefController";
                log.Log.metodo = "Actualizar_Datos_Empresa";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
            }
            var response = new
            {
                success = resultado.Estatus,
                mensaje = resultado.Detalle
            };
            return new JsonResult(response);
        }


        #endregion

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Route("condusef/redeco/subir-reporte", Name = "Redeco_Subir_Reporte")]
        //public async Task<JsonResult> Redeco_Subir_Reporte([FromForm] IFormFile formFile, int periodo, int anio, int estatus)
        //{
        //    ClsResultadoAccion resultado = new ClsResultadoAccion();
        //    Condusef_DLL.Funciones.Condusef.Redeco condusef = new();

        //    try
        //    {
        //        var user = HttpContext.User; // Obtiene el principal del usuario actual
        //        var idEmpresa = user?.FindFirst(ClaimTypes.GroupSid)?.Value;
        //        string extension = Path.GetExtension(formFile.FileName);
        //        string path = Path.GetFullPath("Documentos/Archivos");
        //        string fileName = condusef.GenerarNombreRedeco(idEmpresa, periodo, anio, extension);
        //        path = Path.Combine(path, idEmpresa, "Redeco", fileName);
        //        ClsStreamFile file = new(formFile.OpenReadStream(), path, fileName);
        //        resultado = await condusef.SubirReporteRedeco(idEmpresa, file, periodo, anio, estatus);
        //    }
        //    catch (Exception ex)
        //    {
        //        //FntLog log = new FntLog();
        //        //log.Log.nameSpace = "Condusef.CondusefController";
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
        //        code = resultado.Code
        //    };
        //    return new JsonResult(response);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Route("condusef/redeco/obtener-reporte", Name = "Redeco_Obtener_Estatus")]
        //public async Task<JsonResult> Redeco_Obtener_Estatus(int idTicket, string ticket)
        //{
        //    ClsResultadoAccion resultado = new ClsResultadoAccion();
        //    Condusef_DLL.Funciones.Condusef.Redeco condusef = new();

        //    try
        //    {
        //        var user = HttpContext.User; // Obtiene el principal del usuario actual
        //        var idEmpresa = user?.FindFirst(ClaimTypes.GroupSid)?.Value;
        //        resultado = await condusef.ObtenerEstatusTicketRedeco(idEmpresa, ticket, idTicket);
        //        //using (Stream stream = file.OpenReadStream())
        //        //{
        //        //    resultado = await condusef.SubirReporteRedeco(stream, path, file.FileName, periodo, anio);
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        //FntLog log = new FntLog();
        //        //log.Log.nameSpace = "Condusef.CondusefController";
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
        //        code = resultado.Code,
        //        estatus = condusef.EstatusTicket
        //    };
        //    return new JsonResult(response);
        //}

        //[HttpPut]
        //[ValidateAntiForgeryToken]
        //[Route("condusef/redeco/corregir-reporte", Name = "Redeco_Corregir_Reporte")]
        //public async Task<JsonResult> Redeco_Corregir_Reporte([FromForm] IFormFile formFile, ClsTicket ticket)
        //{
        //    ClsResultadoAccion resultado = new ClsResultadoAccion();
        //    Condusef_DLL.Funciones.Condusef.Redeco condusef = new();

        //    try
        //    {
        //        var user = HttpContext.User; // Obtiene el principal del usuario actual
        //        var idEmpresa = user?.FindFirst(ClaimTypes.GroupSid)?.Value;
        //        //string path = Path.GetFullPath("Documentos/Archivos");
        //        //path = Path.Combine(path, formFile.FileName);
        //        ClsStreamFile file = new(formFile.OpenReadStream(), "", formFile.FileName);
        //        resultado = await condusef.CorregirTicketRedeco(idEmpresa, file, ticket);
        //        //using (Stream stream = file.OpenReadStream())
        //        //{
        //        //    resultado = await condusef.SubirReporteRedeco(stream, path, file.FileName, periodo, anio);
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        //FntLog log = new FntLog();
        //        //log.Log.nameSpace = "Condusef.CondusefController";
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
        //        code = resultado.Code
        //    };
        //    return new JsonResult(response);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Route("condusef/reune/consultar-tickets", Name = "Reune_Consultar_Tickets")]
        //public JsonResult Reune_Consultar_Tickets()
        //{
        //    ClsResultadoAccion resultado = new();
        //    Condusef_DLL.Funciones.Condusef.Redeco condusef = new();
        //    try
        //    {
        //        var user = HttpContext.User; // Obtiene el principal del usuario actual
        //        Console.Write(user.Identity);
        //        var groupSidClaim = user?.FindFirst(ClaimTypes.GroupSid);

        //        if (groupSidClaim != null)
        //        {
        //            var idEmpresa = groupSidClaim.Value;
        //            condusef.ConsultaTicketsReune(idEmpresa);
        //            resultado.Estatus = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //FntLog log = new FntLog();
        //        //log.Log.nameSpace = "Condusef.CondusefController";
        //        //log.Log.metodo = "Consulta_Aqlist";
        //        //log.Log.error = ex.Message;
        //        //log.insertaLog();
        //        resultado.Estatus = false;
        //        resultado.Detalle = ex.Message;
        //    }
        //    var response = new
        //    {
        //        success = resultado.Estatus,
        //        message = resultado.Detalle,
        //        code = resultado.Code,
        //        data = condusef.ListaTickets
        //    };
        //    return new JsonResult(response);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Route("condusef/reune/subir-reporte", Name = "Reune_Subir_Reporte")]
        //public async Task<JsonResult> Reune_Subir_Reporte([FromForm] IFormFile formFile, int periodo, int anio, int estatus, int tipoDocumento)
        //{
        //    ClsResultadoAccion resultado = new ClsResultadoAccion();
        //    Condusef_DLL.Funciones.Condusef.Redeco condusef = new();

        //    try
        //    {

        //        var user = HttpContext.User; // Obtiene el principal del usuario actual
        //        var idEmpresa = user?.FindFirst(ClaimTypes.GroupSid)?.Value;
        //        string extension = Path.GetExtension(formFile.FileName);
        //        string path = Path.GetFullPath("Documentos/Archivos");
        //        string fileName = condusef.GenerarNombreReune(idEmpresa, tipoDocumento, periodo, anio, extension);
        //        path = Path.Combine(path, idEmpresa, "Reune", fileName);
        //        ClsStreamFile file = new(formFile.OpenReadStream(), path, fileName);
        //        resultado = await condusef.SubirReporteReune(idEmpresa, file, periodo, anio, estatus, tipoDocumento);
        //    }
        //    catch (Exception ex)
        //    {
        //        //FntLog log = new FntLog();
        //        //log.Log.nameSpace = "Condusef.CondusefController";
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
        //        code = resultado.Code
        //    };
        //    return new JsonResult(response);
        //}

    }
}
