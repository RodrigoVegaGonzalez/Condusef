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
using System.Linq;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Condusef_DLL.Funciones.Condusef
{
    public class Empresa
    {
        public ClsDatosEmpresa DatosGeneralesPorEmpresa { get; set; }
        public List<ClsCatalogo> localidades { get; set; }
        public List<ClsCatalogo> estados { get; set; }
        public List<ClsCatalogo> perfiles { get; set; }
        public List<ClsCatalogo> Sectores { get; set; }
        public List<ClsUsuario> usuarios { get; set; }
        public ClsUsuario usuario { get; set; }
        public List<ClsCatalogo> ListadoUsuarios { get; set; }

        public RespuestaProductos ListadoProductos { get; set; }
        public RespuestaCausas ListadoCausas { get; set; }

        public Empresa() {
            DatosGeneralesPorEmpresa = new ClsDatosEmpresa();
            localidades = new List<ClsCatalogo>();
            estados = new List<ClsCatalogo>();
            usuarios = new List<ClsUsuario>();
            usuario = new ClsUsuario();
            perfiles = new List<ClsCatalogo>();
            Sectores = new List<ClsCatalogo>();
            ListadoProductos = new RespuestaProductos();
            ListadoCausas = new RespuestaCausas();
            ListadoUsuarios = new List<ClsCatalogo>();
        }
        public void CargaCatalogos()
        {
            localidades = Catalogos.consultaLocalidadesCNBV();
            estados = Catalogos.consultaEstadosCNBV();
            perfiles = Catalogos.consultaRoles();
            Sectores = Catalogos.ConsultaSectores();
            ListadoUsuarios = Catalogos.CargarListadoUsuarios();
        }

        public void ConsultaDatosGeneralesPorEmpresa(string idEmpresa)
        {
            string[] parametro = new string[] { "idEmpresa" };
            string[] valorParametro = new string[] { idEmpresa };
            try
            {
                DataSet resultadoConsulta = BD_DLL.ClsBD.Consulta_con_parametros("empresa.consultar_infor_empresa", Conexion.CadenaConexion, parametro, valorParametro, Conexion.isolationLevelSnap());
                if (resultadoConsulta.Tables.Count > 0)
                {
                    foreach (DataRow fila in resultadoConsulta.Tables[0].Rows)
                    {
                        DatosGeneralesPorEmpresa.RFC = FntGenericas.ValidaNullString(fila["rfc"].ToString(), "");
                        DatosGeneralesPorEmpresa.Nombre = FntGenericas.ValidaNullString(fila["nombre"].ToString(), "");
                        DatosGeneralesPorEmpresa.NomCorto = FntGenericas.ValidaNullString(fila["nombre_corto"].ToString(), "");
                        DatosGeneralesPorEmpresa.Sector = FntGenericas.ValidaNullString(fila["sector"].ToString(), "");

                        DatosGeneralesPorEmpresa.DirCalle = FntGenericas.ValidaNullString(fila["calle"].ToString(), "");
                        DatosGeneralesPorEmpresa.DirColonia = FntGenericas.ValidaNullString(fila["colonia"].ToString(), "");
                        DatosGeneralesPorEmpresa.DirNumInt = FntGenericas.ValidaNullString(fila["num_int"].ToString(), "");
                        DatosGeneralesPorEmpresa.DirNumExt = FntGenericas.ValidaNullString(fila["num_ext"].ToString(), "");
                        DatosGeneralesPorEmpresa.DirCP = FntGenericas.ValidaNullString(fila["cp"].ToString(), "");
                        DatosGeneralesPorEmpresa.DirMunicipio = FntGenericas.ValidaNullString(fila["municipio"].ToString(), "");
                        DatosGeneralesPorEmpresa.DirCiudad = FntGenericas.ValidaNullString(fila["ciudad"].ToString(), "");
                        DatosGeneralesPorEmpresa.DirEstado = FntGenericas.ValidaNullString(fila["id_estado"].ToString(), "");

                        DatosGeneralesPorEmpresa.CorreoContacto = FntGenericas.ValidaNullString(fila["email"].ToString(), "");
                        DatosGeneralesPorEmpresa.TelefonoContacto = FntGenericas.ValidaNullString(fila["telefono"].ToString(), "");
                        DatosGeneralesPorEmpresa.PersonaContacto = FntGenericas.ValidaNullString(fila["nombre_contacto"].ToString(), "");
                        DatosGeneralesPorEmpresa.Estatus = FntGenericas.ValidaNullBool(fila["estatus"].ToString(), false);
                        DatosGeneralesPorEmpresa.Registrado = FntGenericas.ValidaNullBool(fila["registrado"].ToString(), false);
                        DatosGeneralesPorEmpresa.Plan = FntGenericas.ValidaNullString(fila["plan_desc"].ToString(), "");
                        DatosGeneralesPorEmpresa.TokenRedeco = FntGenericas.ValidaNullString(fila["token_redeco"].ToString(), "");
                        DatosGeneralesPorEmpresa.TokenReune = FntGenericas.ValidaNullString(fila["token_reune"].ToString(), "");

                        DatosGeneralesPorEmpresa.TokenRedeco = FntEncriptar.Desencriptar(DatosGeneralesPorEmpresa.TokenRedeco);
                        DatosGeneralesPorEmpresa.TokenReune = FntEncriptar.Desencriptar(DatosGeneralesPorEmpresa.TokenReune);
                        DatosGeneralesPorEmpresa.Servicios = FntGenericas.ValidaNullint(fila["servicios"].ToString(), 0);
                    }
                }
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Empresa.DatosEmpresa";
                log.Log.metodo = "consultaDatosGeneralesPorEmpresa";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
        }

        public List<ClsUsuario> ConsultaUsuariosEmpresa(string idEmpresa)
        {
            List<ClsUsuario> listado = new List<ClsUsuario>();
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            string[] parametro = new string[] { "idEmpresa" };
            string[] valorParametro = new string[] { idEmpresa };
            try
            {
                DataSet resultadoConsulta = BD_DLL.ClsBD.Consulta_con_parametros("usuario.consulta_usuarios_empresa", Conexion.CadenaConexion, parametro, valorParametro, Conexion.isolationLevelSnap());
                if (resultadoConsulta.Tables.Count > 0)
                {
                    foreach (DataRow fila in resultadoConsulta.Tables[0].Rows)
                    {
                        ClsUsuario usuario = new ClsUsuario();
                        usuario.IdUsuario = FntGenericas.ValidaNullString(fila["id_usuario"].ToString(), "");
                        usuario.Usuario = FntGenericas.ValidaNullString(fila["usuario"].ToString(), "");
                        usuario.CorreoResponsable = FntGenericas.ValidaNullString(fila["email_responsable"].ToString(), "");
                        usuario.Activo = FntGenericas.ValidaNullBool(fila["estatus"].ToString(), false);
                        //usuario.idRol = FntGenericas.ValidaNullint(fila["rol"].ToString(), 0);
                        usuario.Rol = FntGenericas.ValidaNullString(fila["rol"].ToString(), "");
                        listado.Add(usuario);
                    }
                }
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Empresa.DatosEmpresa";
                log.Log.metodo = "consultaUsuariosEmpresa";
                log.Log.error = ex.Message;
                log.insertaLog();
                listado = new List<ClsUsuario>();
            }
            return listado;
        }

        public ClsResultadoAccion HabilitarEmpresa(string idEmpresa)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            Condusef condusef = new Condusef();
            FntEnvioCorreo correo = new FntEnvioCorreo();

            try
            {
                ConsultaDatosGeneralesPorEmpresa(idEmpresa);
                if (DatosGeneralesPorEmpresa.Nombre != "")
                {
                    resultado = Db_ActivarEmpresa(idEmpresa);
                    if (resultado.Estatus)
                    {
                        string idUsuario = GenerarUid();
                        string username = "administrador@" + DatosGeneralesPorEmpresa.NomCorto;
                        string password = FntGenericas.GenerarContraseña(16);
                        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
                        string apiPassword = FntEncriptar.Encriptar(password);
                        resultado = Db_GuardarNuevoUsuario(idUsuario, idEmpresa, username, hashedPassword, "1", "", "", "1", apiPassword, DateTime.Now, DatosGeneralesPorEmpresa.CorreoContacto);
                        if (resultado.Estatus)
                        {
                            correo.IdEmpresa = idEmpresa;
                            correo.Empresa = DatosGeneralesPorEmpresa.Nombre;
                            correo.Llave = VariablesGlobales.Llave;
                            correo.Usuario = username;
                            correo.Password = password;
                            correo.rutaPlantillas = VariablesGlobales.RutaPlantillasCorreo;
                            resultado = correo.preparaEnvio("CredencialesAdmin");
                            if (resultado.Estatus)
                            {
                                correo.Enviacorreo();
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
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef.Empresa";
                log.Log.metodo = "HabilitarEmpresa";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return resultado;
        }

        public ClsResultadoAccion RegistrarNuevaEmpresa(ClsDatosEmpresa datosEmpresa)
        {
            FntEnvioCorreo correo = new FntEnvioCorreo();

            ClsResultadoAccion resultado = new ClsResultadoAccion();
            try
            {
                string idEmpresa = GenerarUid();
                resultado = Db_RegistrarNuevaEmpresa(idEmpresa, datosEmpresa);
                if (resultado.Estatus)
                {
                    correo.Empresa = datosEmpresa.Nombre;
                    correo.IdEmpresa = idEmpresa;
                    correo.Llave = VariablesGlobales.Llave;
                    correo.rutaPlantillas = VariablesGlobales.RutaPlantillasCorreo;
                    correo.nombrePlantilla = "AvisoRegistro.html";
                    resultado = correo.preparaEnvio("AvisoRegistro");
                    if (resultado.Estatus)
                    {
                        correo.Enviacorreo();
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef.Empresa";
                log.Log.metodo = "RegistroEmpresa";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return resultado;
        }

        public async Task<ClsResultadoAccion> DarAltaSuperUsuarioCondusef(string idEmpresa, string idUsuario, string username)
        {
            ClsResultadoAccion resultado = new();
            Condusef condusef = new();
            try
            {
                resultado = Db_ObtenerTokensEmpresa(idEmpresa);
                if (resultado.Estatus)
                {
                    //[0] es REDECO y [1] es REUNE
                    string[] tokens = resultado.Detalle.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                    resultado = Db_ValidarExistenciaTokenAdmin(idUsuario);
                    if (resultado.Estatus)
                    {
                        //Validamos cuales servicios ya tienen registrado al super usuario
                        string[] servicios = resultado.Detalle.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                        //Obtenemos la contraseña de API del usuario adminsitrador
                        string apiPassword = Db_ConsultaPasswordUsuario(idUsuario);

                        if (servicios.Length == 0) //No hay token de REDECO ni de REUNE
                        {
                            //Dar de alta en REDECO
                            resultado = await condusef.Api_CrearSuperUsuario(tokens[0], username, apiPassword);
                            if (resultado.Estatus)
                            {
                                string userToken = condusef._RespuestaSuperUser.data.token_access;
                                resultado = Db_ActualizarTokenUsuario(idUsuario, "1", userToken, DateTime.Now);
                                //do
                                //{
                                //    resultado = Db_ActualizarTokenUsuario(idUsuario, "1", userToken, DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fff"));
                                //} while (resultado.Estatus);

                                ////Dar de alta en REUNE
                                //resultado = await condusef.Api_ReuneCrearSuperUsuario(tokens[1], username, apiPassword);
                                //if (resultado.Estatus)
                                //{
                                //    userToken = condusef._RespuestaSuperUser.data.token_access;
                                //    resultado = Db_ActualizarTokenUsuario(idUsuario, "2", userToken, DateTime.Now);
                                //    resultado.Estatus = true;
                                //    //do
                                //    //{
                                //    //    resultado = Db_ActualizarTokenUsuario(idUsuario, "1", userToken, DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fff"));
                                //    //} while (resultado.Estatus);
                                //}

                            }

                            
                        }
                        else if (servicios.Length == 1) //Hay token de un servicio (se debe dar de alta en el servicio faltante)
                        {
                            if (servicios[0] == "1") //Ya se dió de alta en REDECO, por lo que falta REUNE
                            {
                                //Dar de alta en REUNE
                                resultado = await condusef.Api_ReuneCrearSuperUsuario(tokens[1], username, apiPassword);
                                if (resultado.Estatus)
                                {
                                    string userToken = condusef._RespuestaSuperUser.data.token_access;
                                    resultado = Db_ActualizarTokenUsuario(idUsuario, "2", userToken, DateTime.Now);
                                    resultado.Estatus = true;
                                    //do
                                    //{
                                    //    resultado = Db_ActualizarTokenUsuario(idUsuario, "1", userToken, DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fff"));
                                    //} while (resultado.Estatus);
                                }
                            }
                            else //Ya se dió de alta en REUNE, por lo que falta REDECO
                            {
                                //Dar de alta en REDECO
                                resultado = await condusef.Api_CrearSuperUsuario(tokens[0], username, apiPassword);
                                if (resultado.Estatus)
                                {
                                    string userToken = condusef._RespuestaSuperUser.data.token_access;
                                    resultado = Db_ActualizarTokenUsuario(idUsuario, "1", userToken, DateTime.Now);
                                    resultado.Estatus = true;
                                    //do
                                    //{
                                    //    resultado = Db_ActualizarTokenUsuario(idUsuario, "1", userToken, DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fff"));
                                    //} while (resultado.Estatus);
                                }
                            }
                        }
                        
                    }
                }

            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Empresa";
                log.Log.metodo = "DarAltaSuperUsuarioCondusef";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
                resultado.Code = 500;
            }
            return resultado;
        }

        public async Task<ClsResultadoAccion> CrearNuevoUsuario(string idEmpresa, string idSuperUsuario, string username, string password, int idPerfil, bool activo, string correoResponsable)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            Condusef condusef = new Condusef();
            string tokenRedeco = "";
            string tokenReune = "";
            string tokenRedecoUsuarioNuevo = "";
            string tokenReuneUsuarioNuevo = "";
            try
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
                string idUsuario = GenerarUid();
                if (idPerfil == 3) //auditor
                {
                    resultado = Db_GuardarNuevoUsuario(idUsuario, idEmpresa, username, hashedPassword, idPerfil.ToString(), "", "", activo.ToString(), "", DateTime.Now, correoResponsable);
                }
                else if (idPerfil == 2) //capturista
                {
                    //Obtener tokens de CONDUSEF del usuario administrador

                    //REDECO
                    resultado = await condusef.ConsultaTokenUsuario(idSuperUsuario, "1");
                    if (resultado.Estatus)
                    {
                        tokenRedeco = resultado.Detalle;
                    }

                    //REUNE
                    resultado = await condusef.ConsultaTokenUsuario(idSuperUsuario, "2");
                    if (resultado.Estatus)
                    {
                        tokenReune = resultado.Detalle;
                    }

                    if (tokenRedeco == "" && tokenReune == "")
                    {
                        resultado.Estatus = false;
                        resultado.Code = 403;
                    }
                    else
                    {
                        bool creadoEnRedeco = false, creadoEnReune = false;
                        if (tokenRedeco != string.Empty)
                        {
                            resultado = await condusef.Api_CrearUsuario(tokenRedeco, username, password);
                            if (resultado.Estatus) tokenRedecoUsuarioNuevo = resultado.Detalle; 
                            creadoEnRedeco = resultado.Estatus;
                        }

                        //if (tokenReune != string.Empty)
                        //{
                        //    resultado = await condusef.Api_ReuneCrearUsuario(tokenReune, username, password);
                        //    if (resultado.Estatus) tokenReuneUsuarioNuevo = resultado.Detalle;
                        //    creadoEnReune = resultado.Estatus;
                        //}
                        ////resultado.Estatus = true;
                        ////resultado.Detalle = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1aWQiOiIxNzgwMjE4Ni0xMTQyLTQyNmQtYmQ0MC03NThmNTFkYzI5MGYiLCJ1c2VybmFtZSI6InVzZXJwcnVlYmFAbWV4ZmFjdG9yIiwiaW5zdGl0dWNpb25pZCI6Ijc5NDdDQjVGLTQ3MzAtNDk2MS05MzQ3LUVGRjA3MDM5IiwiaW5zdGl0dWNpb25DbGF2ZSI6MTgyNSwiZGVub21pbmFjaW9uX3NvY2lhbCI6Ik1leC1GYWN0b3IsIFMuQS4gZGUgQy5WLiBTT0ZPTSBFLk4uUi4iLCJzZWN0b3JpZCI6MjQsInNlY3RvciI6IlNPQ0lFREFERVMgRklOQU5DSUVSQVMgREUgT0JKRVRPIE1VTFRJUExFIEVOUiIsImlhdCI6MTcxMDk3NDQzNCwiZXhwIjoxNzExODM4NDM0fQ.Vo3veZbJoSm2fRX-ryKcRZX_KRt-A1B32IxWj6RC_N4";

                        if(creadoEnRedeco /*&& creadoEnReune*/)
                        {
                            string tokenRedecoEncriptado = FntEncriptar.Encriptar(tokenRedecoUsuarioNuevo);
                            //string tokenReuneEncriptado = FntEncriptar.Encriptar(tokenReuneUsuarioNuevo);
                            string tokenReuneEncriptado = "";

                            string apiPassword = FntEncriptar.Encriptar(password);

                            DateTime fechaActual = DateTime.Now;

                            // Aumentar la fecha en 30 días
                            DateTime fechaExpiracion = fechaActual.AddDays(30);

                            resultado = Db_GuardarNuevoUsuario(idUsuario, idEmpresa, username, hashedPassword, idPerfil.ToString(), tokenRedecoEncriptado, tokenReuneEncriptado, activo.ToString(), apiPassword, fechaExpiracion, correoResponsable);
                        }
                        else
                        {
                            resultado.Estatus = false;
                            resultado.Code = 500;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef.Empresa";
                log.Log.metodo = "CrearNuevoUsuario";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return resultado;
        }

        public ClsResultadoAccion EliminarUsuario(string idEmpresa, string idUsuario)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            try
            {
                string[] parametros = { "idUsuario", "idEmpresa" };
                string[] values = { idUsuario, idEmpresa };
                BD_DLL.ClsBD.Consulta_con_parametros("usuario.eliminar_usuario", Conexion.CadenaConexion, parametros, values, Conexion.isolationLevelSnap());
                resultado.Estatus = true;
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Empresa";
                log.Log.metodo = "EliminarUsuario";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
            }
            return resultado;
        }

        public ClsResultadoAccion DesactivarUsuario(string idEmpresa, string idUsuario, bool estaActivado)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            try
            {
                string[] parametros = { "idUsuario", "idEmpresa", "estaActivado" };
                string[] values = { idUsuario, idEmpresa, estaActivado.ToString() };
                BD_DLL.ClsBD.Consulta_con_parametros("usuario.desactivar_usuario", Conexion.CadenaConexion, parametros, values, Conexion.isolationLevelSnap());
                resultado.Estatus = true;
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Empresa";
                log.Log.metodo = "DesactivarUsuario";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
            }
            return resultado;
        }

        public ClsResultadoAccion CambiarCorreoResponsable(string idEmpresa, string idUsuario, string correo)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            try
            {
                string[] parametros = { "idUsuario", "idEmpresa", "correo" };
                string[] values = { idUsuario, idEmpresa, correo };
                BD_DLL.ClsBD.Consulta_con_parametros("usuario.cambiar_correo_responsable", Conexion.CadenaConexion, parametros, values, Conexion.isolationLevelSnap());
                resultado.Estatus = true;
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Empresa";
                log.Log.metodo = "CambiarCorreoResponsable";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
            }
            return resultado;
        }

        public ClsResultadoAccion CambiarPasswordUsuarioConfiguracion(string idUsuario, string newPassword)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();

            try
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);
                resultado = Db_CambiarPasswordUsuario(idUsuario, hashedPassword);
            }
            catch (Exception ex)
            {
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef.Empresa";
                log.Log.metodo = "CambiarPasswordUsuarioConfiguracion";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return resultado;
        }

        public ClsResultadoAccion SolicitarCambioPassword(string username)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            Condusef condusef = new Condusef();
            FntEnvioCorreo correo = new FntEnvioCorreo();
            
            try
            {
                ClsUsuario usuario = new();
                usuario = Db_ConsultaCorreoUsuario(username);
                if (usuario.CorreoResponsable != "")
                {
                    string otp = FntGenericas.GenerarContraseña(16);
                    correo.CodigoOTP = otp;
                    correo.Empresa = usuario.Empresa;
                    correo.Llave = VariablesGlobales.Llave;
                    correo.Usuario = username;
                    correo.rutaPlantillas = VariablesGlobales.RutaPlantillasCorreo;
                    resultado = correo.preparaEnvio("CambioPassword", usuario.CorreoResponsable);
                    if (resultado.Estatus)
                    {
                        DateTime fechaExpiracion = DateTime.Now.AddMinutes(20);
                        resultado = Db_InsertarOTP(otp, usuario.IdUsuario, fechaExpiracion);
                        
                        if (resultado.Estatus) {
                            resultado = correo.Enviacorreo();
                        }
                    }
                }
                else
                {
                    resultado.Estatus = false;
                    resultado.Code = 400;
                }
            }
            catch (Exception ex)
            {
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef.Empresa";
                log.Log.metodo = "SolicitarCambioPassword";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return resultado;
        }

        public ClsResultadoAccion CambiarPasswordUsuario(string otp, string newPassword)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            Condusef condusef = new Condusef();
            FntEnvioCorreo correo = new FntEnvioCorreo();

            try
            {
                ClsUsuario usuario = new();
                resultado = Db_ConsultaOTP(otp);
                if (resultado.Estatus)
                {
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);
                    resultado = Db_CambiarPasswordUsuario(resultado.Detalle, hashedPassword);
                }
            }
            catch (Exception ex)
            {
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef.Empresa";
                log.Log.metodo = "CambiarPasswordUsuario";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return resultado;
        }



        //public async Task<ClsResultadoAccion> AgregarEmpresaManual(string nombre, string nomCorto, string rfc, string casfim, string tokenRedeco)
        //{
        //    ClsResultadoAccion resultado = new ClsResultadoAccion();
        //    Condusef condusef = new Condusef();
        //    FntEnvioCorreo correo = new FntEnvioCorreo();

        //    try
        //    {
        //        string username = "administrador@" + nomCorto;
        //        string password = FntGenericas.GenerarContraseña();
        //        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
        //        string apiPassword = FntEncriptar.Encriptar(password);
        //        resultado = await condusef.Api_CrearSuperUsuario(tokenRedeco, username, password);
        //        if (resultado.Estatus)
        //        {
        //            string userToken = condusef._RespuestaSuperUser.data.token_access;
        //            resultado = Db_RegistrarEmpresa(nombre, nomCorto, rfc, casfim, tokenRedeco, username, hashedPassword, userToken, apiPassword);
        //            usuario.usuario = username;
        //            usuario.password = password;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        resultado.Estatus = false;
        //        resultado.Detalle = ex.Message;
        //        FntLog log = new FntLog();
        //        log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef.Empresa";
        //        log.Log.metodo = "AgregarEmpresa";
        //        log.Log.error = ex.Message;
        //        log.insertaLog();
        //    }
        //    return resultado;
        //}

        #region Base de datos
        private ClsResultadoAccion Db_GuardarNuevoUsuario(string idUsuario, string idEmpresa, string usuario, string password, string rol, string tokenRedeco, string tokenReune, string activo, string apiPassword, DateTime expiracion, string correoResponsable)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            try
            {
                DateTime fechaCreacion = DateTime.Now;
                string[] parametros = { "idUsuario", "usuario", "password", "idEmpresa", "idRol", "tokenRedeco", "tokenReune", "activo", "apiPassword", "expiracion", "correoResponsable", "fechaCreacion" };
                string[] values = { idUsuario, usuario, password, idEmpresa, rol, tokenRedeco, tokenReune, activo, apiPassword, expiracion.ToString("yyyyMMdd HH:mm:ss.fff"), correoResponsable, fechaCreacion.ToString("yyyyMMdd HH:mm:ss.fff") };
                BD_DLL.ClsBD.Consulta_con_parametros("usuario.insertar_nuevo_usuario", Conexion.CadenaConexion, parametros, values, Conexion.isolationLevelSnap());
                resultado.Estatus = true;
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Empresa";
                log.Log.metodo = "Db_GuardarNuevoUsuario";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
            }
            return resultado;
        }

        public ClsResultadoAccion ActualizarDatosEmpresa(string idEmpresa, ClsDatosEmpresa datos)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            try
            {
                string[] parametros = { "idEmpresa", "nombre", "email", "telefono", "nombreContacto", "calle", "colonia", 
                    "numExt", "numInt", "municipio", "ciudad", "cp", "idEstado" };
                string[] values = { idEmpresa, datos.Nombre, datos.CorreoContacto, datos.TelefonoContacto, datos.PersonaContacto, 
                    datos.DirCalle, datos.DirColonia, datos.DirNumExt, datos.DirNumInt, datos.DirMunicipio, datos.DirCiudad, datos.DirCP, datos.DirEstado };
                BD_DLL.ClsBD.Consulta_con_parametros("empresa.actualizar_datos", Conexion.CadenaConexion, parametros, values, Conexion.isolationLevelSnap());
                resultado.Estatus = true;
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Empresa";
                log.Log.metodo = "ActualizarDatosEmpresa";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
            }
            return resultado;
        }

        private ClsResultadoAccion Db_RegistrarNuevaEmpresa(string idEmpresa, ClsDatosEmpresa datosEmpresa)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            try
            {
                string[] parametros = { "idEmpresa", "nombre", "nombre_corto", "rfc", "email", "telefono", "nombre_contacto",
                        "id_plan", "calle", "colonia", "num_ext", "num_int", "municipio", "ciudad", "cp", "id_estado", "id_sector" };
                string[] values = { idEmpresa, datosEmpresa.Nombre, datosEmpresa.NomCorto, datosEmpresa.RFC, datosEmpresa.CorreoContacto,
                    datosEmpresa.TelefonoContacto, datosEmpresa.PersonaContacto, "3", datosEmpresa.DirCalle, datosEmpresa.DirColonia, datosEmpresa.DirNumExt,
                    datosEmpresa.DirNumInt, datosEmpresa.DirMunicipio, datosEmpresa.DirCiudad, datosEmpresa.DirCP, datosEmpresa.DirEstado, datosEmpresa.Sector };
                BD_DLL.ClsBD.Consulta_con_parametros("empresa.registrar_empresa", Conexion.CadenaConexion, parametros, values, Conexion.isolationLevelSnap());
                resultado.Estatus = true;
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Empresa";
                log.Log.metodo = "Db_RegistrarEmpresa";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
            }
            return resultado;
        }

        private ClsResultadoAccion Db_ActivarEmpresa(string idEmpresa)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            try
            {
                string[] parametros = { "idEmpresa" };
                string[] values = { idEmpresa };
                BD_DLL.ClsBD.Consulta_con_parametros("empresa.activar_empresa", Conexion.CadenaConexion, parametros, values, Conexion.isolationLevelSnap());
                resultado.Estatus = true;
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Empresa";
                log.Log.metodo = "Db_ActivarEmpresa";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
            }
            return resultado;
        }

        private ClsResultadoAccion Db_ObtenerTokensEmpresa(string idEmpresa)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            try
            {
                string[] parametros = { "idEmpresa" };
                string[] values = { idEmpresa };
                DataSet resultadoConsulta = BD_DLL.ClsBD.Consulta_con_parametros("empresa.obtener_tokens_condusef", Conexion.CadenaConexion, parametros, values, Conexion.isolationLevelSnap());
                if (resultadoConsulta.Tables.Count > 0)
                {
                    foreach (DataRow fila in resultadoConsulta.Tables[0].Rows)
                    {
                        string redeco = string.Empty;
                        string reune = string.Empty;
                        redeco = FntEncriptar.Desencriptar(FntGenericas.ValidaNullString(fila["token_redeco"].ToString(), ""));
                        reune = FntEncriptar.Desencriptar(FntGenericas.ValidaNullString(fila["token_reune"].ToString(), ""));

                        if (redeco != "" && reune != "")
                        {
                            resultado.Estatus = true;
                            resultado.Detalle = redeco + ";" + reune;
                        }
                        else
                        {
                            resultado.Estatus = false;
                            resultado.Code = 501;
                        }
                    }
                }
                else
                {
                    resultado.Estatus = false;
                    resultado.Code = 502;
                }
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Empresa";
                log.Log.metodo = "Db_RegistrarEmpresa";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
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
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef.Empresa";
                log.Log.metodo = "Db_ConsultaPasswordUsuario";
                log.Log.error = ex.Message;
                log.insertaLog();
                apiPassword = string.Empty;
            }
            return apiPassword;
        }

        private ClsResultadoAccion Db_ActualizarTokenUsuario(string idUsuario, string idServicio, string token, DateTime _fechaExpiracion)
        {
            ClsResultadoAccion resultado = new();
            string apiPassword = string.Empty;

            try
            {
                string tokenEcripted = FntEncriptar.Encriptar(token);
                DateTime fechaExpiracion = _fechaExpiracion.AddDays(30);
                string[] parametros = { "idUsuario", "idServicio", "token", "fechaExpiracion" };
                string[] values = { idUsuario, idServicio, tokenEcripted, fechaExpiracion.ToString("yyyyMMdd HH:mm:ss.fff") };
                BD_DLL.ClsBD.Consulta_con_parametros("usuario.actualizar_token_usuario", Conexion.CadenaConexion,
                    parametros, values, Conexion.isolationLevelSnap());
                resultado.Estatus = true;
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef.Empresa";
                log.Log.metodo = "Db_ConsultaPasswordUsuario";
                log.Log.error = ex.Message;
                log.insertaLog();
                apiPassword = string.Empty;
            }
            return resultado;
        }

        private ClsResultadoAccion Db_ValidarExistenciaTokenAdmin(string idUsuario)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            try
            {
                string[] parametros = { "idUsuario" };
                string[] values = { idUsuario };
                DataSet resultadoConsulta = BD_DLL.ClsBD.Consulta_con_parametros("usuario.validar_existencia_token_administrador", Conexion.CadenaConexion, parametros, values, Conexion.isolationLevelSnap());
                if (resultadoConsulta.Tables.Count > 0)
                {
                    foreach (DataRow fila in resultadoConsulta.Tables[0].Rows)
                    {
                        string servicio = string.Empty;
                        servicio = FntGenericas.ValidaNullString(fila["servicio"].ToString(), "");
                        resultado.Detalle += servicio + ";";
                    }
                    resultado.Estatus = true;
                }
                else
                {
                    resultado.Estatus = false;
                    resultado.Code = 502;
                }
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Empresa";
                log.Log.metodo = "Db_RegistrarEmpresa";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
            }
            return resultado;
        }

        private ClsUsuario Db_ConsultaCorreoUsuario(string username)
        {
            ClsResultadoAccion resultado = new();
            ClsUsuario usuario = new();

            try
            {
                string[] parametros = { "username" };
                string[] values = { username };
                DataSet resultadoConsulta = BD_DLL.ClsBD.Consulta_con_parametros("usuario.consultar_email_responsable", Conexion.CadenaConexion,
                    parametros, values, Conexion.isolationLevelSnap());
                if (resultadoConsulta.Tables.Count > 0)
                {
                    if (resultadoConsulta.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow fila in resultadoConsulta.Tables[0].Rows)
                        {
                            usuario.CorreoResponsable = FntGenericas.ValidaNullString(fila["email_responsable"].ToString(), "");
                            usuario.IdUsuario = FntGenericas.ValidaNullString(fila["id_usuario"].ToString(), "");
                            usuario.Empresa = FntGenericas.ValidaNullString(fila["empresa"].ToString(), "");
                            usuario.Usuario = username;
                        }
                        resultado.Estatus = true;
                    }
                }
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef.Empresa";
                log.Log.metodo = "Db_ConsultaCorreoUsuario";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return usuario;
        }

        private ClsResultadoAccion Db_InsertarOTP(string otp, string idUsuario, DateTime fechaExpiracion)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            try
            {
                string[] parametros = { "otp", "idUsuario", "fechaExpiracion" };
                string[] values = { otp, idUsuario, fechaExpiracion.ToString("yyyyMMdd HH:mm:ss.fff") };
                BD_DLL.ClsBD.Consulta_con_parametros("usuario.insertar_otp", Conexion.CadenaConexion, parametros, values, Conexion.isolationLevelSnap());
                resultado.Estatus = true;
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Empresa";
                log.Log.metodo = "Db_InsertarOTP";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
            }
            return resultado;
        }

        private ClsResultadoAccion Db_ConsultaOTP(string otp)
        {
            ClsResultadoAccion resultado = new();

            try
            {
                string[] parametros = { "otp" };
                string[] values = { otp };

                string idUsuario = "";
                DateTime fechaExpiracion = new();
                DataSet resultadoConsulta = BD_DLL.ClsBD.Consulta_con_parametros("usuario.consultar_otp", Conexion.CadenaConexion,
                    parametros, values, Conexion.isolationLevelSnap());
                if (resultadoConsulta.Tables.Count > 0)
                {
                    if (resultadoConsulta.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow fila in resultadoConsulta.Tables[0].Rows)
                        {
                            idUsuario = FntGenericas.ValidaNullString(fila["id_usuario"].ToString(), "");
                            fechaExpiracion = FntGenericas.ValidaNullDateTime(fila["fecha_expiracion"].ToString(), Convert.ToDateTime("01/01/1990"));
                        }

                        if(DateTime.Now < fechaExpiracion)
                        {
                            resultado.Estatus = true;
                            resultado.Detalle = idUsuario;
                            resultado.Code = 200;
                        }
                        else
                        {
                            resultado.Estatus = false;
                            resultado.Code = 400;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef.Empresa";
                log.Log.metodo = "Db_ConsultaCorreoUsuario";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return resultado;
        }

        private ClsResultadoAccion Db_CambiarPasswordUsuario(string idUsuario, string newPassword)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            try
            {
                string[] parametros = { "idUsuario", "password" };
                string[] values = { idUsuario, newPassword };
                BD_DLL.ClsBD.Consulta_con_parametros("usuario.cambiar_password", Conexion.CadenaConexion, parametros, values, Conexion.isolationLevelSnap());
                resultado.Estatus = true;
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Empresa";
                log.Log.metodo = "Db_CambiarPasswordUsuario";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
            }
            return resultado;
        }


        #endregion

        #region Productos Empresa
        private void Db_RegistrarProductoEmpresa(string idEmpresa, string idProducto, string descripcion)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            try
            {
                string[] parametros = { "idEmpresa", "idProducto", "descripcion" };
                string[] values = { idEmpresa, idProducto, descripcion };
                BD_DLL.ClsBD.Consulta_con_parametros("catalogo.insertar_productos_empresa", Conexion.CadenaConexion, parametros, values, Conexion.isolationLevelSnap());
                resultado.Estatus = true;
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Empresa";
                log.Log.metodo = "Db_RegistrarProductoEmpresa";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
            }
            //return resultado;
        }

        private void Db_RegistrarCausasProducto(string idEmpresa, string idProducto, string idCausa, string descripcion)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            try
            {
                string[] parametros = { "idEmpresa", "idProducto", "idCausa", "descripcion" };
                string[] values = { idEmpresa, idProducto, idCausa, descripcion };
                BD_DLL.ClsBD.Consulta_con_parametros("catalogo.insertar_causas_productos_empresa", Conexion.CadenaConexion, parametros, values, Conexion.isolationLevelSnap());
                resultado.Estatus = true;
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Empresa";
                log.Log.metodo = "Db_RegistrarCausasProducto";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
            }
            //return resultado;
        }

        public async Task<ClsResultadoAccion> DescargarCatalogoProductos(string idEmpresa, string idSuperUsuario)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            Condusef condusef = new Condusef();
            try
            {
                resultado = await condusef.ConsultaTokenUsuario(idSuperUsuario,"1");
                if (resultado.Estatus)
                {
                    string token = resultado.Detalle;
                    if(token != null|| token != "") {
                        resultado = await Api_Productos(token);
                        if (resultado.Estatus)
                        {
                            //Parallel.ForEach(ListadoProductos.products, async (item, loopState) =>
                            //{
                            //    string nombreProducto = ObtenerNombreProducto(item.product);
                            //    Db_RegistrarProductoEmpresa(idEmpresa, item.productId, nombreProducto);
                            //    resultado = await Api_Causas(token, item.productId);
                            //    if (resultado.Estatus)
                            //    {
                            //        foreach (var causa in ListadoCausas.causas)
                            //        {
                            //            Db_RegistrarCausasProducto(idEmpresa, item.productId, causa.causaId, causa.causa);
                            //        }
                            //    }
                            //});

                            foreach (var item in ListadoProductos.products)
                            {
                                string nombreProducto = ObtenerNombreProducto(item.product);
                                Db_RegistrarProductoEmpresa(idEmpresa, item.productId, nombreProducto);
                                resultado = await Api_Causas(token, item.productId);
                                if (resultado.Estatus)
                                {
                                    foreach (var causa in ListadoCausas.causas)
                                    {
                                        Db_RegistrarCausasProducto(idEmpresa, item.productId, causa.causaId, causa.causa);
                                    }
                                }
                            }
                            resultado.Estatus = true;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef.Empresa";
                log.Log.metodo = "DescargarCatalogoProductos";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return resultado;
        }

        private async Task<ClsResultadoAccion> Api_Productos(string authorization)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            RespuestaProductos respuesta = new RespuestaProductos();
            try
            {
                string apiUrl = VariablesGlobales.Url.ConsultaListaProductos;
                // Crear una instancia de HttpClient
                using (HttpClient client = new HttpClient())
                {

                    HttpRequestMessage request = new HttpRequestMessage();
                    HttpResponseMessage response2 = new HttpResponseMessage();

                    request.RequestUri = new Uri(apiUrl);
                    request.Method = HttpMethod.Get;
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
                        respuesta = JsonSerializer.Deserialize<RespuestaProductos>(responseJson);
                        resultado.Estatus = true;
                        ListadoProductos = respuesta;
                    }
                    else
                    {
                        string responseJson = await response2.Content.ReadAsStringAsync();
                        resultado.Estatus = false;
                        resultado.Code = (int)response2.StatusCode;

                        FntLog.insertaLogApi(Constantes.REDECO, "Descargar Catalogo Productos", resultado.Code, apiUrl, responseJson);
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.Estatus = false;
                resultado.Detalle = $"Error en la solicitud: {ex.Message}";
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef.Empresa";
                log.Log.metodo = "Api_Productos";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            //respuestaToken.resultado = resultado;
            return resultado;
        }

        private async Task<ClsResultadoAccion> Api_Causas(string authorization, string idProducto)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            RespuestaCausas respuesta = new RespuestaCausas();
            try
            {
                string apiUrl = VariablesGlobales.Url.ConsultaListaCausas;
                string urlWithParameters = $"{apiUrl}?product={idProducto}";
                // Crear una instancia de HttpClient
                using (HttpClient client = new HttpClient())
                {

                    HttpRequestMessage request = new HttpRequestMessage();
                    HttpResponseMessage response2 = new HttpResponseMessage();

                    request.RequestUri = new Uri(urlWithParameters);
                    request.Method = HttpMethod.Get;
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
                        respuesta = JsonSerializer.Deserialize<RespuestaCausas>(responseJson);
                        resultado.Estatus = true;
                        ListadoCausas = respuesta;
                    }
                    else
                    {
                        string responseJson = await response2.Content.ReadAsStringAsync();
                        resultado.Estatus = false;
                        resultado.Code = (int)response2.StatusCode;

                        FntLog.insertaLogApi(Constantes.REDECO, "Descargar Catalogo Causas", resultado.Code, urlWithParameters, responseJson);
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.Estatus = false;
                resultado.Detalle = $"Error en la solicitud: {ex.Message}";
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Condusef.Empresa";
                log.Log.metodo = "Api_Causas";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            //respuestaToken.resultado = resultado;
            return resultado;
        }

        #endregion

        #region functiones

        private string GenerarUid()
        {
            // Crear un nuevo Guid
            Guid uid = Guid.NewGuid();

            // Puedes convertirlo a formato string si necesitas
            string uidString = uid.ToString();
            return uidString;
        }

        private string ObtenerNombreProducto(string producto)
        {
            string nombreProducto = string.Empty;

            string siglas = ExtractSiglas(producto);
            if (!string.IsNullOrEmpty(siglas))
            {
                switch (siglas)
                {
                    case "AF":
                        nombreProducto = "Arrendamiento Financiero / " + producto;
                        break;
                    case "Auto":
                        nombreProducto = "Crédito al auto / " + producto;
                        break;
                    case "CGH":
                        nombreProducto = "Crédito con garantía hipotecaria / " + producto;
                        break;
                    case "CGP":
                        nombreProducto = "Crédito con garantía prendaria / " + producto;
                        break;
                    case "CN":
                        nombreProducto = "Crédito de nómina / " + producto;
                        break;
                    case "CE":
                        nombreProducto = "Crédito empresarial / " + producto;
                        break;
                    case "CH":
                        nombreProducto = "Crédito hipotecario / " + producto;
                        break;
                    case "CAM":
                        nombreProducto = "Crédito para adultos mayores (pensionados o jubilados) / " + producto;
                        break;
                    case "CP":
                        nombreProducto = "Crédito personal / " + producto;
                        break;
                    case "CPYME":
                        nombreProducto = "Crédito PYME / " + producto;
                        break;
                    case "CQ":
                        nombreProducto = "Crédito quirografario / " + producto;
                        break;
                    case "CSG":
                        nombreProducto = "Créditos solidarios y/o grupal / " + producto;
                        break;
                    case "CPHA":
                        nombreProducto = "Créditos y préstamos de habilitación y avío / " + producto;
                        break;
                    case "CPR":
                        nombreProducto = "Créditos y préstamos refaccionarios / " + producto;
                        break;
                    case "FF":
                        nombreProducto = "Factoraje Financiero / " + producto;
                        break;
                    case "MC":
                        nombreProducto = "Microcréditos / " + producto;
                        break;
                    case "TC":
                        nombreProducto = "Tarjeta de crédito / " + producto;
                        break;
                    case "CA":
                        nombreProducto = "Cajero automático / " + producto;
                        break;
                    case "CORR":
                        nombreProducto = "Corresponsales / " + producto;
                        break;
                    case "D":
                        nombreProducto = "Domiciliación / " + producto;
                        break;
                    case "F":
                        nombreProducto = "Fideicomisos / " + producto;
                        break;
                    case "SM":
                        nombreProducto = "Servicio móvil / " + producto;
                        break;
                    case "UNE":
                        nombreProducto = "Servicios en sucursal y/o UNE / " + producto;
                        break;
                    default:
                        nombreProducto = producto;
                        break;
                }
            }
            return nombreProducto;
        }

        static string ExtractSiglas(string input)
        {
            string pattern = @"\(([^)]*)\)";
            Match match = Regex.Match(input, pattern);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            return string.Empty;
        }

        #endregion
    }
}
