using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Xml;
using log4net;
using log4net.Appender;
using log4net.Config;

namespace Cno.Roca.Core.Utils
{


    /// <summary>
    /// Logea mensajes (eventos, errores, etc.) en un archivo de texto 
    /// </summary>
    public class Logger : ILogger
    {
        private static List<string> _fileNames = new List<string>();
        private string _fileName;
        private ILog _logger;

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        static Logger()
        {
            XmlConfigurator.Configure();
        }

        private void Logger_ConfigurationChanged(object sender, EventArgs e)
        {
            ConfigurarNombreArchivoLog(_fileName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename">Nombre del archivo (debe incluir path)</param>
        public Logger(string filename)
        {
            _fileName = filename;

            //Si despues alguna otra clase llama a XmlConfigurator.Configure() vuelvo a reconfigurar el nombre del archivo log
            LogManager.GetRepository().ConfigurationChanged += Logger_ConfigurationChanged;

            lock (_fileNames)
            {
                if (!_fileNames.Contains(filename))
                    _fileNames.Add(filename);
            }
          
            _logger = LogManager.GetLogger(GetLoggerName(filename));
            ConfigurarNombreArchivoLog(filename);
        }

        private string GetLoggerName(string fileName)
        {
            lock (_fileNames)
            {
                return "Logger" + (_fileNames.IndexOf(fileName) + 1);
            }
        }

        private string GetAppenderName(string fileName)
        {
            lock (_fileNames)
            {
                return "RollingLogFileAppender" + (_fileNames.IndexOf(fileName) + 1);
            }
        }


        /// <summary>
        /// Configura el archivo del log del gestor que instancia el logger.        
        /// </summary>
        /// <param name="filename">El nombre del archivo de log dependiente de cada gestor.</param>
        private void ConfigurarNombreArchivoLog(string filename)
        {
            log4net.Repository.ILoggerRepository RootRep;
            RootRep = LogManager.GetRepository();
            string appenderName = GetAppenderName(filename);

            foreach (IAppender iApp in RootRep.GetAppenders())
            {
                if (iApp.Name.CompareTo(appenderName) == 0 && iApp is FileAppender)
                {
                    FileAppender fApp = (FileAppender) iApp;
                    fApp.File = filename;
                    fApp.ActivateOptions();

                    return;
                }
            }

            throw new ConfigurationErrorsException("No esta definido en la configuracion log4net el logger: " +
                                                   GetLoggerName(filename));
        }

        /// <summary>
        /// Graba un mensaje en el Log en modo Info.
        /// </summary>
        /// <param name="message"></param>
        [Obsolete]
        public void Log(string message)
        {
            lock (this)
            {
                try
                {
                    _logger.Info(message);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Trace.WriteLine("Excepción grabando en Log " + _fileName + ": " + ex.Message);
                    System.Diagnostics.Trace.WriteLine("Mensaje: " + message);
                }
            }
        }

        /// <summary>
        /// Loggea excepciones.
        /// Message en modo Error, Stack Trace en modo Debug.
        /// </summary>
        /// <param name="exception"></param>
        public void Log(Exception exception)
        {
            try
            {
                if (_logger.IsDebugEnabled)
                    _logger.Error(exception.GetType().ToString() + " " + exception.Message, exception);
                else
                    _logger.Error(exception.GetType().ToString() + " " + exception.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("Excepción grabando en Log " + _fileName + ": " + ex.Message);
                System.Diagnostics.Trace.WriteLine("Mensaje: " + ex);
            }
        }
        
        /// <summary>
        /// Loggear un mensaje en modo Debug.
        /// </summary>
        /// <param name="message"></param>
        public void LogDebug(string message)
        {
            try
            {
                _logger.Debug(message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("Excepción grabando en Log " + _fileName + ": " + ex.Message);
                System.Diagnostics.Trace.WriteLine("Mensaje: " + message);
            }
        }

        /// <summary>
        /// Loggear un mensaje en modo Info.
        /// </summary>
        /// <param name="message"></param>
        public void LogInfo(string message)
        {
            try
            {
                _logger.Info(message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("Excepción grabando en Log " + _fileName + ": " + ex.Message);
                System.Diagnostics.Trace.WriteLine("Mensaje: " + message);
            }
        }

        /// <summary>
        /// Loggear un mensaje en modo Error.
        /// </summary>
        /// <param name="message"></param>
        public void LogError(string message)
        {
            try
            {
                _logger.Error(message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("Excepción grabando en Log " + _fileName + ": " + ex.Message);
                System.Diagnostics.Trace.WriteLine("Mensaje: " + message);
            }
        }
    }
}
