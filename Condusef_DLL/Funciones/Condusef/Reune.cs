using Condusef_DLL.Clases;
using Condusef_DLL.Clases.Condusef;
using Condusef_DLL.Clases.Condusef.API;
using Condusef_DLL.Clases.Condusef.Configuracion;
using Condusef_DLL.Clases.General;
using Condusef_DLL.Clases.Generales;
using Condusef_DLL.Funciones.Generales;
using OfficeOpenXml;
using System;
using System.Collections;
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

namespace Condusef_DLL.Funciones.Condusef
{

    public class Reune
    {
        public ClsConsultasGenerales ConsultasGenerales { get; set; }
        public List<ClsConsultasGenerales> ListadoConsultasGenerales { get; set; }

        public ClsReclamacionesGenerales ReclamacionesGenerales { get; set; }
        public List<ClsReclamacionesGenerales> ListadoReclamacionesGenerales { get; set; }
        public ClsAclaracionesGenerales AclaracionesGenerales { get; set; }
        public List<ClsAclaracionesGenerales> ListadoAclaracionesGenerales { get; set; }


        public List<ClsCatalogo> Trimestre { get; set; }
        public List<ClsCatalogo> Anios { get; set; }
        public List<ClsCatalogo> Respuestas { get; set; }
        public List<ClsCatalogo> Penalizaciones { get; set; }
        public List<ClsCatalogo> MediosRecepcion { get; set; }
        public List<ClsCatalogo> NivelesAtencion { get; set; }
        public List<ClsCatalogo> Productos { get; set; }
        public List<ClsCatalogo> CausasProductos { get; set; }
        public List<ClsCatalogo> Estados { get; set; }
        public List<ClsCatalogo> CodigosPostales { get; set; }
        public List<ClsCatalogo> Municipios { get; set; }
        public List<ClsCatalogo> Colonias { get; set; }


        public List<ClsConsultasGenerales> ConsultasConErrores { get; set; }
        public List<ClsReclamacionesGenerales> ReclamacionesConErrores { get; set; }
        public List<ClsAclaracionesGenerales> AclaracionesConErrores { get; set; }

        public List<BolsaErroresConsultas> BolsaErroresConsultas { get; set; }
        public List<BolsaErroresReclamaciones> BolsaErroresReclamaciones { get; set; }
        public List<BolsaErroresAclaraciones> BolsaErroresAclaraciones { get; set; }

        public List<ClsConsultasLayout> ListadoConsultasLayout { get; set; }
        public List<ClsReclamacionesLayout> ListadoReclamacionesLayout { get; set; }
        public List<ClsAclaracionesLayout> ListadoAclaracionesLayout { get; set; }

        public RespuestaSuperUser _RespuestaSuperUser { get; set; }
        public ErrorEnvioCondusef ErrorEnvioCondusef { get; set; }

        public Reune()
        {
            Trimestre = new List<ClsCatalogo>();
            Respuestas = new List<ClsCatalogo>();
            Penalizaciones = new List<ClsCatalogo>();
            MediosRecepcion = new List<ClsCatalogo>();
            NivelesAtencion = new List<ClsCatalogo>();
            Productos = new List<ClsCatalogo>();
            CausasProductos = new List<ClsCatalogo>();
            Estados = new List<ClsCatalogo>();
            CodigosPostales = new List<ClsCatalogo>();
            Municipios = new List<ClsCatalogo>();
            Colonias = new List<ClsCatalogo>();
            Anios = new List<ClsCatalogo>();

            _RespuestaSuperUser = new RespuestaSuperUser();

            ConsultasGenerales = new ClsConsultasGenerales();
            ListadoConsultasGenerales = new List<ClsConsultasGenerales>();
            ReclamacionesGenerales = new ClsReclamacionesGenerales();
            ListadoReclamacionesGenerales = new List<ClsReclamacionesGenerales>();
            AclaracionesGenerales = new ClsAclaracionesGenerales();
            ListadoAclaracionesGenerales = new List<ClsAclaracionesGenerales>();

            ConsultasConErrores = new List<ClsConsultasGenerales>();
            ReclamacionesConErrores = new List<ClsReclamacionesGenerales>();
            AclaracionesConErrores = new List<ClsAclaracionesGenerales>();
            ErrorEnvioCondusef = new ErrorEnvioCondusef();
            BolsaErroresAclaraciones = new List<BolsaErroresAclaraciones>();
            BolsaErroresConsultas = new List<BolsaErroresConsultas>();
            BolsaErroresReclamaciones = new List<BolsaErroresReclamaciones>();
            ListadoConsultasLayout = new List<ClsConsultasLayout>();
            ListadoReclamacionesLayout = new List<ClsReclamacionesLayout>();
            ListadoAclaracionesLayout = new List<ClsAclaracionesLayout>();

        }

        #region Catalogos
        public void CargarCatalogos(string idEmpresa, int vista, string sector)
        {
            Trimestre = Catalogos.CargarTrimestres();
            Respuestas = Catalogos.CargarRespuestas();
            MediosRecepcion = Catalogos.CargarMediosRecepcion(2);
            NivelesAtencion = Catalogos.CargarNivelesAtencion();
            Estados = Catalogos.CargarEstados();
            Productos = Catalogos.CargarProductos("",2, sector, vista);
            Anios = Catalogos.CargarAniosEmpresaReune(idEmpresa, vista);
        }

        public ClsResultadoAccion ConsultaCausasProducto(string idEmpresa, string idProducto, int vista, string sector)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            try
            {
                CausasProductos = Catalogos.CargarCausasProductos(idEmpresa, idProducto, 2, vista, sector);
                resultado.Estatus = true;
            }
            catch (Exception e)
            {
                resultado.Estatus = false;
                resultado.Detalle = e.Message;
            }
            return resultado;
        }

