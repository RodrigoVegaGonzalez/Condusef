using Condusef_DLL.Clases;
using Condusef_DLL.Clases.Condusef;
using Condusef_DLL.Clases.Condusef.API;
using Condusef_DLL.Clases.Condusef.Configuracion;
using Condusef_DLL.Clases.General;
using Condusef_DLL.Clases.Generales;
using Condusef_DLL.Funciones.Administracion;
using Condusef_DLL.Funciones.Generales;
using OfficeOpenXml;
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
    public class RespuestaEliminarQueja
    {
        public string msg { get; set; }
        public RespuestaEliminarQueja()
        {
            msg = string.Empty;
        }
    }

    public class Redeco
    {
        public ClsQuejas QuejasRedeco { get; set; }
        public List<ClsQuejas> ListadoQuejas { get; set; }
        public List<ClsQuejasLayout> ListadoQuejasLayout { get; set; }

        public List<ClsCatalogo> Meses { get; set; }
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

        public ErrorEnvioCondusef ErrorEnvioCondusef { get; set; }
        public RespuestaEnvioQuejas _RespuestaEnvioQuejas { get; set; }
        public RespuestaCodigoPostal _RespuestaCodigoPostal { get; set; }
        public RespuestaColonias _RespuestaColonias { get; set; }
        public RespuestaSuperUser _RespuestaSuperUser { get; set; }
        public List<BolsaErroresQuejas> BolsaErroresQuejas { get; set; }
        public Redeco()
        {
            QuejasRedeco = new ClsQuejas();
            _RespuestaEnvioQuejas = new RespuestaEnvioQuejas();
            ListadoQuejas = new List<ClsQuejas>();
            ListadoQuejasLayout = new List<ClsQuejasLayout>();
            Meses = new List<ClsCatalogo>();
            Anios = new List<ClsCatalogo>();
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

            _RespuestaCodigoPostal = new RespuestaCodigoPostal();
            _RespuestaSuperUser = new RespuestaSuperUser();
            _RespuestaColonias = new RespuestaColonias();
            ErrorEnvioCondusef = new ErrorEnvioCondusef();
            BolsaErroresQuejas = new List<BolsaErroresQuejas>();
        }

        #region Catalogos
        public void CargarCatalogos(string idEmpresa)
        {
            Meses = Catalogos.CargarMeses();
            Anios = Catalogos.CargarAniosQuejasRedeco(idEmpresa);
            Respuestas = Catalogos.CargarRespuestas();
            Penalizaciones = Catalogos.CargarPenalizaciones();
            MediosRecepcion = Catalogos.CargarMediosRecepcion(1);
            NivelesAtencion = Catalogos.CargarNivelesAtencion();
            Estados = Catalogos.CargarEstados();
            Productos = Catalogos.CargarProductos(idEmpresa, 1, "");
        }

        public ClsResultadoAccion ConsultaCausasProducto(string idEmpresa, string idProducto)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            try
            {
                CausasProductos = Catalogos.CargarCausasProductos(idEmpresa, idProducto, 1, 0, "");
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

        private async Task<ClsResultadoAccion> Api_EnvioQuejas(string authorization, ClsQuejas queja)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            RespuestaEnvioQuejas respuesta = new RespuestaEnvioQuejas();
            try
            {
                string apiUrl = VariablesGlobales.Url.Redeco_EnvioQuejas;
                // Crear una instancia de HttpClient
                using (HttpClient client = new HttpClient())
                {
                    // Convertir los datos a formato JSON

                    List<ClsQuejas> quejas = new List<ClsQuejas> { queja };
                    string json = JsonSerializer.Serialize(quejas);

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
                        //respuesta = JsonSerializer.Deserialize<RespuestaEnvioQuejas>(responseJson);
                        
                    }
                    else
                    {
                        string responseJson = await response2.Content.ReadAsStringAsync();
                        resultado.Estatus = false;
                        resultado.Code = (int)response2.StatusCode;

                        FntLog.insertaLogApi(Constantes.REDECO,"Envío Quejas", resultado.Code,json,responseJson);

                        if(resultado.Code == 400)
                        {
                            ErrorEnvioCondusef error = JsonSerializer.Deserialize<ErrorEnvioCondusef>(responseJson);
                            ErrorEnvioCondusef = error;
                        }
                        else
                        {
                            ClsErrorPeticion error = JsonSerializer.Deserialize<ClsErrorPeticion>(responseJson);
                            if (resultado.Code == 500) resultado.Detalle = error.msg;
                            else if (resultado.Code == 403) resultado.Detalle = error.error;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.Estatus = false;
                resultado.Detalle = $"Error en la solicitud: {ex.Message}";
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef.Redeco";
                log.Log.metodo = "Api_EnvioQuejas";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            //_RespuestaEnvioQuejas = respuesta;
            //respuestaToken.resultado = resultado;
            return resultado;
        }

        private async Task<ClsResultadoAccion> Api_EliminarQueja(string authorization, string quejaFolio)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            RespuestaEnvioQuejas respuesta = new RespuestaEnvioQuejas();
            try
            {
                string apiUrl = VariablesGlobales.Url.Redeco_EliminarQuejas;
                string urlWithParameters = $"{apiUrl}?quejaFolio={quejaFolio}";
                // Crear una instancia de HttpClient
                using (HttpClient client = new HttpClient())
                {

                    HttpRequestMessage request = new HttpRequestMessage();
                    HttpResponseMessage response2 = new HttpResponseMessage();

                    request.RequestUri = new Uri(urlWithParameters);
                    request.Method = HttpMethod.Delete;
                    client.DefaultRequestHeaders.Add("Accept", "application/json;charset=UTF-8");

                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    request.Headers.Authorization = new AuthenticationHeaderValue(authorization);

                    client.Timeout = TimeSpan.FromMinutes(15); // Aumentar el tiempo de espera a 5 minutos
                                                               // Realizar la solicitud POST
                    response2 = client.SendAsync(request).Result;

                    // Verificar si la respuesta fue exitosa
                    if (response2.IsSuccessStatusCode)
                    {
                        // Leer la respuesta como una cadena de texto
                        string responseJson = await response2.Content.ReadAsStringAsync();
                        // Deserializar la respuesta JSON en un objeto anónimo
                        var responseObject = JsonSerializer.Deserialize<RespuestaEliminarQueja>(responseJson);

                        // Verificar el contenido de la propiedad "msg" para determinar el resultado
                        if (responseObject.msg.Contains("La queja con el folio"))
                        {
                            resultado.Code = 200;
                        }
                        else if (responseObject.msg.Contains("No existe ninguna queja con el folio"))
                        {
                            resultado.Code = 400;
                        }
                        else
                        {
                            resultado.Code = 500;
                        }

                        resultado.Estatus = true;
                    }
                    else
                    {
                        string responseJson = await response2.Content.ReadAsStringAsync();
                        resultado.Estatus = false;
                        resultado.Code = (int)response2.StatusCode;
                        FntLog.insertaLogApi(Constantes.REDECO, "Eliminar Quejas", resultado.Code, urlWithParameters, responseJson);
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.Estatus = false;
                resultado.Detalle = $"Error en la solicitud: {ex.Message}";
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef";
                log.Log.metodo = "Api_ElimnarQuejas";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return resultado;
        }

        #endregion

        public async Task<ClsResultadoAccion> EnviarQueja(string idEmpresa, string idUsuario, string username, int anio, List<ClsQuejas> quejas)
        {
            ClsResultadoAccion resultado = new();
            //RespuestaEnvioQuejas respuesta = new RespuestaEnvioQuejas();
            Condusef condusef = new Condusef();
            List<BolsaErroresQuejas> bolsaErrores = new List<BolsaErroresQuejas>();
            try
            {
                resultado = await condusef.ConsultaTokenUsuario(idUsuario,"1");
                if (resultado.Estatus)
                {
                    string tokenRedeco = resultado.Detalle;
                    foreach (var queja in quejas)
                    {
                        resultado = await Api_EnvioQuejas(tokenRedeco, queja);
                        if (resultado.Estatus)
                        {
                            string respuestaJson = resultado.Detalle;

                            resultado = Db_InsertaQueja(idEmpresa, anio, queja, respuestaJson);
                            
                            //if (_RespuestaEnvioQuejas.errors.Count > 0) resultado.Code = 400;
                            //else resultado.Code = 200;
                            //resultado.Estatus = true;
                        }
                        else
                        {
                            if(resultado.Code == 400)
                            {
                                BolsaErroresQuejas quejaConErrores = new BolsaErroresQuejas();
                                List<string> erroresDevueltos = new List<string>();
                                foreach (var error in ErrorEnvioCondusef.errors)
                                {
                                    string key = error.Key;
                                    List<string> values = error.Value;
                                    erroresDevueltos = error.Value;
                                }
                                if (ErrorEnvioCondusef.error != string.Empty) erroresDevueltos.Add(ErrorEnvioCondusef.error);
                                quejaConErrores = new BolsaErroresQuejas(queja,erroresDevueltos);
                                bolsaErrores.Add(quejaConErrores);
                            }
                        }
                        //else
                        //{
                        //    if (resultado.Detalle == "El token ha expirado")
                        //    {
                        //        resultado = await condusef.RenovarTokenUsuario(idUsuario, username, "1");
                        //        if (resultado.Estatus)
                        //        {
                        //            resultado = await Api_EnvioQuejas(resultado.Detalle, quejas);
                        //            if (resultado.Estatus)
                        //            {
                        //                string respuestaJson = resultado.Detalle;
                        //                foreach (var queja in _RespuestaEnvioQuejas.addedRows)
                        //                {
                        //                    resultado = Db_InsertaQueja(idEmpresa, anio, queja, respuestaJson);
                        //                }
                        //                if (_RespuestaEnvioQuejas.errors.Count > 0) resultado.Code = 400;
                        //                else resultado.Code = 200;
                        //                resultado.Estatus = true;
                        //            }
                        //        }
                        //    }
                        //}
                    }
                    if(resultado.Code < 500)
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
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef";
                log.Log.metodo = "EnviarQueja";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            BolsaErroresQuejas = bolsaErrores;
            return resultado;
        }

        public ClsResultadoAccion ConsultarQuejas(string idEmpresa, string periodo, string anio)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            resultado = Db_ConsultaQuejasEmpresa(idEmpresa, periodo, anio);
            return resultado;
        }

        public async Task<ClsResultadoAccion> EliminarQueja(string idEmpresa, string idUsuario, string username, string folio)
        {
            ClsResultadoAccion resultado = new();
            RespuestaEnvioQuejas respuesta = new RespuestaEnvioQuejas();
            Condusef condusef = new Condusef();
            try
            {
                resultado = await condusef.ConsultaTokenUsuario(idUsuario, "1");
                if (resultado.Estatus)
                {
                    resultado = await Api_EliminarQueja(resultado.Detalle, folio);
                    if (resultado.Estatus)
                    {
                        Db_EliminaQueja(idEmpresa, folio);
                    }
                    else
                    {
                        if (resultado.Detalle == "El token ha expirado")
                        {
                            resultado = await condusef.RenovarTokenUsuario(idUsuario, username, "1");
                            if (resultado.Estatus)
                            {
                                resultado = await Api_EliminarQueja(resultado.Detalle, folio);
                                if (resultado.Estatus)
                                {
                                    Db_EliminaQueja(idEmpresa, folio);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef";
                log.Log.metodo = "EnviarQueja";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return resultado;
        }

        public ClsResultadoAccion ConsultarRespuestaJson(string idEmpresa, string folio)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            try
            {
                string[] parametros = { "idEmpresa", "folio" };
                string[] values = { idEmpresa, folio };
                DataSet resultadoConsulta = BD_DLL.ClsBD.Consulta_con_parametros("redeco.consultar_respuesta_json", Conexion.CadenaConexion,
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
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef.Redeco";
                log.Log.metodo = "ConsultarRespuestaJson";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return resultado;
        }

        public async Task<ClsResultadoAccion> ProcesarLayoutQuejas(ClsStreamFile file)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            List<ClsQuejasLayout> quejasLayout = new List<ClsQuejasLayout>();
            try
            {
                resultado = await FntArchivos.GuardarArchivo(file.Path, file.File);
                if (resultado.Estatus)
                {
                    ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                    using (var templatePackage = new ExcelPackage(new FileInfo(file.Path)))
                    {
                        var ws = templatePackage.Workbook.Worksheets["Quejas"];
                        if (ws != null)
                        {
                            // Establece el rango a partir de la fila 2
                            var startRow = 3;
                            // Encuentra la última fila con datos en la hoja actual
                            var endRow = ws.Dimension?.End.Row ?? startRow;

                            // Limpia el contenido del rango especificado
                            for (int row = startRow; row <= endRow; row++)
                            {
                                ClsQuejasLayout queja = new ClsQuejasLayout();

                                queja.QuejasDenominacion = FntGenericas.ValidaNullString(ws.Cells[row, 1].Value?.ToString().Trim(), "").Trim();
                                queja.QuejasSector = FntGenericas.ValidaNullString(ws.Cells[row, 2].Value?.ToString().Trim(), "").Trim();
                                queja.QuejasNoMes = FntGenericas.ValidaNullint(ws.Cells[row, 3].Value?.ToString().Trim(), 0);
                                queja.QuejasNum = FntGenericas.ValidaNullint(ws.Cells[row, 4].Value?.ToString().Trim(), 1);
                                queja.QuejasFolio = FntGenericas.ValidaNullString(ws.Cells[row, 5].Value?.ToString().Trim(), "").Trim();
                                queja.QuejasFecRecepcion = FntGenericas.ValidaNullString(ws.Cells[row, 6].Value?.ToString().Trim(), "").Trim();
                                queja.QuejasMedio = FntGenericas.ValidaNullint(ws.Cells[row, 7].Value?.ToString().Trim(), 0);
                                queja.QuejasNivelAT = FntGenericas.ValidaNullint(ws.Cells[row, 8].Value?.ToString().Trim(), 0);
                                queja.QuejasProducto = FntGenericas.ValidaNullString(ws.Cells[row, 9].Value?.ToString().Trim(), "").Trim();
                                queja.QuejasCausa = FntGenericas.ValidaNullString(ws.Cells[row, 10].Value?.ToString().Trim(), "").Trim();
                                queja.QuejasPORI = FntGenericas.ValidaNullString(ws.Cells[row, 11].Value?.ToString().Trim(), "NO").Trim().ToUpper();
                                queja.QuejasEstatus = FntGenericas.ValidaNullint(ws.Cells[row, 12].Value?.ToString().Trim(), 1);
                                queja.QuejasEstados = FntGenericas.ValidaNullint(ws.Cells[row, 13].Value?.ToString().Trim(), 0);
                                queja.QuejasMunId = FntGenericas.ValidaNullint(ws.Cells[row, 14].Value?.ToString().Trim(), 0);
                                queja.QuejasLocId = FntGenericas.ValidaNullint(ws.Cells[row, 15].Value?.ToString().Trim(), 0);
                                queja.QuejasColId = FntGenericas.ValidaNullint(ws.Cells[row, 16].Value?.ToString().Trim(), 0);
                                queja.QuejasCP = FntGenericas.ValidaNullString(ws.Cells[row, 17].Value?.ToString().Trim(), "");
                                queja.QuejasTipoPersona = FntGenericas.ValidaNullint(ws.Cells[row, 18].Value?.ToString().Trim(), 1);
                                queja.QuejasSexo = FntGenericas.ValidaNullString(ws.Cells[row, 19].Value?.ToString().Trim(), "H").ToUpper();
                                queja.QuejasEdad = FntGenericas.ValidaNullString(ws.Cells[row, 20].Value?.ToString().Trim(), "").Trim();
                                queja.QuejasFecResolucion = FntGenericas.ValidaNullString(ws.Cells[row, 21].Value?.ToString().Trim(), "").Trim();
                                queja.QuejasFecNotificacion = FntGenericas.ValidaNullString(ws.Cells[row, 22].Value?.ToString().Trim(), "").Trim();
                                queja.QuejasRespuesta = FntGenericas.ValidaNullString(ws.Cells[row, 23].Value?.ToString().Trim(), "").Trim();
                                queja.QuejasNumPenal = FntGenericas.ValidaNullString(ws.Cells[row, 24].Value?.ToString().Trim(), "").Trim();
                                queja.QuejasPenalizacion = FntGenericas.ValidaNullString(ws.Cells[row, 25].Value?.ToString().Trim(), "").Trim();

                                queja.QuejasFecRecepcion = ExtractDate(queja.QuejasFecRecepcion);
                                queja.QuejasFecResolucion = ExtractDate(queja.QuejasFecResolucion);
                                queja.QuejasFecNotificacion = ExtractDate(queja.QuejasFecNotificacion);

                                queja = ValidarDatosNull(queja);

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
                log.Log.nameSpace = "Condusef_DLL.Funciones.Redeco";
                log.Log.metodo = "ProcesarLayoutQuejas";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
            }
            ListadoQuejasLayout = quejasLayout;
            return resultado;
        }

        #region DB
        private ClsResultadoAccion Db_InsertaQueja(string idEmpresa, int anio, ClsQuejas queja, string respuestaJson)
        {
            ClsResultadoAccion resultado = new();
            try
            {
                DateTime fechaSubida = DateTime.Now;

                string[] parametros = { "id_empresa", "periodo_mes", "periodo_anio", "quejas_num", "quejas_folio", "fecha_recepcion", "medio_id",
                    "nivel_at","product_id","causas_id","quejas_pori","quejas_estatus","id_estado","id_municipio","id_localidad","id_colonia",
                    "quejas_cp","tipo_persona","quejas_sexo","quejas_edad","fecha_resolucion","fecha_notificacion","id_tipo_respuesta",
                    "quejas_numero_penal","id_tipo_penalizacion", "fechaSubida", "respuestaJson"
                };
                string[] values = { idEmpresa, queja.QuejasNoMes.ToString(), anio.ToString(), queja.QuejasNum.ToString(), queja.QuejasFolio,
                    queja.QuejasFecRecepcion, queja.QuejasMedio.ToString(),
                    FntGenericas.ValidaNullString(queja.QuejasNivelAT.ToString(),"0"),
                    queja.QuejasProducto,queja.QuejasCausa.ToString(),
                    queja.QuejasPORI,queja.QuejasEstatus.ToString(),queja.QuejasEstados.ToString(),queja.QuejasMunId.ToString(),
                    FntGenericas.ValidaNullString(queja.QuejasLocId.ToString(),"0"),
                    FntGenericas.ValidaNullString(queja.QuejasColId.ToString(),"0"),
                    FntGenericas.ValidaNullString(queja.QuejasCP.ToString(), ""),
                    queja.QuejasTipoPersona.ToString(),
                    FntGenericas.ValidaNullString(queja.QuejasSexo,""),
                    FntGenericas.ValidaNullString(queja.QuejasEdad.ToString(),"0"),
                    FntGenericas.ValidaNullString(queja.QuejasFecResolucion,"01/01/1990"),
                    FntGenericas.ValidaNullString(queja.QuejasFecNotificacion,"01/01/1990"),
                    FntGenericas.ValidaNullString(queja.QuejasRespuesta.ToString(),"0"),
                    FntGenericas.ValidaNullString(queja.QuejasNumPenal.ToString(),"0"),
                    FntGenericas.ValidaNullString(queja.QuejasPenalizacion.ToString(),"0"),
                    fechaSubida.ToString("yyyyMMdd HH:mm:ss.fff"), respuestaJson
                };
                BD_DLL.ClsBD.Consulta_con_parametros("redeco.insertar_queja", Conexion.CadenaConexion,
                    parametros, values, Conexion.isolationLevelSnap());
                resultado.Estatus = true;
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef";
                log.Log.metodo = "Db_InsertaQueja";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
            }
            return resultado;
        }

        private ClsResultadoAccion Db_EliminaQueja(string idEmpresa, string quejaFolio)
        {
            ClsResultadoAccion resultado = new();
            try
            {
                string[] parametros = { "id_empresa", "quejas_folio" };
                string[] values = { idEmpresa, quejaFolio };
                BD_DLL.ClsBD.Consulta_con_parametros("redeco.eliminar_queja", Conexion.CadenaConexion,
                    parametros, values, Conexion.isolationLevelSnap());
                resultado.Estatus = true;
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef";
                log.Log.metodo = "Db_EliminaQueja";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
            }
            return resultado;
        }

        private ClsResultadoAccion Db_ConsultaQuejasEmpresa(string idEmpresa, string periodo, string anio)
        {
            ClsResultadoAccion resultado = new();
            List<ClsQuejas> quejas = new List<ClsQuejas>();
            try
            {
                string[] parametros = { "idEmpresa", "periodo", "anio" };
                string[] values = { idEmpresa, periodo, anio };
                DataSet resultadoConsulta = BD_DLL.ClsBD.Consulta_con_parametros("redeco.consultar_quejas_empresa", Conexion.CadenaConexion,
                    parametros, values, Conexion.isolationLevelSnap());
                if (resultadoConsulta.Tables.Count > 0)
                {
                    if (resultadoConsulta.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow fila in resultadoConsulta.Tables[0].Rows)
                        {
                            ClsQuejas queja = new ClsQuejas();
                            //queja.QuejasNoMes = FntGenericas.ValidaNullint(fila["periodo_mes"].ToString(), int.Parse(periodo));
                            queja.QuejasNum = FntGenericas.ValidaNullint(fila["quejas_num"].ToString(), 1); ;
                            queja.QuejasFolio = FntGenericas.ValidaNullString(fila["quejas_folio"].ToString(), ""); ;
                            queja.QuejasFecRecepcion = FntGenericas.ValidaNullString(fila["fecha_recepcion"].ToString().Substring(0,10), ""); ;
                            queja.QuejasMedio = FntGenericas.ValidaNullint(fila["medio_id"].ToString(), int.Parse(periodo)); ;
                            queja.QuejasNivelAT = FntGenericas.ValidaNullint(fila["nivel_at"].ToString(), int.Parse(periodo)); ;
                            queja.QuejasProducto = FntGenericas.ValidaNullString(fila["product_id"].ToString(), ""); ;
                            //queja.QuejasCausa = FntGenericas.ValidaNullint(fila["causas_id"].ToString(), int.Parse(periodo)); ;
                            queja.QuejasPORI = FntGenericas.ValidaNullString(fila["quejas_pori"].ToString(), "NO"); ;
                            queja.QuejasEstatus = FntGenericas.ValidaNullint(fila["quejas_estatus"].ToString(), 2); ;
                            queja.QuejasEstados = FntGenericas.ValidaNullint(fila["id_estado"].ToString(), 0); ;
                            //queja.QuejasMunId = FntGenericas.ValidaNullint(fila["id_municipio"].ToString(), 0); ;
                            //queja.QuejasColId = FntGenericas.ValidaNullint(fila["id_colonia"].ToString(), 0); ;
                            //queja.QuejasLocId = FntGenericas.ValidaNullint(fila["id_localidad"].ToString(), 0); ;
                            queja.QuejasCP = FntGenericas.ValidaNullint(fila["quejas_cp"].ToString(), 0); ;
                            queja.QuejasTipoPersona = FntGenericas.ValidaNullint(fila["tipo_persona"].ToString(), 1); ;
                            queja.QuejasSexo = FntGenericas.ValidaNullString(fila["quejas_sexo"].ToString(), "H"); ;
                            queja.QuejasEdad = FntGenericas.ValidaNullint(fila["quejas_edad"].ToString(), 0); ;
                            queja.QuejasFecResolucion = FntGenericas.ValidaNullString(fila["fecha_resolucion"].ToString().Substring(0, 10), ""); ;
                            queja.QuejasFecNotificacion = FntGenericas.ValidaNullString(fila["fecha_notificacion"].ToString().Substring(0, 10), ""); ;
                            queja.QuejasRespuesta = FntGenericas.ValidaNullint(fila["id_tipo_respuesta"].ToString(), 0); ;
                            queja.QuejasNumPenal = FntGenericas.ValidaNullint(fila["quejas_numero_penal"].ToString(), 0); ;
                            queja.QuejasPenalizacion = FntGenericas.ValidaNullint(fila["id_tipo_penalizacion"].ToString(), 0); ;

                            queja.CausaText = FntGenericas.ValidaNullString(fila["causa"].ToString(), "");
                            queja.ColoniaText = FntGenericas.ValidaNullString(fila["colonia"].ToString(), "");
                            queja.MunicipioText = FntGenericas.ValidaNullString(fila["municipio"].ToString(), "");
                            queja.FechaSubida = FntGenericas.ValidaNullDateTime(fila["fecha_subida"].ToString(), new DateTime());

                            quejas.Add(queja);
                        }
                        
                    }
                    resultado.Estatus = true;
                }
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef";
                log.Log.metodo = "Db_ConsultaQuejasEmpresa";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            ListadoQuejas = quejas;
            return resultado;
        }

        #endregion

        private ClsQuejasLayout ValidarDatosNull(ClsQuejasLayout info)
        {
            if (info.QuejasTipoPersona == 2)
            {
                info.QuejasSexo = "null";
                info.QuejasEdad = "null";
            }
            if (info.QuejasEstatus == 1)
            {
                info.QuejasPenalizacion = "null";
                info.QuejasRespuesta = "null";
                info.QuejasNumPenal = "null";
                info.QuejasFecResolucion = "null";
                info.QuejasFecNotificacion = "null";
            }

            if (string.IsNullOrEmpty(info.QuejasNumPenal) || info.QuejasNumPenal == "0")
                info.QuejasNumPenal = "null";

            if (string.IsNullOrEmpty(info.QuejasPenalizacion))
                info.QuejasPenalizacion = "null";

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
