using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using Cno.Roca.Core.Utils;

namespace Cno.Roca.Core.Entity
{
    /// <summary>
    /// Loguea operaciones de base de datos
    /// </summary>
    public class DBLogger
    {
        private string _fileName;
        private bool _log;
        private bool _trace;
    
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        public DBLogger(string fileName, bool log, bool trace)
        {
            _fileName = fileName;
            _log = log;
            _trace = trace;
        }

        public void LogCommand(DbCommand command)
        {
            if (_log || _trace)
            {
                Semaphore semaphore = SemaphoreFactory.CreateSemaphore(1, 1, Path.GetFileNameWithoutExtension(_fileName));
                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(GetDate() + " - Command: " + command.CommandText + " Application: " +
                                  AppDomain.CurrentDomain.FriendlyName);
                    string spaces = new string(' ', GetDate().Length + 5);
                    foreach (DbParameter parameter in command.Parameters)
                    {
                        sb.AppendLine(spaces + parameter.ParameterName + ": " + parameter.Value);
                    }

                    if (_log)
                    {
                        semaphore.WaitOne();
                        using (StreamWriter sw = new StreamWriter(_fileName, true))
                        {
                            sw.Write(sb.ToString());
                        }
                    }

                    if (_trace)
                        System.Diagnostics.Trace.Write(sb.ToString());
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Trace.WriteLine(ex.ToString());
                }
                finally
                {
                    semaphore.Release();
                }
            }
        }

        public void LogEndCommand(DbCommand command)
        {
            if (_log || _trace)
            {
                Semaphore semaphore = new Semaphore(1, 1, Path.GetFileNameWithoutExtension(_fileName));
                try
                {
                    string s = GetDate() + " - Command Finished: " + command.CommandText + " Application: " +
                                AppDomain.CurrentDomain.FriendlyName;

                    if (_log)
                    {
                        semaphore.WaitOne();
                        using (StreamWriter sw = new StreamWriter(_fileName, true))
                        {
                            sw.WriteLine(s);
                        }
                    }

                    if (_trace)
                        System.Diagnostics.Trace.Write(s);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Trace.WriteLine(ex.ToString());
                }
                finally
                {
                    semaphore.Release();
                }
            }
        }

        private string GetDate()
        {
            return DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff");
        }
    }
}