        public ClsResultadoAccion ConsultaMunicipios(string idEstado)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            try
            {
                Municipios = Catalogos.CargarMunicipios(idEstado);
                resultado.Estatus = true;
            }
            catch (Exception e)
            {
                resultado.Estatus = false;
                resultado.Detalle = e.Message;
            }
            return resultado;
        }

        public ClsResultadoAccion ConsultaCodigosPostales(string idEstado, string idMunicipio)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            try
            {
                CodigosPostales = Catalogos.CargarCP(idEstado, idMunicipio);
                resultado.Estatus = true;
            }
            catch (Exception e)
            {
                resultado.Estatus = false;
                resultado.Detalle = e.Message;
            }
            return resultado;
        }

        public ClsResultadoAccion ConsultaColonias(string idEstado, string idMunicipio, string cp)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            try
            {
                Colonias = Catalogos.CargarColonias(idEstado, idMunicipio, cp);
                resultado.Estatus = true;
            }
            catch (Exception e)
            {
                resultado.Estatus = false;
                resultado.Detalle = e.Message;
            }
            return resultado;
        }

        #endregion

        #region API CONDUSEF

        private async Task<ClsResultadoAccion> Api_EnvioConsultasGenerales(string authorization, ClsConsultasGenerales quejas)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            ClsRespuestasCondusef respuesta = new ClsRespuestasCondusef();
            try
            {
                string apiUrl = VariablesGlobales.Url.Reune_EnvioConsultasGenerales;
                // Crear una instancia de HttpClient
                using (HttpClient client = new HttpClient())
                {
                    // Convertir los datos a formato JSON
                    List<ClsConsultasGenerales> listado = new List<ClsConsultasGenerales>();
                    listado.Add(quejas);
                    string json = JsonSerializer.Serialize(listado);

                    HttpRequestMessage request = new HttpRequestMessage();
                    HttpResponseMessage response2 = new HttpResponseMessage();

                    request.RequestUri = new Uri(apiUrl);
                    request.Method = HttpMethod.Post;
                    client.DefaultRequestHeaders.Add("Accept", "application/json;charset=UTF-8");

                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    request.Headers.Authorization = new AuthenticationHeaderValue(authorization);
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    request.Content = content;

                    client.Timeout = TimeSpan.FromMinutes(15); // Aumentar el tiempo de espera a 15 minutos
                                                               // Realizar la solicitud POST
                    response2 = client.SendAsync(request).Result;

                    // Verificar si la respuesta fue exitosa
                    if (response2.IsSuccessStatusCode)
                    {
                        // Leer la respuesta como una cadena de texto
                        string responseJson = await response2.Content.ReadAsStringAsync();
                        resultado.Estatus = true;
                        resultado.Detalle = responseJson;
                        resultado.Code = 200;
                        //respuesta = JsonSerializer.Deserialize<ClsRespuestasCondusef>(responseJson);
                        
                    }
                    else
                    {
                        string responseJson = await response2.Content.ReadAsStringAsync();
                        resultado.Code = (int)response2.StatusCode;
                        resultado.Estatus = false;

                        FntLog.insertaLogApi(Constantes.REUNE, "Enviar Consultas Generales", resultado.Code, json, responseJson);

                        if (resultado.Code == 400)
                        {
                            ErrorEnvioCondusef error = JsonSerializer.Deserialize<ErrorEnvioCondusef>(responseJson);
                            ErrorEnvioCondusef = error;
                        }
                        else
                        {
                            ClsErrorPeticion error = JsonSerializer.Deserialize<ClsErrorPeticion>(responseJson);

                            if (error.error == "Token no válido" || error.error == "El token ha expirado")
                            {
                                resultado.Code = 401;
                                resultado.Detalle = error.error;
                            }

                            //else if (resultado.Code == 400) resultado.Detalle = error.error;
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.Estatus = false;
                resultado.Detalle = $"Error en la solicitud: {ex.Message}";
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef.Reune";
                log.Log.metodo = "Api_EnvioConsultasGenerales";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            //_RespuestaEnvioConsultas = respuesta;
            //respuestaToken.resultado = resultado;
            return resultado;
        }

        private async Task<ClsResultadoAccion> Api_EnvioReclamacionesGenerales(string authorization, ClsReclamacionesGenerales quejas)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            ClsRespuestasCondusef respuesta = new ClsRespuestasCondusef();
            try
            {
                string apiUrl = VariablesGlobales.Url.Reune_EnvioReclamacionesGenerales;
                // Crear una instancia de HttpClient
                using (HttpClient client = new HttpClient())
                {
                    // Convertir los datos a formato JSON
                    List<ClsReclamacionesGenerales> listado = new List<ClsReclamacionesGenerales>();
                    listado.Add(quejas);
                    string json = JsonSerializer.Serialize(listado);

                    HttpRequestMessage request = new HttpRequestMessage();
                    HttpResponseMessage response2 = new HttpResponseMessage();

                    request.RequestUri = new Uri(apiUrl);
                    request.Method = HttpMethod.Post;
                    client.DefaultRequestHeaders.Add("Accept", "application/json;charset=UTF-8");

                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    request.Headers.Authorization = new AuthenticationHeaderValue(authorization);
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    request.Content = content;

                    client.Timeout = TimeSpan.FromMinutes(15); // Aumentar el tiempo de espera a 15 minutos
                                                               // Realizar la solicitud POST
                    response2 = client.SendAsync(request).Result;

                    // Verificar si la respuesta fue exitosa
                    if (response2.IsSuccessStatusCode)
                    {
                        // Leer la respuesta como una cadena de texto
                        string responseJson = await response2.Content.ReadAsStringAsync();
                        resultado.Estatus = true;
                        resultado.Detalle = responseJson;
                        resultado.Code = 200;
                        //respuesta = JsonSerializer.Deserialize<ClsRespuestasCondusef>(responseJson);
                    }
                    else
                    {
                        string responseJson = await response2.Content.ReadAsStringAsync();
                        resultado.Code = (int)response2.StatusCode;
                        resultado.Estatus = false;

                        FntLog.insertaLogApi(Constantes.REUNE, "Enviar Reclamaciones Generales", resultado.Code, json, responseJson);

                        if (resultado.Code == 400)
                        {
                            ErrorEnvioCondusef error = JsonSerializer.Deserialize<ErrorEnvioCondusef>(responseJson);
                            ErrorEnvioCondusef = error;
                        }
                        else
                        {
                            ClsErrorPeticion error = JsonSerializer.Deserialize<ClsErrorPeticion>(responseJson);

                            if (error.error == "Token no válido" || error.error == "El token ha expirado")
                            {
                                resultado.Code = 401;
                                resultado.Detalle = error.error;
                            }

                            //else if (resultado.Code == 400) resultado.Detalle = error.error;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                resultado.Estatus = false;
                resultado.Detalle = $"Error en la solicitud: {ex.Message}";
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef.Reune";
                log.Log.metodo = "Api_EnvioReclamacionesGenerales";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            //respuestaToken.resultado = resultado;
            return resultado;
        }

        private async Task<ClsResultadoAccion> Api_EnvioAclaracionesGenerales(string authorization, ClsAclaracionesGenerales quejas)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            ClsRespuestasCondusef respuesta = new ClsRespuestasCondusef();
            try
            {
                string apiUrl = VariablesGlobales.Url.Reune_EnvioAclaracionesGenerales;
                // Crear una instancia de HttpClient
                using (HttpClient client = new HttpClient())
                {
                    // Convertir los datos a formato JSON
                    List<ClsAclaracionesGenerales> listado = new List<ClsAclaracionesGenerales>();
                    listado.Add(quejas);
                    string json = JsonSerializer.Serialize(listado);

                    HttpRequestMessage request = new HttpRequestMessage();
                    HttpResponseMessage response2 = new HttpResponseMessage();

                    request.RequestUri = new Uri(apiUrl);
                    request.Method = HttpMethod.Post;
                    client.DefaultRequestHeaders.Add("Accept", "application/json;charset=UTF-8");

                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    request.Headers.Authorization = new AuthenticationHeaderValue(authorization);
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    request.Content = content;

                    client.Timeout = TimeSpan.FromMinutes(15); // Aumentar el tiempo de espera a 15 minutos
                                                               // Realizar la solicitud POST
                    response2 = client.SendAsync(request).Result;

                    // Verificar si la respuesta fue exitosa
                    if (response2.IsSuccessStatusCode)
                    {
                        // Leer la respuesta como una cadena de texto
                        string responseJson = await response2.Content.ReadAsStringAsync();
                        resultado.Estatus = true;
                        resultado.Detalle = responseJson;
                        resultado.Code = 200;
                        //respuesta = JsonSerializer.Deserialize<ClsRespuestasCondusef>(responseJson);
                        
                    }
                    else
                    {
                        string responseJson = await response2.Content.ReadAsStringAsync();
                        resultado.Code = (int)response2.StatusCode;
                        resultado.Estatus = false;

                        FntLog.insertaLogApi(Constantes.REUNE, "Enviar Aclaraciones Generales", resultado.Code, json, responseJson);

                        if (resultado.Code == 400)
                        {
                            ErrorEnvioCondusef error = JsonSerializer.Deserialize<ErrorEnvioCondusef>(responseJson);
                            ErrorEnvioCondusef = error;
                        }
                        else
                        {
                            ClsErrorPeticion error = JsonSerializer.Deserialize<ClsErrorPeticion>(responseJson);

                            if (error.error == "Token no válido" || error.error == "El token ha expirado")
                            {
                                resultado.Code = 401;
                                resultado.Detalle = error.error;
                            }

                            //else if (resultado.Code == 400) resultado.Detalle = error.error;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                resultado.Estatus = false;
                resultado.Detalle = $"Error en la solicitud: {ex.Message}";
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef.Reune";
                log.Log.metodo = "Api_EnvioAclaracionesGenerales";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            //respuestaToken.resultado = resultado;
            return resultado;
        }



        #endregion

        public async Task<ClsResultadoAccion> EnviarConsultasGenerales(ClsUsuario usuario, int anio, List<ClsConsultasGenerales> consultas)
        {
            ClsResultadoAccion resultado = new();
            RespuestaEnvioQuejas respuesta = new RespuestaEnvioQuejas();
            Condusef condusef = new Condusef();
            List<BolsaErroresConsultas> bolsaErrores = new List<BolsaErroresConsultas>();
            try
            {
                resultado = await condusef.ConsultaTokenUsuario(usuario.IdUsuario, "2");
                if (resultado.Estatus)
                {
                    string tokenReune = resultado.Detalle;
                    foreach (var consulta in consultas)
                    {
                        resultado = await Api_EnvioConsultasGenerales(tokenReune, consulta);
                        if (resultado.Estatus)
                        {
                            resultado = Db_InsertaConsulta(usuario.IdEmpresa, anio, consulta, resultado.Detalle);

                        }
                        else
                        {
                            if (resultado.Code == 400)
                            {
                                BolsaErroresConsultas consultaConErrores = new BolsaErroresConsultas();
                                List<string> erroresDevueltos = new List<string>();
                                foreach (var error in ErrorEnvioCondusef.errors)
                                {
                                    string key = error.Key;
                                    List<string> values = error.Value;
                                    erroresDevueltos = error.Value;
                                }
                                if (ErrorEnvioCondusef.error != string.Empty) erroresDevueltos.Add(ErrorEnvioCondusef.error);
                                consultaConErrores = new BolsaErroresConsultas(consulta, erroresDevueltos);
                                bolsaErrores.Add(consultaConErrores);
                            }
                            else if(resultado.Code == 401)
                            {
                                resultado = await condusef.RenovarTokenUsuario(usuario.IdUsuario, Conexion.Username, "2");
                                if (resultado.Estatus)
                                {
                                    resultado = await Api_EnvioConsultasGenerales(tokenReune, consulta);
                                    if (resultado.Estatus)
                                    {
                                        resultado = Db_InsertaConsulta(usuario.IdEmpresa, anio, consulta, resultado.Detalle);
                                    }
                                    //else
                                    //{
                                    //    if (resultado.Code == 400)
                                    //    {
                                    //        consulta.errors.Add(resultado.Detalle);
                                    //        consultasConErrores.Add(consulta);
                                    //    }
                                    //}
                                }
                            }
                            else if(resultado.Code == 500)
                            {
                                resultado.Estatus = false;
                                resultado.Detalle = "Sistema de Reune está teniendo problemas. Por favor suba su información más tarde";
                                break;
                            }
                        }
                    }
                    if (resultado.Code < 500)
                    {
                        if (bolsaErrores.Count > 0)
                        {
                            resultado.Estatus = true;
                            resultado.Code = 400; //Hay quejas con errores
                        }
                        else
                        {
                            resultado.Estatus = true;
                            resultado.Code = 200; //Todas las quejas se subieron
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef.Reune";
                log.Log.metodo = "EnviarConsultasGenerales";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            BolsaErroresConsultas = bolsaErrores;
            return resultado;
        }

        public async Task<ClsResultadoAccion> EnviarReclamacionesGenerales(ClsUsuario usuario, int anio, List<ClsReclamacionesGenerales> reclamaciones)
        {
            ClsResultadoAccion resultado = new();
            RespuestaEnvioQuejas respuesta = new RespuestaEnvioQuejas();
            Condusef condusef = new Condusef();
            //List<ClsReclamacionesGenerales> reclamacionesConErrores = new List<ClsReclamacionesGenerales>();
            List<BolsaErroresReclamaciones> bolsaErrores = new List<BolsaErroresReclamaciones>();

            try
            {
                resultado = await condusef.ConsultaTokenUsuario(usuario.IdUsuario, "2");
                if (resultado.Estatus)
                {
                    string tokenReune = resultado.Detalle;
                    foreach (var reclamacion in reclamaciones)
                    {
                        resultado = await Api_EnvioReclamacionesGenerales(tokenReune, reclamacion);
                        if (resultado.Estatus)
                        {
                            resultado = Db_InsertaReclamaciones(usuario.IdEmpresa, anio, reclamacion, resultado.Detalle);
                        }
                        else
                        {
                            //if (resultado.Code == 400)
                            //{
                            //    reclamacion.errors.Add(resultado.Detalle);
                            //    reclamacionesConErrores.Add(reclamacion);
                            //}

                            if (resultado.Code == 400)
                            {
                                BolsaErroresReclamaciones reclamacionConErrores = new BolsaErroresReclamaciones();
                                List<string> erroresDevueltos = new List<string>();
                                foreach (var error in ErrorEnvioCondusef.errors)
                                {
                                    string key = error.Key;
                                    List<string> values = error.Value;
                                    erroresDevueltos = error.Value;
                                }
                                if (ErrorEnvioCondusef.error != string.Empty) erroresDevueltos.Add(ErrorEnvioCondusef.error);
                                reclamacionConErrores = new BolsaErroresReclamaciones(reclamacion, erroresDevueltos);
                                bolsaErrores.Add(reclamacionConErrores);
                            }

                            else if (resultado.Code == 401)
                            {
                                resultado = await condusef.RenovarTokenUsuario(usuario.IdUsuario, Conexion.Username, "2");
                                if (resultado.Estatus)
                                {
                                    resultado = await Api_EnvioReclamacionesGenerales(tokenReune, reclamacion);
                                    if (resultado.Estatus)
                                    {
                                        resultado = Db_InsertaReclamaciones(usuario.IdEmpresa, anio, reclamacion, resultado.Detalle);
                                    }
                                    //else
                                    //{
                                    //    if (resultado.Code == 400)
                                    //    {
                                    //        reclamacion.errors.Add(resultado.Detalle);
                                    //        reclamacionesConErrores.Add(reclamacion);
                                    //        break;
                                    //    }
                                    //}
                                }
                            }
                            else if (resultado.Code == 500)
                            {
                                resultado.Estatus = false;
                                resultado.Detalle = "Sistema de Reune está teniendo problemas. Por favor suba su información más tarde";
                            }
                        }
                    }
                    //if (resultado.Code != 500)
                    //{
                    //    if (reclamacionesConErrores.Count > 0)
                    //    {
                    //        resultado.Code = 400;
                    //        resultado.Estatus = true;
                    //    }
                    //    else
                    //    {
                    //        resultado.Code = 200;
                    //        resultado.Estatus = true;
                    //    }
                    //}
                    if (resultado.Code < 500)
                    {
                        if (bolsaErrores.Count > 0)
                        {
                            resultado.Estatus = true;
                            resultado.Code = 400; //Hay quejas con errores
                        }
                        else
                        {
                            resultado.Estatus = true;
                            resultado.Code = 200; //Todas las quejas se subieron
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef.Reune";
                log.Log.metodo = "EnviarReclamacionesGenerales";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            //ReclamacionesConErrores = reclamacionesConErrores;
            BolsaErroresReclamaciones = bolsaErrores;
            return resultado;
        }

        public async Task<ClsResultadoAccion> EnviarAclaracionesGenerales(ClsUsuario usuario, int anio, List<ClsAclaracionesGenerales> aclaraciones)
        {
            ClsResultadoAccion resultado = new();
            RespuestaEnvioQuejas respuesta = new RespuestaEnvioQuejas();
            Condusef condusef = new Condusef();
            //List<ClsAclaracionesGenerales> aclaracionesConErrores = new List<ClsAclaracionesGenerales>();
            List<BolsaErroresAclaraciones> bolsaErrores = new List<BolsaErroresAclaraciones>();
            try
            {
                resultado = await condusef.ConsultaTokenUsuario(usuario.IdUsuario, "2");
                if (resultado.Estatus)
                {
                    string tokenReune = resultado.Detalle;
                    foreach (var aclaracion in aclaraciones)
                    {
                        resultado = await Api_EnvioAclaracionesGenerales(tokenReune, aclaracion);
                        if (resultado.Estatus)
                        {
                            resultado = Db_InsertaAclaraciones(usuario.IdEmpresa, anio, aclaracion, resultado.Detalle);
                        }
                        else
                        {
                            //if (resultado.Code == 400)
                            //{
                            //    aclaracion.errors.Add(resultado.Detalle);
                            //    aclaracionesConErrores.Add(aclaracion);
                            //}
                            if (resultado.Code == 400)
                            {
                                BolsaErroresAclaraciones aclaracionConErrores = new BolsaErroresAclaraciones();
                                List<string> erroresDevueltos = new List<string>();
                                foreach (var error in ErrorEnvioCondusef.errors)
                                {
                                    string key = error.Key;
                                    List<string> values = error.Value;
                                    erroresDevueltos = error.Value;
                                }
                                if (ErrorEnvioCondusef.error != string.Empty) erroresDevueltos.Add(ErrorEnvioCondusef.error);
                                aclaracionConErrores = new BolsaErroresAclaraciones(aclaracion, erroresDevueltos);
                                bolsaErrores.Add(aclaracionConErrores);
                            }
                            else if (resultado.Code == 401)
                            {
                                resultado = await condusef.RenovarTokenUsuario(usuario.IdUsuario, Conexion.Username, "2");
                                if (resultado.Estatus)
                                {
                                    resultado = await Api_EnvioAclaracionesGenerales(tokenReune, aclaracion);
                                    if (resultado.Estatus)
                                    {
                                        resultado = Db_InsertaAclaraciones(usuario.IdEmpresa, anio, aclaracion, resultado.Detalle);
                                    }
                                    //else
                                    //{
                                    //    if (resultado.Code == 400)
                                    //    {
                                    //        aclaracion.errors.Add(resultado.Detalle);
                                    //        aclaracionesConErrores.Add(aclaracion);
                                    //    }
                                    //}
                                }
                            }
                            else if (resultado.Code == 500)
                            {
                                resultado.Estatus = false;
                                resultado.Detalle = "Sistema de Reune está teniendo problemas. Por favor suba su información más tarde";
                                break;
                            }
                        }
                    }
                    //if (resultado.Code != 500)
                    //{
                    //    if (aclaracionesConErrores.Count > 0)
                    //    {
                    //        resultado.Code = 400;
                    //        resultado.Estatus = true;
                    //    }
                    //    else
                    //    {
                    //        resultado.Code = 200;
                    //        resultado.Estatus = true;
                    //    }
                    //}
                    if (resultado.Code < 500)
                    {
                        if (bolsaErrores.Count > 0)
                        {
                            resultado.Estatus = true;
                            resultado.Code = 400; //Hay quejas con errores
                        }
                        else
                        {
                            resultado.Estatus = true;
                            resultado.Code = 200; //Todas las quejas se subieron
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef.Reune";
                log.Log.metodo = "EnviarAclaracionesGenerales";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            //AclaracionesConErrores = aclaracionesConErrores;
            BolsaErroresAclaraciones = bolsaErrores;
            return resultado;
        }

        public ClsResultadoAccion BuscarConsultas(string idEmpresa, string periodo, string anio)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            resultado = Db_BuscarConsultasEmpresa(idEmpresa, periodo, anio);
            return resultado;
        }

        public ClsResultadoAccion ConsultarReclamaciones(string idEmpresa, string periodo, string anio)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            resultado = Db_ConsultaReclamacionesEmpresa(idEmpresa, periodo, anio);
            return resultado;
        }

        public ClsResultadoAccion ConsultarAclaraciones(string idEmpresa, string periodo, string anio)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            resultado = Db_ConsultaAclaracionesEmpresa(idEmpresa, periodo, anio);
            return resultado;
        }

        public ClsResultadoAccion ConsultarRespuestaJson(string idEmpresa, string folio, string vista)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            try
            {
                string[] parametros = { "idEmpresa", "folio", "vista" };
                string[] values = { idEmpresa, folio, vista };
                DataSet resultadoConsulta = BD_DLL.ClsBD.Consulta_con_parametros("reune.consultar_respuesta_json", Conexion.CadenaConexion,
                    parametros, values, Conexion.isolationLevelSnap());
                if (resultadoConsulta.Tables.Count > 0)
                {
                    if (resultadoConsulta.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow fila in resultadoConsulta.Tables[0].Rows)
                        {
                            resultado.Detalle = FntGenericas.ValidaNullString(fila["respuesta_json"].ToString(), "");
                            resultado.Estatus = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef.Reune";
                log.Log.metodo = "ConsultarRespuestaJson";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return resultado;
        }


        public async Task<ClsResultadoAccion> ProcesarLayoutConsultas(ClsStreamFile file)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            List<ClsConsultasLayout> quejasLayout = new List<ClsConsultasLayout>();
            try
            {
                resultado = await FntArchivos.GuardarArchivo(file.Path, file.File);
                if (resultado.Estatus)
                {
                    ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                    using (var templatePackage = new ExcelPackage(new FileInfo(file.Path)))
                    {
                        var ws = templatePackage.Workbook.Worksheets["Consultas"];
                        if (ws != null)
                        {
                            // Establece el rango a partir de la fila 2
                            var startRow = 3;
                            // Encuentra la última fila con datos en la hoja actual
                            var endRow = ws.Dimension?.End.Row ?? startRow;

                            // Limpia el contenido del rango especificado
                            for (int row = startRow; row <= endRow; row++)
                            {
                                ClsConsultasLayout queja = new();

                                queja.InstitucionClave = FntGenericas.ValidaNullString(ws.Cells[row, 1].Value?.ToString().Trim(), "").Trim();
                                queja.Sector = FntGenericas.ValidaNullString(ws.Cells[row, 2].Value?.ToString().Trim(), "").Trim();
                                queja.ConsultasTrim = FntGenericas.ValidaNullint(ws.Cells[row, 3].Value?.ToString().Trim(), 0);
                                queja.NumConsultas = FntGenericas.ValidaNullint(ws.Cells[row, 4].Value?.ToString().Trim(), 1);
                                queja.ConsultasFolio = FntGenericas.ValidaNullString(ws.Cells[row, 5].Value?.ToString().Trim(), "").Trim();
                                queja.ConsultasEstatusCon = FntGenericas.ValidaNullint(ws.Cells[row, 6].Value?.ToString().Trim(), 1);
                                queja.ConsultasFecRecepcion = FntGenericas.ValidaNullString(ws.Cells[row, 7].Value?.ToString().Trim(), "").Trim();
                                queja.MediosId = FntGenericas.ValidaNullint(ws.Cells[row, 8].Value?.ToString().Trim(), 0);
                                queja.ConsultascatnivelatenId = FntGenericas.ValidaNullString(ws.Cells[row, 9].Value?.ToString().Trim(), "");
                                queja.Producto = FntGenericas.ValidaNullString(ws.Cells[row, 10].Value?.ToString().Trim(), "").Trim();
                                queja.CausaId = FntGenericas.ValidaNullString(ws.Cells[row, 11].Value?.ToString().Trim(), "").Trim();
                                queja.ConsultasPori = FntGenericas.ValidaNullString(ws.Cells[row, 12].Value?.ToString().Trim(), "NO").Trim().ToUpper();
                                queja.ConsultasFecAten = FntGenericas.ValidaNullString(ws.Cells[row, 13].Value?.ToString().Trim(), "");
                                queja.EstadosId = FntGenericas.ValidaNullint(ws.Cells[row, 14].Value?.ToString().Trim(), 0);
                                queja.ConsultasMpioId = FntGenericas.ValidaNullint(ws.Cells[row, 15].Value?.ToString().Trim(), 0);
                                queja.ConsultasLocId = FntGenericas.ValidaNullString(ws.Cells[row, 16].Value?.ToString().Trim(), "");
                                queja.ConsultasColId = FntGenericas.ValidaNullString(ws.Cells[row, 17].Value?.ToString().Trim(), "");
                                queja.ConsultasCP = FntGenericas.ValidaNullString(ws.Cells[row, 18].Value?.ToString().Trim(), "");
                                

                                queja.ConsultasFecRecepcion = ExtractDate(queja.ConsultasFecRecepcion);
                                queja.ConsultasFecAten = ExtractDate(queja.ConsultasFecAten);

                                queja = ValidarDatosNullConsultas(queja);

                                quejasLayout.Add(queja);
                            }
                        }
                    }
                }
                resultado.Estatus = true;
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Reune";
                log.Log.metodo = "ProcesarLayoutConsultas";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
            }
            ListadoConsultasLayout = quejasLayout;
            return resultado;
        }

        public async Task<ClsResultadoAccion> ProcesarLayoutReclamaciones(ClsStreamFile file)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            List<ClsReclamacionesLayout> quejasLayout = new List<ClsReclamacionesLayout>();
            try
            {
                resultado = await FntArchivos.GuardarArchivo(file.Path, file.File);
                if (resultado.Estatus)
                {
                    ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                    using (var templatePackage = new ExcelPackage(new FileInfo(file.Path)))
                    {
                        var ws = templatePackage.Workbook.Worksheets["Reclamaciones"];
                        if (ws != null)
                        {
                            // Establece el rango a partir de la fila 2
                            var startRow = 3;
                            // Encuentra la última fila con datos en la hoja actual
                            var endRow = ws.Dimension?.End.Row ?? startRow;

                            // Limpia el contenido del rango especificado
                            for (int row = startRow; row <= endRow; row++)
                            {
                                ClsReclamacionesLayout queja = new();

                                queja.RecDenominacion = FntGenericas.ValidaNullString(ws.Cells[row, 1].Value?.ToString().Trim(), "").Trim();
                                queja.RecSector = FntGenericas.ValidaNullString(ws.Cells[row, 2].Value?.ToString().Trim(), "").Trim();
                                queja.RecTrimestre = FntGenericas.ValidaNullint(ws.Cells[row, 3].Value?.ToString().Trim(), 0);
                                queja.RecNumero = FntGenericas.ValidaNullint(ws.Cells[row, 4].Value?.ToString().Trim(), 1);
                                queja.RecFolioAtencion = FntGenericas.ValidaNullString(ws.Cells[row, 5].Value?.ToString().Trim(), "").Trim();
                                queja.RecEstadoConPend = FntGenericas.ValidaNullint(ws.Cells[row, 6].Value?.ToString().Trim(), 1);
                                queja.RecFechaReclamacion = FntGenericas.ValidaNullString(ws.Cells[row, 7].Value?.ToString().Trim(), "").Trim();
                                queja.RecMedioRecepcionCanal = FntGenericas.ValidaNullint(ws.Cells[row, 8].Value?.ToString().Trim(), 0);
                                queja.RecNivelAtencion = FntGenericas.ValidaNullString(ws.Cells[row, 9].Value?.ToString().Trim(), "");
                                queja.RecProductoServicio = FntGenericas.ValidaNullString(ws.Cells[row, 10].Value?.ToString().Trim(), "").Trim();
                                queja.RecCausaMotivo = FntGenericas.ValidaNullString(ws.Cells[row, 11].Value?.ToString().Trim(), "").Trim();
                                queja.RecPori = FntGenericas.ValidaNullString(ws.Cells[row, 12].Value?.ToString().Trim(), "NO").Trim().ToUpper();
                                queja.RecFechaAtencion = FntGenericas.ValidaNullString(ws.Cells[row, 13].Value?.ToString().Trim(), "");
                                queja.RecEntidadFederativa = FntGenericas.ValidaNullint(ws.Cells[row, 14].Value?.ToString().Trim(), 0);
                                queja.RecMunicipioAlcaldia = FntGenericas.ValidaNullint(ws.Cells[row, 15].Value?.ToString().Trim(), 0);
                                queja.RecLocalidad = FntGenericas.ValidaNullString(ws.Cells[row, 16].Value?.ToString().Trim(), "");
                                queja.RecColonia = FntGenericas.ValidaNullString(ws.Cells[row, 17].Value?.ToString().Trim(), "");
                                queja.RecCodigoPostal = FntGenericas.ValidaNullString(ws.Cells[row, 18].Value?.ToString().Trim(), "");
                                queja.RecTipoPersona = FntGenericas.ValidaNullint(ws.Cells[row, 19].Value?.ToString().Trim(), 1);
                                queja.RecSexo = FntGenericas.ValidaNullString(ws.Cells[row, 20].Value?.ToString().Trim(), "").Trim().ToUpper();
                                queja.RecEdad = FntGenericas.ValidaNullString(ws.Cells[row, 21].Value?.ToString().Trim(), "");
                                queja.RecMonetario = FntGenericas.ValidaNullString(ws.Cells[row, 22].Value?.ToString().Trim(), "NO").ToUpper();
                                queja.RecMontoReclamado = FntGenericas.ValidaNullString(ws.Cells[row, 23].Value?.ToString().Trim(), "");
                                queja.RecImporteAbonado = FntGenericas.ValidaNullString(ws.Cells[row, 24].Value?.ToString().Trim(), "");
                                queja.RecFechaAbonoImporte = FntGenericas.ValidaNullString(ws.Cells[row, 25].Value?.ToString().Trim(), "");
                                queja.RecFechaResolucion = FntGenericas.ValidaNullString(ws.Cells[row, 26].Value?.ToString().Trim(), "");
                                queja.RecFechaNotifiUsuario = FntGenericas.ValidaNullString(ws.Cells[row, 27].Value?.ToString().Trim(), "");
                                queja.RecSentidoResolucion = FntGenericas.ValidaNullString(ws.Cells[row, 28].Value?.ToString().Trim(), "");
                                queja.RecFolioCondusef = FntGenericas.ValidaNullString(ws.Cells[row, 29].Value?.ToString().Trim(), "");
                                queja.RecReversa = FntGenericas.ValidaNullString(ws.Cells[row, 30].Value?.ToString().Trim(), "");
                                

                                queja.RecFechaReclamacion = ExtractDate(queja.RecFechaReclamacion);
                                queja.RecFechaResolucion = ExtractDate(queja.RecFechaResolucion);
                                queja.RecFechaAbonoImporte = ExtractDate(queja.RecFechaAbonoImporte);
                                queja.RecFechaAtencion = ExtractDate(queja.RecFechaAtencion);
                                queja.RecFechaNotifiUsuario = ExtractDate(queja.RecFechaNotifiUsuario);

                                queja = ValidarDatosNullReclamaciones(queja);

                                quejasLayout.Add(queja);
                            }
                        }
                    }
                }
                resultado.Estatus = true;
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Reune";
                log.Log.metodo = "ProcesarLayoutReclamaciones";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
            }
            ListadoReclamacionesLayout = quejasLayout;
            return resultado;
        }

        public async Task<ClsResultadoAccion> ProcesarLayoutAclaraciones(ClsStreamFile file)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            List<ClsAclaracionesLayout> quejasLayout = new List<ClsAclaracionesLayout>();
            try
            {
                resultado = await FntArchivos.GuardarArchivo(file.Path, file.File);
                if (resultado.Estatus)
                {
                    ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                    using (var templatePackage = new ExcelPackage(new FileInfo(file.Path)))
                    {
                        var ws = templatePackage.Workbook.Worksheets["Aclaraciones"];
                        if (ws != null)
                        {
                            // Establece el rango a partir de la fila 2
                            var startRow = 3;
                            // Encuentra la última fila con datos en la hoja actual
                            var endRow = ws.Dimension?.End.Row ?? startRow;

                            // Limpia el contenido del rango especificado
                            for (int row = startRow; row <= endRow; row++)
                            {
                                ClsAclaracionesLayout queja = new();

                                queja.AclaracionDenominacion = FntGenericas.ValidaNullString(ws.Cells[row, 1].Value?.ToString().Trim(), "").Trim();
                                queja.AclaracionSector = FntGenericas.ValidaNullString(ws.Cells[row, 2].Value?.ToString().Trim(), "").Trim();
                                queja.AclaracionTrimestre = FntGenericas.ValidaNullint(ws.Cells[row, 3].Value?.ToString().Trim(), 0);
                                queja.AclaracionNumero = FntGenericas.ValidaNullint(ws.Cells[row, 4].Value?.ToString().Trim(), 1);
                                queja.AclaracionFolioAtencion = FntGenericas.ValidaNullString(ws.Cells[row, 5].Value?.ToString().Trim(), "").Trim();
                                queja.AclaracionEstadoConPend = FntGenericas.ValidaNullint(ws.Cells[row, 6].Value?.ToString().Trim(), 1);
                                queja.AclaracionFechaAclaracion = FntGenericas.ValidaNullString(ws.Cells[row, 7].Value?.ToString().Trim(), "").Trim();
                                queja.AclaracionMedioRecepcionCanal = FntGenericas.ValidaNullint(ws.Cells[row, 8].Value?.ToString().Trim(), 0);
                                queja.AclaracionNivelAtencion = FntGenericas.ValidaNullString(ws.Cells[row, 9].Value?.ToString().Trim(), "");
                                queja.AclaracionProductoServicio = FntGenericas.ValidaNullString(ws.Cells[row, 10].Value?.ToString().Trim(), "").Trim();
                                queja.AclaracionCausaMotivo = FntGenericas.ValidaNullString(ws.Cells[row, 11].Value?.ToString().Trim(), "").Trim();
                                queja.AclaracionPori = FntGenericas.ValidaNullString(ws.Cells[row, 12].Value?.ToString().Trim(), "NO").Trim().ToUpper();
                                queja.AclaracionFechaAtencion = FntGenericas.ValidaNullString(ws.Cells[row, 13].Value?.ToString().Trim(), "");
                                queja.AclaracionEntidadFederativa = FntGenericas.ValidaNullint(ws.Cells[row, 14].Value?.ToString().Trim(), 0);
                                queja.AclaracionMunicipioAlcaldia = FntGenericas.ValidaNullint(ws.Cells[row, 15].Value?.ToString().Trim(), 0);
                                queja.AclaracionLocalidad = FntGenericas.ValidaNullString(ws.Cells[row, 16].Value?.ToString().Trim(), "");
                                queja.AclaracionColonia = FntGenericas.ValidaNullString(ws.Cells[row, 17].Value?.ToString().Trim(), "");
                                queja.AclaracionCodigoPostal = FntGenericas.ValidaNullString(ws.Cells[row, 18].Value?.ToString().Trim(), "");
                                queja.AclaracionTipoPersona = FntGenericas.ValidaNullint(ws.Cells[row, 19].Value?.ToString().Trim(), 1);
                                queja.AclaracionSexo = FntGenericas.ValidaNullString(ws.Cells[row, 20].Value?.ToString().Trim(), "").ToUpper();
                                queja.AclaracionEdad = FntGenericas.ValidaNullString(ws.Cells[row, 21].Value?.ToString().Trim(), "");
                                queja.AclaracionMonetario = FntGenericas.ValidaNullString(ws.Cells[row, 22].Value?.ToString().Trim(), "NO").Trim().ToUpper();
                                queja.AclaracionMontoReclamado = FntGenericas.ValidaNullString(ws.Cells[row, 23].Value?.ToString().Trim(), "");
                                queja.AclaracionFechaResolucion = FntGenericas.ValidaNullString(ws.Cells[row, 24].Value?.ToString().Trim(), "");
                                queja.AclaracionFechaNotifiUsuario = FntGenericas.ValidaNullString(ws.Cells[row, 25].Value?.ToString().Trim(), "");
                                queja.AclaracionFolioCondusef = FntGenericas.ValidaNullString(ws.Cells[row, 26].Value?.ToString().Trim(), "");
                                queja.AclaracionReversa = FntGenericas.ValidaNullString(ws.Cells[row, 27].Value?.ToString().Trim(), "");
                                queja.AclaracionOperacionExtranjero = FntGenericas.ValidaNullString(ws.Cells[row, 28].Value?.ToString().Trim(), "NO").ToUpper();

                                queja.AclaracionFechaAclaracion = ExtractDate(queja.AclaracionFechaAclaracion);
                                queja.AclaracionFechaResolucion = ExtractDate(queja.AclaracionFechaResolucion);
                                queja.AclaracionFechaNotifiUsuario = ExtractDate(queja.AclaracionFechaNotifiUsuario);
                                queja.AclaracionFechaAtencion = ExtractDate(queja.AclaracionFechaAtencion);

                                queja = ValidarDatosNullAclaraciones(queja);

                                quejasLayout.Add(queja);
                            }
                        }
                    }
                }
                resultado.Estatus = true;
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Reune";
                log.Log.metodo = "ProcesarLayoutAclaraciones";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
            }
            ListadoAclaracionesLayout = quejasLayout;
            return resultado;
        }


        #region DB
        private ClsResultadoAccion Db_EliminaQueja(string idEmpresa, string idQueja, string quejaFolio)
        {
            ClsResultadoAccion resultado = new();
            try
            {
                string[] parametros = { "id_empresa", "id_queja", "quejas_folio" };
                string[] values = { idEmpresa, idQueja, quejaFolio };
                BD_DLL.ClsBD.Consulta_con_parametros("redeco.eliminar_queja", Conexion.CadenaConexion,
                    parametros, values, Conexion.isolationLevelSnap());
                resultado.Estatus = true;
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef.Reune";
                log.Log.metodo = "Db_EliminaQueja";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
            }
            return resultado;
        }

        private ClsResultadoAccion Db_BuscarConsultasEmpresa(string idEmpresa, string periodo, string anio)
        {
            ClsResultadoAccion resultado = new();
            List<ClsConsultasGenerales> listado = new();
            try
            {
                string[] parametros = { "idEmpresa", "periodo", "anio" };
                string[] values = { idEmpresa, periodo, anio };
                DataSet resultadoConsulta = BD_DLL.ClsBD.Consulta_con_parametros("reune.buscar_consultas_empresa", Conexion.CadenaConexion,
                    parametros, values, Conexion.isolationLevelSnap());
                if (resultadoConsulta.Tables.Count > 0)
                {
                    if (resultadoConsulta.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow fila in resultadoConsulta.Tables[0].Rows)
                        {
                            ClsConsultasGenerales consulta = new ClsConsultasGenerales();

                            //consulta.InstitucionClave = FntGenericas.ValidaNullString(fila["institucion_clave"].ToString(), "");
                            //consulta.Sector = FntGenericas.ValidaNullString(fila["sector"].ToString(), "");

                            //consulta.ConsultasTrim = FntGenericas.ValidaNullint(fila["periodo_trimestre"].ToString(), int.Parse(periodo));
                            consulta.NumConsultas = FntGenericas.ValidaNullint(fila["numero_consultas"].ToString(), 1);
                            consulta.ConsultasFolio = FntGenericas.ValidaNullString(fila["folio_consulta"].ToString(), "");
                            consulta.ConsultasEstatusCon = FntGenericas.ValidaNullint(fila["estatus_consulta"].ToString(), 2);
                            consulta.ConsultasPori = FntGenericas.ValidaNullString(fila["consultas_pori"].ToString(), "NO");

                            consulta.ConsultasFecRecepcion = FntGenericas.ValidaNullString(fila["fecha_recepcion"].ToString().Substring(0, 10), "");
                            consulta.ConsultasFecAten = FntGenericas.ValidaNullString(fila["fecha_atencion"].ToString().Substring(0, 10), "");

                            consulta.MediosId = FntGenericas.ValidaNullint(fila["medio_id"].ToString(), int.Parse(periodo));
                            consulta.ConsultascatnivelatenId = FntGenericas.ValidaNullint(fila["nivel_at"].ToString(), int.Parse(periodo));
                            consulta.Producto = FntGenericas.ValidaNullString(fila["producto"].ToString(), "");
                            //consulta.CausaId = FntGenericas.ValidaNullString(fila["causas_id"].ToString(), "");
                            consulta.EstadosId = FntGenericas.ValidaNullint(fila["id_estado"].ToString(), 0);
                            //consulta.ConsultasMpioId = FntGenericas.ValidaNullint(fila["id_municipio"].ToString(), 0);
                            //consulta.ConsultasColId = FntGenericas.ValidaNullint(fila["id_colonia"].ToString(), 0);
                            //consulta.ConsultasLocId = FntGenericas.ValidaNullint(fila["id_localidad"].ToString(), 0);
                            consulta.ConsultasCP = FntGenericas.ValidaNullint(fila["consulta_cp"].ToString(), 0);

                            consulta.CausaId = FntGenericas.ValidaNullString(fila["causa"].ToString(), "");
                            consulta.ColoniaText = FntGenericas.ValidaNullString(fila["colonia"].ToString(), "");
                            consulta.MunicipioText = FntGenericas.ValidaNullString(fila["municipio"].ToString(), "");
                            consulta.FechaSubida = FntGenericas.ValidaNullDateTime(fila["fecha_subida"].ToString(), new DateTime());

                            listado.Add(consulta);
                        }
                    }
                    resultado.Estatus = true;
                }
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef.Reune";
                log.Log.metodo = "Db_BuscarConsultasEmpresa";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            ListadoConsultasGenerales = listado;
            return resultado;
        }

        private ClsResultadoAccion Db_ConsultaReclamacionesEmpresa(string idEmpresa, string periodo, string anio)
        {
            ClsResultadoAccion resultado = new();
            List<ClsReclamacionesGenerales> listado = new();
            try
            {
                string[] parametros = { "idEmpresa", "periodo", "anio" };
                string[] values = { idEmpresa, periodo, anio };
                DataSet resultadoConsulta = BD_DLL.ClsBD.Consulta_con_parametros("reune.consultar_reclamaciones_empresa", Conexion.CadenaConexion,
                    parametros, values, Conexion.isolationLevelSnap());
                if (resultadoConsulta.Tables.Count > 0)
                {
                    if (resultadoConsulta.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow fila in resultadoConsulta.Tables[0].Rows)
                        {
                            ClsReclamacionesGenerales reclamacion = new ClsReclamacionesGenerales();

                            //reclamacion.RecDenominacion = FntGenericas.ValidaNullString(fila["institucion_clave"].ToString(), "");
                            //reclamacion.RecSector = FntGenericas.ValidaNullString(fila["sector"].ToString(), "");

                            //reclamacion.RecTrimestre = FntGenericas.ValidaNullint(fila["periodo_trimestre"].ToString(), int.Parse(periodo));
                            reclamacion.RecNumero = FntGenericas.ValidaNullint(fila["numero_reclamacion"].ToString(), 1);
                            reclamacion.RecFolioAtencion = FntGenericas.ValidaNullString(fila["folio_atencion"].ToString(), "");
                            reclamacion.RecEstadoConPend = FntGenericas.ValidaNullint(fila["estatus_reclamacion"].ToString(), 2);
                            reclamacion.RecPori = FntGenericas.ValidaNullString(fila["pori"].ToString(), "NO");

                            reclamacion.RecFechaReclamacion = FntGenericas.ValidaNullString(fila["fecha_reclamacion"].ToString().Substring(0, 10), "");
                            reclamacion.RecFechaAtencion = FntGenericas.ValidaNullString(fila["fecha_atencion"].ToString().Substring(0, 10), "");
                            reclamacion.RecFechaResolucion = FntGenericas.ValidaNullString(fila["fecha_resolucion"].ToString().Substring(0, 10), "");
                            reclamacion.RecFechaNotifiUsuario = FntGenericas.ValidaNullString(fila["fecha_notificacion"].ToString().Substring(0, 10), "");

                            reclamacion.RecMedioRecepcionCanal = FntGenericas.ValidaNullint(fila["medio_id"].ToString(), int.Parse(periodo));
                            reclamacion.RecNivelAtencion = FntGenericas.ValidaNullint(fila["nivel_at"].ToString(), int.Parse(periodo));
                            reclamacion.RecProductoServicio = FntGenericas.ValidaNullString(fila["producto"].ToString(), "");
                            //reclamacion.RecCausaMotivo = FntGenericas.ValidaNullString(fila["causas_id"].ToString(), "");
                            reclamacion.RecEntidadFederativa = FntGenericas.ValidaNullint(fila["id_estado"].ToString(), 0);
                            //reclamacion.RecMunicipioAlcaldia = FntGenericas.ValidaNullint(fila["id_municipio"].ToString(), 0);
                            //reclamacion.RecColonia = FntGenericas.ValidaNullint(fila["id_colonia"].ToString(), 0);
                            //reclamacion.RecLocalidad = FntGenericas.ValidaNullint(fila["id_localidad"].ToString(), 0);
                            reclamacion.RecCodigoPostal = FntGenericas.ValidaNullint(fila["cp"].ToString(), 0);

                            reclamacion.RecMonetario = FntGenericas.ValidaNullString(fila["monetario"].ToString(), "NO");
                            reclamacion.RecMontoReclamado = FntGenericas.ValidaNulldecimal(fila["monto_reclamado"].ToString(), 0.00M);
                            reclamacion.RecImporteAbonado = FntGenericas.ValidaNulldecimal(fila["importe_abonado"].ToString(), 0.00M);
                            reclamacion.RecFechaAbonoImporte = FntGenericas.ValidaNullString(fila["fecha_abono_importe"].ToString().Substring(0, 10), "");

                            reclamacion.RecTipoPersona = FntGenericas.ValidaNullint(fila["tipo_persona"].ToString(), 1);
                            reclamacion.RecSexo = FntGenericas.ValidaNullString(fila["sexo"].ToString(), "H");
                            reclamacion.RecEdad = FntGenericas.ValidaNullint(fila["edad"].ToString(), 0);
                            reclamacion.RecSentidoResolucion = FntGenericas.ValidaNullint(fila["sentido_resolucion"].ToString(), 0);
                            reclamacion.RecFolioCondusef = FntGenericas.ValidaNullString(fila["sexo"].ToString(), "H");
                            reclamacion.RecReversa = FntGenericas.ValidaNullint(fila["reclamacion_reversa"].ToString(), 0);

                            reclamacion.RecCausaMotivo = FntGenericas.ValidaNullString(fila["causa"].ToString(), "");
                            reclamacion.ColoniaText = FntGenericas.ValidaNullString(fila["colonia"].ToString(), "");
                            reclamacion.MunicipioText = FntGenericas.ValidaNullString(fila["municipio"].ToString(), "");
                            reclamacion.FechaSubida = FntGenericas.ValidaNullDateTime(fila["fecha_subida"].ToString(), new DateTime());

                            listado.Add(reclamacion);
                        }
                    }
                    resultado.Estatus = true;
                }
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef.Reune";
                log.Log.metodo = "Db_ConsultaReclamacionesEmpresa";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            ListadoReclamacionesGenerales = listado;
            return resultado;
        }

        private ClsResultadoAccion Db_ConsultaAclaracionesEmpresa(string idEmpresa, string periodo, string anio)
        {
            ClsResultadoAccion resultado = new();
            List<ClsAclaracionesGenerales> listado = new();
            try
            {
                string[] parametros = { "idEmpresa", "periodo", "anio" };
                string[] values = { idEmpresa, periodo, anio };
                DataSet resultadoConsulta = BD_DLL.ClsBD.Consulta_con_parametros("reune.consultar_aclaraciones_empresa", Conexion.CadenaConexion,
                    parametros, values, Conexion.isolationLevelSnap());
                if (resultadoConsulta.Tables.Count > 0)
                {
                    if (resultadoConsulta.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow fila in resultadoConsulta.Tables[0].Rows)
                        {
                            ClsAclaracionesGenerales aclaracion = new ClsAclaracionesGenerales();

                            //aclaracion.AclaracionDenominacion = FntGenericas.ValidaNullString(fila["institucion_clave"].ToString(), "");
                            //aclaracion.AclaracionSector = FntGenericas.ValidaNullString(fila["sector"].ToString(), "");

                            //aclaracion.AclaracionTrimestre = FntGenericas.ValidaNullint(fila["periodo_trimestre"].ToString(), int.Parse(periodo));
                            aclaracion.AclaracionNumero = FntGenericas.ValidaNullint(fila["numero_reclamacion"].ToString(), 1);
                            aclaracion.AclaracionFolioAtencion = FntGenericas.ValidaNullString(fila["folio_atencion"].ToString(), "");
                            aclaracion.AclaracionEstadoConPend = FntGenericas.ValidaNullint(fila["estatus_aclaracion"].ToString(), 2);
                            aclaracion.AclaracionPori = FntGenericas.ValidaNullString(fila["pori"].ToString(), "NO");

                            aclaracion.AclaracionFechaAclaracion = FntGenericas.ValidaNullString(fila["fecha_aclaracion"].ToString().Substring(0, 10), "");
                            aclaracion.AclaracionFechaAtencion = FntGenericas.ValidaNullString(fila["fecha_atencion"].ToString().Substring(0, 10), "");
                            aclaracion.AclaracionFechaResolucion = FntGenericas.ValidaNullString(fila["fecha_resolucion"].ToString().Substring(0, 10), "");
                            aclaracion.AclaracionFechaNotifiUsuario = FntGenericas.ValidaNullString(fila["fecha_notificacion"].ToString().Substring(0, 10), "");

                            aclaracion.AclaracionMedioRecepcionCanal = FntGenericas.ValidaNullint(fila["medio_id"].ToString(), int.Parse(periodo));
                            aclaracion.AclaracionNivelAtencion = FntGenericas.ValidaNullint(fila["nivel_at"].ToString(), int.Parse(periodo));
                            aclaracion.AclaracionProductoServicio = FntGenericas.ValidaNullString(fila["producto"].ToString(), "");
                            //aclaracion.AclaracionCausaMotivo = FntGenericas.ValidaNullString(fila["causas_id"].ToString(), "");
                            aclaracion.AclaracionEntidadFederativa = FntGenericas.ValidaNullint(fila["id_estado"].ToString(), 0);
                            //aclaracion.AclaracionMunicipioAlcaldia = FntGenericas.ValidaNullint(fila["id_municipio"].ToString(), 0);
                            //aclaracion.AclaracionColonia = FntGenericas.ValidaNullint(fila["id_colonia"].ToString(), 0);
                            //aclaracion.AclaracionLocalidad = FntGenericas.ValidaNullint(fila["id_localidad"].ToString(), 0);
                            aclaracion.AclaracionCodigoPostal = FntGenericas.ValidaNullint(fila["cp"].ToString(), 0);

                            aclaracion.AclaracionMonetario = FntGenericas.ValidaNullString(fila["monetario"].ToString(), "NO");
                            aclaracion.AclaracionMontoReclamado = FntGenericas.ValidaNulldecimal(fila["monto_reclamado"].ToString(), 0.00M);

                            aclaracion.AclaracionTipoPersona = FntGenericas.ValidaNullint(fila["tipo_persona"].ToString(), 1);
                            aclaracion.AclaracionSexo = FntGenericas.ValidaNullString(fila["sexo"].ToString(), "H");
                            aclaracion.AclaracionEdad = FntGenericas.ValidaNullint(fila["edad"].ToString(), 0);
                            aclaracion.AclaracionFolioCondusef = FntGenericas.ValidaNullString(fila["folio_condusef"].ToString(), "");
                            aclaracion.AclaracionReversa = FntGenericas.ValidaNullint(fila["reversa"].ToString(), 0);
                            aclaracion.AclaracionOperacionExtranjero = FntGenericas.ValidaNullString(fila["operacion_extranjero"].ToString(), "NO");

                            aclaracion.AclaracionCausaMotivo = FntGenericas.ValidaNullString(fila["causa"].ToString(), "");
                            aclaracion.ColoniaText = FntGenericas.ValidaNullString(fila["colonia"].ToString(), "");
                            aclaracion.MunicipioText = FntGenericas.ValidaNullString(fila["municipio"].ToString(), "");
                            aclaracion.FechaSubida = FntGenericas.ValidaNullDateTime(fila["fecha_subida"].ToString(), new DateTime());

                            listado.Add(aclaracion);
                        }
                    }
                    resultado.Estatus = true;
                }
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef.Reune";
                log.Log.metodo = "Db_ConsultaAclaracionesEmpresa";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            ListadoAclaracionesGenerales = listado;
            return resultado;
        }

        private ClsResultadoAccion Db_InsertaConsulta(string idEmpresa, int anio, ClsConsultasGenerales consulta, string respuestaJson)
        {
            ClsResultadoAccion resultado = new();
            try
            {
                DateTime fechaSubida = DateTime.Now;
                string cp = "";
                if (consulta.ConsultasCP == null) cp = "";
                else cp = consulta.ConsultasCP <= 9999 ? $"0{consulta.ConsultasCP}" : consulta.ConsultasCP.ToString();

                string[] parametros = { "idEmpresa", "institucion", "sector", "trimestre", "anio", "numeroConsultas", "folio", "estatus",
                    "pori","fechaAtencion", "fechaRecepcion", "medio", "nivel", "producto", "idCausas", "idEstado", "idMunicipio",
                    "idColonia", "idLocalidad", "cp", "fechaSubida", "respuestaJson"
                };
                string[] values = { idEmpresa, consulta.InstitucionClave, consulta.Sector, consulta.ConsultasTrim.ToString(), anio.ToString(),
                    consulta.NumConsultas.ToString(), consulta.ConsultasFolio, consulta.ConsultasEstatusCon.ToString(), consulta.ConsultasPori,
                    FntGenericas.ValidaNullString(consulta.ConsultasFecAten, "01/01/1990"), consulta.ConsultasFecRecepcion, consulta.MediosId.ToString(), consulta.ConsultascatnivelatenId.ToString(),
                    consulta.Producto, consulta.CausaId, consulta.EstadosId.ToString(), consulta.ConsultasMpioId.ToString(),
                    FntGenericas.ValidaNullString(consulta.ConsultasColId.ToString(),"0"),
                    FntGenericas.ValidaNullString(consulta.ConsultasLocId.ToString(),"0"),
                    cp, fechaSubida.ToString("yyyyMMdd HH:mm:ss.fff"), respuestaJson
                };
                BD_DLL.ClsBD.Consulta_con_parametros("reune.insertar_consultas_generales", Conexion.CadenaConexion,
                    parametros, values, Conexion.isolationLevelSnap());
                resultado.Estatus = true;
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef.Reune";
                log.Log.metodo = "Db_InsertaConsulta";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
            }
            return resultado;
        }

        private ClsResultadoAccion Db_InsertaReclamaciones(string idEmpresa, int anio, ClsReclamacionesGenerales reclamaciones, string respuestaJson)
        {
            ClsResultadoAccion resultado = new();
            try
            {
                DateTime fechaSubida = DateTime.Now;

                string cp = "";
                if (reclamaciones.RecCodigoPostal == null) cp = "";
                else cp = reclamaciones.RecCodigoPostal <= 9999 ? $"0{reclamaciones.RecCodigoPostal}" : reclamaciones.RecCodigoPostal.ToString();

                string[] parametros = { "idEmpresa", "institucion", "sector", "trimestre", "anio", "numeroReclamacion", "folioAtencion",
                    "estatusReclamacion", "fechaReclamacion","fechaAtencion", "medio", "nivel", "producto", "idCausas", "fechaResolucion", 
                    "fechaNotificacion", "idEstado", "idMunicipio", "idColonia", "idLocalidad", "cp", "monetario", "montoReclamado",
                    "importeAbonado", "fechaAbonoImporte", "pori", "tipoPersona", "sexo", "edad", "sentidoResolucion", "folioCondusef", 
                    "reversa", "fechaSubida", "respuestaJson"
                };
                string[] values = { idEmpresa, reclamaciones.RecDenominacion, reclamaciones.RecSector, reclamaciones.RecTrimestre.ToString(), 
                    anio.ToString(), reclamaciones.RecNumero.ToString(), reclamaciones.RecFolioAtencion, reclamaciones.RecEstadoConPend.ToString(), 
                    reclamaciones.RecFechaReclamacion, 
                    FntGenericas.ValidaNullString(reclamaciones.RecFechaAtencion, "01/01/1990"), 
                    reclamaciones.RecMedioRecepcionCanal.ToString(),
                    FntGenericas.ValidaNullString(reclamaciones.RecNivelAtencion.ToString(), "0"),
                    reclamaciones.RecProductoServicio, reclamaciones.RecCausaMotivo,
                    FntGenericas.ValidaNullString(reclamaciones.RecFechaResolucion, "01/01/1990"),
                    FntGenericas.ValidaNullString(reclamaciones.RecFechaNotifiUsuario, "01/01/1990"), 
                    reclamaciones.RecEntidadFederativa.ToString(), 
                    reclamaciones.RecMunicipioAlcaldia.ToString(), 
                    FntGenericas.ValidaNullString(reclamaciones.RecColonia.ToString(), "0"),
                    FntGenericas.ValidaNullString(reclamaciones.RecLocalidad.ToString(),"0"), 
                    cp, reclamaciones.RecMonetario.ToString(),
                    FntGenericas.ValidaNullString(reclamaciones.RecMontoReclamado.ToString(),"0.00"),
                    FntGenericas.ValidaNullString(reclamaciones.RecImporteAbonado.ToString(),"0.00"),
                    FntGenericas.ValidaNullString(reclamaciones.RecFechaAbonoImporte, "01/01/1990"), 
                    reclamaciones.RecPori, reclamaciones.RecTipoPersona.ToString(),
                    FntGenericas.ValidaNullString(reclamaciones.RecSexo,""), 
                    FntGenericas.ValidaNullString(reclamaciones.RecEdad.ToString(),"0"),
                    FntGenericas.ValidaNullString(reclamaciones.RecSentidoResolucion.ToString(),"0"), 
                    FntGenericas.ValidaNullString(reclamaciones.RecFolioCondusef,""),
                    FntGenericas.ValidaNullString(reclamaciones.RecReversa.ToString(),"0"), fechaSubida.ToString("yyyyMMdd HH:mm:ss.fff"),
                    respuestaJson
                };
                BD_DLL.ClsBD.Consulta_con_parametros("reune.insertar_reclamacion_general", Conexion.CadenaConexion,
                    parametros, values, Conexion.isolationLevelSnap());
                resultado.Estatus = true;
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef.Reune";
                log.Log.metodo = "Db_InsertaReclamaciones";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
            }
            return resultado;
        }

        private ClsResultadoAccion Db_InsertaAclaraciones(string idEmpresa, int anio, ClsAclaracionesGenerales aclaraciones, string respuestaJson)
        {
            ClsResultadoAccion resultado = new();
            try
            {
                DateTime fechaSubida = DateTime.Now;
                string cp = "";
                if (aclaraciones.AclaracionCodigoPostal == null) cp = "";
                else cp = aclaraciones.AclaracionCodigoPostal <= 9999 ? $"0{aclaraciones.AclaracionCodigoPostal}" : aclaraciones.AclaracionCodigoPostal.ToString();

                string[] parametros = { "idEmpresa", "institucion", "sector", "trimestre", "anio", "numeroAclaracion", "folioAtencion",
                    "estatusAclaracion", "fechaAclaracion","fechaAtencion", "medio", "nivel", "producto", "idCausas", "fechaResolucion",
                    "fechaNotificacion", "idEstado", "idMunicipio", "idColonia", "idLocalidad", "cp", "monetario", "montoReclamado",
                    "pori", "tipoPersona", "sexo", "edad", "folioCondusef", "reversa", "operacionExtranjero", "fechaSubida", "respuestaJson"
                };
                string[] values = { idEmpresa, aclaraciones.AclaracionDenominacion, aclaraciones.AclaracionSector, aclaraciones.AclaracionTrimestre.ToString(),
                    anio.ToString(), aclaraciones.AclaracionNumero.ToString(), aclaraciones.AclaracionFolioAtencion, aclaraciones.AclaracionEstadoConPend.ToString(),
                    aclaraciones.AclaracionFechaAclaracion, 
                    FntGenericas.ValidaNullString(aclaraciones.AclaracionFechaAtencion, "01/01/1990"),
                    aclaraciones.AclaracionMedioRecepcionCanal.ToString(),
                    FntGenericas.ValidaNullString(aclaraciones.AclaracionNivelAtencion.ToString(),"0"), aclaraciones.AclaracionProductoServicio, aclaraciones.AclaracionCausaMotivo,
                    FntGenericas.ValidaNullString(aclaraciones.AclaracionFechaResolucion, "01/01/1990"),
                    FntGenericas.ValidaNullString(aclaraciones.AclaracionFechaNotifiUsuario, "01/01/1990"),
                    aclaraciones.AclaracionEntidadFederativa.ToString(), 
                    aclaraciones.AclaracionMunicipioAlcaldia.ToString(),
                    FntGenericas.ValidaNullString(aclaraciones.AclaracionColonia.ToString(),"0"),
                    FntGenericas.ValidaNullString(aclaraciones.AclaracionLocalidad.ToString(),"0"), 
                    cp, aclaraciones.AclaracionMonetario.ToString(), 
                    FntGenericas.ValidaNullString(aclaraciones.AclaracionMontoReclamado.ToString(),"0.00"),
                    aclaraciones.AclaracionPori, aclaraciones.AclaracionTipoPersona.ToString(),
                    FntGenericas.ValidaNullString(aclaraciones.AclaracionSexo,""),
                    FntGenericas.ValidaNullString(aclaraciones.AclaracionEdad.ToString(),"0"),
                    FntGenericas.ValidaNullString(aclaraciones.AclaracionFolioCondusef,""),
                    FntGenericas.ValidaNullString(aclaraciones.AclaracionReversa.ToString(),"0"), aclaraciones.AclaracionOperacionExtranjero,
                    fechaSubida.ToString("yyyyMMdd HH:mm:ss.fff"),
                    respuestaJson
                };
                BD_DLL.ClsBD.Consulta_con_parametros("reune.insertar_aclaracion_general", Conexion.CadenaConexion,
                    parametros, values, Conexion.isolationLevelSnap());
                resultado.Estatus = true;
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef.Reune";
                log.Log.metodo = "Db_InsertaAclaraciones";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
            }
            return resultado;
        }

        #endregion

        private ClsConsultasLayout ValidarDatosNullConsultas(ClsConsultasLayout info)
        {
            if (info.ConsultasEstatusCon == 1)
            {
                info.ConsultascatnivelatenId = "null";
                info.ConsultasFecAten = "null";
            }

            return info;
        }

        private ClsReclamacionesLayout ValidarDatosNullReclamaciones(ClsReclamacionesLayout info)
        {
            if (info.RecMonetario == "NO")
            {
                info.RecMontoReclamado = "null";
                info.RecImporteAbonado = "null";
                info.RecFechaAbonoImporte = "null";
            }
            if (info.RecMedioRecepcionCanal < 6 && info.RecMedioRecepcionCanal > 7) info.RecFolioCondusef = "null";
            if (info.RecEstadoConPend == 1)
            {
                info.RecNivelAtencion = "null";
                info.RecFechaAtencion = "null";
                info.RecFechaResolucion = "null";
                info.RecFechaNotifiUsuario = "null";
                info.RecImporteAbonado = "null";
                info.RecFechaAbonoImporte = "null";
                info.RecSentidoResolucion = "null";
            }

            if (info.RecImporteAbonado == "0" || info.RecImporteAbonado == "null")
            {
                info.RecFechaAbonoImporte = "null";
            }

            if (info.RecMedioRecepcionCanal != 6) info.RecReversa = "null";

            //if (info.RecMedioRecepcionCanal != 3 && info.RecMedioRecepcionCanal != 5 && info.RecMedioRecepcionCanal != 17) {
            //    info.RecCodigoPostal = "null";
            //    info.RecColonia = "null";
            //    info.RecLocalidad = "null";
            //}

            return info;
        }

        private ClsAclaracionesLayout ValidarDatosNullAclaraciones(ClsAclaracionesLayout info)
        {
            if (info.AclaracionMonetario == "NO") info.AclaracionMontoReclamado = "null";
            if (info.AclaracionMedioRecepcionCanal < 6 && info.AclaracionMedioRecepcionCanal > 7) info.AclaracionFolioCondusef = "null";
            if (info.AclaracionEstadoConPend == 1)
            {
                info.AclaracionNivelAtencion = "null";
                info.AclaracionFechaAtencion = "null";
                info.AclaracionFechaResolucion = "null";
                info.AclaracionFechaNotifiUsuario = "null";
            }

            if (info.AclaracionMedioRecepcionCanal != 6) info.AclaracionReversa = "null";

            return info;
        }

        static string ExtractDate(string dateTimeString)
        {
            if (dateTimeString == null || dateTimeString == "") return dateTimeString;
            // Parsear la cadena a un objeto DateTime
            DateTime dateTime = DateTime.Parse(dateTimeString);

            // Formatear el objeto DateTime a solo la parte de la fecha
            return dateTime.ToString("dd/MM/yyyy");
        }






    }
}
