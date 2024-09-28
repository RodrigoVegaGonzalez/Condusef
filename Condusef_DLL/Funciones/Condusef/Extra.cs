//using Condusef_DLL.Clases.Condusef.Reune;
//using Condusef_DLL.Clases.Condusef;
//using Condusef_DLL.Clases.General;
//using Condusef_DLL.Clases.Generales;
//using Condusef_DLL.Clases;
//using Condusef_DLL.Funciones.Generales;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Net.Http.Headers;
//using System.Text;
//using System.Text.Json;
//using System.Threading.Tasks;

//namespace Condusef_DLL.Funciones.Condusef
//{
//    internal class Extra
//    {
//        #region Reune
//        public void ConsultaTicketsReune(string idEmpresa)
//        {
//            try
//            {
//                string[] parametros = { "idEmpresa" };
//                string[] values = { idEmpresa };
//                DataSet resultadoConsulta = BD_DLL.ClsBD.Consulta_con_parametros("reune.consulta_tickets_empresa", Conexion.CadenaConexion, parametros, values, Conexion.isolationLevelSnap());
//                if (resultadoConsulta.Tables.Count > 0)
//                {
//                    foreach (DataRow fila in resultadoConsulta.Tables[0].Rows)
//                    {
//                        ClsTicket ticket = new ClsTicket();
//                        ticket.Id = FntGenericas.ValidaNullint(fila["id"].ToString(), 0);
//                        ticket.Ticket = FntGenericas.ValidaNullString(fila["ticket"].ToString(), "");
//                        ticket.Archivo = FntGenericas.ValidaNullString(fila["ruta_archivo"].ToString(), "");
//                        ticket.Archivo = Path.GetFileName(ticket.Archivo);
//                        ticket.Año = FntGenericas.ValidaNullint(fila["año"].ToString(), 0);
//                        ticket.FechaEnvio = FntGenericas.ValidaNullString(fila["fecha_envio"].ToString(), "");
//                        ticket.Periodo = FntGenericas.ValidaNullint(fila["periodo"].ToString(), 0);
//                        ticket.Estatus = FntGenericas.ValidaNullint(fila["estatus"].ToString(), 0);
//                        ticket.TipoDocumento = FntGenericas.ValidaNullint(fila["tipo_documento"].ToString(), 0);
//                        ListaTickets.Add(ticket);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                //FntLog log = new FntLog();
//                //log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef";
//                //log.Log.metodo = "ConsultaAqlist";
//                //log.Log.error = ex.Message;
//                //log.insertaLog();
//            }
//        }

//        public string GenerarNombreReune(string idEmpresa, int tipoDocumento, int periodo, int anio, string extension)
//        {
//            return string.Join("_", "Reune", idEmpresa, tipoDocumento, periodo, anio) + extension;
//        }

//        public async Task<ClsResultadoAccion> SubirReporteReune(string idEmpresa, ClsStreamFile file, int periodo, int anio, int estatus, int tipoDocumento)
//        {
//            ClsResultadoAccion resultado = new();
//            try
//            {
//                resultado = await FntArchivos.GuardarArchivo(file.Path, file.File);
//                if (resultado.Estatus)
//                {
//                    ClsStreamFile tempFile = await FntArchivos.LeerArchivoStream(file.Path);
//                    ClsToken token = ConsultarTokens(idEmpresa);
//                    resultado = await Api_SubirReune(tempFile.File, tempFile.FileName, token.TokenReune, tipoDocumento);
//                    string fechaEnvio = DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fff");
//                    if (resultado.Estatus)
//                    {
//                        string ticketClave = resultado.Detalle;
//                        resultado = await Api_EstatusTicketReune(token.TokenReune, ticketClave);
//                        if (resultado.Estatus)
//                        {
//                            //ClsTicket ticket = new(0, resultado.Detalle, file.Path, anio, periodo, estatus, fechaEnvio, tipoDocumento);
//                            ClsTicket ticket = new(0, ticketClave, file.Path, anio, periodo, estatus, fechaEnvio, tipoDocumento);
//                            resultado = Db_InsertaTicketReune(idEmpresa, ticket);

//                            if (resultado.Estatus)
//                            {
//                                foreach (ClsReuneFiles archivo in EstatusTicketReune.files)
//                                {
//                                    Db_InsertaArchivoTicketReune(resultado.Detalle, archivo);
//                                }
//                            }
//                        }
//                    }
//                }

//            }
//            catch (Exception ex)
//            {

//            }


//            return resultado;
//        }

//        public async Task<ClsResultadoAccion> ObtenerEstatusTicketReune(string idEmpresa, string ticket, int idTicket)
//        {
//            ClsResultadoAccion resultado = new();
//            try
//            {
//                ClsToken token = ConsultarTokens(idEmpresa);
//                resultado = await Api_EstatusTicketReune(token.TokenReune, ticket);
//                if (resultado.Estatus)
//                {
//                    int estatus = VerificarEstatusTicket2();
//                    resultado = Db_ActualizarEstatusTicketReune(idEmpresa, idTicket, estatus);
//                    foreach (ClsReuneFiles archivo in EstatusTicketReune.files)
//                    {
//                        Db_ActualizarArchivoTicketReune(idTicket, archivo);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {

//            }


//            return resultado;
//        }

