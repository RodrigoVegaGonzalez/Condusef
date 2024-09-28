using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Clases.Generales
{
    public class Hash
    {
        public static string Encripta(string cadena, string llave)
        {
            byte[] iv = new byte[16];
            byte[] passEncriptado = EncriptadorAES(cadena, llave, iv);
            return Convert.ToBase64String(passEncriptado);
        }

        public static string Desencripta(string cadena, string llave)
        {
            byte[] iv = new byte[16];
            return DescriptadorAES(cadena, llave, iv);
        }

        private static byte[] EncriptadorAES(string pass, string llave, byte[] iv)
        {
            byte[] passEnciptada;
            using (Aes valoresAres = Aes.Create())
            {
                valoresAres.Key = Encoding.UTF8.GetBytes(llave);
                valoresAres.IV = iv;
                ICryptoTransform tr = valoresAres.CreateEncryptor(valoresAres.Key, valoresAres.IV);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cr = new CryptoStream(ms, tr, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cr))
                        {
                            sw.Write(pass);
                        }
                        passEnciptada = ms.ToArray();
                    }
                }
            }
            return passEnciptada;
        }

        private static string DescriptadorAES(string pass, string llave, byte[] iv)
        {
            string valorDesencriptado = string.Empty;
            byte[] buffer = Convert.FromBase64String(pass);
            using (Aes valoresAres = Aes.Create())
            {
                valoresAres.Key = Encoding.UTF8.GetBytes(llave);
                valoresAres.IV = iv;
                ICryptoTransform tr = valoresAres.CreateDecryptor(valoresAres.Key, valoresAres.IV);
                using (MemoryStream ms = new MemoryStream(buffer))
                {
                    using (CryptoStream cs = new CryptoStream(ms, tr, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            valorDesencriptado = sr.ReadToEnd();
                        }
                    }
                }
            }
            return valorDesencriptado;
        }
    }
}
