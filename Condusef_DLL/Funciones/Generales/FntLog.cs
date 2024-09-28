using Condusef_DLL.Clases.Generales;
using Condusef_DLL.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Condusef_DLL.Funciones.Generales
{
    public class FntLog
    {
        public ClsLog Log { get; set; }

        public FntLog()
        {
            Log = new ClsLog();
        }

        public void insertaLog()
        {
            try
            {
                //Crear archivo log
                var message = new
                {
                    usuario = Log.usuario,
                    nameSpace = Log.nameSpace,
                    metodo = Log.metodo,
                    error = Log.error
                };
                GeneraRegistroLog(JsonSerializer.Serialize(message));

                string[] Parametros = new string[] { "usuario", "nameSpace", "metodo", "error" };
                string[] ValorParametros = new string[] { Log.empresa, Log.nameSpace, Log.metodo, Log.error };
                BD_DLL.ClsBD.Inserta_con_parametros("bitacora.insertaLog", Conexion.CadenaConexion, Parametros, ValorParametros, Conexion.isolationLevelSnap());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void insertaLogApi(int idServicio, string metodo,int codigoError, string peticion, string error)
        {
            try
            {
                string[] Parametros = new string[] { "empresa", "usuario", "idServicio", "metodo", "codigoError", "peticion", "error", "fecha" };
                string[] ValorParametros = new string[] { Conexion.Empresa, Conexion.Usuario, idServicio.ToString(), metodo, 
                    codigoError.ToString(), peticion, error, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") };
                BD_DLL.ClsBD.Inserta_con_parametros("bitacora.inserta_log_api", Conexion.CadenaConexion, Parametros, ValorParametros, Conexion.isolationLevelSnap());
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }

        public void GeneraRegistroLog(string message)
        {
            // Crear el nombre del archivo basado en la fecha y hora actuales
            string fileName = $"log_{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}.txt";
            string filePath = Path.Combine(Conexion.RutaLog, fileName);

            // Escribir el mensaje en el archivo
            File.WriteAllText(filePath, message);
        }
    }
}
