using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cno.Roca.Core.Utils
{
    public interface ILogger
    {
        /// <summary>
        /// Loggea excepciones.
        /// Message en modo Error, Stack Trace en modo Debug.
        /// </summary>
        /// <param name="exception"></param>
        void Log(Exception exception);

        /// <summary>
        /// Loggear un mensaje en modo Debug.
        /// </summary>
        /// <param name="message"></param>
        void LogDebug(string message);

        /// <summary>
        /// Loggear un mensaje en modo Info.
        /// </summary>
        /// <param name="message"></param>
        void LogInfo(string message);

        /// <summary>
        /// Loggear un mensaje en modo Error.
        /// </summary>
        /// <param name="message"></param>
        void LogError(string message);
    }
}