//        public async Task<ClsResultadoAccion> CorregirTicketReune(string idEmpresa, ClsStreamFile file, ClsTicket ticket)
//        {
//            ClsResultadoAccion resultado = new();
//            try
//            {
//                ClsToken token = ConsultarTokens(idEmpresa);
//                resultado = await Api_CorregirReune(file.File, file.FileName, token.TokenReune, ticket.Ticket);
//                string fechaEnvio = DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fff");
//                if (resultado.Estatus)
//                {
//                    resultado = Db_ObtenerRutaTicketReune(idEmpresa, ticket);
//                    if (resultado.Estatus)
//                    {
//                        string directorio = Path.GetDirectoryName(resultado.Detalle);
//                        string newFile = Path.Combine(directorio, file.FileName);
//                        resultado = await FntArchivos.ActualizarArchivo(resultado.Detalle, file.File, newFile);
//                        if (resultado.Estatus)
//                        {
//                            ticket.Archivo = newFile;
//                            ticket.FechaEnvio = fechaEnvio;
//                            resultado = Db_ActualizarTicketReune(idEmpresa, ticket);
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {

//            }


//            return resultado;
//        }

//        public async Task<ClsResultadoAccion> EliminarTicket(string idEmpresa, string ticket, int idTicket)
//        {
//            ClsResultadoAccion resultado = new();
//            try
//            {
//                ClsToken token = ConsultarTokens(idEmpresa);
//                resultado = await Api_EliminarTicket(token.TokenReune, ticket);
//                if (resultado.Estatus)
//                {
//                    int estatus = 4; //Eliminado
//                    resultado = Db_ActualizarEstatusTicketReune(idEmpresa, idTicket, estatus);
//                }
//            }
//            catch (Exception ex)
//            {

//            }


//            return resultado;
//        }

//        public async Task<ClsResultadoAccion> Api_SubirReune(Stream file, string fileName, string token, int tipoDocumento)
//        {
//            ClsResultadoAccion resultado = new ClsResultadoAccion();

//            try
//            {
//                // URL de la API
//                string apiUrl = VariablesGlobales.Url.SubirReune;

//                // Crear una instancia de HttpClient
//                using (HttpClient client = new HttpClient())
//                {
//                    // Crear un objeto MultipartFormDataContent para contener el archivo
//                    using (var form = new MultipartFormDataContent())
//                    {
//                        // Crear un objeto StreamContent a partir del archivo
//                        var streamContent = new StreamContent(file);
//                        streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

//                        // Agregar el archivo al formulario
//                        form.Add(streamContent, "new_file", fileName);

//                        var tipoDocumentoContent = new StringContent(tipoDocumento.ToString());
//                        form.Add(tipoDocumentoContent, "tipo_documento");
//                        // Crear la solicitud HTTP POST
//                        using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, apiUrl))
//                        {
//                            // Agregar el token de autorización en los encabezados
//                            request.Headers.Add("Authorization", token);

//                            // Asignar el contenido del formulario a la solicitud
//                            request.Content = form;

//                            // Realizar la solicitud POST
//                            using (HttpResponseMessage response = await client.SendAsync(request))
//                            {
//                                // Verificar si la respuesta fue exitosa
//                                if (response.IsSuccessStatusCode)
//                                {
//                                    string content = await response.Content.ReadAsStringAsync();

//                                    var responseData = JsonSerializer.Deserialize<ClsSubirReune>(content);
//                                    resultado.Estatus = true;
//                                    resultado.Detalle = responseData.ticket;
//                                }
//                                else
//                                {
//                                    resultado.Estatus = false;
//                                    resultado.Detalle = $"Error en la solicitud: {response.StatusCode}";
//                                }
//                            }
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                resultado.Estatus = false;
//                resultado.Detalle = $"Error en la solicitud: {ex.Message}";
//            }

//            return resultado;
//        }

//        public async Task<ClsResultadoAccion> Api_ObtenerTicketSReune(string token)
//        {
//            ClsResultadoAccion resultado = new ClsResultadoAccion();
//            try
//            {
//                // URL de la API
//                string url = VariablesGlobales.Url.ObtenerTicketReune;

//                using (HttpClient client = new HttpClient())
//                {


//                    // Agrega el encabezado de autorización con el token
//                    client.DefaultRequestHeaders.Add("Authorization", token);

//                    // Realiza la solicitud GET
//                    HttpResponseMessage response = await client.GetAsync(url);

//                    // Verifica si la solicitud fue exitosa (código de estado 200)
//                    if (response.IsSuccessStatusCode)
//                    {
//                        // Lee y muestra el contenido de la respuesta
//                        string content = await response.Content.ReadAsStringAsync();

//                        var responseData = JsonSerializer.Deserialize<List<ClsTicketReune>>(content);
//                        resultado.Estatus = true;
//                        //resultado.Detalle = responseData[0].ticket;
//                    }

//                }

//            }
//            catch (Exception ex)
//            {
//                //FntLog log = new FntLog();
//                //log.Log.nameSpace = "Tareas_PLD.PLD.Funciones.LNAutomatica";
//                //log.Log.metodo = "EjecutarAPI";
//                //log.Log.error = ex.Message;
//                resultado.Estatus = false;
//                resultado.Detalle = ex.Message;
//                //log.insertaLog();
//            }
//            return resultado;
//        }

//        public async Task<ClsResultadoAccion> Api_EstatusTicketReune(string token, string ticket_clave)
//        {
//            ClsResultadoAccion resultado = new ClsResultadoAccion();
//            try
//            {
//                //// URL de la API
//                string url = VariablesGlobales.Url.EstatusTicketReune;

//                // Crear objeto JSON con el campo "ticket"
//                var json = new { ticket = ticket_clave };

//                // Convertir el objeto JSON a una cadena
//                var jsonString = JsonSerializer.Serialize(json);

//                // Crear objeto StringContent con la cadena JSON y el tipo de contenido "application/json"
//                var content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");

//                // Crear objeto HttpClient con el encabezado de autorización
//                using (var client = new HttpClient())
//                {
//                    client.DefaultRequestHeaders.Add("Authorization", token);

//                    // Realizar solicitud POST a la URL con el objeto StringContent en el cuerpo
//                    var response = await client.PostAsync(url, content);
//                    if (response.IsSuccessStatusCode)
//                    {
//                        // Leer la respuesta como una cadena
//                        var responseString = await response.Content.ReadAsStringAsync();

//                        // Devolver la cadena de respuesta
//                        var responseData = JsonSerializer.Deserialize<ClsEstatusReune>(responseString);
//                        EstatusTicketReune = responseData;
//                        resultado.Estatus = true;
//                        resultado.Detalle = responseData.ticketData.ticket;
//                    }

//                }

//            }
//            catch (Exception ex)
//            {
//                //FntLog log = new FntLog();
//                //log.Log.nameSpace = "Tareas_PLD.PLD.Funciones.LNAutomatica";
//                //log.Log.metodo = "EjecutarAPI";
//                //log.Log.error = ex.Message;
//                resultado.Estatus = false;
//                resultado.Detalle = ex.Message;
//                //log.insertaLog();
//            }
//            return resultado;
//        }

//        public async Task<ClsResultadoAccion> Api_CorregirReune(Stream file, string fileName, string token, string ticket)
//        {
//            ClsResultadoAccion resultado = new ClsResultadoAccion();
//            try
//            {
//                // URL de la API
//                string apiUrl = VariablesGlobales.Url.CorregirDocumentoReune;
//                // Datos que enviarás en el cuerpo de la solicitud POST (en formato JSON)

//                // Crear una instancia de HttpClient
//                using (HttpClient client = new HttpClient())
//                {
//                    // Convertir los datos a formato JSON
//                    //string json = JsonSerializer.Serialize();
//                    var form = new MultipartFormDataContent();
//                    form.Add(new StringContent(ticket), "ticket");
//                    form.Add(new StreamContent(file)
//                    {
//                        Headers =
//                        {
//                            ContentDisposition = new ContentDispositionHeaderValue("form-data")
//                            {
//                                Name = "file",
//                                FileName = fileName
//                            }
//                        }
//                    }, "new_file");

//                    HttpRequestMessage request = new HttpRequestMessage();
//                    HttpResponseMessage response = null;

//                    request.RequestUri = new Uri(apiUrl);
//                    request.Method = HttpMethod.Put;
//                    //client.DefaultRequestHeaders.Add("Accept", "application/json;charset=UTF-8");

//                    //request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//                    request.Headers.Add("Authorization", token);
//                    //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
//                    //HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
//                    //request.Content = content;
//                    request.Content = form;

//                    //client.Timeout = TimeSpan.FromMinutes(15); // Aumentar el tiempo de espera a 5 minutos
//                    // Realizar la solicitud POST
//                    response = client.SendAsync(request).Result;

//                    // Verificar si la respuesta fue exitosa
//                    if (response.IsSuccessStatusCode)
//                    {
//                        // Leer la respuesta como una cadena de texto
//                        //string responseJson = await response.Content.ReadAsStringAsync();
//                        //var responseData = JsonConvert.DeserializeObject(responseJson);

//                        //RespuestaPeticion respuesta = JsonConvert.DeserializeObject<RespuestaPeticion>(responseJson);
//                        // Acceder al contenido de "resultados"
//                        //List<ClsMBAv2> resultados = respuesta.Resultados;
//                        //mba_v2 = resultados;
//                        resultado.Estatus = true;
//                    }
//                }

//            }
//            catch (Exception ex)
//            {
//                //FntLog log = new FntLog();
//                //log.Log.nameSpace = "Tareas_PLD.PLD.Funciones.LNAutomatica";
//                //log.Log.metodo = "EjecutarAPI";
//                //log.Log.error = ex.Message;
//                resultado.Estatus = false;
//                resultado.Detalle = ex.Message;
//                //log.insertaLog();
//            }
//            return resultado;
//        }

//        public async Task<ClsResultadoAccion> Api_EliminarTicket(string token, string ticket_clave)
//        {
//            ClsResultadoAccion resultado = new ClsResultadoAccion();
//            try
//            {
//                //// URL de la API
//                string url = VariablesGlobales.Url.EliminarDocumentoReune;

//                // Crear objeto JSON con el campo "ticket"
//                var json = new { id_file = ticket_clave };

//                // Convertir el objeto JSON a una cadena
//                var jsonString = JsonSerializer.Serialize(json);

//                // Crear objeto StringContent con la cadena JSON y el tipo de contenido "application/json"
//                var content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");

//                // Crear objeto HttpClient con el encabezado de autorización
//                using (var client = new HttpClient())
//                {
//                    client.DefaultRequestHeaders.Add("Authorization", token);

//                    // Realizar solicitud POST a la URL con el objeto StringContent en el cuerpo
//                    var response = await client.PostAsync(url, content);
//                    if (response.IsSuccessStatusCode)
//                    {
//                        // Leer la respuesta como una cadena
//                        //var responseString = await response.Content.ReadAsStringAsync();

//                        // Devolver la cadena de respuesta
//                        //var responseData = JsonSerializer.Deserialize<ClsEstatusReune>(responseString);
//                        //EstatusTicketReune = responseData;
//                        resultado.Estatus = true;
//                    }

//                }

//            }
//            catch (Exception ex)
//            {
//                //FntLog log = new FntLog();
//                //log.Log.nameSpace = "Tareas_PLD.PLD.Funciones.LNAutomatica";
//                //log.Log.metodo = "EjecutarAPI";
//                //log.Log.error = ex.Message;
//                resultado.Estatus = false;
//                resultado.Detalle = ex.Message;
//                //log.insertaLog();
//            }
//            return resultado;
//        }

//        public ClsResultadoAccion Db_InsertaTicketReune(string idEmpresa, ClsTicket ticket)
//        {
//            ClsResultadoAccion resultado = new();
//            try
//            {
//                string[] parametros = { "idEmpresa", "ticket", "archivo", "anio", "periodo", "estatus", "fechaEnvio", "tipoDocumento" };
//                string[] values = { idEmpresa, ticket.Ticket, ticket.Archivo, ticket.Año.ToString(), ticket.Periodo.ToString(),
//                    ticket.Estatus.ToString(), ticket.FechaEnvio, ticket.TipoDocumento.ToString() };
//                DataSet resultadoConsulta = BD_DLL.ClsBD.Consulta_con_parametros("reune.insertar_nuevo_ticket", Conexion.CadenaConexion,
//                    parametros, values, Conexion.isolationLevelSnap());
//                if (resultadoConsulta.Tables.Count > 0)
//                {
//                    foreach (DataRow fila in resultadoConsulta.Tables[0].Rows)
//                    {
//                        resultado.Detalle = FntGenericas.ValidaNullString(fila["id_ticket"].ToString(), "");
//                    }
//                }
//                resultado.Estatus = true;
//            }
//            catch (Exception ex)
//            {
//                //FntLog log = new FntLog();
//                //log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef";
//                //log.Log.metodo = "ConsultaAqlist";
//                //log.Log.error = ex.Message;
//                //log.insertaLog();
//                resultado.Estatus = false;
//                resultado.Code = 1;
//            }
//            return resultado;
//        }

//        public void Db_InsertaArchivoTicketReune(string idTicket, ClsReuneFiles archivo)
//        {
//            ClsResultadoAccion resultado = new();
//            try
//            {
//                bool validado = ValidarEstatus(archivo.status);
//                string nombreArchivo = EliminarPalabra(archivo.file, VariablesGlobales.RutaReune);
//                string[] parametros = { "idTicket", "idArchivo", "validado", "nombreArchivo", "tieneErrores" };
//                string[] values = { idTicket, archivo.file_id, validado.ToString(), nombreArchivo,
//                    archivo.file_with_error.ToString() };
//                BD_DLL.ClsBD.Consulta_con_parametros("reune.insertar_archivo_ticket",
//                    Conexion.CadenaConexion, parametros, values, Conexion.isolationLevelSnap());
//                resultado.Estatus = true;
//            }
//            catch (Exception ex)
//            {
//                //FntLog log = new FntLog();
//                //log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef";
//                //log.Log.metodo = "ConsultaAqlist";
//                //log.Log.error = ex.Message;
//                //log.insertaLog();
//                //resultado.Estatus = false;
//                //resultado.Code = 1;
//            }
//            //return resultado;
//        }

//        public void Db_ActualizarArchivoTicketReune(int idTicket, ClsReuneFiles archivo)
//        {
//            ClsResultadoAccion resultado = new();
//            try
//            {
//                bool validado = ValidarEstatus(archivo.status);
//                string nombreArchivo = EliminarPalabra(archivo.file, VariablesGlobales.RutaReune);
//                string[] parametros = { "idTicket", "idArchivo", "validado", "nombreArchivo", "tieneErrores" };
//                string[] values = { idTicket.ToString(), archivo.file_id, validado.ToString(), nombreArchivo,
//                    archivo.file_with_error.ToString() };
//                BD_DLL.ClsBD.Consulta_con_parametros("reune.actualizar_archivo_ticket",
//                    Conexion.CadenaConexion, parametros, values, Conexion.isolationLevelSnap());
//                resultado.Estatus = true;
//            }
//            catch (Exception ex)
//            {
//                //FntLog log = new FntLog();
//                //log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef";
//                //log.Log.metodo = "ConsultaAqlist";
//                //log.Log.error = ex.Message;
//                //log.insertaLog();
//                //resultado.Estatus = false;
//                //resultado.Code = 1;
//            }
//            //return resultado;
//        }

//        public ClsResultadoAccion Db_ActualizarTicketReune(string idEmpresa, ClsTicket ticket)
//        {
//            ClsResultadoAccion resultado = new();
//            try
//            {
//                string[] parametros = { "idEmpresa", "idTicket", "archivo", "fechaEnvio" };
//                string[] values = { idEmpresa, ticket.Id.ToString(), ticket.Archivo, ticket.FechaEnvio };
//                BD_DLL.ClsBD.Consulta_con_parametros("reune.corregir_ticket", Conexion.CadenaConexion,
//                    parametros, values, Conexion.isolationLevelSnap());
//                resultado.Estatus = true;
//            }
//            catch (Exception ex)
//            {
//                //FntLog log = new FntLog();
//                //log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef";
//                //log.Log.metodo = "ConsultaAqlist";
//                //log.Log.error = ex.Message;
//                //log.insertaLog();
//                resultado.Estatus = false;
//            }
//            return resultado;
//        }

//        public ClsResultadoAccion Db_ActualizarEstatusTicketReune(string idEmpresa, int idTicket, int estatus)
//        {
//            ClsResultadoAccion resultado = new();
//            try
//            {
//                string[] parametros = { "idEmpresa", "idTicket", "estatus" };
//                string[] values = { idEmpresa, idTicket.ToString(), estatus.ToString() };
//                BD_DLL.ClsBD.Consulta_con_parametros("reune.actualizar_ticket_reune", Conexion.CadenaConexion,
//                    parametros, values, Conexion.isolationLevelSnap());
//                resultado.Estatus = true;
//            }
//            catch (Exception ex)
//            {
//                //FntLog log = new FntLog();
//                //log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef";
//                //log.Log.metodo = "ConsultaAqlist";
//                //log.Log.error = ex.Message;
//                //log.insertaLog();
//                resultado.Estatus = false;
//            }
//            return resultado;
//        }

//        public ClsResultadoAccion Db_ObtenerRutaTicketReune(string idEmpresa, ClsTicket ticket)
//        {
//            ClsResultadoAccion resultado = new();
//            try
//            {
//                string[] parametros = { "idEmpresa", "idTicket" };
//                string[] values = { idEmpresa, ticket.Id.ToString() };
//                DataSet resultadoConsulta = BD_DLL.ClsBD.Consulta_con_parametros("reune.obtener_archivo_ticket", Conexion.CadenaConexion, parametros, values, Conexion.isolationLevelSnap());
//                if (resultadoConsulta.Tables.Count > 0)
//                {
//                    foreach (DataRow fila in resultadoConsulta.Tables[0].Rows)
//                    {
//                        resultado.Estatus = true;
//                        resultado.Detalle = FntGenericas.ValidaNullString(fila["ruta_archivo"].ToString(), "");
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                //FntLog log = new FntLog();
//                //log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef";
//                //log.Log.metodo = "ConsultaAqlist";
//                //log.Log.error = ex.Message;
//                //log.insertaLog();
//                resultado.Estatus = false;
//            }
//            return resultado;
//        }

//        public ClsResultadoAccion ConsultaArchivosTicketReune(int idTicket)
//        {
//            ClsResultadoAccion resultado = new();
//            try
//            {
//                List<ClsReuneFiles> archivos = new();
//                string[] parametros = { "idTicket" };
//                string[] values = { idTicket.ToString() };
//                DataSet resultadoConsulta = BD_DLL.ClsBD.Consulta_con_parametros("reune.consultar_archivos_ticket", Conexion.CadenaConexion, parametros, values, Conexion.isolationLevelSnap());
//                if (resultadoConsulta.Tables.Count > 0)
//                {
//                    foreach (DataRow fila in resultadoConsulta.Tables[0].Rows)
//                    {
//                        ClsReuneFiles archivo = new();
//                        archivo.id = FntGenericas.ValidaNullint(fila["id"].ToString(), 0);
//                        archivo.file_id = FntGenericas.ValidaNullString(fila["id_archivo"].ToString(), "");
//                        archivo.file = FntGenericas.ValidaNullString(fila["nombre_archivo"].ToString(), "");
//                        archivo.status = FntGenericas.ValidaNullString(fila["validado"].ToString(), "");
//                        archivo.file_with_error = FntGenericas.ValidaNullBool(fila["tiene_errores"].ToString(), false);
//                        archivos.Add(archivo);
//                    }
//                    ArchivosReune = archivos;
//                    resultado.Estatus = true;
//                }
//            }
//            catch (Exception ex)
//            {
//                //FntLog log = new FntLog();
//                //log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef";
//                //log.Log.metodo = "ConsultaAqlist";
//                //log.Log.error = ex.Message;
//                //log.insertaLog();
//                resultado.Estatus = false;
//            }
//            return resultado;
//        }

//        #endregion

//        #region functions
//        private int VerificarEstatusTicket()
//        {
//            if (EstatusTicket.totalErrors == 0) return 3;
//            else return 2;

//        }
//        private int VerificarEstatusTicket2()
//        {
//            return 1;

//        }
//        private bool ValidarEstatus(string input)
//        {
//            // Convertir la cadena a minúsculas para hacer la comparación insensible a mayúsculas
//            string lowerCaseInput = input.ToLower();

//            // Verificar si la cadena contiene "no"
//            return !lowerCaseInput.Contains("no");
//        }

//        private string EliminarPalabra(string informacion, string palabraEliminar)
//        {
//            // Utilizar Replace para eliminar la palabra de la cadena
//            string resultado = informacion.Replace(palabraEliminar, "");

//            return resultado;
//        }

//        #endregion
//    }
//}


//using Condusef_DLL.Clases.Condusef.Redeco;
//using Condusef_DLL.Clases.Condusef;
//using Condusef_DLL.Clases.General;
//using Condusef_DLL.Clases.Generales;
//using Condusef_DLL.Clases;
//using Condusef_DLL.Funciones.Generales;
//using System.Data;
//using System.Net.Http.Headers;
//using System.Text.Json;

//public void ConsultaTicketsRedeco(string idEmpresa)
//{
//    try
//    {
//        string[] parametros = { "idEmpresa" };
//        string[] values = { idEmpresa };
//        DataSet resultadoConsulta = BD_DLL.ClsBD.Consulta_con_parametros("redeco.consulta_tickets_empresa", Conexion.CadenaConexion, parametros, values, Conexion.isolationLevelSnap());
//        if (resultadoConsulta.Tables.Count > 0)
//        {
//            foreach (DataRow fila in resultadoConsulta.Tables[0].Rows)
//            {
//                ClsTicket ticket = new ClsTicket();
//                ticket.Id = FntGenericas.ValidaNullint(fila["id"].ToString(), 0);
//                ticket.Ticket = FntGenericas.ValidaNullString(fila["ticket"].ToString(), "");
//                ticket.Archivo = FntGenericas.ValidaNullString(fila["ruta_archivo"].ToString(), "");
//                ticket.Archivo = Path.GetFileName(ticket.Archivo);
//                ticket.Año = FntGenericas.ValidaNullint(fila["año"].ToString(), 0);
//                ticket.FechaEnvio = FntGenericas.ValidaNullString(fila["fecha_envio"].ToString(), "");
//                ticket.Periodo = FntGenericas.ValidaNullint(fila["periodo"].ToString(), 0);
//                ticket.Estatus = FntGenericas.ValidaNullint(fila["estatus"].ToString(), 0);
//                ListaTickets.Add(ticket);
//            }
//        }
//    }
//    catch (Exception ex)
//    {
//        //FntLog log = new FntLog();
//        //log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef";
//        //log.Log.metodo = "ConsultaAqlist";
//        //log.Log.error = ex.Message;
//        //log.insertaLog();
//    }
//}

//public string GenerarNombreRedeco(string idEmpresa, int periodo, int anio, string extension)
//{
//    return string.Join("_", "Redeco", idEmpresa, periodo, anio) + extension;
//}

//public async Task<ClsResultadoAccion> SubirReporteRedeco(string idEmpresa, ClsStreamFile file, int periodo, int anio, int estatus)
//{
//    ClsResultadoAccion resultado = new();
//    try
//    {
//        resultado = await FntArchivos.GuardarArchivo(file.Path, file.File);
//        if (resultado.Estatus)
//        {
//            ClsStreamFile tempFile = await FntArchivos.LeerArchivoStream(file.Path);
//            ClsToken token = ConsultarTokens(idEmpresa);
//            resultado = await Api_SubirRedeco(tempFile.File, tempFile.FileName, token.TokenRedeco);
//            string fechaEnvio = DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fff");
//            if (resultado.Estatus)
//            {
//                if (resultado.Estatus)
//                {
//                    ClsTicket ticket = new(0, resultado.Detalle, file.Path, anio, periodo, estatus, fechaEnvio, 0);
//                    resultado = Db_InsertaTicketRedeco(idEmpresa, ticket);
//                }
//            }
//        }

//    }
//    catch (Exception ex)
//    {

//    }


//    return resultado;
//}

//public async Task<ClsResultadoAccion> ObtenerEstatusTicketRedeco(string idEmpresa, string ticket, int idTicket)
//{
//    ClsResultadoAccion resultado = new();
//    try
//    {
//        ClsToken token = ConsultarTokens(idEmpresa);
//        resultado = await Api_EstatusTicketRedeco(token.TokenRedeco, ticket);
//        if (resultado.Estatus)
//        {
//            if (EstatusTicket.processed)
//            {
//                int estatus = VerificarEstatusTicket();
//                resultado = Db_ActualizarEstatusTicketRedeco(idEmpresa, idTicket, estatus);
//            }
//        }
//    }
//    catch (Exception ex)
//    {

//    }


//    return resultado;
//}

//public async Task<ClsResultadoAccion> CorregirTicketRedeco(string idEmpresa, ClsStreamFile file, ClsTicket ticket)
//{
//    ClsResultadoAccion resultado = new();
//    try
//    {
//        ClsToken token = ConsultarTokens(idEmpresa);
//        resultado = await Api_CorregirRedeco(file.File, file.FileName, token.TokenRedeco, ticket.Ticket);
//        string fechaEnvio = DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fff");
//        if (resultado.Estatus)
//        {
//            resultado = Db_ObtenerRutaTicketRedeco(idEmpresa, ticket);
//            if (resultado.Estatus)
//            {
//                string directorio = Path.GetDirectoryName(resultado.Detalle);
//                string newFile = Path.Combine(directorio, file.FileName);
//                resultado = await FntArchivos.ActualizarArchivo(resultado.Detalle, file.File, newFile);
//                if (resultado.Estatus)
//                {
//                    ticket.Archivo = newFile;
//                    ticket.FechaEnvio = fechaEnvio;
//                    resultado = Db_ActualizarTicketRedeco(idEmpresa, ticket);
//                }
//            }
//        }
//    }
//    catch (Exception ex)
//    {

//    }


//    return resultado;
//}

//public async Task<ClsResultadoAccion> Api_SubirRedeco(Stream file, string fileName, string token)
//{
//    ClsResultadoAccion resultado = new ClsResultadoAccion();

//    try
//    {
//        // URL de la API
//        string apiUrl = VariablesGlobales.Url.SubirRedeco;

//        // Crear una instancia de HttpClient
//        using (HttpClient client = new HttpClient())
//        {
//            // Crear un objeto MultipartFormDataContent para contener el archivo
//            using (var form = new MultipartFormDataContent())
//            {
//                // Crear un objeto StreamContent a partir del archivo
//                var streamContent = new StreamContent(file);
//                streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

//                // Agregar el archivo al formulario
//                form.Add(streamContent, "new_file", fileName);

//                // Crear la solicitud HTTP POST
//                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, apiUrl))
//                {
//                    // Agregar el token de autorización en los encabezados
//                    request.Headers.Add("Authorization", token);

//                    // Asignar el contenido del formulario a la solicitud
//                    request.Content = form;

//                    // Realizar la solicitud POST
//                    using (HttpResponseMessage response = await client.SendAsync(request))
//                    {
//                        // Verificar si la respuesta fue exitosa
//                        if (response.IsSuccessStatusCode)
//                        {
//                            // Realizar acciones adicionales si es necesario
//                            resultado = await Api_ObtenerTicketRedeco(token);
//                        }
//                        else
//                        {
//                            resultado.Estatus = false;
//                            resultado.Detalle = $"Error en la solicitud: {response.StatusCode}";
//                        }
//                    }
//                }
//            }
//        }
//    }
//    catch (Exception ex)
//    {
//        resultado.Estatus = false;
//        resultado.Detalle = $"Error en la solicitud: {ex.Message}";
//    }

//    return resultado;
//}

//public async Task<ClsResultadoAccion> Api_ObtenerTicketRedeco(string token)
//{
//    ClsResultadoAccion resultado = new ClsResultadoAccion();
//    try
//    {
//        // URL de la API
//        string url = VariablesGlobales.Url.ObtenerTicketRedeco;

//        using (HttpClient client = new HttpClient())
//        {


//            // Agrega el encabezado de autorización con el token
//            client.DefaultRequestHeaders.Add("Authorization", token);

//            // Realiza la solicitud GET
//            HttpResponseMessage response = await client.GetAsync(url);

//            // Verifica si la solicitud fue exitosa (código de estado 200)
//            if (response.IsSuccessStatusCode)
//            {
//                // Lee y muestra el contenido de la respuesta
//                string content = await response.Content.ReadAsStringAsync();

//                var responseData = JsonSerializer.Deserialize<List<ClsTicketRedeco>>(content);
//                Console.WriteLine("Respuesta exitosa:");
//                Console.WriteLine(content);
//                resultado.Estatus = true;
//                resultado.Detalle = responseData[0].ticket;
//            }

//        }

//    }
//    catch (Exception ex)
//    {
//        //FntLog log = new FntLog();
//        //log.Log.nameSpace = "Tareas_PLD.PLD.Funciones.LNAutomatica";
//        //log.Log.metodo = "EjecutarAPI";
//        //log.Log.error = ex.Message;
//        resultado.Estatus = false;
//        resultado.Detalle = ex.Message;
//        //log.insertaLog();
//    }
//    return resultado;
//}

//public async Task<ClsResultadoAccion> Api_EstatusTicketRedeco(string token, string ticket_clave)
//{
//    ClsResultadoAccion resultado = new ClsResultadoAccion();
//    try
//    {
//        //// URL de la API
//        string url = VariablesGlobales.Url.EstatusTicketRedeco;

//        // Crear objeto JSON con el campo "ticket"
//        var json = new { ticket = ticket_clave };

//        // Convertir el objeto JSON a una cadena
//        var jsonString = JsonSerializer.Serialize(json);

//        // Crear objeto StringContent con la cadena JSON y el tipo de contenido "application/json"
//        var content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");

//        // Crear objeto HttpClient con el encabezado de autorización
//        using (var client = new HttpClient())
//        {
//            client.DefaultRequestHeaders.Add("Authorization", token);

//            // Realizar solicitud POST a la URL con el objeto StringContent en el cuerpo
//            var response = await client.PostAsync(url, content);
//            if (response.IsSuccessStatusCode)
//            {
//                // Leer la respuesta como una cadena
//                var responseString = await response.Content.ReadAsStringAsync();

//                // Devolver la cadena de respuesta
//                var responseData = JsonSerializer.Deserialize<ClsEstatusTicket>(responseString);
//                EstatusTicket = responseData;
//                resultado.Estatus = true;
//            }

//        }

//    }
//    catch (Exception ex)
//    {
//        //FntLog log = new FntLog();
//        //log.Log.nameSpace = "Tareas_PLD.PLD.Funciones.LNAutomatica";
//        //log.Log.metodo = "EjecutarAPI";
//        //log.Log.error = ex.Message;
//        resultado.Estatus = false;
//        resultado.Detalle = ex.Message;
//        //log.insertaLog();
//    }
//    return resultado;
//}

//public async Task<ClsResultadoAccion> Api_CorregirRedeco(Stream file, string fileName, string token, string ticket)
//{
//    ClsResultadoAccion resultado = new ClsResultadoAccion();
//    try
//    {
//        // URL de la API
//        string apiUrl = VariablesGlobales.Url.CorregirTicketRedeco;
//        // Datos que enviarás en el cuerpo de la solicitud POST (en formato JSON)

//        // Crear una instancia de HttpClient
//        using (HttpClient client = new HttpClient())
//        {
//            // Convertir los datos a formato JSON
//            //string json = JsonSerializer.Serialize();
//            var form = new MultipartFormDataContent();
//            form.Add(new StringContent(ticket), "ticket");
//            form.Add(new StreamContent(file)
//            {
//                Headers =
//                        {
//                            ContentDisposition = new ContentDispositionHeaderValue("form-data")
//                            {
//                                Name = "file",
//                                FileName = fileName
//                            }
//                        }
//            }, "new_file");

//            HttpRequestMessage request = new HttpRequestMessage();
//            HttpResponseMessage response = null;

//            request.RequestUri = new Uri(apiUrl);
//            request.Method = HttpMethod.Put;
//            //client.DefaultRequestHeaders.Add("Accept", "application/json;charset=UTF-8");

//            //request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//            request.Headers.Add("Authorization", token);
//            //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
//            //HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
//            //request.Content = content;
//            request.Content = form;

//            //client.Timeout = TimeSpan.FromMinutes(15); // Aumentar el tiempo de espera a 5 minutos
//            // Realizar la solicitud POST
//            response = client.SendAsync(request).Result;

//            // Verificar si la respuesta fue exitosa
//            if (response.IsSuccessStatusCode)
//            {
//                // Leer la respuesta como una cadena de texto
//                //string responseJson = await response.Content.ReadAsStringAsync();
//                //var responseData = JsonConvert.DeserializeObject(responseJson);

//                //RespuestaPeticion respuesta = JsonConvert.DeserializeObject<RespuestaPeticion>(responseJson);
//                // Acceder al contenido de "resultados"
//                //List<ClsMBAv2> resultados = respuesta.Resultados;
//                //mba_v2 = resultados;
//                resultado.Estatus = true;
//            }
//        }

//    }
//    catch (Exception ex)
//    {
//        //FntLog log = new FntLog();
//        //log.Log.nameSpace = "Tareas_PLD.PLD.Funciones.LNAutomatica";
//        //log.Log.metodo = "EjecutarAPI";
//        //log.Log.error = ex.Message;
//        resultado.Estatus = false;
//        resultado.Detalle = ex.Message;
//        //log.insertaLog();
//    }
//    return resultado;
//}

//public ClsResultadoAccion Db_InsertaTicketRedeco(string idEmpresa, ClsTicket ticket)
//{
//    ClsResultadoAccion resultado = new();
//    try
//    {
//        string[] parametros = { "idEmpresa", "ticket", "archivo", "anio", "periodo", "estatus", "fechaEnvio" };
//        string[] values = { idEmpresa, ticket.Ticket, ticket.Archivo, ticket.Año.ToString(), ticket.Periodo.ToString(),
//                    ticket.Estatus.ToString(), ticket.FechaEnvio };
//        BD_DLL.ClsBD.Consulta_con_parametros("redeco.insertar_nuevo_ticket", Conexion.CadenaConexion,
//            parametros, values, Conexion.isolationLevelSnap());
//        resultado.Estatus = true;
//    }
//    catch (Exception ex)
//    {
//        //FntLog log = new FntLog();
//        //log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef";
//        //log.Log.metodo = "ConsultaAqlist";
//        //log.Log.error = ex.Message;
//        //log.insertaLog();
//        resultado.Estatus = false;
//    }
//    return resultado;
//}

//public ClsResultadoAccion Db_ActualizarTicketRedeco(string idEmpresa, ClsTicket ticket)
//{
//    ClsResultadoAccion resultado = new();
//    try
//    {
//        string[] parametros = { "idEmpresa", "idTicket", "archivo", "fechaEnvio" };
//        string[] values = { idEmpresa, ticket.Id.ToString(), ticket.Archivo, ticket.FechaEnvio };
//        BD_DLL.ClsBD.Consulta_con_parametros("redeco.corregir_ticket", Conexion.CadenaConexion,
//            parametros, values, Conexion.isolationLevelSnap());
//        resultado.Estatus = true;
//    }
//    catch (Exception ex)
//    {
//        //FntLog log = new FntLog();
//        //log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef";
//        //log.Log.metodo = "ConsultaAqlist";
//        //log.Log.error = ex.Message;
//        //log.insertaLog();
//        resultado.Estatus = false;
//    }
//    return resultado;
//}

//public ClsResultadoAccion Db_ActualizarEstatusTicketRedeco(string idEmpresa, int idTicket, int estatus)
//{
//    ClsResultadoAccion resultado = new();
//    try
//    {
//        string[] parametros = { "idEmpresa", "idTicket", "estatus" };
//        string[] values = { idEmpresa, idTicket.ToString(), estatus.ToString() };
//        BD_DLL.ClsBD.Consulta_con_parametros("redeco.actualizar_ticket_redeco", Conexion.CadenaConexion,
//            parametros, values, Conexion.isolationLevelSnap());
//        resultado.Estatus = true;
//    }
//    catch (Exception ex)
//    {
//        //FntLog log = new FntLog();
//        //log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef";
//        //log.Log.metodo = "ConsultaAqlist";
//        //log.Log.error = ex.Message;
//        //log.insertaLog();
//        resultado.Estatus = false;
//    }
//    return resultado;
//}

//public ClsResultadoAccion Db_ObtenerRutaTicketRedeco(string idEmpresa, ClsTicket ticket)
//{
//    ClsResultadoAccion resultado = new();
//    try
//    {
//        string[] parametros = { "idEmpresa", "idTicket" };
//        string[] values = { idEmpresa, ticket.Id.ToString() };
//        DataSet resultadoConsulta = BD_DLL.ClsBD.Consulta_con_parametros("redeco.obtener_archivo_ticket", Conexion.CadenaConexion, parametros, values, Conexion.isolationLevelSnap());
//        if (resultadoConsulta.Tables.Count > 0)
//        {
//            foreach (DataRow fila in resultadoConsulta.Tables[0].Rows)
//            {
//                resultado.Estatus = true;
//                resultado.Detalle = FntGenericas.ValidaNullString(fila["ruta_archivo"].ToString(), "");
//            }
//        }
//    }
//    catch (Exception ex)
//    {
//        //FntLog log = new FntLog();
//        //log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef";
//        //log.Log.metodo = "ConsultaAqlist";
//        //log.Log.error = ex.Message;
//        //log.insertaLog();
//        resultado.Estatus = false;
//    }
//    return resultado;
//}

//#endregion