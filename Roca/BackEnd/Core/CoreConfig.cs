using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Cno.Roca.Core
{
    public static class CoreConfig
    {
        /// <summary>
        /// Indica si se deben enviar al trace output los comandos de base de datos
        /// </summary>
        public static bool TraceDBCommands
        {
            get
            {
                string s= ConfigurationManager.AppSettings["TraceDBCommands"];
                if (s != null)
                    return Convert.ToBoolean(s);
                else
                    return false;
            }
        }

        /// <summary>
        /// Indica si se deben loguear en un archivo los comandos de base de datos
        /// </summary>
        public static bool LogDBCommands
        {
            get
            {
                string s = ConfigurationManager.AppSettings["LogDBCommands"];
                if (s != null)
                    return Convert.ToBoolean(s);
                else
                    return false;
            }
        }

        /// <summary>
        /// nombre del archivo en donde se loguean los comandos de base de datos
        /// </summary>
        public static string LogDBFileName
        {
            get { return ConfigurationManager.AppSettings["LogDBFileName"]; }
        }
    }
}
