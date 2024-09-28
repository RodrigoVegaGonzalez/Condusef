using Condusef_DLL.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Funciones.Generales
{
    public class FntEncriptar
    {
        static byte[] DeriveKeyFromPassword(string password)
        {
            // PBKDF2 parameters
            int iterations = 10000; // Número de iteraciones recomendado
            int keySize = 256 / 8;  // Tamaño de clave en bytes (256 bits)

            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, new byte[8], iterations))
            {
                return pbkdf2.GetBytes(keySize);
            }
        }
        static string EncryptString(string plainText, byte[] key, byte[] iv)
        {
            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;
                aesAlg.Padding = PaddingMode.PKCS7;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }

                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        static string DecryptString(string cipherText, byte[] key, byte[] iv)
        {
            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        static byte[] HexStringToBytes(string hex)
        {
            // Asegurarse de que la cadena tenga una longitud par
            if (hex.Length % 2 != 0)
            {
                throw new ArgumentException("La cadena debe tener una longitud par.", nameof(hex));
            }

            // Crear un array de bytes de la mitad de la longitud de la cadena
            byte[] bytes = new byte[hex.Length / 2];

            // Iterar sobre pares de caracteres en la cadena y convertirlos a bytes
            for (int i = 0; i < hex.Length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }

            return bytes;
        }

        public static string Encriptar(string texto)
        {
            try
            {
                // Derivar la clave utilizando PBKDF2
                byte[] key = DeriveKeyFromPassword(VariablesGlobales.Llave);
                byte[] iv = HexStringToBytes(VariablesGlobales.IV);

                // Cifrar la cadena original
                string encryptedText = EncryptString(texto, key, iv);
                // Concatenar IV con texto cifrado antes de devolverlo
                return encryptedText;
            }
            catch(Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Generales.FntEncriptar";
                log.Log.metodo = "Encriptar";
                log.Log.error = ex.Message;
                log.insertaLog();
                return "";
            }
            
        }
        public static string Desencriptar(string encryptedText)
        {
            try
            {
                // Derivar la clave utilizando PBKDF2
                byte[] key = DeriveKeyFromPassword(VariablesGlobales.Llave);
                // Obtener IV de la cadena cifrada
                byte[] iv = HexStringToBytes(VariablesGlobales.IV);

                // Descifrar la cadena original
                string decryptedText = DecryptString(encryptedText, key, iv);
                return decryptedText;
            }
            catch (Exception ex)
            {
                FntLog log = new FntLog();
                log.Log.nameSpace = "Condusef_DLL.Funciones.Generales.FntEncriptar";
                log.Log.metodo = "Desencriptar";
                log.Log.error = ex.Message;
                log.insertaLog();
                return "";
            }
            
        }
    }
}
