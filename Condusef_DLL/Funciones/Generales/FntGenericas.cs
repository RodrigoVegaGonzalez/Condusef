using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condusef_DLL.Funciones.Generales
{
    public class FntGenericas
    {
        public static bool IsDate(object Expression)
        {
            if (Expression != null)
            {
                if (Expression is DateTime)
                {
                    return true;
                }
                if (Expression is string)
                {
                    DateTime time1;
                    return DateTime.TryParse((string)Expression, out time1);
                }
            }
            return false;
        }
        public static int Weekday(DateTime dt, DayOfWeek startOfWeek)
        {
            return (dt.DayOfWeek - startOfWeek + 7) % 7;
        }

        public static String ValidaNullString(String valor, String valorDefault)
        {
            if (String.IsNullOrEmpty(valor))
                return valorDefault;
            else
                return valor;
        }

        public static decimal ValidaNulldecimal(String value, decimal defaultValue)
        {
            decimal temp;
            if (decimal.TryParse(value, out temp))
                return temp;
            else
                return defaultValue;
        }

        public static int ValidaNullint(String value, int defaultValue)
        {
            int temp;
            if (int.TryParse(value, out temp))
                return temp;
            else
                return defaultValue;
        }

        public static DateTime ValidaNullDateTime(String value, DateTime defaultValue)
        {
            DateTime temp;
            if (DateTime.TryParse(value, out temp))
                return temp;
            else
                return defaultValue;
        }

        public static long ValidaNulllong(String value, long defaultValue)
        {
            long temp;
            if (long.TryParse(value, out temp))
                return temp;
            else
                return defaultValue;
        }

        public static bool ValidaNullBool(String value, bool defaultValue)
        {
            bool temp;
            if (bool.TryParse(value, out temp))
                return temp;
            else
                return defaultValue;
        }

        public static string IsNullOrDefault(object elemento, string salida = "")
        {
            return elemento != null
                ? (string.IsNullOrEmpty(elemento.ToString()) ? salida : elemento.ToString().Trim())
                : salida;
        }

        public static string IsDbNullOrDefault(object elemento, string salida = "")
        {
            return elemento != null
                ? (elemento == DBNull.Value ? salida : elemento.ToString().Trim())
                : salida;
        }

        public static decimal IsDbNullOrDefault(object elemento, decimal salida = 0)
        {
            return elemento != null
                ? (elemento == DBNull.Value ? salida
                    : (decimal.TryParse(elemento.ToString(), out decimal salida_dec)
                    ? salida_dec : salida))
                : salida;
        }

        public static int IsDbNullOrDefault(object elemento, int salida = 0)
        {
            return elemento != null
                ? (elemento == DBNull.Value ? salida
                    : (int.TryParse(elemento.ToString(), out int salida_int)
                    ? salida_int : salida))
                : salida;
        }

        public static bool IsDbNullOrDefault(object elemento, bool salida = false)
        {
            return elemento != null
                ? (elemento == DBNull.Value ? salida
                    : (bool.TryParse(elemento.ToString(), out bool salida_bool)
                    ? salida_bool : salida))
                : salida;
        }

        public static DateTime IsDbNullOrDefault(object elemento, DateTime salida)
        {
            return elemento != null
                ? (elemento == DBNull.Value ? salida
                    : (DateTime.TryParse(elemento.ToString(), out DateTime salida_date)
                    ? salida_date : salida))
                : salida;
        }

        public static string IsDbNullOrDefault(object elemento, string salida = "", string formato = "")
        {
            return elemento != null
                ? (elemento == DBNull.Value ? salida
                    : (DateTime.TryParse(elemento.ToString(), out DateTime salida_date)
                    ? salida_date.ToString(formato) : salida))
                : salida;
        }

        public static string IsDbNullOrDefault(object elemento, decimal salida = 0, string formato = "")
        {
            return elemento != null
                ? (elemento == DBNull.Value ? salida.ToString(formato)
                    : (decimal.TryParse(elemento.ToString(), out decimal salida_dec)
                    ? salida_dec.ToString(formato) : salida.ToString(formato)))
                : salida.ToString(formato);
        }

        public static string IsDbNullOrDefault(object elemento, string salidastring, int salida = 0)
        {
            return elemento != null
                ? (elemento == DBNull.Value ? salidastring
                    : (int.TryParse(elemento.ToString(), out int salida_int)
                    ? salida_int.ToString() : salidastring))
                : salidastring;
        }

        public static string IsDbNullOrDefault(object elemento, string salidastring, decimal salida = 0)
        {
            return elemento != null
                ? (elemento == DBNull.Value ? salidastring
                    : (decimal.TryParse(elemento.ToString(), out decimal salida_dec)
                    ? salida_dec.ToString() : salidastring))
                : salidastring;
        }

        public static string IsDbNullOrDefault(object elemento, bool vacio, decimal salida = 0, string formato = "")
        {
            return elemento != null
                ? (elemento == DBNull.Value ? (vacio ? string.Empty : salida.ToString(formato))
                    : (decimal.TryParse(elemento.ToString(), out decimal salida_dec)
                    ? salida_dec.ToString(formato) : (vacio ? string.Empty : salida.ToString(formato))))
                : (vacio ? string.Empty : salida.ToString(formato));
        }

        public static string IsDbNullOrDefault(object elemento, bool vacio, IFormatProvider proveedor, decimal salida = 0, string formato = "")
        {
            return elemento != null
                ? (elemento == DBNull.Value ? (vacio ? string.Empty : salida.ToString(formato, proveedor))
                    : (decimal.TryParse(elemento.ToString(), out decimal salida_dec)
                    ? salida_dec.ToString(formato, proveedor) : (vacio ? string.Empty : salida.ToString(formato, proveedor))))
                : (vacio ? string.Empty : salida.ToString(formato, proveedor));
        }

        public static string IsDbNullOrDefault(object elemento, bool vacio, NumberStyles numberStyles, IFormatProvider proveedor, decimal salida = 0, string formato = "")
        {
            return elemento != null
                ? (elemento == DBNull.Value ? (vacio ? string.Empty : salida.ToString(formato))
                    : (decimal.TryParse(elemento.ToString(), numberStyles, proveedor, out decimal salida_dec)
                    ? salida_dec.ToString(formato) : (vacio ? string.Empty : salida.ToString(formato))))
                : (vacio ? string.Empty : salida.ToString(formato));
        }

        public static int ConvertDayOfWeekInt(DateTime date)
        {
            return ((int)date.DayOfWeek == 0) ? 7 : (int)date.DayOfWeek;
        }

        public static String RemoveEnd(String str, int len)
        {
            if (str.Length < len)
            {
                return string.Empty;
            }

            return str.Remove(str.Length - len);
        }

        public static string ConvertirFecha(string fechaEnFormatoYMD)
        {
            if (string.IsNullOrEmpty(fechaEnFormatoYMD)) return "";

            try
            {
                // Convierte la cadena de fecha al formato "yyyy-MM-dd" en un objeto DateTime
                DateTime fecha = DateTime.ParseExact(fechaEnFormatoYMD, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                // Formatea la fecha en el formato "dd/MM/yyyy" y la devuelve como una cadena
                return fecha.ToString("dd/MM/yyyy");
            }
            catch (FormatException)
            {
                // En caso de que la cadena no sea una fecha válida en el formato esperado
                return "";
            }
        }


        public static string GenerarContraseña(int longitud)
        {
            string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()-_";

            Random random = new Random();
            StringBuilder contraseña = new StringBuilder();

            for (int i = 0; i < longitud; i++)
            {
                contraseña.Append(caracteres[random.Next(caracteres.Length)]);
            }

            return contraseña.ToString();
        }

    }
}
