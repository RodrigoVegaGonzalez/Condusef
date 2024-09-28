using Condusef_DLL.Clases.Admin;
using Condusef_DLL.Clases.General;
using Condusef_DLL.Clases;
using Condusef_DLL.Funciones.Generales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Condusef_DLL.Clases.Condusef.Configuracion;

namespace Condusef_DLL.Funciones.Home
{
    public class Home
    {
        public ClsUsuario User { get; set; }

        public Home()
        {
            User = new();
        }
        public ClsResultadoAccion IniciarSesion(string username, string password)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            ClsUsuario user = new();
            try
            {
                string hashedPassword_ = BCrypt.Net.BCrypt.HashPassword(password);
                string hashedPassword = string.Empty;
                bool activo = false;
                string[] parametro = new string[] { "username" };
                string[] valorParametro = new string[] { username };

                //DataSet result = BD_DLL.ClsBD

                DataSet resultadoConsulta = BD_DLL.ClsBD.Consulta_con_parametros("usuario.loguear_usuario", Conexion.CadenaConexion,
                    parametro, valorParametro, Conexion.isolationLevelSnap());
                if (resultadoConsulta.Tables.Count > 0)
                {
                    foreach (DataRow fila in resultadoConsulta.Tables[0].Rows)
                    {
                        user.Usuario = FntGenericas.ValidaNullString(fila["username"].ToString(), "");
                        hashedPassword = FntGenericas.ValidaNullString(fila["password"].ToString(), "");
                        user.IdRol = FntGenericas.ValidaNullint(fila["id_rol"].ToString(), 0);
                        user.IdUsuario = FntGenericas.ValidaNullString(fila["id_usuario"].ToString(), "");
                        user.IdEmpresa = FntGenericas.ValidaNullString(fila["id_empresa"].ToString(), "");
                        user.Empresa = FntGenericas.ValidaNullString(fila["nombre"].ToString(), "");
                        user.Sector = FntGenericas.ValidaNullString(fila["sector"].ToString(), "");
                        user.IdSector = FntGenericas.ValidaNullString(fila["id_sector"].ToString(), "");
                        user.NombreCortoEmpresa = FntGenericas.ValidaNullString(fila["nombre_corto"].ToString(), "");
                        activo = FntGenericas.ValidaNullBool(fila["estatus_empresa"].ToString(), false);
                    }

                    if (activo)
                    {
                        bool passwordMatch = BCrypt.Net.BCrypt.Verify(password, hashedPassword);
                        resultado.Estatus = passwordMatch;
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
                resultado.Estatus = false;
                resultado.Detalle = ex.Message;
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Home";
                log.Log.metodo = "Db_InsertaQueja";
                log.Log.error = ex.Message;
                log.insertaLog();
            }
            User = user;
            return resultado;
        }
    }
}
