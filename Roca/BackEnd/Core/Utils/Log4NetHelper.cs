using log4net;
using log4net.Appender;

namespace Cno.Roca.Core.Utils
{
    public class Log4NetHelper
    {
        /// <summary>
        /// Cambia el path default de logueo
        /// </summary>
        /// <param name="nuevoLogPath">Nuevo directorio completo de logueo</param>
        public static void UpdateFileAppenderPaths(string nuevoLogPath)
        {
            var h = (log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository();
            foreach (var a in h.Root.Appenders)
            {
                if (!(a is FileAppender)) continue;
                var fa = (FileAppender)a;
                fa.File = nuevoLogPath;
                fa.ActivateOptions();
            }
        }
    }
}
