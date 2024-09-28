using Condusef_DLL.Clases;
using Condusef_DLL.Clases.Condusef;
using Condusef_DLL.Clases.Condusef.API;
using Condusef_DLL.Clases.Condusef.Configuracion;
using Condusef_DLL.Clases.General;
using Condusef_DLL.Clases.Generales;
using Condusef_DLL.Funciones.Generales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Condusef_DLL.Funciones.Condusef
{
    public class ClsColonia
    {
        public int estadoId { get; set; }
        public string estado { get; set; }
        public int municipioId { get; set; }
        public string municipio { get; set; }
        public int coloniaId { get; set; }
        public string colonia { get; set; }
        public int? tipoLocalidadId { get; set; }
        public string? tipoLocalidad { get; set; }
        public ClsColonia()
        {
            estadoId = 0;
            estado = string.Empty;
            municipioId = 0;
            municipio = string.Empty;
            coloniaId = 0;
            colonia = string.Empty;
            tipoLocalidadId = 0;
            tipoLocalidad = string.Empty;
        }
    }

    public class RespuestaColonias
    {
        public List<ClsColonia> colonias { get; set; }
        public RespuestaColonias()
        {
            colonias = new List<ClsColonia>();
        }
    }

    public class codigoPostal {
        public int estadoId { get; set; }
        public string estado { get; set; }
        public int codigo_sepomex { get; set; }
        public codigoPostal()
        {
            estadoId = 0;
            estado = string.Empty;
            codigo_sepomex = 0;
        }
    }
    
    public class RespuestaCodigoPostal
    {
        public List<codigoPostal> codigos_postales { get; set; }
        public RespuestaCodigoPostal()
        {
            codigos_postales = new List<codigoPostal>();
        }
    }

    public class Condusef
    {
        public RespuestaCodigoPostal _RespuestaCodigoPostal { get; set; }
        public RespuestaColonias _RespuestaColonias { get; set; }
        public RespuestaSuperUser _RespuestaSuperUser { get; set; }

        public Condusef() { 
            _RespuestaCodigoPostal = new RespuestaCodigoPostal();
            _RespuestaSuperUser = new RespuestaSuperUser();
            _RespuestaColonias = new RespuestaColonias();
        }

        #region Configuracion
        public ClsResultadoAccion ActualizarToken(string idEmpresa, string tokenRedeco, string tokenReune)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            try
            {
                string redeco = FntEncriptar.Encriptar(tokenRedeco);
                string reune = FntEncriptar.Encriptar(tokenReune);
                string[] parametros = { "idEmpresa","tokenRedeco","tokenReune" };
                string[] values = { idEmpresa, redeco, reune };
                BD_DLL.ClsBD.Consulta_con_parametros("empresa.actualizar_tokens_empresa", Conexion.CadenaConexion, parametros, values, Conexion.isolationLevelSnap());
                resultado.Estatus = true;
            }
            catch (Exception ex)
            {
                //FntLog log = new FntLog();
                //log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef";
                //log.Log.metodo = "ConsultaAqlist";
                //log.Log.error = ex.Message;
                //log.insertaLog();
                resultado.Estatus = false;
            }
            return resultado;
        }

        public async Task<ClsResultadoAccion> RenovarTokenUsuario(string idUsuario, string username, string idServicio)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            ClsRenovarToken respuestaToken = new ClsRenovarToken();
            string apiPassword = string.Empty;
            try
            {
                apiPassword = Db_ConsultaPasswordUsuario(idUsuario);
                if (apiPassword != string.Empty)
                {
                    if(idServicio == "1") respuestaToken = await Api_RenovarToken(username, apiPassword);
                    else respuestaToken = await Api_ReuneRenovarToken(username, apiPassword);
                    if (respuestaToken.resultado.Estatus)
                    {
                        DateTime fechaActual = DateTime.Now;

                        // Aumentar la fecha en 30 días
                        DateTime fechaAumentada = fechaActual.AddDays(30);

                        // Convertir la fecha aumentada a la representación de cadena deseada
                        string fechaAumentadaString = fechaAumentada.ToString("yyyyMMdd HH:mm:ss.fff");

                        string token = FntEncriptar.Encriptar(respuestaToken.user.token_access);
                        string[] parametros = { "idUsuario", "idServicio", "token", "fechaExpiracion" };
                        string[] values = { idUsuario.ToString(), idServicio, token, fechaAumentadaString };
                        BD_DLL.ClsBD.Consulta_con_parametros("usuario.actualizar_token_usuario", Conexion.CadenaConexion, parametros, values, Conexion.isolationLevelSnap());
                        resultado.Estatus = true;
                        resultado.Detalle = respuestaToken.user.token_access;
                    }
                }
                
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef";
                log.Log.metodo = "RenovarTokenUsuario";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
            }
            return resultado;
        }

        private string Db_ConsultaPasswordUsuario(string idUsuario)
        {
            ClsResultadoAccion resultado = new();
            string apiPassword = string.Empty;

            try
            {
                string[] parametros = { "idUsuario" };
                string[] values = { idUsuario };
                DataSet resultadoConsulta = BD_DLL.ClsBD.Consulta_con_parametros("usuario.consulta_api_password_usuario", Conexion.CadenaConexion,
                    parametros, values, Conexion.isolationLevelSnap());
                if (resultadoConsulta.Tables.Count > 0)
                {
                    if (resultadoConsulta.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow fila in resultadoConsulta.Tables[0].Rows)
                        {
                            apiPassword = FntEncriptar.Desencriptar(FntGenericas.ValidaNullString(fila["api_password"].ToString(), ""));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef";
                log.Log.metodo = "Db_ConsultaPasswordUsuario";
                log.Log.error = ex.Message;
                log.insertaLog();
                apiPassword = string.Empty;
            }
            return apiPassword;
        }

        public async Task<ClsResultadoAccion> ConsultaTokenUsuario(string idUsuario, string idServicio)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            string[] parametro = new string[] { "idUsuario", "idServicio" };
            string[] valorParametro = new string[] { idUsuario, idServicio };
            try
            {
                string token = string.Empty;
                DateTime fechaExpiracion = new DateTime();
                DataSet resultadoConsulta = BD_DLL.ClsBD.Consulta_con_parametros("usuario.consulta_token_usuario", Conexion.CadenaConexion, parametro, valorParametro, Conexion.isolationLevelSnap());
                if (resultadoConsulta.Tables.Count > 0)
                {
                    if(resultadoConsulta.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow fila in resultadoConsulta.Tables[0].Rows)
                        {
                            //resultado.Detalle = FntGenericas.ValidaNullString(fila["token"].ToString(), "");
                            token = FntEncriptar.Desencriptar(FntGenericas.ValidaNullString(fila["token"].ToString(), ""));
                            fechaExpiracion = FntGenericas.ValidaNullDateTime(fila["fecha_expiracion"].ToString(), Convert.ToDateTime("01/01/1990"));
                        }
                        if(token != "")
                        {
                            if (fechaExpiracion < DateTime.Now)
                            {
                                resultado = await RenovarTokenUsuario(idUsuario, Conexion.Username, idServicio);
                            }
                            else
                            {
                                resultado.Detalle = token;
                            }
                            resultado.Estatus = true;
                        }
                    }
                    else
                    {
                        resultado.Estatus = false;
                        resultado.Code = 403;
                    }
                }
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef";
                log.Log.metodo = "ConsultaTokenUsuario";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
            }
            return resultado;
        }

        public ClsResultadoAccion CambiaTokenUsuario(string idUsuario, string token, string idServicio)
        {
            string tokenEncriptado = FntEncriptar.Encriptar(token);
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            string[] parametro = new string[] { "idUsuario", "token", "idServicio"  };
            string[] valorParametro = new string[] { idUsuario, tokenEncriptado, idServicio };
            try
            {
                BD_DLL.ClsBD.Consulta_con_parametros("usuario.actualizar_token_manual", Conexion.CadenaConexion, parametro, valorParametro, Conexion.isolationLevelSnap());
                resultado.Estatus = true;
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef";
                log.Log.metodo = "CambiaTokenUsuario";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
            }
            return resultado;
        }

        #endregion

        #region API CONDUSEF REDECO
        public async Task<ClsRenovarToken> Api_RenovarToken(string username, string password)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            ClsRenovarToken respuestaToken = new ClsRenovarToken();
            try
            {
                string apiUrl = VariablesGlobales.Url.Redeco_RenovarToken;
                // Crear una instancia de HttpClient
                using (HttpClient client = new HttpClient())
                {
                    PeticionRenovarToken peticion = new(username, password);
                    // Convertir los datos a formato JSON
                    string json = JsonSerializer.Serialize(peticion);

                    HttpRequestMessage request = new HttpRequestMessage();
                    HttpResponseMessage response2 = new HttpResponseMessage();

                    request.RequestUri = new Uri(apiUrl);
                    request.Method = HttpMethod.Get;
                    client.DefaultRequestHeaders.Add("Accept", "application/json;charset=UTF-8");

                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    request.Content = content;

                    client.Timeout = TimeSpan.FromMinutes(15); // Aumentar el tiempo de espera a 5 minutos
                                                               // Realizar la solicitud POST
                    response2 = client.SendAsync(request).Result;

                    // Verificar si la respuesta fue exitosa
                    if (response2.IsSuccessStatusCode)
                    {
                        // Leer la respuesta como una cadena de texto
                        string responseJson = await response2.Content.ReadAsStringAsync();
                        ClsRenovarToken respuesta = JsonSerializer.Deserialize<ClsRenovarToken>(responseJson);
                        respuestaToken = respuesta;
                        resultado.Estatus = true;
                    }
                    else
                    {
                        string responseJson = await response2.Content.ReadAsStringAsync();
                        resultado.Estatus = false;
                        resultado.Code = (int)response2.StatusCode;

                        FntLog.insertaLogApi(Constantes.REDECO, "Renovar Token", resultado.Code, json, responseJson);
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.Estatus = false;
                resultado.Detalle = $"Error en la solicitud: {ex.Message}";
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef";
                log.Log.metodo = "Api_RenovarToken";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            respuestaToken.resultado = resultado;
            return respuestaToken;
        }

        public async Task<ClsResultadoAccion> Api_CrearSuperUsuario(string key, string username, string password)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            ClsRenovarToken respuestaToken = new ClsRenovarToken();
            try
            {
                string apiUrl = VariablesGlobales.Url.Redeco_CrearSuperUsuario;
                // Crear una instancia de HttpClient
                using (HttpClient client = new HttpClient())
                {
                    PeticionSuperUser peticion = new(key, username, password, password);
                    // Convertir los datos a formato JSON
                    string json = JsonSerializer.Serialize(peticion);

                    HttpRequestMessage request = new HttpRequestMessage();
                    HttpResponseMessage response2 = new HttpResponseMessage();

                    request.RequestUri = new Uri(apiUrl);
                    request.Method = HttpMethod.Post;
                    client.DefaultRequestHeaders.Add("Accept", "application/json;charset=UTF-8");

                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    request.Content = content;

                    client.Timeout = TimeSpan.FromMinutes(15); // Aumentar el tiempo de espera a 5 minutos
                                                               // Realizar la solicitud POST
                    response2 = client.SendAsync(request).Result;

                    // Verificar si la respuesta fue exitosa
                    if (response2.IsSuccessStatusCode)
                    {
                        // Leer la respuesta como una cadena de texto
                        string responseJson = await response2.Content.ReadAsStringAsync();
                        RespuestaSuperUser respuesta = JsonSerializer.Deserialize<RespuestaSuperUser>(responseJson);
                        resultado.Estatus = true;
                        _RespuestaSuperUser = respuesta;
                    }
                    else
                    {
                        string responseJson = await response2.Content.ReadAsStringAsync();
                        resultado.Code = (int)response2.StatusCode;

                        FntLog.insertaLogApi(Constantes.REDECO, "Crear Super Usuario", resultado.Code, json, responseJson);

                        ClsErrorPeticion respuesta = JsonSerializer.Deserialize<ClsErrorPeticion>(responseJson);
                        
                        if (resultado.Code == 500) resultado.Detalle = respuesta.msg;
                        else if (resultado.Code == 400) resultado.Detalle = respuesta.error;
                        else if (resultado.Code == 401 && respuesta.error == "La key proporcionada no es válida") resultado.Code = 401;

                        resultado.Estatus = false;
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.Estatus = false;
                resultado.Detalle = $"Error en la solicitud: {ex.Message}";
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef";
                log.Log.metodo = "Api_CrearSuperUsuario";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            respuestaToken.resultado = resultado;
            return resultado;
        }

        public async Task<ClsResultadoAccion> Api_CrearUsuario(string authorization, string username, string password)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            try
            {
                string apiUrl = VariablesGlobales.Url.Redeco_CrearUsuario;
                // Crear una instancia de HttpClient
                using (HttpClient client = new HttpClient())
                {
                    PeticionUsuario peticion = new(username, password, password);
                    // Convertir los datos a formato JSON
                    string json = JsonSerializer.Serialize(peticion);

                    HttpRequestMessage request = new HttpRequestMessage();
                    HttpResponseMessage response2 = new HttpResponseMessage();

                    request.RequestUri = new Uri(apiUrl);
                    request.Method = HttpMethod.Post;
                    client.DefaultRequestHeaders.Add("Accept", "application/json;charset=UTF-8");

                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    request.Headers.Authorization = new AuthenticationHeaderValue(authorization);
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    request.Content = content;

                    client.Timeout = TimeSpan.FromMinutes(15); // Aumentar el tiempo de espera a 5 minutos
                                                               // Realizar la solicitud POST
                    response2 = client.SendAsync(request).Result;

                    // Verificar si la respuesta fue exitosa
                    if (response2.IsSuccessStatusCode)
                    {
                        // Leer la respuesta como una cadena de texto
                        string responseJson = await response2.Content.ReadAsStringAsync();
                        RespuestaSuperUser respuesta = JsonSerializer.Deserialize<RespuestaSuperUser>(responseJson);
                        resultado.Estatus = true;
                        resultado.Detalle = respuesta.data.token_access;
                    }
                    else
                    {
                        string responseJson = await response2.Content.ReadAsStringAsync();
                        resultado.Code = (int)response2.StatusCode;
                        FntLog.insertaLogApi(Constantes.REDECO, "Crear Usuario", resultado.Code, json, responseJson);

                        ClsErrorPeticion respuesta = JsonSerializer.Deserialize<ClsErrorPeticion>(responseJson);
                        if (resultado.Code == 500) resultado.Detalle = respuesta.msg;
                        else if (resultado.Code == 400) resultado.Detalle = respuesta.error;

                        resultado.Estatus = false;
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.Estatus = false;
                resultado.Detalle = $"Error en la solicitud: {ex.Message}";
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef";
                log.Log.metodo = "Api_CrearUsuario";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return resultado;
        }

        private async Task<ClsRenovarToken> Api_ListaUsuario(string authorization, string username, string password)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            ClsRenovarToken respuestaToken = new ClsRenovarToken();
            try
            {
                string apiUrl = VariablesGlobales.Url.Redeco_CrearSuperUsuario;
                // Crear una instancia de HttpClient
                using (HttpClient client = new HttpClient())
                {
                    PeticionSuperUser peticion = new("", username, password, password);
                    // Convertir los datos a formato JSON
                    string json = JsonSerializer.Serialize(peticion);

                    HttpRequestMessage request = new HttpRequestMessage();
                    HttpResponseMessage response2 = new HttpResponseMessage();

                    request.RequestUri = new Uri(apiUrl);
                    request.Method = HttpMethod.Post;
                    client.DefaultRequestHeaders.Add("Accept", "application/json;charset=UTF-8");

                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    request.Headers.Authorization = new AuthenticationHeaderValue(authorization);
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    request.Content = content;

                    client.Timeout = TimeSpan.FromMinutes(15); // Aumentar el tiempo de espera a 5 minutos
                                                               // Realizar la solicitud POST
                    response2 = client.SendAsync(request).Result;

                    // Verificar si la respuesta fue exitosa
                    if (response2.IsSuccessStatusCode)
                    {
                        // Leer la respuesta como una cadena de texto
                        string responseJson = await response2.Content.ReadAsStringAsync();
                        RespuestaSuperUser respuesta = JsonSerializer.Deserialize<RespuestaSuperUser>(responseJson);
                        resultado.Estatus = true;
                    }
                    else
                    {
                        string responseJson = await response2.Content.ReadAsStringAsync();
                        resultado.Estatus = false;
                        resultado.Code = (int)response2.StatusCode;
                        FntLog.insertaLogApi(Constantes.REDECO, "Obtener Usuarios", resultado.Code, json, responseJson);
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.Estatus = false;
                resultado.Detalle = $"Error en la solicitud: {ex.Message}";
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef";
                log.Log.metodo = "Api_ListaUsuario";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            respuestaToken.resultado = resultado;
            return respuestaToken;
        }

        private async Task<ClsRenovarToken> Api_EliminarUsuario(string authorization, string username)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            ClsRenovarToken respuestaToken = new ClsRenovarToken();
            try
            {
                string apiUrl = VariablesGlobales.Url.Redeco_CrearSuperUsuario;
                // Crear una instancia de HttpClient
                using (HttpClient client = new HttpClient())
                {
                    var peticion = new
                    {
                        username = username
                    };
                    // Convertir los datos a formato JSON
                    string json = JsonSerializer.Serialize(peticion);

                    HttpRequestMessage request = new HttpRequestMessage();
                    HttpResponseMessage response2 = new HttpResponseMessage();

                    request.RequestUri = new Uri(apiUrl);
                    request.Method = HttpMethod.Delete;
                    client.DefaultRequestHeaders.Add("Accept", "application/json;charset=UTF-8");

                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    request.Headers.Authorization = new AuthenticationHeaderValue(authorization);
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    request.Content = content;

                    client.Timeout = TimeSpan.FromMinutes(15); // Aumentar el tiempo de espera a 5 minutos
                                                               // Realizar la solicitud POST
                    response2 = client.SendAsync(request).Result;

                    // Verificar si la respuesta fue exitosa
                    if (response2.IsSuccessStatusCode)
                    {
                        // Leer la respuesta como una cadena de texto
                        string responseJson = await response2.Content.ReadAsStringAsync();
                        RespuestaSuperUser respuesta = JsonSerializer.Deserialize<RespuestaSuperUser>(responseJson);
                        resultado.Estatus = true;
                    }
                    else
                    {
                        string responseJson = await response2.Content.ReadAsStringAsync();
                        resultado.Estatus = false;
                        resultado.Code = (int)response2.StatusCode;

                        FntLog.insertaLogApi(Constantes.REDECO, "Eliminar Usuario", resultado.Code, json, responseJson);
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.Estatus = false;
                resultado.Detalle = $"Error en la solicitud: {ex.Message}";
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef";
                log.Log.metodo = "Api_EliminarUsuario";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            respuestaToken.resultado = resultado;
            return respuestaToken;
        }

        #endregion

        #region API CONDUSEF REUNE
        public async Task<ClsRenovarToken> Api_ReuneRenovarToken(string username, string password)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            ClsRenovarToken respuestaToken = new ClsRenovarToken();
            try
            {
                string apiUrl = VariablesGlobales.Url.Reune_RenovarToken;
                // Crear una instancia de HttpClient
                using (HttpClient client = new HttpClient())
                {
                    PeticionRenovarToken peticion = new(username+"_reune", password);
                    // Convertir los datos a formato JSON
                    string json = JsonSerializer.Serialize(peticion);

                    HttpRequestMessage request = new HttpRequestMessage();
                    HttpResponseMessage response2 = new HttpResponseMessage();

                    request.RequestUri = new Uri(apiUrl);
                    request.Method = HttpMethod.Get;
                    client.DefaultRequestHeaders.Add("Accept", "application/json;charset=UTF-8");

                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    request.Content = content;

                    client.Timeout = TimeSpan.FromMinutes(15); // Aumentar el tiempo de espera a 5 minutos
                                                               // Realizar la solicitud POST
                    response2 = client.SendAsync(request).Result;

                    // Verificar si la respuesta fue exitosa
                    if (response2.IsSuccessStatusCode)
                    {
                        // Leer la respuesta como una cadena de texto
                        string responseJson = await response2.Content.ReadAsStringAsync();
                        ClsRenovarToken respuesta = JsonSerializer.Deserialize<ClsRenovarToken>(responseJson);
                        respuestaToken = respuesta;
                        resultado.Estatus = true;
                    }
                    else
                    {
                        string responseJson = await response2.Content.ReadAsStringAsync();
                        resultado.Code = (int)response2.StatusCode;
                        resultado.Estatus = false;
                        FntLog.insertaLogApi(Constantes.REUNE, "Renovar Token", resultado.Code, json, responseJson);
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.Estatus = false;
                resultado.Detalle = $"Error en la solicitud: {ex.Message}";
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef.Reune";
                log.Log.metodo = "Api_ReuneRenovarToken";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            respuestaToken.resultado = resultado;
            return respuestaToken;
        }

        public async Task<ClsResultadoAccion> Api_ReuneCrearSuperUsuario(string key, string username, string password)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            ClsRenovarToken respuestaToken = new ClsRenovarToken();
            try
            {
                string apiUrl = VariablesGlobales.Url.Reune_CrearSuperUsuario;
                // Crear una instancia de HttpClient
                using (HttpClient client = new HttpClient())
                {
                    PeticionSuperUser peticion = new(key, username+"_reune", password, password);
                    // Convertir los datos a formato JSON
                    string json = JsonSerializer.Serialize(peticion);

                    HttpRequestMessage request = new HttpRequestMessage();
                    HttpResponseMessage response2 = new HttpResponseMessage();

                    request.RequestUri = new Uri(apiUrl);
                    request.Method = HttpMethod.Post;
                    client.DefaultRequestHeaders.Add("Accept", "application/json;charset=UTF-8");

                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    request.Content = content;

                    client.Timeout = TimeSpan.FromMinutes(15); // Aumentar el tiempo de espera a 5 minutos
                                                               // Realizar la solicitud POST
                    response2 = client.SendAsync(request).Result;

                    // Verificar si la respuesta fue exitosa
                    if (response2.IsSuccessStatusCode)
                    {
                        // Leer la respuesta como una cadena de texto
                        string responseJson = await response2.Content.ReadAsStringAsync();
                        RespuestaSuperUser respuesta = JsonSerializer.Deserialize<RespuestaSuperUser>(responseJson);
                        resultado.Estatus = true;
                        _RespuestaSuperUser = respuesta;
                    }
                    else
                    {
                        string responseJson = await response2.Content.ReadAsStringAsync();
                        resultado.Code = (int)response2.StatusCode;
                        FntLog.insertaLogApi(Constantes.REUNE, "Crear Super Usuario", resultado.Code, json, responseJson);

                        ClsErrorPeticion respuesta = JsonSerializer.Deserialize<ClsErrorPeticion>(responseJson);

                        if (resultado.Code == 500) resultado.Detalle = respuesta.msg;
                        else if (resultado.Code == 400) resultado.Detalle = respuesta.error;
                        else if (resultado.Code == 401 && respuesta.error == "La key proporcionada no es válida") resultado.Code = 401;

                        resultado.Estatus = false;
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.Estatus = false;
                resultado.Detalle = $"Error en la solicitud: {ex.Message}";
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef.Reune";
                log.Log.metodo = "Api_ReuneCrearSuperUsuario";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            respuestaToken.resultado = resultado;
            return resultado;
        }

        public async Task<ClsResultadoAccion> Api_ReuneCrearUsuario(string authorization, string username, string password)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            try
            {
                string apiUrl = VariablesGlobales.Url.Reune_CrearUsuario;
                // Crear una instancia de HttpClient
                using (HttpClient client = new HttpClient())
                {
                    PeticionUsuario peticion = new(username + "_reune", password, password);
                    // Convertir los datos a formato JSON
                    string json = JsonSerializer.Serialize(peticion);

                    HttpRequestMessage request = new HttpRequestMessage();
                    HttpResponseMessage response2 = new HttpResponseMessage();

                    request.RequestUri = new Uri(apiUrl);
                    request.Method = HttpMethod.Post;
                    client.DefaultRequestHeaders.Add("Accept", "application/json;charset=UTF-8");

                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    request.Headers.Authorization = new AuthenticationHeaderValue(authorization);
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    request.Content = content;

                    client.Timeout = TimeSpan.FromMinutes(15); // Aumentar el tiempo de espera a 5 minutos
                                                               // Realizar la solicitud POST
                    response2 = client.SendAsync(request).Result;

                    // Verificar si la respuesta fue exitosa
                    if (response2.IsSuccessStatusCode)
                    {
                        // Leer la respuesta como una cadena de texto
                        string responseJson = await response2.Content.ReadAsStringAsync();
                        RespuestaSuperUser respuesta = JsonSerializer.Deserialize<RespuestaSuperUser>(responseJson);
                        resultado.Estatus = true;
                        resultado.Detalle = respuesta.data.token_access;
                    }
                    else
                    {
                        string responseJson = await response2.Content.ReadAsStringAsync();
                        resultado.Code = (int)response2.StatusCode;
                        FntLog.insertaLogApi(Constantes.REUNE, "Crear Usuario", resultado.Code, json, responseJson);
                        ClsErrorPeticion respuesta = JsonSerializer.Deserialize<ClsErrorPeticion>(responseJson);

                        if (resultado.Code == 500) resultado.Detalle = respuesta.msg;
                        else if (resultado.Code == 400) resultado.Detalle = respuesta.error;
                        resultado.Estatus = false;
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.Estatus = false;
                resultado.Detalle = $"Error en la solicitud: {ex.Message}";
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef";
                log.Log.metodo = "Api_ReuneCrearUsuario";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return resultado;
        }

        //private async Task<ClsRenovarToken> Api_ReuneListaUsuario(string authorization, string username, string password)
        //{
        //    ClsResultadoAccion resultado = new ClsResultadoAccion();
        //    ClsRenovarToken respuestaToken = new ClsRenovarToken();
        //    try
        //    {
        //        string apiUrl = VariablesGlobales.Url.Redeco_CrearSuperUsuario;
        //        // Crear una instancia de HttpClient
        //        using (HttpClient client = new HttpClient())
        //        {
        //            PeticionSuperUser peticion = new("", username, password, password);
        //            // Convertir los datos a formato JSON
        //            string json = JsonSerializer.Serialize(peticion);

        //            HttpRequestMessage request = new HttpRequestMessage();
        //            HttpResponseMessage response2 = new HttpResponseMessage();

        //            request.RequestUri = new Uri(apiUrl);
        //            request.Method = HttpMethod.Post;
        //            client.DefaultRequestHeaders.Add("Accept", "application/json;charset=UTF-8");

        //            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //            request.Headers.Authorization = new AuthenticationHeaderValue(authorization);
        //            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
        //            request.Content = content;

        //            client.Timeout = TimeSpan.FromMinutes(15); // Aumentar el tiempo de espera a 5 minutos
        //                                                       // Realizar la solicitud POST
        //            response2 = client.SendAsync(request).Result;

        //            // Verificar si la respuesta fue exitosa
        //            if (response2.IsSuccessStatusCode)
        //            {
        //                // Leer la respuesta como una cadena de texto
        //                string responseJson = await response2.Content.ReadAsStringAsync();
        //                RespuestaSuperUser respuesta = JsonSerializer.Deserialize<RespuestaSuperUser>(responseJson);
        //                resultado.Estatus = true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        resultado.Estatus = false;
        //        resultado.Detalle = $"Error en la solicitud: {ex.Message}";
        //        FntLog log = new FntLog();
        //        log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef";
        //        log.Log.metodo = "Api_ReuneListaUsuario";
        //        log.Log.error = ex.Message;
        //        log.insertaLog();
        //    }
        //    respuestaToken.resultado = resultado;
        //    return respuestaToken;
        //}

        //private async Task<ClsRenovarToken> Api_ReuneEliminarUsuario(string authorization, string username)
        //{
        //    ClsResultadoAccion resultado = new ClsResultadoAccion();
        //    ClsRenovarToken respuestaToken = new ClsRenovarToken();
        //    try
        //    {
        //        string apiUrl = VariablesGlobales.Url.Redeco_CrearSuperUsuario;
        //        // Crear una instancia de HttpClient
        //        using (HttpClient client = new HttpClient())
        //        {
        //            var peticion = new
        //            {
        //                username = username
        //            };
        //            // Convertir los datos a formato JSON
        //            string json = JsonSerializer.Serialize(peticion);

        //            HttpRequestMessage request = new HttpRequestMessage();
        //            HttpResponseMessage response2 = new HttpResponseMessage();

        //            request.RequestUri = new Uri(apiUrl);
        //            request.Method = HttpMethod.Delete;
        //            client.DefaultRequestHeaders.Add("Accept", "application/json;charset=UTF-8");

        //            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //            request.Headers.Authorization = new AuthenticationHeaderValue(authorization);
        //            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
        //            request.Content = content;

        //            client.Timeout = TimeSpan.FromMinutes(15); // Aumentar el tiempo de espera a 5 minutos
        //                                                       // Realizar la solicitud POST
        //            response2 = client.SendAsync(request).Result;

        //            // Verificar si la respuesta fue exitosa
        //            if (response2.IsSuccessStatusCode)
        //            {
        //                // Leer la respuesta como una cadena de texto
        //                string responseJson = await response2.Content.ReadAsStringAsync();
        //                RespuestaSuperUser respuesta = JsonSerializer.Deserialize<RespuestaSuperUser>(responseJson);
        //                resultado.Estatus = true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        resultado.Estatus = false;
        //        resultado.Detalle = $"Error en la solicitud: {ex.Message}";
        //        FntLog log = new FntLog();
        //        log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef";
        //        log.Log.metodo = "Api_ReuneEliminarUsuario";
        //        log.Log.error = ex.Message;
        //        log.insertaLog();
        //    }
        //    respuestaToken.resultado = resultado;
        //    return respuestaToken;
        //}

        #endregion

        #region API CONDUSEF Catalogos

        public async Task<ClsResultadoAccion> ObtenerCatalogosCP()
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            try
            {
                for (int i = 5; i <= 5; i++)
                {
                    resultado = await Api_CatalogoCP(i);
                    if (resultado.Estatus)
                    {
                        Parallel.ForEach(_RespuestaCodigoPostal.codigos_postales, async (codigo, loopState) =>
                        {
                            string cp = codigo.codigo_sepomex <= 9999 ? $"0{codigo.codigo_sepomex}" : codigo.codigo_sepomex.ToString();
                            //Db_InsertaCP(codigo.estadoId.ToString(), cp);
                            resultado = await Api_CatalogoColonias(cp);
                            if (resultado.Estatus)
                            {
                                Parallel.ForEach(_RespuestaColonias.colonias, (col, loopState) =>
                                {
                                    Db_InsertaColonia(col.estadoId, col.municipioId, col.municipio, cp, col.coloniaId, col.colonia, col.tipoLocalidadId, col.tipoLocalidad);
                                });
                            }
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef";
                log.Log.metodo = "ObtenerCatalogosCP";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return resultado;
        }

        private async Task<ClsResultadoAccion> Api_CatalogoCP(int idEstado)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            RespuestaCodigoPostal respuesta = new RespuestaCodigoPostal();
            try
            {
                string apiUrl = VariablesGlobales.Url.ConsultaCP;
                string urlWithParameters = $"{apiUrl}?estado_id={idEstado}";
                // Crear una instancia de HttpClient
                using (HttpClient client = new HttpClient())
                {

                    HttpRequestMessage request = new HttpRequestMessage();
                    HttpResponseMessage response2 = new HttpResponseMessage();

                    request.RequestUri = new Uri(urlWithParameters);
                    request.Method = HttpMethod.Get;
                    client.DefaultRequestHeaders.Add("Accept", "application/json;charset=UTF-8");

                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    client.Timeout = TimeSpan.FromMinutes(15); // Aumentar el tiempo de espera a 5 minutos
                                                               // Realizar la solicitud POST
                    response2 = client.SendAsync(request).Result;

                    // Verificar si la respuesta fue exitosa
                    if (response2.IsSuccessStatusCode)
                    {
                        // Leer la respuesta como una cadena de texto
                        string responseJson = await response2.Content.ReadAsStringAsync();
                        respuesta = JsonSerializer.Deserialize<RespuestaCodigoPostal>(responseJson);
                        resultado.Estatus = true;
                        _RespuestaCodigoPostal = respuesta;
                    }
                    else
                    {
                        string responseJson = await response2.Content.ReadAsStringAsync();
                        resultado.Estatus = false;
                        resultado.Code = (int)response2.StatusCode;

                        FntLog.insertaLogApi(Constantes.REDECO, "Descargar Catalogo CP", resultado.Code, urlWithParameters, responseJson);
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.Estatus = false;
                resultado.Detalle = $"Error en la solicitud: {ex.Message}";
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef";
                log.Log.metodo = "Api_CatalogoCP";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            //respuestaToken.resultado = resultado;
            return resultado;
        }

        private async Task<ClsResultadoAccion> Api_CatalogoColonias(string cp)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            RespuestaColonias respuesta = new RespuestaColonias();
            string resJson = "";
            try
            {
                string apiUrl = VariablesGlobales.Url.ConsultaColonias;
                string urlWithParameters = $"{apiUrl}?cp={cp}";
                // Crear una instancia de HttpClient
                using (HttpClient client = new HttpClient())
                {

                    HttpRequestMessage request = new HttpRequestMessage();
                    HttpResponseMessage response2 = new HttpResponseMessage();

                    request.RequestUri = new Uri(urlWithParameters);
                    request.Method = HttpMethod.Get;
                    client.DefaultRequestHeaders.Add("Accept", "application/json;charset=UTF-8");

                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    client.Timeout = TimeSpan.FromMinutes(15); // Aumentar el tiempo de espera a 5 minutos
                                                               // Realizar la solicitud POST
                    response2 = client.SendAsync(request).Result;

                    // Verificar si la respuesta fue exitosa
                    if (response2.IsSuccessStatusCode)
                    {
                        // Leer la respuesta como una cadena de texto
                        string responseJson = await response2.Content.ReadAsStringAsync();
                        resJson = responseJson;
                        respuesta = JsonSerializer.Deserialize<RespuestaColonias>(responseJson);
                        resultado.Estatus = true;
                        _RespuestaColonias = respuesta;
                    }
                    else
                    {
                        string responseJson = await response2.Content.ReadAsStringAsync();
                        resultado.Estatus = false;
                        resultado.Code = (int)response2.StatusCode;

                        FntLog.insertaLogApi(Constantes.REDECO, "Descargar Catalogo Colonias", resultado.Code, urlWithParameters, responseJson);
                    }
                }
            }
            catch (Exception ex)
            {
                string a = resJson;
                resultado.Estatus = false;
                resultado.Detalle = $"Error en la solicitud: {ex.Message}";
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef";
                log.Log.metodo = "Api_CatalogoColonias";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            //respuestaToken.resultado = resultado;
            return resultado;
        }

        private void Db_InsertaColonia(int idEstado, int idMunicipio, string municipio, string cp, int idColonia, string colonia, int? idTipoLocalidad, string? tipoLocalidad)
        {
            ClsResultadoAccion resultado = new();
            try
            {
                string[] parametros = { "idEstado", "idMunicipio", "municipio", "cp", "idColonia", "colonia", "idTipoLocalidad", "tipoLocalidad" };
                string[] values = { idEstado.ToString(), idMunicipio.ToString(), municipio, cp, idColonia.ToString(), colonia, 
                    FntGenericas.ValidaNullint(idTipoLocalidad.ToString(),0).ToString(),
                    FntGenericas.ValidaNullString(tipoLocalidad,"") 
                };
                BD_DLL.ClsBD.Consulta_con_parametros("catalogo.insertar_colonia", Conexion.CadenaConexion,
                    parametros, values, Conexion.isolationLevelSnap());
                resultado.Estatus = true;
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef";
                log.Log.metodo = "Db_InsertaColonia";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
            }
            //return resultado;
        }


        #endregion

    }
}
