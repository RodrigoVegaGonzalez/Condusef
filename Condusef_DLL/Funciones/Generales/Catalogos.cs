using Condusef_DLL.Clases;
using Condusef_DLL.Clases.Generales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Funciones.Generales
{
    public class Catalogos
    {
       
        public static List<ClsCatalogo> ConsultarTiposDocumentos()
        {
            List<ClsCatalogo> valoresCatalogo = new List<ClsCatalogo>();
            try
            {
                DataSet ds = BD_DLL.ClsBD.Consulta_sin_parametros("catalogo.consultar_tipo_documentos", Conexion.CadenaConexion, Conexion.isolationLevelSnap());
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow fila in ds.Tables[0].Rows)
                    {
                        ClsCatalogo valor = new ClsCatalogo();
                        valor.Id = fila["id_documento"].ToString();
                        valor.Descripcion = fila["descripcion"].ToString();
                        valoresCatalogo.Add(valor);
                    }
                }
            }
            catch (Exception ex)
            {
                valoresCatalogo = new List<ClsCatalogo>();
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Generales.Catalogos";
                log.Log.metodo = "ConsultarTiposDocumentos";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return valoresCatalogo;
        }

        public static List<ClsCatalogo> ConsultarListadoEmpresas()
        {
            List<ClsCatalogo> valoresCatalogo = new List<ClsCatalogo>();
            try
            {
                DataSet ds = BD_DLL.ClsBD.Consulta_sin_parametros("catalogo.consultar_empresas", Conexion.CadenaConexion, Conexion.isolationLevelSnap());
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow fila in ds.Tables[0].Rows)
                    {
                        ClsCatalogo valor = new ClsCatalogo();
                        valor.Id = fila["id"].ToString();
                        valor.Descripcion = fila["descripcion"].ToString();
                        valoresCatalogo.Add(valor);
                    }
                }
            }
            catch (Exception ex)
            {
                valoresCatalogo = new List<ClsCatalogo>();
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Generales.Catalogos";
                log.Log.metodo = "ConsultarListadoEmpresas";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return valoresCatalogo;
        }

        public static List<ClsCatalogo> ConsultarListadoServicios()
        {
            List<ClsCatalogo> valoresCatalogo = new List<ClsCatalogo>();
            try
            {
                DataSet ds = BD_DLL.ClsBD.Consulta_sin_parametros("catalogo.consultar_servicios", Conexion.CadenaConexion, Conexion.isolationLevelSnap());
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow fila in ds.Tables[0].Rows)
                    {
                        ClsCatalogo valor = new ClsCatalogo();
                        valor.Id = fila["id"].ToString();
                        valor.Descripcion = fila["descripcion"].ToString();
                        valoresCatalogo.Add(valor);
                    }
                }
            }
            catch (Exception ex)
            {
                valoresCatalogo = new List<ClsCatalogo>();
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Generales.Catalogos";
                log.Log.metodo = "ConsultarListadoServicios";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return valoresCatalogo;
        }

        public static List<ClsCatalogo> consultaLocalidadesCNBV()
        {
            List<ClsCatalogo> valoresCatalogo = new List<ClsCatalogo>();
            DataSet ds = null;
            try
            {
                ds = BD_DLL.ClsBD.Consulta_sin_parametros("catalogo.consulta_localidadesCNBV", Conexion.CadenaConexion, Conexion.isolationLevelRead());
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow fila in ds.Tables[0].Rows)
                    {
                        ClsCatalogo valor = new ClsCatalogo();
                        valor.Id = fila["id"].ToString();
                        valor.Descripcion = fila["descripcion"].ToString();
                        valoresCatalogo.Add(valor);
                    }
                }
            }
            catch (Exception ex)
            {
                valoresCatalogo = new List<ClsCatalogo>();
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Generales.Catalogos";
                log.Log.metodo = "consultaLocalidadesCNBV";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return valoresCatalogo;
        }
        public static List<ClsCatalogo> consultaEstadosCNBV()
        {
            List<ClsCatalogo> valoresCatalogo = new List<ClsCatalogo>();
            DataSet ds = null;
            try
            {
                ds = BD_DLL.ClsBD.Consulta_sin_parametros("catalogo.consultar_estados", Conexion.CadenaConexion, Conexion.isolationLevelRead());
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow fila in ds.Tables[0].Rows)
                    {
                        ClsCatalogo valor = new ClsCatalogo();
                        valor.Id = fila["id"].ToString();
                        valor.Descripcion = fila["descripcion"].ToString();
                        valoresCatalogo.Add(valor);
                    }
                }
            }
            catch (Exception ex)
            {
                valoresCatalogo = new List<ClsCatalogo>();
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Generales.Catalogos";
                log.Log.metodo = "consultaEstadosCNBV";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return valoresCatalogo;
        }

        public static List<ClsCatalogo> consultaRoles()
        {
            List<ClsCatalogo> valoresCatalogo = new List<ClsCatalogo>();
            DataSet ds = null;
            try
            {
                ds = BD_DLL.ClsBD.Consulta_sin_parametros("catalogo.consulta_roles", Conexion.CadenaConexion, Conexion.isolationLevelRead());
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow fila in ds.Tables[0].Rows)
                    {
                        ClsCatalogo valor = new ClsCatalogo();
                        valor.Id = fila["id"].ToString();
                        valor.Descripcion = fila["descripcion"].ToString();
                        valoresCatalogo.Add(valor);
                    }
                }
            }
            catch (Exception ex)
            {
                valoresCatalogo = new List<ClsCatalogo>();
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Generales.Catalogos";
                log.Log.metodo = "consultaRoles";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return valoresCatalogo;
        }

        public static List<ClsCatalogo> ConsultaSectores()
        {
            List<ClsCatalogo> valoresCatalogo = new List<ClsCatalogo>();
            DataSet ds = null;
            try
            {
                ds = BD_DLL.ClsBD.Consulta_sin_parametros("catalogo.consultar_sectores", Conexion.CadenaConexion, Conexion.isolationLevelRead());
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow fila in ds.Tables[0].Rows)
                    {
                        ClsCatalogo valor = new ClsCatalogo();
                        valor.Id = fila["id_sector"].ToString();
                        valor.Descripcion = fila["descripcion"].ToString();
                        valoresCatalogo.Add(valor);
                    }
                }
            }
            catch (Exception ex)
            {
                valoresCatalogo = new List<ClsCatalogo>();
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Generales.Catalogos";
                log.Log.metodo = "ConsultaSectores";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return valoresCatalogo;
        }

        public static List<ClsCatalogo> CargarMeses()
        {
            List<ClsCatalogo> valoresCatalogo = new List<ClsCatalogo>();
            DataSet ds = null;
            try
            {
                ds = BD_DLL.ClsBD.Consulta_sin_parametros("catalogo.cargar_meses", Conexion.CadenaConexion, Conexion.isolationLevelRead());
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow fila in ds.Tables[0].Rows)
                    {
                        ClsCatalogo valor = new ClsCatalogo();
                        valor.Id = fila["id_mes"].ToString();
                        valor.Descripcion = fila["descripcion"].ToString();
                        valor.Descripcion_en = fila["descripcion_en"].ToString();
                        valoresCatalogo.Add(valor);
                    }
                }
            }
            catch (Exception ex)
            {
                valoresCatalogo = new List<ClsCatalogo>();
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Generales.Catalogos";
                log.Log.metodo = "CargarMeses";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return valoresCatalogo;
        }

        public static List<ClsCatalogo> CargarTrimestres()
        {
            List<ClsCatalogo> valoresCatalogo = new List<ClsCatalogo>();
            DataSet ds = null;
            try
            {
                ds = BD_DLL.ClsBD.Consulta_sin_parametros("catalogo.cargar_trimestres", Conexion.CadenaConexion, Conexion.isolationLevelRead());
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow fila in ds.Tables[0].Rows)
                    {
                        ClsCatalogo valor = new ClsCatalogo();
                        valor.Id = fila["id_periodo"].ToString();
                        valor.Descripcion = fila["descripcion"].ToString();
                        valoresCatalogo.Add(valor);
                    }
                }
            }
            catch (Exception ex)
            {
                valoresCatalogo = new List<ClsCatalogo>();
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Generales.Catalogos";
                log.Log.metodo = "CargarMeses";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return valoresCatalogo;
        }

        public static List<ClsCatalogo> CargarAniosQuejasRedeco(string idEmpresa)
        {
            List<ClsCatalogo> valoresCatalogo = new List<ClsCatalogo>();
            DataSet ds = null;
            try
            {
                string[] parametros = { "idEmpresa" };
                string[] values = { idEmpresa };
                ds = BD_DLL.ClsBD.Consulta_con_parametros("redeco.cargar_anios_quejas_empresas", Conexion.CadenaConexion, parametros, values, Conexion.isolationLevelRead());
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow fila in ds.Tables[0].Rows)
                    {
                        ClsCatalogo valor = new ClsCatalogo();
                        valor.Id = fila["periodo_anio"].ToString();
                        valor.Descripcion = fila["periodo_anio"].ToString();
                        valoresCatalogo.Add(valor);
                    }
                }
            }
            catch (Exception ex)
            {
                valoresCatalogo = new List<ClsCatalogo>();
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Generales.Catalogos";
                log.Log.metodo = "CargarAniosQuejas";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return valoresCatalogo;
        }

        public static List<ClsCatalogo> CargarAniosEmpresaReune(string idEmpresa, int vista)
        {
            List<ClsCatalogo> valoresCatalogo = new List<ClsCatalogo>();
            DataSet ds = null;
            try
            {
                string[] parametros = { "idEmpresa", "vista" };
                string[] values = { idEmpresa, vista.ToString() };
                ds = BD_DLL.ClsBD.Consulta_con_parametros("reune.cargar_anios_datos_empresas", Conexion.CadenaConexion, parametros, values, Conexion.isolationLevelRead());
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow fila in ds.Tables[0].Rows)
                    {
                        ClsCatalogo valor = new ClsCatalogo();
                        valor.Id = fila["periodo_anio"].ToString();
                        valor.Descripcion = fila["periodo_anio"].ToString();
                        valoresCatalogo.Add(valor);
                    }
                }
            }
            catch (Exception ex)
            {
                valoresCatalogo = new List<ClsCatalogo>();
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Generales.Catalogos";
                log.Log.metodo = "CargarAniosQuejas";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return valoresCatalogo;
        }

        public static List<ClsCatalogo> CargarTrimeses()
        {
            List<ClsCatalogo> valoresCatalogo = new List<ClsCatalogo>();
            DataSet ds = null;
            try
            {
                ds = BD_DLL.ClsBD.Consulta_sin_parametros("catalogo.cargar_trimeses", Conexion.CadenaConexion, Conexion.isolationLevelRead());
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow fila in ds.Tables[0].Rows)
                    {
                        ClsCatalogo valor = new ClsCatalogo();
                        valor.Id = fila["id_trimestre"].ToString();
                        valor.Descripcion = fila["descripcion"].ToString();
                        valor.Descripcion_en = fila["descripcion_en"].ToString();
                        valoresCatalogo.Add(valor);
                    }
                }
            }
            catch (Exception ex)
            {
                valoresCatalogo = new List<ClsCatalogo>();
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Generales.Catalogos";
                log.Log.metodo = "CargarMeses";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return valoresCatalogo;
        }

        public static List<ClsCatalogo> CargarRespuestas()
        {
            List<ClsCatalogo> valoresCatalogo = new List<ClsCatalogo>();
            DataSet ds = null;
            try
            {
                ds = BD_DLL.ClsBD.Consulta_sin_parametros("catalogo.cargar_tipo_respuesta", Conexion.CadenaConexion, Conexion.isolationLevelRead());
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow fila in ds.Tables[0].Rows)
                    {
                        ClsCatalogo valor = new ClsCatalogo();
                        valor.Id = fila["id_catalogo"].ToString();
                        valor.Descripcion = fila["descripcion"].ToString();
                        //valor.Descripcion_en = fila["descripcion_en"].ToString();
                        valoresCatalogo.Add(valor);
                    }
                }
            }
            catch (Exception ex)
            {
                valoresCatalogo = new List<ClsCatalogo>();
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Generales.Catalogos";
                log.Log.metodo = "CargarRespuestas";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return valoresCatalogo;
        }

        public static List<ClsCatalogo> CargarPenalizaciones()
        {
            List<ClsCatalogo> valoresCatalogo = new List<ClsCatalogo>();
            DataSet ds = null;
            try
            {
                ds = BD_DLL.ClsBD.Consulta_sin_parametros("catalogo.cargar_tipo_penalizacion", Conexion.CadenaConexion, Conexion.isolationLevelRead());
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow fila in ds.Tables[0].Rows)
                    {
                        ClsCatalogo valor = new ClsCatalogo();
                        valor.Id = fila["id_catalogo"].ToString();
                        valor.Descripcion = fila["descripcion"].ToString();
                        //valor.Descripcion_en = fila["descripcion_en"].ToString();
                        valoresCatalogo.Add(valor);
                    }
                }
            }
            catch (Exception ex)
            {
                valoresCatalogo = new List<ClsCatalogo>();
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Generales.Catalogos";
                log.Log.metodo = "CargarPenalizaciones";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return valoresCatalogo;
        }

        public static List<ClsCatalogo> CargarMediosRecepcion(int servicio)
        {
            List<ClsCatalogo> valoresCatalogo = new List<ClsCatalogo>();
            DataSet ds = null;
            try
            {
                if(servicio == 1) //Redeco
                {
                    ds = BD_DLL.ClsBD.Consulta_sin_parametros("catalogo.cargar_medio_recepcion", Conexion.CadenaConexion, Conexion.isolationLevelRead());
                    if (ds.Tables.Count > 0)
                    {
                        foreach (DataRow fila in ds.Tables[0].Rows)
                        {
                            ClsCatalogo valor = new ClsCatalogo();
                            valor.Id = fila["id_medio"].ToString();
                            valor.Descripcion = fila["descripcion"].ToString();
                            valoresCatalogo.Add(valor);
                        }
                    }
                }
                else
                {
                    ds = BD_DLL.ClsBD.Consulta_sin_parametros("catalogo.cargar_medio_recepcion_reune", Conexion.CadenaConexion, Conexion.isolationLevelRead());
                    if (ds.Tables.Count > 0)
                    {
                        foreach (DataRow fila in ds.Tables[0].Rows)
                        {
                            ClsCatalogo valor = new ClsCatalogo();
                            valor.Id = fila["id_medio"].ToString();
                            valor.Descripcion = fila["descripcion"].ToString();
                            valoresCatalogo.Add(valor);
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                valoresCatalogo = new List<ClsCatalogo>();
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Generales.Catalogos";
                log.Log.metodo = "CargarMediosRecepcion";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return valoresCatalogo;
        }

        public static List<ClsCatalogo> CargarNivelesAtencion()
        {
            List<ClsCatalogo> valoresCatalogo = new List<ClsCatalogo>();
            DataSet ds = null;
            try
            {
                ds = BD_DLL.ClsBD.Consulta_sin_parametros("catalogo.cargar_niveles_atencion", Conexion.CadenaConexion, Conexion.isolationLevelRead());
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow fila in ds.Tables[0].Rows)
                    {
                        ClsCatalogo valor = new ClsCatalogo();
                        valor.Id = fila["id_catalogo"].ToString();
                        valor.Descripcion = fila["descripcion"].ToString();
                        valoresCatalogo.Add(valor);
                    }
                }
            }
            catch (Exception ex)
            {
                valoresCatalogo = new List<ClsCatalogo>();
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Generales.Catalogos";
                log.Log.metodo = "CargarNivelesAtencion";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return valoresCatalogo;
        }

        public static List<ClsCatalogo> CargarProductos(string idEmpresa, int servicio, string sector, int vista = 0)
        {
            List<ClsCatalogo> valoresCatalogo = new List<ClsCatalogo>();
            DataSet ds = null;
            try
            {
                if(servicio == 1) //Redeco
                {
                    string[] parametros = { "idEmpresa" };
                    string[] values = { idEmpresa };
                    ds = BD_DLL.ClsBD.Consulta_con_parametros("catalogo.consultar_productos_empresa", Conexion.CadenaConexion, parametros, values, Conexion.isolationLevelRead());
                    if (ds.Tables.Count > 0)
                    {
                        foreach (DataRow fila in ds.Tables[0].Rows)
                        {
                            ClsCatalogo valor = new ClsCatalogo();
                            valor.Id = fila["id"].ToString();
                            valor.Descripcion = fila["descripcion"].ToString();
                            valoresCatalogo.Add(valor);
                        }
                    }
                }
                else
                {
                    string[] parametros = { "vista", "id_sector" };
                    string[] values = { vista.ToString(), sector };
                    ds = BD_DLL.ClsBD.Consulta_con_parametros("catalogo.consultar_productos_reune", Conexion.CadenaConexion, parametros, values, Conexion.isolationLevelRead());
                    if (ds.Tables.Count > 0)
                    {
                        foreach (DataRow fila in ds.Tables[0].Rows)
                        {
                            ClsCatalogo valor = new ClsCatalogo();
                            valor.Id = fila["id"].ToString();
                            valor.Descripcion = fila["descripcion"].ToString();
                            valoresCatalogo.Add(valor);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                valoresCatalogo = new List<ClsCatalogo>();
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Generales.Catalogos";
                log.Log.metodo = "CargarProductos";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return valoresCatalogo;
        }

        public static List<ClsCatalogo> CargarCausasProductos(string idEmpresa, string idProducto, int servicio, int vista, string sector)
        {
            List<ClsCatalogo> valoresCatalogo = new List<ClsCatalogo>();
            DataSet ds = null;
            try
            {
                if(servicio == 1) //Redeco
                {
                    string[] parametros = { "idEmpresa", "idProducto" };
                    string[] values = { idEmpresa, idProducto };
                    ds = BD_DLL.ClsBD.Consulta_con_parametros("catalogo.consultar_causas_productos_empresa", Conexion.CadenaConexion, parametros, values, Conexion.isolationLevelRead());
                    if (ds.Tables.Count > 0)
                    {
                        foreach (DataRow fila in ds.Tables[0].Rows)
                        {
                            ClsCatalogo valor = new ClsCatalogo();
                            valor.Id = fila["id"].ToString();
                            valor.Descripcion = fila["descripcion"].ToString();
                            valoresCatalogo.Add(valor);
                        }
                    }
                }
                else //Reune
                {
                    string[] parametros = { "idProducto", "vista", "id_sector" };
                    string[] values = { idProducto, vista.ToString(), sector };
                    ds = BD_DLL.ClsBD.Consulta_con_parametros("catalogo.consultar_causas_productos_reune", Conexion.CadenaConexion, parametros, values, Conexion.isolationLevelRead());
                    if (ds.Tables.Count > 0)
                    {
                        foreach (DataRow fila in ds.Tables[0].Rows)
                        {
                            ClsCatalogo valor = new ClsCatalogo();
                            valor.Id = fila["id"].ToString();
                            valor.Descripcion = fila["descripcion"].ToString();
                            valoresCatalogo.Add(valor);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                valoresCatalogo = new List<ClsCatalogo>();
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Generales.Catalogos";
                log.Log.metodo = "CargarCausasProductos";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return valoresCatalogo;
        }

        public static List<ClsCatalogo> CargarEstados()
        {
            List<ClsCatalogo> valoresCatalogo = new List<ClsCatalogo>();
            DataSet ds = null;
            try
            {
                ds = BD_DLL.ClsBD.Consulta_sin_parametros("catalogo.consultar_estados", Conexion.CadenaConexion, Conexion.isolationLevelRead());
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow fila in ds.Tables[0].Rows)
                    {
                        ClsCatalogo valor = new ClsCatalogo();
                        valor.Id = fila["id"].ToString();
                        valor.Descripcion = fila["descripcion"].ToString();
                        valoresCatalogo.Add(valor);
                    }
                }
            }
            catch (Exception ex)
            {
                valoresCatalogo = new List<ClsCatalogo>();
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Generales.Catalogos";
                log.Log.metodo = "CargarEstados";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return valoresCatalogo;
        }

        public static List<ClsCatalogo> CargarCP(string idEstado, string idMunicipio)
        {
            List<ClsCatalogo> valoresCatalogo = new List<ClsCatalogo>();
            DataSet ds = null;
            try
            {
                string[] parametros = { "idEstado", "idMunicipio" };
                string[] values = { idEstado, idMunicipio };
                ds = BD_DLL.ClsBD.Consulta_con_parametros("catalogo.consultar_codigos_postales", Conexion.CadenaConexion, parametros, values, Conexion.isolationLevelRead());
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow fila in ds.Tables[0].Rows)
                    {
                        ClsCatalogo valor = new ClsCatalogo();
                        valor.Id = fila["id"].ToString();
                        valor.Descripcion = fila["descripcion"].ToString();
                        valoresCatalogo.Add(valor);
                    }
                }
            }
            catch (Exception ex)
            {
                valoresCatalogo = new List<ClsCatalogo>();
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Generales.Catalogos";
                log.Log.metodo = "CargarCP";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return valoresCatalogo;
        }

        public static List<ClsCatalogo> CargarMunicipios(string idEstado)
        {
            List<ClsCatalogo> valoresCatalogo = new List<ClsCatalogo>();
            DataSet ds = null;
            try
            {
                string[] parametros = { "idEstado" };
                string[] values = { idEstado };
                ds = BD_DLL.ClsBD.Consulta_con_parametros("catalogo.consultar_municipios", Conexion.CadenaConexion, parametros, values, Conexion.isolationLevelRead());
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow fila in ds.Tables[0].Rows)
                    {
                        ClsCatalogo valor = new ClsCatalogo();
                        valor.Id = fila["id"].ToString();
                        valor.Descripcion = fila["descripcion"].ToString();
                        valoresCatalogo.Add(valor);
                    }
                }
            }
            catch (Exception ex)
            {
                valoresCatalogo = new List<ClsCatalogo>();
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Generales.Catalogos";
                log.Log.metodo = "CargarMunicipios";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return valoresCatalogo;
        }

        public static List<ClsCatalogo> CargarColonias(string idEstado, string idMunicipio, string cp)
        {
            List<ClsCatalogo> valoresCatalogo = new List<ClsCatalogo>();
            DataSet ds = null;
            try
            {
                string[] parametros = { "idEstado", "idMunicipio", "cp" };
                string[] values = { idEstado, idMunicipio, cp };
                ds = BD_DLL.ClsBD.Consulta_con_parametros("catalogo.consultar_colonias", Conexion.CadenaConexion, parametros, values, Conexion.isolationLevelRead());
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow fila in ds.Tables[0].Rows)
                    {
                        ClsCatalogo valor = new ClsCatalogo();
                        valor.Id = fila["id"].ToString();
                        valor.Descripcion = fila["descripcion"].ToString();
                        valor.Valor = fila["localidad"].ToString();
                        valoresCatalogo.Add(valor);
                    }
                }
            }
            catch (Exception ex)
            {
                valoresCatalogo = new List<ClsCatalogo>();
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Generales.Catalogos";
                log.Log.metodo = "CargarColonias";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return valoresCatalogo;
        }

        public static List<ClsCatalogo> CargarColonias(string idEstado, string idMunicipio)
        {
            List<ClsCatalogo> valoresCatalogo = new List<ClsCatalogo>();
            DataSet ds = null;
            try
            {
                string[] parametros = { "idEstado", "idMunicipio" };
                string[] values = { idEstado, idMunicipio };
                ds = BD_DLL.ClsBD.Consulta_con_parametros("catalogo.consultar_colonias", Conexion.CadenaConexion, parametros, values, Conexion.isolationLevelRead());
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow fila in ds.Tables[0].Rows)
                    {
                        ClsCatalogo valor = new ClsCatalogo();
                        valor.Id = fila["id"].ToString();
                        valor.Descripcion = fila["descripcion"].ToString();
                        valoresCatalogo.Add(valor);
                    }
                }
            }
            catch (Exception ex)
            {
                valoresCatalogo = new List<ClsCatalogo>();
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Generales.Catalogos";
                log.Log.metodo = "CargarColonias";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return valoresCatalogo;
        }

        public static List<ClsCatalogo> CargarListadoUsuarios()
        {
            List<ClsCatalogo> valoresCatalogo = new List<ClsCatalogo>();
            DataSet ds = null;
            try
            {
                ds = BD_DLL.ClsBD.Consulta_sin_parametros("usuario.obtener_listado_usuarios", Conexion.CadenaConexion, Conexion.isolationLevelRead());
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow fila in ds.Tables[0].Rows)
                    {
                        ClsCatalogo valor = new ClsCatalogo();
                        valor.Id = fila["id"].ToString();
                        valor.Descripcion = fila["descripcion"].ToString();
                        valoresCatalogo.Add(valor);
                    }
                }
            }
            catch (Exception ex)
            {
                valoresCatalogo = new List<ClsCatalogo>();
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Generales.Catalogos";
                log.Log.metodo = "CargarListadoUsuarios";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            return valoresCatalogo;
        }









    }
}
