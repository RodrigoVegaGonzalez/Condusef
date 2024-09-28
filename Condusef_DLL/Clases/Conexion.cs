using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Clases
{
    public class Conexion
    {
        public static string CadenaConexion;
        public static string CadenaConexionSeguridad;
        public static string Idioma;
        public static string Usuario;
        public static string Empresa;
        public static string Username;
        public static string RutaLog;

        public virtual void set_CadenaConexion(string valor)
        {
            CadenaConexion = valor;
        }

        public virtual void set_CadenaConexionSeguridad(string valor)
        {
            CadenaConexionSeguridad = valor;
        }

        public static IsolationLevel isolationLevelRead()
        {
            return IsolationLevel.ReadCommitted;
        }

        public static IsolationLevel isolationLevelSnap()
        {
            return IsolationLevel.Snapshot;
        }
    }
}
