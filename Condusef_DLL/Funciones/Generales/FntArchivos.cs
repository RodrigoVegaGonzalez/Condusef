using Condusef_DLL.Clases.General;
using Condusef_DLL.Clases.Generales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Funciones.Generales
{
    public class FntArchivos
    {
        public static async Task<ClsResultadoAccion> GuardarArchivo(string filePath, Stream file)
        {
            ClsResultadoAccion resultado = new();
            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    await file.CopyToAsync(fileStream);
                }
                resultado.Estatus = true;
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Generales.FntArchivos";
                log.Log.metodo = "Db_InsertaQueja";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
            }
            return resultado;
        }

        public static async Task<ClsResultadoAccion> BorrarArchivo(string filePath)
        {
            ClsResultadoAccion resultado = new();
            try
            {
                if (File.Exists(filePath))
                {
                    // Eliminar el archivo
                    File.Delete(filePath);

                    // Actualizar el resultado u realizar cualquier otra lógica necesaria
                    resultado.Estatus = true;
                }
                resultado.Estatus = true;
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Generales.FntArchivos";
                log.Log.metodo = "BorrarArchivo";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
            }
            return resultado;
        }

        public static async Task<ClsResultadoAccion> ActualizarArchivo(string fileToDeleted, Stream file, string newFile)
        {
            ClsResultadoAccion resultado = new();
            try
            {
                resultado = await BorrarArchivo(fileToDeleted);
                if (resultado.Estatus)
                {
                    resultado = await GuardarArchivo(newFile, file);
                }
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Generales.FntArchivos";
                log.Log.metodo = "ActualizarArchivo";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
            }
            return resultado;
        }

        public static async Task<ClsResultadoAccion> LeerArchivoMime(string filePath)
        {
            ClsResultadoAccion resultado = new();
            try
            {
                FileInfo Fi = new FileInfo(filePath);
                if (Fi.Exists)
                {
                    ClsArchivoMime mime = new ClsArchivoMime();
                    mime.Archivo = File.ReadAllBytes(filePath);
                    mime.NombreArchivo = Fi.Name;
                    mime.ArchivoB64 = Convert.ToBase64String(mime.Archivo);
                    //mime.MimeArchivo = MimeMapping.GetMimeMapping(mime.NombreArchivo);
                    mime.Extension = Fi.Extension.ToLower();
                    resultado.Estatus = true;
                }
                else
                {
                    resultado.Estatus = false;
                    resultado.Detalle = "El documento no existe";
                }
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Generales.FntArchivos";
                log.Log.metodo = "LeerArchivoMime";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
            }
            return resultado;
        }

        public static async Task<ClsStreamFile> LeerArchivoStream(string filePath)
        {
            ClsResultadoAccion resultado = new();
            ClsStreamFile streamFile = new();
            try
            {
                byte[] stream;

                using (FileStream fileStream = File.OpenRead(filePath))
                {
                    stream = new byte[fileStream.Length];
                    fileStream.Read(stream, 0, (int)fileStream.Length);
                }

                streamFile.Path = filePath;
                streamFile.FileName = Path.GetFileName(filePath);
                streamFile.File = new MemoryStream(stream);

                resultado.Estatus = true;
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Generales.FntArchivos";
                log.Log.metodo = "LeerArchivoStream";
                log.Log.error = ex.Message;
                log.insertaLog();
                resultado.Estatus = false;
            }
            return streamFile;
        }
    }
}
