using Condusef.Classes;
using Condusef_DLL.Clases.Condusef.API;
using Condusef_DLL.Clases.Condusef.Configuracion;
using Condusef_DLL.Clases.General;
using Condusef_DLL.Clases.Generales;
using Condusef_DLL.Funciones.Generales;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Condusef.Controllers
{
    [Authorize(AuthenticationSchemes = "UserScheme")]
    public class ReuneController : Controller
    {
        public IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public ReuneController(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        #region Consultas

        [Route("condusef/reune/consultas-general", Name = "Consultas")]
        public IActionResult Consultas()
        {
            ViewData["Lang"] = Request.Cookies["lang"] ?? "es";
            var conexion = _configuration.GetSection("Conexion").Get<Conexion>();
            var variables = _configuration.GetSection("VariablesGlobales").Get<VariablesGlobales>();
            Condusef_DLL.Clases.Conexion.CadenaConexion = conexion.DB;
            Condusef_DLL.Clases.Conexion.CadenaConexionSeguridad = conexion.Seguridad;
            Condusef_DLL.Clases.Conexion.Usuario = HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;Condusef_DLL.Clases.Conexion.Empresa = HttpContext.User?.FindFirst(ClaimTypes.GroupSid)?.Value;
            Condusef_DLL.Clases.Conexion.Username = HttpContext.User?.FindFirst(ClaimTypes.Name)?.Value;
            Condusef_DLL.Clases.Conexion.Idioma = conexion.Idioma;Condusef_DLL.Clases.Conexion.RutaLog = Path.GetFullPath("Log");Condusef_DLL.Clases.Conexion.RutaLog = Path.GetFullPath("Log");
            Condusef_DLL.Clases.VariablesGlobales.Url = variables.Url;
            Condusef_DLL.Clases.VariablesGlobales.Llave = variables.Llave;
            Condusef_DLL.Clases.VariablesGlobales.IV = variables.IV;
            Condusef_DLL.Funciones.Condusef.Reune reune = new();
            var idEmpresa = HttpContext.User?.FindFirst(ClaimTypes.GroupSid)?.Value;
            var idSector = HttpContext.User?.FindFirst("IdSector")?.Value;
            reune.CargarCatalogos(idEmpresa, 1, idSector);
            return View(reune);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("condusef/reune/enviar-consultas", Name = "Enviar_Consultas")]
        public async Task<JsonResult> Enviar_Consultas(int anio, List<ClsConsultasGenerales> consultas)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            Condusef_DLL.Funciones.Condusef.Reune reune = new();

            try
            {
                var user = HttpContext.User; // Obtiene el principal del usuario actual
                var idEmpresa = user?.FindFirst(ClaimTypes.GroupSid)?.Value;
                var idUsuario = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var username = user?.FindFirst(ClaimTypes.Name)?.Value;
                if (idEmpresa != null || idUsuario != null)
                {
                    ClsUsuario usuario = new(idEmpresa, idUsuario, username);
                    resultado = await reune.EnviarConsultasGenerales(usuario, anio, consultas);
                }

            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef.ReuneController";
                log.Log.metodo = "Enviar_Consultas";
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
                errores = reune.BolsaErroresConsultas
            };
            return new JsonResult(response);
        }

        [Route("condusef/reune/buscar-consultas-general", Name = "BuscarConsultas")]
        public IActionResult BuscarConsultas()
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
            Condusef_DLL.Funciones.Condusef.Reune reune = new();
            var idEmpresa = HttpContext.User?.FindFirst(ClaimTypes.GroupSid)?.Value;
            var idSector = HttpContext.User?.FindFirst("IdSector")?.Value;
            reune.CargarCatalogos(idEmpresa, 1, idSector);
            return View(reune);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("condusef/reune/buscar-consultas-general-post", Name = "Buscar_Consultas_Empresa")]
        public JsonResult Buscar_Consultas_Empresa(string anio, string periodo)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            Condusef_DLL.Funciones.Condusef.Reune reune = new();

            try
            {
                var user = HttpContext.User; // Obtiene el principal del usuario actual
                var idEmpresa = user?.FindFirst(ClaimTypes.GroupSid)?.Value;
                if (idEmpresa != null)
                {
                    resultado = reune.BuscarConsultas(idEmpresa, periodo, anio);
                }

            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef.ReuneController";
                log.Log.metodo = "Buscar_Consultas_Empresa";
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
                listado = reune.ListadoConsultasGenerales
            };
            return new JsonResult(response);
        }

        [Route("condusef/reune/layout-consultas", Name = "SubirLayoutConsultas")]
        public IActionResult SubirLayoutConsultas()
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
            Condusef_DLL.Funciones.Condusef.Reune reune = new();
            var idEmpresa = HttpContext.User?.FindFirst(ClaimTypes.GroupSid)?.Value;
            var idSector = HttpContext.User?.FindFirst("IdSector")?.Value;
            reune.CargarCatalogos(idEmpresa, 1, idSector);
            return View(reune);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("condusef/reune/subir-layout-consultas", Name = "Subir_Layout_Consultas")]
        public async Task<JsonResult> Subir_Layout_Consultas([FromForm] IFormFile layout)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            Condusef_DLL.Funciones.Condusef.Reune reune = new();

            try
            {
                string path = Path.GetFullPath("Documentos/Temp");
                path = Path.Combine(path, layout.FileName);
                ClsStreamFile file = new(layout.OpenReadStream(), path, layout.FileName);
                resultado = await reune.ProcesarLayoutConsultas(file);
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef.ReuneControllers";
                log.Log.metodo = "Subir_Layout_Consultas";
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
                data = reune.ListadoConsultasLayout
            };
            return new JsonResult(response);
        }

        #endregion


        #region Reclamaciones
        [Route("condusef/reune/reclamaciones-general", Name = "Reclamaciones")]
        public IActionResult Reclamaciones()
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
            Condusef_DLL.Funciones.Condusef.Reune reune = new();
            var idEmpresa = HttpContext.User?.FindFirst(ClaimTypes.GroupSid)?.Value;
            var idSector = HttpContext.User?.FindFirst("IdSector")?.Value;
            reune.CargarCatalogos(idEmpresa, 2, idSector);
            return View(reune);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("condusef/reune/enviar-reclamaciones", Name = "Enviar_Reclamaciones")]
        public async Task<JsonResult> Enviar_Reclamaciones(int anio, List<ClsReclamacionesGenerales> reclamaciones)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            Condusef_DLL.Funciones.Condusef.Reune reune = new();

            try
            {
                var user = HttpContext.User; // Obtiene el principal del usuario actual
                var idEmpresa = user?.FindFirst(ClaimTypes.GroupSid)?.Value;
                var idUsuario = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var username = user?.FindFirst(ClaimTypes.Name)?.Value;
                if (idEmpresa != null || idUsuario != null)
                {
                    ClsUsuario usuario = new(idEmpresa, idUsuario, username);
                    resultado = await reune.EnviarReclamacionesGenerales(usuario, anio, reclamaciones);
                }

            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef.ReuneController";
                log.Log.metodo = "Enviar_Reclamaciones";
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
                errores = reune.BolsaErroresReclamaciones
            };
            return new JsonResult(response);
        }

        [Route("condusef/reune/consultar-reclamaciones-general", Name = "ConsultarReclamaciones")]
        public IActionResult ConsultarReclamaciones()
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
            Condusef_DLL.Funciones.Condusef.Reune reune = new();
            var idEmpresa = HttpContext.User?.FindFirst(ClaimTypes.GroupSid)?.Value;
            var idSector = HttpContext.User?.FindFirst("IdSector")?.Value;
            reune.CargarCatalogos(idEmpresa, 2, idSector);
            return View(reune);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("condusef/reune/consultar-reclamaciones-general-post", Name = "Consultar_Reclamaciones_Empresa")]
        public JsonResult Consultar_Reclamaciones_Empresa(string anio, string periodo)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            Condusef_DLL.Funciones.Condusef.Reune reune = new();

            try
            {
                var user = HttpContext.User; // Obtiene el principal del usuario actual
                var idEmpresa = user?.FindFirst(ClaimTypes.GroupSid)?.Value;
                if (idEmpresa != null)
                {
                    resultado = reune.ConsultarReclamaciones(idEmpresa, periodo, anio);
                }

            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef.ReuneController";
                log.Log.metodo = "Consultar_Reclamaciones_Empresa";
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
                listado = reune.ListadoReclamacionesGenerales
            };
            return new JsonResult(response);
        }

        [Route("condusef/reune/layout-reclamaciones", Name = "SubirLayoutReclamaciones")]
        public IActionResult SubirLayoutReclamaciones()
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
            Condusef_DLL.Funciones.Condusef.Reune reune = new();
            var idEmpresa = HttpContext.User?.FindFirst(ClaimTypes.GroupSid)?.Value;
            var idSector = HttpContext.User?.FindFirst("IdSector")?.Value;
            reune.CargarCatalogos(idEmpresa, 1, idSector);
            return View(reune);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("condusef/reune/subir-layout-reclamaciones", Name = "Subir_Layout_Reclamaciones")]
        public async Task<JsonResult> Subir_Layout_Reclamaciones([FromForm] IFormFile layout)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            Condusef_DLL.Funciones.Condusef.Reune reune = new();

            try
            {
                string path = Path.GetFullPath("Documentos/Temp");
                path = Path.Combine(path, layout.FileName);
                ClsStreamFile file = new(layout.OpenReadStream(), path, layout.FileName);
                resultado = await reune.ProcesarLayoutReclamaciones(file);
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef.ReuneControllers";
                log.Log.metodo = "Subir_Layout_Reclamaciones";
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
                data = reune.ListadoReclamacionesLayout
            };
            return new JsonResult(response);
        }


        #endregion

        #region Aclaraciones
        [Route("condusef/reune/aclaraciones-general", Name = "Aclaraciones")]
        public IActionResult Aclaraciones()
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
            Condusef_DLL.Funciones.Condusef.Reune reune = new();
            var idEmpresa = HttpContext.User?.FindFirst(ClaimTypes.GroupSid)?.Value;
            var idSector = HttpContext.User?.FindFirst("IdSector")?.Value;
            reune.CargarCatalogos(idEmpresa, 3, idSector);
            return View(reune);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("condusef/reune/enviar-aclaraciones", Name = "Enviar_Aclaraciones")]
        public async Task<JsonResult> Enviar_Aclaraciones(int anio, List<ClsAclaracionesGenerales> aclaraciones)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            Condusef_DLL.Funciones.Condusef.Reune reune = new();

            try
            {
                var user = HttpContext.User; // Obtiene el principal del usuario actual
                var idEmpresa = user?.FindFirst(ClaimTypes.GroupSid)?.Value;
                var idUsuario = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var username = user?.FindFirst(ClaimTypes.Name)?.Value;
                if (idEmpresa != null || idUsuario != null)
                {
                    ClsUsuario usuario = new(idEmpresa, idUsuario, username);
                    resultado = await reune.EnviarAclaracionesGenerales(usuario, anio, aclaraciones);
                }

            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef.ReuneController";
                log.Log.metodo = "Enviar_Aclaraciones";
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
                errores = reune.BolsaErroresAclaraciones
            };
            return new JsonResult(response);
        }

        [Route("condusef/reune/consultar-aclaraciones-general", Name = "ConsultarAclaraciones")]
        public IActionResult ConsultarAclaraciones()
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
            Condusef_DLL.Funciones.Condusef.Reune reune = new();
            var idEmpresa = HttpContext.User?.FindFirst(ClaimTypes.GroupSid)?.Value;
            var idSector = HttpContext.User?.FindFirst("IdSector")?.Value;
            reune.CargarCatalogos(idEmpresa, 3, idSector);
            return View(reune);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("condusef/reune/consultar-aclaraciones-general-post", Name = "Consultar_Aclaraciones_Empresa")]
        public JsonResult Consultar_Aclaraciones_Empresa(string anio, string periodo)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            Condusef_DLL.Funciones.Condusef.Reune reune = new();

            try
            {
                var user = HttpContext.User; // Obtiene el principal del usuario actual
                var idEmpresa = user?.FindFirst(ClaimTypes.GroupSid)?.Value;
                if (idEmpresa != null)
                {
                    resultado = reune.ConsultarAclaraciones(idEmpresa, periodo, anio);
                }

            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef.ReuneController";
                log.Log.metodo = "Consultar_Aclaraciones_Empresa";
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
                listado = reune.ListadoAclaracionesGenerales
            };
            return new JsonResult(response);
        }

        [Route("condusef/reune/layout-aclaraciones", Name = "SubirLayoutAclaraciones")]
        public IActionResult SubirLayoutAclaraciones()
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
            Condusef_DLL.Funciones.Condusef.Reune reune = new();
            var idEmpresa = HttpContext.User?.FindFirst(ClaimTypes.GroupSid)?.Value;
            var idSector = HttpContext.User?.FindFirst("IdSector")?.Value;
            reune.CargarCatalogos(idEmpresa, 1, idSector);
            return View(reune);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("condusef/reune/subir-layout-aclaraciones", Name = "Subir_Layout_Aclaraciones")]
        public async Task<JsonResult> Subir_Layout_Aclaraciones([FromForm] IFormFile layout)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            Condusef_DLL.Funciones.Condusef.Reune reune = new();

            try
            {
                string path = Path.GetFullPath("Documentos/Temp");
                path = Path.Combine(path, layout.FileName);
                ClsStreamFile file = new(layout.OpenReadStream(), path, layout.FileName);
                resultado = await reune.ProcesarLayoutAclaraciones(file);
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef.ReuneControllers";
                log.Log.metodo = "Subir_Layout_Aclaraciones";
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
                data = reune.ListadoAclaracionesLayout
            };
            return new JsonResult(response);
        }

        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("condusef/reune/consultar-respuesta-json", Name = "Reune_Consultar_Respuesta_Json")]
        public JsonResult Reune_Consultar_Respuesta_Json(string folio, string vista)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            Condusef_DLL.Funciones.Condusef.Reune reune = new();

            try
            {
                var user = HttpContext.User; // Obtiene el principal del usuario actual
                var idEmpresa = user?.FindFirst(ClaimTypes.GroupSid)?.Value;
                if (idEmpresa != null)
                {
                    resultado = reune.ConsultarRespuestaJson(idEmpresa, folio, vista);
                }

            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef.ReuneController";
                log.Log.metodo = "Reune_Consultar_Respuesta_Json";
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

        #region catalogos

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("condusef/reune/consultar-causas-producto", Name = "Reune_Consultar_Causas_Producto")]
        public JsonResult Reune_Consultar_Causas_Producto(string idProducto, int vista)
        {
            ClsResultadoAccion resultado = new();
            Condusef_DLL.Funciones.Condusef.Reune modelo = new();
            try
            {
                //var user = HttpContext.User; // Obtiene el principal del usuario actual
                //var groupSidClaim = user?.FindFirst(ClaimTypes.GroupSid);

                //if (groupSidClaim != null)
                //{
                //    var idEmpresa = groupSidClaim.Value;
                //    modelo.ConsultaCausasProducto("", idProducto, vista);
                //    resultado.Estatus = true;
                //}
                var idSector = HttpContext.User?.FindFirst("IdSector")?.Value;
                modelo.ConsultaCausasProducto("", idProducto, vista, idSector);
                resultado.Estatus = true;
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef.ReuneController";
                log.Log.metodo = "Reune_Consultar_Causas_Producto";
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
                data = modelo.CausasProductos
            };
            return new JsonResult(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("condusef/reune/consultar-municipios", Name = "Reune_Consultar_Municipios")]
        public JsonResult Reune_Consultar_Municipios(string idEstado)
        {
            ClsResultadoAccion resultado = new();
            Condusef_DLL.Funciones.Condusef.Reune modelo = new();
            try
            {
                resultado = modelo.ConsultaMunicipios(idEstado);
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef.ReuneController";
                log.Log.metodo = "Reune_Consultar_Municipios";
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
                data = modelo.Municipios
            };
            return new JsonResult(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("condusef/reune/consultar-cp", Name = "Reune_Consultar_CP")]
        public JsonResult Reune_Consultar_CP(string idEstado, string idMunicipio)
        {
            ClsResultadoAccion resultado = new();
            Condusef_DLL.Funciones.Condusef.Reune modelo = new();
            try
            {
                resultado = modelo.ConsultaCodigosPostales(idEstado, idMunicipio);
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef.ReuneController";
                log.Log.metodo = "Reune_Consultar_CP";
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
                data = modelo.CodigosPostales
            };
            return new JsonResult(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("condusef/reune/consultar-colonias", Name = "Reune_Consultar_Colonias")]
        public JsonResult Reune_Consultar_Colonias(string idEstado, string idMunicipio, string cp)
        {
            ClsResultadoAccion resultado = new();
            Condusef_DLL.Funciones.Condusef.Reune modelo = new();
            try
            {
                resultado = modelo.ConsultaColonias(idEstado, idMunicipio, cp);
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef.ReuneController";
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
                data = modelo.Colonias
            };
            return new JsonResult(response);
        }

        #endregion
    }
}
