using Condusef_DLL.Clases.General;
using Condusef_DLL.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Condusef_DLL.Clases.Generales;

namespace Condusef_DLL.Funciones.Generales
{
    public class FntEnvioCorreo
    {

        public ClsCorreo correo { get; set; }
        public string IdEmpresa { get; set; }
        public string Llave { get; set; }
        public string NombreDestinatario { get; set; }
        public string Empresa { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public string CodigoOTP { get; set; }

        public string nombrePlantilla { get; set; }
        public string rutaPlantillas { get; set; }

        public List<byte[]> Archivos_Adjuntos { get; set; }
        public List<string> Nombre_Archivos { get; set; }

        public FntEnvioCorreo()
        {
            correo = new ClsCorreo();
            IdEmpresa = string.Empty;
            Llave = "";
            NombreDestinatario = "";
            Empresa = "";
            Usuario = "";
            Password= "";
            nombrePlantilla = "";
            rutaPlantillas = "";
            Archivos_Adjuntos = new List<byte[]>();
            Nombre_Archivos = new List<string>();
        }

        public ClsResultadoAccion Enviacorreo()
        {
            MailMessage mailMessage = new MailMessage();
            SmtpClient smtpClient = new SmtpClient(correo.Host, correo.Puerto);
            mailMessage.From = new MailAddress(correo.correo, correo.nombreMostrar);
            foreach (string destino in correo.CorreoDestinos)
                mailMessage.To.Add(destino);
            foreach (string copia in correo.conCopia)
                mailMessage.CC.Add(copia);
            foreach (string oculta in correo.copiaOculta)
                mailMessage.Bcc.Add(oculta);
            mailMessage.Subject = correo.Asunto;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = correo.Mensaje;
            smtpClient.Port = correo.Puerto;
            smtpClient.Host = correo.Host;
            ClsResultadoAccion result = new ClsResultadoAccion();
            try
            {
                if (!(correo.Authentificacion == "Normal"))
                {
                    if (!(correo.Authentificacion == "SSL"))
                        throw new Exception("Error en el tipo de autenticacion");
                    smtpClient.EnableSsl = true;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(correo.usuario, correo.Pass);
                    smtpClient.Timeout = 20000;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                }
                else
                {
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.EnableSsl = false;
                    smtpClient.Credentials = new NetworkCredential(correo.usuario, correo.Pass);
                }
                Stream pdf = null;
                Stream xml = null;
                using (xml = new MemoryStream())
                {
                    using (pdf = new MemoryStream())
                    {
                        int contador = 0;
                        foreach (byte[] un_archivo in Archivos_Adjuntos)
                        {
                            if (contador <= 0)
                            {
                                xml.Write(un_archivo, 0, un_archivo.Length);
                                xml.Seek(0, SeekOrigin.Begin);
                            }
                            else
                            {
                                pdf.Write(un_archivo, 0, un_archivo.Length);
                                pdf.Seek(0, SeekOrigin.Begin);
                            }
                            Attachment item = new Attachment(contador <= 0 ? xml
                                : pdf, Nombre_Archivos[contador], contador <= 0
                                ? "application/xml" : "application/pdf");
                            mailMessage.Attachments.Add(item);
                            contador++;
                        }
                        smtpClient.Send(mailMessage);
                    }
                }
                result.Detalle = "";
                result.Estatus = true;
            }
            catch (Exception ex)
            {
                result.Estatus = false;
                result.Detalle = ex.Message;
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Generales.FntEnvioCorreo";
                log.Log.metodo = "Enviacorreo";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return result;
        }

        public ClsResultadoAccion preparaEnvio(string Proceso, string destinatario = "")
        {
            ClsResultadoAccion accion = new ClsResultadoAccion();
            try
            {
                accion = consultaCorreo();
                if (accion.Estatus)
                {
                    if (destinatario == "") accion = consultaDestinatarios();
                    else
                    {
                        accion.Estatus = true;
                        correo.CorreoDestinos.Add(destinatario);
                    }
                }
                if (accion.Estatus)
                    accion = ConsultaPlantillaProceso(Proceso);
                if (accion.Estatus)
                    accion = ConsumePlantilla();
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Generales.FntEnvioCorreo";
                log.Log.metodo = "preparaEnvio";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return accion;
        }

        public ClsResultadoAccion preparaEnvioPrueba(string Proceso, string destinatario = "")
        {
            ClsResultadoAccion accion = new ClsResultadoAccion();
            try
            {
                accion = consultaDestinatarios();               
                if (accion.Estatus)
                    accion = ConsultaPlantillaProceso(Proceso);
                if (accion.Estatus)
                    accion = ConsumePlantilla();
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Generales.FntEnvioCorreo";
                log.Log.metodo = "preparaEnvioPrueba";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return accion;
        }

        public ClsResultadoAccion consultaCorreo()
        {
            ClsResultadoAccion accion = new ClsResultadoAccion();
            try
            {
                DataSet ds = BD_DLL.ClsBD.Consulta_sin_parametros("web.consulta_datos_correo", Conexion.CadenaConexionSeguridad, Conexion.isolationLevelSnap());
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        correo.correo = dr["correo"] == DBNull.Value ? string.Empty
                            : Hash.Desencripta(dr["correo"].ToString(), VariablesGlobales.Llave);
                        correo.usuario = dr["correo"] == DBNull.Value ? string.Empty
                            : Hash.Desencripta(dr["correo"].ToString(), VariablesGlobales.Llave);
                        correo.Pass = dr["contrasena"] == DBNull.Value ? string.Empty
                            : Hash.Desencripta(dr["contrasena"].ToString(), VariablesGlobales.Llave);
                        correo.Host = dr["servidor_salida"] == DBNull.Value ? string.Empty
                            : Hash.Desencripta(dr["servidor_salida"].ToString(), VariablesGlobales.Llave);
                        correo.Puerto = FntGenericas.ValidaNullint(dr["puerto_salida"].ToString(), 0);
                        correo.Authentificacion = FntGenericas.IsDbNullOrDefault(dr["requiereSSL"], false) ? "SSL" : "Normal";
                        correo.nombreMostrar = FntGenericas.IsDbNullOrDefault(dr["nombre_mostrar"], "");
                        correo.correoMostrar = FntGenericas.IsDbNullOrDefault(dr["correo_mostrar"], "");
                        break;
                    }
                }
                accion.Estatus = true;
            }
            catch (Exception ex)
            {
                accion.Estatus = false;
                accion.Detalle = ex.Message;
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Generales.FntEnvioCorreo";
                log.Log.metodo = "consultaCorreo";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return accion;
        }

        public ClsResultadoAccion consultaDestinatarios()
        {
            ClsResultadoAccion accion = new ClsResultadoAccion();
            try
            {
                string[] parametros = { "idEmpresa" };
                string[] values = { IdEmpresa };
                DataSet ds = BD_DLL.ClsBD.Consulta_con_parametros("empresa.consulta_destinatario", Conexion.CadenaConexion, parametros, values, Conexion.isolationLevelSnap());
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        NombreDestinatario = FntGenericas.ValidaNullString(dr["destinatario"].ToString(), "");
                        correo.CorreoDestinos.Add(FntGenericas.ValidaNullString(dr["correo"].ToString(), ""));
                    }
                    accion.Estatus = true;
                }
            }
            catch (Exception ex)
            {
                accion.Estatus = false;
                accion.Detalle = ex.Message;
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Generales.FntEnvioCorreo";
                log.Log.metodo = "consultaDestinatarios";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return accion;
        }

        public ClsResultadoAccion ConsumePlantilla()
        {
            ClsResultadoAccion accion = new ClsResultadoAccion();
            string cuerpoHtml = string.Empty;
            try
            {
                StringBuilder sb = new StringBuilder();
                using (StreamReader sr = new StreamReader(Path.Combine(rutaPlantillas,nombrePlantilla)))
                {
                    sb.Append(sr.ReadToEnd());
                    sb.Replace("$NombreDestinatario$", NombreDestinatario);
                    sb.Replace("$Empresa$", Empresa);
                    sb.Replace("$Usuario$", Usuario);
                    sb.Replace("$Password$", Password);
                    sb.Replace("$CodigoOTP$", CodigoOTP);
                    cuerpoHtml = sb.ToString();
                }
                accion.Estatus = true;
            }
            catch (Exception ex)
            {
                accion.Estatus = false;
                accion.Detalle = ex.Message;
                cuerpoHtml = nombrePlantilla;
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Generales.FntEnvioCorreo";
                log.Log.metodo = "ConsumePlantillaPruebaEnvio";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            correo.Mensaje = cuerpoHtml;
            return accion;
        }

        public ClsResultadoAccion ConsultaPlantillaProceso(string Proceso)
        {
            ClsResultadoAccion accion = new ClsResultadoAccion();
            try
            {
                string[] parametros;
                string[] ValorParametros;

                parametros = new string[] { "plantilla" };
                ValorParametros = new string[] { Proceso };
                DataSet ds = BD_DLL.ClsBD.Consulta_con_parametros("catalogo.consulta_plantilla_proceso", Conexion.CadenaConexionSeguridad, parametros, ValorParametros, Conexion.isolationLevelSnap());
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        nombrePlantilla = FntGenericas.ValidaNullString(dr["nombre_plantilla"].ToString(), "");
                        correo.Asunto = FntGenericas.ValidaNullString(dr["asunto"].ToString(), "");
                    }
                    accion.Estatus = true;
                }
            }
            catch (Exception ex)
            {
                accion.Estatus = false;
                accion.Detalle = ex.Message;
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Generales.FntEnvioCorreo";
                log.Log.metodo = "ConsultaPlantillaProceso";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return accion;
        }
        
    }
}
