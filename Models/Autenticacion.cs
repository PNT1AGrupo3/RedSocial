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
        public static string getSessionId(HttpContext context)
        {
            return context.Session.GetString("user");
        }
        public static void login(HttpContext context, string currentUser)
        {
            context.Session.SetString("user", currentUser);
        }
        public static void logout(HttpContext context)
        {
            context.Session.SetString("user", "");
        }
        public static bool estaAutenticado(HttpContext context)
        {
            bool resultado = false;
            String user;
            if (context != null)
            {
                user = context.Session.GetString("user");
                if (user != null && !user.Equals(""))
                    resultado = true;
            }
            return resultado;
        }
    }
}
