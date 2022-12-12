using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Models
{
    public static class Autenticacion
    {
        private static String currentUser = "";
        public static bool estaAutenticado(HttpContext context, string currentUser)
        {
            bool resultado = false;
            if (context.Session.GetString("user") == currentUser) resultado = true;
            return resultado;
        }
        public static string getSessionId()
        {
            return currentUser;
        }
        public static void login(HttpContext context, string currentUser)
        {
            context.Session.SetString("user", currentUser);
            Autenticacion.currentUser = currentUser;
        }
        public static void logout(HttpContext context)
        {
            context.Session.SetString("user", "");
            Autenticacion.currentUser = "";
        }
        /*public static bool estaAutenticado2(HttpContext context)
        {
            bool resultado = false;
            if (context.Session.GetString("user") != "") resultado = true;
            return resultado;
        }*/
        /*public static byte[] stringToByte(String texto)
        {
            byte[] resultado = Encoding.ASCII.GetBytes(texto);
            return resultado;
        }*/
        /*public static String bytesToString(byte[] bytes)
        {
            return System.Text.Encoding.Default.GetString(bytes);
        }*/

        public static bool estaAutenticado()
        {
            if (currentUser != "")
                return true;
            else
                return false;
        }
    }
}
