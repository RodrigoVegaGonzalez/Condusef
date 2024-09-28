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
using Condusef_DLL.Clases.Home;

namespace Condusef_DLL.Funciones.Home
{
    public class Home_
    {
        public ClsUser User { get; set; }

        public Home_()
        {
            User = new ClsUser();
        }
        public ClsResultadoAccion Login(string username, string password)
        {
            ClsResultadoAccion resultado = new ClsResultadoAccion();
            ClsUser user = new ClsUser();
            try
            {
                string hashedPassword_ = BCrypt.Net.BCrypt.HashPassword("Admin100");
                string hashedPassword = string.Empty;
                bool activo = false;
                String[] parametro = new String[] { "username" };
                String[] valorParametro = new String[] { username };

                DataSet resultadoConsulta = BD_DLL.ClsBD.Consulta_con_parametros("usuario.loguear_usuario", Conexion.CadenaConexion,
                    parametro, valorParametro, Conexion.isolationLevelSnap());
                if (resultadoConsulta.Tables.Count > 0)
                {
                    foreach (DataRow fila in resultadoConsulta.Tables[0].Rows)
                    {
                        user.Username = FntGenericas.ValidaNullString(fila["username"].ToString(), "");
                        hashedPassword = FntGenericas.ValidaNullString(fila["password"].ToString(), "");
                        user.Rol = FntGenericas.ValidaNullint(fila["id_rol"].ToString(), 0);
                        user.Id = FntGenericas.ValidaNullint(fila["id_usuario"].ToString(), 0);
                        user.Empresa = FntGenericas.ValidaNullint(fila["id_empresa"].ToString(), 0);
                        activo = FntGenericas.ValidaNullBool(fila["estatus_empresa"].ToString(), false);
                    }

                    if (activo)
                    {
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
            User = user;
            return resultado;
        }
    }
}
