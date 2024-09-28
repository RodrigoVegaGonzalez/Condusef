using Condusef_DLL.Clases;
using Condusef_DLL.Clases.Admin;
using Condusef_DLL.Clases.Condusef.API;
using Condusef_DLL.Clases.General;
using Condusef_DLL.Clases.Generales;
using Condusef_DLL.Funciones.Administracion;
using Condusef_DLL.Funciones.Condusef;
using Condusef_DLL.Funciones.Generales;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Funciones.Administracion
{
    public class ClsProductosReune
    {
        public string IdProducto { get; set; }
        public string Producto { get; set; }
        public string IdCausa {  get; set; }
        public string Causa { get; set; }
        public bool Consulta { get; set; }
        public bool Reclamacion { get; set; }
        public bool Aclaracion { get; set; }
        public int IdSector { get; set; }
        public ClsProductosReune() { 
            IdProducto = string.Empty;
            Producto = string.Empty;
            IdCausa = string.Empty;
            Causa = string.Empty;
            Consulta = false;
            Reclamacion = false;
            Aclaracion = false;
            IdSector = 0;
        }
    }
    public class Administracion
    {
        public ClsAdministrador Administrador { get; set; }
        public List<ClsEmpresas> EmpresasNoRegistradas { get; set; }
        public List<ClsEmpresas> EmpresasActivas { get; set; }
        public List<ClsCatalogo> ListadoEmpresas { get; set; }
        public List<ClsCatalogo> ListadoServicios { get; set; }
        public List<ClsLogAPI> ListadoLogAPI { get; set; }


        public Administracion() {
            Administrador = new ClsAdministrador();
            EmpresasActivas = new List<ClsEmpresas>();
            EmpresasNoRegistradas = new List<ClsEmpresas>(); 
            ListadoServicios = new List<ClsCatalogo>();
            ListadoEmpresas = new List<ClsCatalogo>();
            ListadoLogAPI = new List<ClsLogAPI>();
        }

        public void CargaCatalogos()
        {
            ListadoEmpresas = Catalogos.ConsultarListadoEmpresas();
            ListadoServicios = Catalogos.ConsultarListadoServicios();
        }

        public ClsResultadoAccion AdminLogin(string username, string password)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            ClsAdministrador admin = new ClsAdministrador();
            try
            {
                //string hashedPassword_ = BCrypt.Net.BCrypt.HashPassword("4dministr4ci0n@Inf0100_U1.");
                //string hashedPassword_1 = BCrypt.Net.BCrypt.HashPassword("1nf0100@admin");
                string hashedPassword = string.Empty;
                string[] parametro = new string[] { "username" };
                string[] valorParametro = new string[] { username };

                DataSet resultadoConsulta = BD_DLL.ClsBD.Consulta_con_parametros("admin.loguear_administrador", Conexion.CadenaConexionSeguridad,
                    parametro, valorParametro, Conexion.isolationLevelSnap());
                if (resultadoConsulta.Tables.Count > 0)
                {
                    if(resultadoConsulta.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow fila in resultadoConsulta.Tables[0].Rows)
                        {
                            admin.Username = FntGenericas.ValidaNullString(fila["username"].ToString(), "");
                            hashedPassword = FntGenericas.ValidaNullString(fila["password"].ToString(), "");
                            admin.Rol = FntGenericas.ValidaNullint(fila["id_rol"].ToString(), 0);
                            admin.Id = FntGenericas.ValidaNullint(fila["id"].ToString(), 0);
                        }

                        bool passwordMatch = BCrypt.Net.BCrypt.Verify(password, hashedPassword);

                        resultado.Estatus = passwordMatch;
                    }
                    
                }

            }
            catch (Exception ex)
            {
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
            }
            Administrador = admin;
            return resultado;
        }

        public ClsResultadoAccion ConsultarEmpresasNoRegistradas()
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            List<ClsEmpresas> empresas = new();
            try
            {
                DataSet resultadoConsulta = BD_DLL.ClsBD.Consulta_sin_parametros("empresa.consultar_empresas_no_registradas", Conexion.CadenaConexion,
                    Conexion.isolationLevelSnap());
                if (resultadoConsulta.Tables.Count > 0)
                {
                    if (resultadoConsulta.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow fila in resultadoConsulta.Tables[0].Rows)
                        {
                            ClsEmpresas empresa = new();
                            empresa.IdEmpresa = FntGenericas.ValidaNullString(fila["id_empresa"].ToString(), "");
                            empresa.NombreEmpresa = FntGenericas.ValidaNullString(fila["nombre"].ToString(), "");
                            empresas.Add(empresa);
                        }
                        resultado.Estatus = true;
                    }
                }
            }
            catch (Exception e)
            {
                resultado.Estatus = false;
                resultado.Detalle = e.Message;
            }
            EmpresasNoRegistradas = empresas;
            return resultado;
        }

        public ClsResultadoAccion ConsultarEmpresasActivas()
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            List<ClsEmpresas> empresas = new();
            try
            {
                DataSet resultadoConsulta = BD_DLL.ClsBD.Consulta_sin_parametros("empresa.consultar_empresas_activas", Conexion.CadenaConexion,
                    Conexion.isolationLevelSnap());
                if (resultadoConsulta.Tables.Count > 0)
                {
                    if (resultadoConsulta.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow fila in resultadoConsulta.Tables[0].Rows)
                        {
                            ClsEmpresas empresa = new();
                            empresa.IdEmpresa = FntGenericas.ValidaNullString(fila["id_empresa"].ToString(), "");
                            empresa.NombreEmpresa = FntGenericas.ValidaNullString(fila["nombre"].ToString(), "");
                            empresa.Estatus = FntGenericas.ValidaNullBool(fila["estatus"].ToString(), false);
                            empresas.Add(empresa);
                        }
                        resultado.Estatus = true;
                    }
                }
            }
            catch (Exception e)
            {
                resultado.Estatus = false;
                resultado.Detalle = e.Message;
            }
            EmpresasActivas = empresas;
            return resultado;
        }

        public async Task<ClsResultadoAccion> CargarCatalogoProductosReune(ClsStreamFile file)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            List<string[]> ValoresFilasArray = new List<string[]>();
            List<ClsProductosReune> productos = new List<ClsProductosReune>();
            try
            {
                resultado = await FntArchivos.GuardarArchivo(file.Path, file.File);
                if (resultado.Estatus)
                {
                    ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                    using (var templatePackage = new ExcelPackage(new FileInfo(file.Path)))
                    {
                        var ws = templatePackage.Workbook.Worksheets["CAT-Producto-Servicio-Causa"];
                        if (ws != null)
                        {
                            // Establece el rango a partir de la fila 2
                            var startRow = 4;
                            // Encuentra la última fila con datos en la hoja actual
                            var endRow = ws.Dimension?.End.Row ?? startRow;

                            // Limpia el contenido del rango especificado
                            for (int row = startRow; row <= endRow; row++)
                            {
                                List<string> ValoresUnaFila = new List<string>();
                                ClsProductosReune per = new ClsProductosReune();
                                string producto = "";
                                per.IdSector = 2;
                                producto = FntGenericas.ValidaNullString(ws.Cells[row, 2].Value?.ToString(), "").Trim();
                                producto += " / " + FntGenericas.ValidaNullString(ws.Cells[row, 3].Value?.ToString(), "").Trim();
                                
                                per.Causa = FntGenericas.ValidaNullString(ws.Cells[row, 4].Value?.ToString(), "");
                                per.IdProducto = FntGenericas.ValidaNullString(ws.Cells[row, 5].Value?.ToString(), "");
                                per.IdCausa = FntGenericas.ValidaNullString(ws.Cells[row, 6].Value?.ToString(), "");
                                string consulta = FntGenericas.ValidaNullString(ws.Cells[row, 7].Value?.ToString(), "");
                                string reclamacion = FntGenericas.ValidaNullString(ws.Cells[row, 8].Value?.ToString(), "");
                                string aclaracion = FntGenericas.ValidaNullString(ws.Cells[row, 9].Value?.ToString(), "");

                                per.Producto = FntGenericas.ValidaNullString(ws.Cells[row, 3].Value?.ToString(), "");
                                per.IdProducto = EliminarComillasYEspacios(per.IdProducto);
                                per.IdCausa = EliminarComillasYEspacios(per.IdCausa);
                                per.Consulta = EsSI(consulta);
                                per.Reclamacion = EsSI(reclamacion);
                                per.Aclaracion = EsSI(aclaracion);

                                productos.Add(per);
                            }
                        }
                    }
                    foreach(var item in productos)
                    {
                        Db_InsertaProd(item);
                    }
                    resultado = await FntArchivos.BorrarArchivo(file.Path);
                }
                resultado.Estatus = true;
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Administracion";
                log.Log.metodo = "CargarCatalogoProductosReune";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
            }
            return resultado;
        }

        private void Db_InsertaProd(ClsProductosReune producto)
        {
            ClsResultadoAccion resultado = new();
            try
            {
                string[] parametros = { "idProducto", "producto", "idCausa", "causa", "consulta", "reclamacion", "aclaracion", "id_sector"
                };
                string[] values = { producto.IdProducto, producto.Producto, producto.IdCausa, producto.Causa, producto.Consulta.ToString(),
                    producto.Reclamacion.ToString(), producto.Aclaracion.ToString(), producto.IdSector.ToString(),
                };
                BD_DLL.ClsBD.Consulta_con_parametros("catalogo.inserta_causa_producto_reune", Conexion.CadenaConexion,
                    parametros, values, Conexion.isolationLevelSnap());
                resultado.Estatus = true;
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Administracion";
                log.Log.metodo = "Db_InsertaQueja";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
                throw ex;
            }
            //return resultado;
        }

        public ClsResultadoAccion ConsultarLogAPI(string idEmpresa, int idServicio)
        {
            ClsResultadoAccion resultado = new();
            List<ClsLogAPI> logAPI = new List<ClsLogAPI>();
            try
            {
                string[] parametros = { "empresa", "idServicio" };
                string[] values = { idEmpresa, idServicio.ToString() };
                DataSet resultadoConsulta = BD_DLL.ClsBD.Consulta_con_parametros("bitacora.consulta_log_api", Conexion.CadenaConexion,
                    parametros, values, Conexion.isolationLevelSnap());
                if (resultadoConsulta.Tables.Count > 0)
                {
                    if (resultadoConsulta.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow fila in resultadoConsulta.Tables[0].Rows)
                        {
                            ClsLogAPI log = new();
                            log.Metodo = FntGenericas.ValidaNullString(fila["metodo"].ToString(), "");
                            log.CodigoError = FntGenericas.ValidaNullint(fila["codigo_error"].ToString(), 0);
                            log.Peticion = FntGenericas.ValidaNullString(fila["peticion"].ToString(), "");
                            log.Error = FntGenericas.ValidaNullString(fila["error"].ToString(), "");
                            log.Fecha = FntGenericas.ValidaNullDateTime(fila["fecha"].ToString(), new DateTime());
                            logAPI.Add(log);
                        }
                        resultado.Estatus = true;
                        if (logAPI.Count > 0) resultado.Code = 200;
                    }
                }
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Administracion";
                log.Log.metodo = "ConsultarLogAPI";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
                resultado.Code = 500;
            }
            ListadoLogAPI = logAPI;
            return resultado;
        }

        //public ClsResultadoAccion ProcesarSolicitud(string idSolicitud)
        //{
        //    ClsResultadoAccion resultado = new ClsResultadoAccion();
        //    try
        //    {
        //        //string hashedPassword_ = BCrypt.Net.BCrypt.HashPassword("4dministr4ci0n@Inf0100_U1.");
        //        //string hashedPassword_1 = BCrypt.Net.BCrypt.HashPassword("1nf0100@admin");
        //        string hashedPassword = string.Empty;
        //        string[] parametro = new string[] { "username" };
        //        string[] valorParametro = new string[] { username };

        //        DataSet resultadoConsulta = BD_DLL.ClsBD.Consulta_con_parametros("admin.loguear_administrador", Conexion.CadenaConexionSeguridad,
        //            parametro, valorParametro, Conexion.isolationLevelSnap());
        //        if (resultadoConsulta.Tables.Count > 0)
        //        {
        //            if (resultadoConsulta.Tables[0].Rows.Count > 0)
        //            {
        //                foreach (DataRow fila in resultadoConsulta.Tables[0].Rows)
        //                {
        //                    admin.Username = FntGenericas.ValidaNullString(fila["username"].ToString(), "");
        //                    hashedPassword = FntGenericas.ValidaNullString(fila["password"].ToString(), "");
        //                    admin.Rol = FntGenericas.ValidaNullint(fila["id_rol"].ToString(), 0);
        //                    admin.Id = FntGenericas.ValidaNullint(fila["id"].ToString(), 0);
        //                }

        //                bool passwordMatch = BCrypt.Net.BCrypt.Verify(password, hashedPassword);

        //                resultado.Estatus = passwordMatch;
        //            }

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        resultado.Estatus = false;
        //        resultado.Detalle = ex.Message;
        //    }
        //    Administrador = admin;
        //    return resultado;
        //}

        //public ClsResultadoAccion ConsultarEmpresas()
        //{
        //    ClsResultadoAccion resultado = new ClsResultadoAccion();
        //    ClsAdministrador admin = new ClsAdministrador();
        //    try
        //    {
        //        DataSet resultadoConsulta = BD_DLL.ClsBD.Consulta_con_parametros("usuario.consultar_empresas", Conexion.CadenaConexion, Conexion.isolationLevelSnap());
        //        if (resultadoConsulta.Tables.Count > 0)
        //        {
        //            if (resultadoConsulta.Tables[0].Rows.Count > 0)
        //            {
        //                foreach (DataRow fila in resultadoConsulta.Tables[0].Rows)
        //                {
        //                    admin.Username = FntGenericas.ValidaNullString(fila["username"].ToString(), "");
        //                    hashedPassword = FntGenericas.ValidaNullString(fila["password"].ToString(), "");
        //                    admin.Rol = FntGenericas.ValidaNullint(fila["id_rol"].ToString(), 0);
        //                    admin.Id = FntGenericas.ValidaNullint(fila["id"].ToString(), 0);
        //                }

        //                bool passwordMatch = BCrypt.Net.BCrypt.Verify(password, hashedPassword);

        //                resultado.Estatus = passwordMatch;
        //            }

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        resultado.Estatus = false;
        //        resultado.Detalle = ex.Message;
        //    }
        //    Administrador = admin;
        //    return resultado;
        //}

        #region Funciones privadas

        private static string GenerarContraseña()
        {
            // Caracteres posibles para la contraseña
            string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()-_";

            // Longitud de la contraseña deseada
            int longitudContraseña = 10;

            // Utilizar un objeto Random para generar caracteres aleatorios
            Random random = new Random();

            // Construir la contraseña
            char[] contraseña = new char[longitudContraseña];
            for (int i = 0; i < longitudContraseña; i++)
            {
                contraseña[i] = caracteres[random.Next(caracteres.Length)];
            }

            // Convertir el array de caracteres en una cadena
            return new string(contraseña);
        }

        static string EliminarComillasYEspacios(string cadena)
        {
            // Eliminar comillas simples
            cadena = cadena.Replace("'", "");

            // Eliminar espacios en blanco
            cadena = cadena.Replace(" ", "");

            return cadena;
        }

        static bool EsSI(string cadena)
        {
            cadena = EliminarComillasYEspacios(cadena);

            // Validar si la cadena es null, vacía o espacio en blanco
            if (string.IsNullOrWhiteSpace(cadena))
            {
                return false;
            }

            // Validar si la cadena es "SI"
            return cadena.Trim().Equals("SI", StringComparison.OrdinalIgnoreCase);
        }

        #endregion
    }
}
