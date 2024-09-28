using Condusef.Classes;
using Condusef_DLL.Clases.Condusef.API;
using Condusef_DLL.Clases.General;
using Condusef_DLL.Clases.Generales;
using Condusef_DLL.Funciones.Generales;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Condusef.Controllers
{
    [Authorize(AuthenticationSchemes = "UserScheme")]
    public class RedecoController : Controller
    {
        public IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public RedecoController(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        #region Redeco

        [Route("condusef/redeco/enviar-quejas", Name = "EnviarQuejas")]
        public IActionResult EnviarQuejas()
        {
            ViewData["Lang"] = Request.Cookies["lang"] ?? "es";
            var conexion = _configuration.GetSection("Conexion").Get<Conexion>();
            var variables = _configuration.GetSection("VariablesGlobales").Get<VariablesGlobales>();
            Condusef_DLL.Clases.Conexion.CadenaConexion = conexion.DB;
            Condusef_DLL.Clases.Conexion.CadenaConexionSeguridad = conexion.Seguridad;
            Condusef_DLL.Clases.Conexion.Usuario = HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Condusef_DLL.Clases.Conexion.Empresa = HttpContext.User?.FindFirst(ClaimTypes.GroupSid)?.Value;
            Condusef_DLL.Clases.Conexion.Username = HttpContext.User?.FindFirst(ClaimTypes.Name)?.Value;
            Condusef_DLL.Clases.Conexion.Idioma = conexion.Idioma;Condusef_DLL.Clases.Conexion.RutaLog = Path.GetFullPath("Log");
            Condusef_DLL.Clases.VariablesGlobales.Url = variables.Url;
            Condusef_DLL.Clases.VariablesGlobales.Llave = variables.Llave;
            Condusef_DLL.Clases.VariablesGlobales.IV = variables.IV;
            Condusef_DLL.Funciones.Condusef.Redeco condusef = new();
            var idEmpresa = HttpContext.User?.FindFirst(ClaimTypes.GroupSid)?.Value;
            condusef.CargarCatalogos(idEmpresa);
            return View(condusef);
        }

        [Route("condusef/redeco/consultar-quejas", Name = "ConsultarQuejas")]
        public IActionResult ConsultarQuejas()
        {
            ViewData["Lang"] = Request.Cookies["lang"] ?? "es";
            var conexion = _configuration.GetSection("Conexion").Get<Conexion>();
            var variables = _configuration.GetSection("VariablesGlobales").Get<VariablesGlobales>();
            Condusef_DLL.Clases.Conexion.CadenaConexion = conexion.DB;
            Condusef_DLL.Clases.Conexion.CadenaConexionSeguridad = conexion.Seguridad;
            Condusef_DLL.Clases.Conexion.Usuario = HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;Condusef_DLL.Clases.Conexion.Empresa = HttpContext.User?.FindFirst(ClaimTypes.GroupSid)?.Value;
            Condusef_DLL.Clases.Conexion.Username = HttpContext.User?.FindFirst(ClaimTypes.Name)?.Value;
            Condusef_DLL.Clases.Conexion.Idioma = conexion.Idioma;Condusef_DLL.Clases.Conexion.RutaLog = Path.GetFullPath("Log");
            Condusef_DLL.Clases.VariablesGlobales.Url = variables.Url;
            Condusef_DLL.Clases.VariablesGlobales.Llave = variables.Llave;
            Condusef_DLL.Clases.VariablesGlobales.IV = variables.IV;
            Condusef_DLL.Funciones.Condusef.Redeco condusef = new();
            var idEmpresa = HttpContext.User?.FindFirst(ClaimTypes.GroupSid)?.Value;
            condusef.CargarCatalogos(idEmpresa);
            return View(condusef);
        }

        [Route("condusef/redeco/subir-layout-quejas", Name = "SubirLayoutQuejas")]
        public IActionResult SubirLayoutQuejas()
        {
            ViewData["Lang"] = Request.Cookies["lang"] ?? "es";
            var conexion = _configuration.GetSection("Conexion").Get<Conexion>();
            var variables = _configuration.GetSection("VariablesGlobales").Get<VariablesGlobales>();
            Condusef_DLL.Clases.Conexion.CadenaConexion = conexion.DB;
            Condusef_DLL.Clases.Conexion.CadenaConexionSeguridad = conexion.Seguridad;
            Condusef_DLL.Clases.Conexion.Usuario = HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;Condusef_DLL.Clases.Conexion.Empresa = HttpContext.User?.FindFirst(ClaimTypes.GroupSid)?.Value;
            Condusef_DLL.Clases.Conexion.Username = HttpContext.User?.FindFirst(ClaimTypes.Name)?.Value;
            Condusef_DLL.Clases.Conexion.Idioma = conexion.Idioma;Condusef_DLL.Clases.Conexion.RutaLog = Path.GetFullPath("Log");
            Condusef_DLL.Clases.VariablesGlobales.Url = variables.Url;
            Condusef_DLL.Clases.VariablesGlobales.Llave = variables.Llave;
            Condusef_DLL.Clases.VariablesGlobales.IV = variables.IV;
            Condusef_DLL.Funciones.Condusef.Redeco condusef = new();
            var idEmpresa = HttpContext.User?.FindFirst(ClaimTypes.GroupSid)?.Value;
            condusef.CargarCatalogos(idEmpresa);
            return View(condusef);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("condusef/redeco/consultar-causas-producto", Name = "Redeco_Consultar_Causas_Producto")]
        public JsonResult Redeco_Consultar_Causas_Producto(string idProducto)
        {
            ClsResultadoAccion resultado = new();
            Condusef_DLL.Funciones.Condusef.Redeco condusef = new();
            try
            {
                var user = HttpContext.User; // Obtiene el principal del usuario actual
                var groupSidClaim = user?.FindFirst(ClaimTypes.GroupSid);

                if (groupSidClaim != null)
                {
                    var idEmpresa = groupSidClaim.Value;
                    condusef.ConsultaCausasProducto(idEmpresa, idProducto);
                    resultado.Estatus = true;
                }
            }
            catch (Exception ex)
            {
                //FntLog log = new FntLog();
                //log.Log.nameSpace = "Condusef.CondusefController";
                //log.Log.metodo = "Consulta_Aqlist";
                //log.Log.error = ex.Message;
                //log.insertaLog();
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
            }
            var response = new
            {
                success = resultado.Estatus,
                message = resultado.Detalle,
                code = resultado.Code,
                data = condusef.CausasProductos
            };
            return new JsonResult(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("condusef/redeco/consultar-municipios", Name = "Redeco_Consultar_Municipios")]
        public JsonResult Redeco_Consultar_Municipios(string idEstado)
        {
            ClsResultadoAccion resultado = new();
            Condusef_DLL.Funciones.Condusef.Redeco redeco = new();
            try
            {
                resultado = redeco.ConsultaMunicipios(idEstado);
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef.CondusefController";
                log.Log.metodo = "Redeco_Consultar_Municipios";
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
                data = redeco.Municipios
            };
            return new JsonResult(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("condusef/redeco/consultar-cp", Name = "Redeco_Consultar_CP")]
        public JsonResult Redeco_Consultar_CP(string idEstado, string idMunicipio)
        {
            ClsResultadoAccion resultado = new();
            Condusef_DLL.Funciones.Condusef.Redeco redeco = new();
            try
            {
                resultado = redeco.ConsultaCodigosPostales(idEstado, idMunicipio);
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef.CondusefController";
                log.Log.metodo = "Redeco_Consultar_CP";
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
                data = redeco.CodigosPostales
            };
            return new JsonResult(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("condusef/redeco/consultar-colonias", Name = "Redeco_Consultar_Colonias")]
        public JsonResult Redeco_Consultar_Colonias(string idEstado, string idMunicipio, string cp)
        {
            ClsResultadoAccion resultado = new();
            Condusef_DLL.Funciones.Condusef.Redeco redeco = new();
            try
            {
                resultado = redeco.ConsultaColonias(idEstado, idMunicipio, cp);
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef.CondusefController";
                log.Log.metodo = "Redeco_Consultar_Colonias";
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
                data = redeco.Colonias
            };
            return new JsonResult(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("condusef/redeco/enviar-queja", Name = "Redeco_Enviar_Queja")]
        public async Task<JsonResult> Redeco_Enviar_Queja(int anio, List<ClsQuejas> quejas)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            Condusef_DLL.Funciones.Condusef.Redeco condusef = new();

            try
            {
                var user = HttpContext.User; // Obtiene el principal del usuario actual
                var idEmpresa = user?.FindFirst(ClaimTypes.GroupSid)?.Value;
                var idUsuario = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var username = user?.FindFirst(ClaimTypes.Name)?.Value;
                if (idEmpresa != null || idUsuario != null)
                {
                    resultado = await condusef.EnviarQueja(idEmpresa, idUsuario, username, anio, quejas);
                }

            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef.CondusefController";
                log.Log.metodo = "Redeco_Enviar_Queja";
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
                errores = condusef.BolsaErroresQuejas
            };
            return new JsonResult(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("condusef/redeco/consultar-quejas", Name = "Redeco_Consultar_Quejas")]
        public JsonResult Redeco_Consultar_Quejas(string periodo, string anio)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            Condusef_DLL.Funciones.Condusef.Redeco condusef = new();

            try
            {
                var user = HttpContext.User; // Obtiene el principal del usuario actual
                var idEmpresa = user?.FindFirst(ClaimTypes.GroupSid)?.Value;
                if (idEmpresa != null)
                {
                    resultado = condusef.ConsultarQuejas(idEmpresa, periodo, anio);
                }

            }
            catch (Exception ex)
            {
                //FntLog log = new FntLog();
                //log.Log.nameSpace = "Condusef.CondusefController";
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
                code = resultado.Code,
                data = condusef.ListadoQuejas
            };
            return new JsonResult(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("condusef/redeco/eliminar-quejas", Name = "Redeco_Eliminar_Quejas")]
        public async Task<JsonResult> Redeco_Eliminar_Quejas(string folio)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            Condusef_DLL.Funciones.Condusef.Redeco condusef = new();

            try
            {
                var user = HttpContext.User; // Obtiene el principal del usuario actual
                var idEmpresa = user?.FindFirst(ClaimTypes.GroupSid)?.Value;
                var idUsuario = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var username = user?.FindFirst(ClaimTypes.Name)?.Value;
                if (idEmpresa != null)
                {
                    resultado = await condusef.EliminarQueja(idEmpresa, idUsuario, username, folio);
                }

            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef.CondusefController";
                log.Log.metodo = "Redeco_Eliminar_Quejas";
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
        [Route("condusef/redeco/consultar-respuesta-json-redeco", Name = "Consultar_Respuesta_Json")]
        public JsonResult Consultar_Respuesta_Json(string folio)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            Condusef_DLL.Funciones.Condusef.Redeco redeco = new();

            try
            {
                var user = HttpContext.User; // Obtiene el principal del usuario actual
                var idEmpresa = user?.FindFirst(ClaimTypes.GroupSid)?.Value;
                if (idEmpresa != null)
                {
                    resultado = redeco.ConsultarRespuestaJson(idEmpresa, folio);
                }

            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef.RedecoController";
                log.Log.metodo = "Consultar_Respuesta_Json";
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
            };
            return new JsonResult(response);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("condusef/redeco/subir-layout-redeco", Name = "Redeco_Subir_Layout")]
        public async Task<JsonResult> Redeco_Subir_Layout([FromForm] IFormFile layout)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            Condusef_DLL.Funciones.Condusef.Redeco redeco = new();

            try
            {
                string path = Path.GetFullPath("Documentos/Temp");
                path = Path.Combine(path, layout.FileName);
                ClsStreamFile file = new(layout.OpenReadStream(), path, layout.FileName);
                resultado = await redeco.ProcesarLayoutQuejas(file);
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef.RedecoControllers";
                log.Log.metodo = "Redeco_Subir_Layout";
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
                data = redeco.ListadoQuejasLayout
            };
            return new JsonResult(response);
        }


        #endregion
    }
}
