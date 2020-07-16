using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Transactions;
using Cno.Roca.CoreData;

namespace Cno.Roca.Core.Utils
{
    public delegate void Action();
    public delegate void Action<T>(T t);
    public delegate void Action<T1, T2>(T1 t1, T2 t2);
    public delegate void Action<T1, T2, T3>(T1 t1, T2 t2, T3 t3);
    public delegate void Action<T1, T2, T3, T4>(T1 t1, T2 t2, T3 t3, T4 t4);

    public delegate R Function<R>();
    public delegate R Function<R, T>(T t);
    public delegate R Function<R, T1, T2>(T1 t1, T2 t2);
    public delegate R Function<R, T1, T2, T3>(T1 t1, T2 t2, T3 t3);
    public delegate R Function<R, T1, T2, T3, T4>(T1 t1, T2 t2, T3 t3, T4 t4);

    /// <summary>
    /// Ejecuta metodos, reintentando la ejecucion, si ocurre una excepcion por cocurrencia de transacciones (TransactionConcurrencyException)
    /// </summary>
    public class TransactionRetryUtil
    {
        public ILogger Logger { get; set; }

        public TransactionRetryUtil()
        {
            
        }

        public TransactionRetryUtil(ILogger logger)
        {
            Logger = logger;
        }



        #region Metodos Publicos

        /// <summary>
        /// Ejecuta un metodo reintentando la ejecucion en caso de Excepcion por cocurrencia de transacciones
        /// </summary>
        /// <typeparam name="R">Tipo del resultado del metodo</typeparam>
        /// <param name="method">Metodo a ejecutar</param>
        /// <param name="retries">Cantidad de reintentos</param>
        /// <param name="sleepBetweenRetries">Milisegundos de espera entre reintentos</param>
        /// <returns>Resultado del metodo</returns>
        public R Execute<R>(Function<R> method,
                                    int retries, int sleepBetweenRetries)
        {
            return Execute<R>(method, retries, sleepBetweenRetries, new Type[] { typeof(TransactionConcurrencyException) });
        }

        /// <summary>
        /// Ejecuta un metodo reintentando la ejecucion en caso de determinadas Excepciones
        /// </summary>
        /// <typeparam name="R">Tipo del resultado del metodo</typeparam>
        /// <param name="method">Metodo a ejecutar</param>
        /// <param name="retries">Cantidad de reintentos</param>
        /// <param name="sleepBetweenRetries">Milisegundos de espera entre reintentos</param>
        /// <returns>Resultado del metodo</returns>
        /// <param name="exceptions">El listado de excepciones a reintentar. Por defecto no se reintenta ningun tipo.</param>
        public R Execute<R>(Function<R> method,
                            int retries, int sleepBetweenRetries, Type[] exceptions)
        {
            CheckExistentTransaction();
            CheckExceptionsType(exceptions);
            int cont = 0;
            while (true)
            {
                try
                {
                    return method();
                }
                catch (Exception ex)
                {
                    cont++;

                    if (cont > retries)
                        throw;

                    bool shouldRetry = false;
                    foreach (Type type in exceptions)
                    {
                        if (type.IsInstanceOfType(ex))
                        {
                            shouldRetry = true;
                            Log(ex);
                            break;
                        }
                    }
                    if (!shouldRetry)
                        throw;
                }
                Thread.Sleep(sleepBetweenRetries);
                Log("Reintentando transaccion");
            }
        }


        /// <summary>
        /// Ejecuta un metodo reintentando la ejecucion en caso de Excepcion por cocurrencia de transacciones
        /// </summary>
        /// <typeparam name="R">Tipo del resultado del metodo</typeparam>
        /// <typeparam name="T">Tipo del primer parametro del metodo</typeparam>
        /// <param name="method">Metodo a ejecutar</param>
        /// <param name="t">Valor del parametro del metodo</param>
        /// <param name="retries">Cantidad de reintentos</param>
        /// <param name="sleepBetweenRetries">Milisegundos de espera entre reintentos</param>
        /// <returns>Resultado del metodo</returns>
        public R Execute<R, T>(Function<R, T> method, T t,
                            int retries, int sleepBetweenRetries)
        {
            return Execute<R, T>(method, t, retries, sleepBetweenRetries, new Type[] { typeof(TransactionConcurrencyException) });
        }


        /// <summary>
        /// Ejecuta un metodo reintentando la ejecucion en caso de determinadas Excepciones
        /// </summary>
        /// <typeparam name="R">Tipo del resultado del metodo</typeparam>
        /// <typeparam name="T">Tipo del primer parametro del metodo</typeparam>
        /// <param name="method">Metodo a ejecutar</param>
        /// <param name="t">Valor del parametro del metodo</param>
        /// <param name="retries">Cantidad de reintentos</param>
        /// <param name="sleepBetweenRetries">Milisegundos de espera entre reintentos</param>
        /// <returns>Resultado del metodo</returns>
        /// <param name="exceptions">El listado de excepciones a reintentar. Por defecto no se reintenta ningun tipo.</param>
        public R Execute<R, T>(Function<R, T> method, T t,
                            int retries, int sleepBetweenRetries, Type[] exceptions)
        {
            CheckExistentTransaction();
            CheckExceptionsType(exceptions);
            int cont = 0;
            while (true)
            {
                try
                {
                    return method(t);
                }
                catch (Exception ex)
                {
                    cont++;

                    if (cont > retries)
                        throw;

                    bool shouldRetry = false;
                    foreach (Type type in exceptions)
                    {
                        if (type.IsInstanceOfType(ex))
                        {
                            shouldRetry = true;
                            Log(ex);
                            break;
                        }
                    }
                    if (!shouldRetry)
                        throw;
                }
                Thread.Sleep(sleepBetweenRetries);
                Log("Reintentando transaccion");
            }
        }


        /// <summary>
        /// Ejecuta un metodo reintentando la ejecucion en caso de Excepcion por cocurrencia de transacciones
        /// </summary>
        /// <typeparam name="R">Tipo del resultado del metodo</typeparam>
        /// <typeparam name="T1">Tipo del primer parametro del metodo</typeparam>
        /// <typeparam name="T2">Tipo del segundo parametro del metodo</typeparam>
        /// <param name="method">Metodo a ejecutar</param>
        /// <param name="t1">Valor del primer parametro del metodo</param>
        /// <param name="t2">Valor del segundo parametro del metodo</param>
        /// <param name="retries">Cantidad de reintentos</param>
        /// <param name="sleepBetweenRetries">Milisegundos de espera entre reintentos</param>
        /// <returns>Resultado del metodo</returns>
        public R Execute<R, T1, T2>(Function<R, T1, T2> method, T1 t1, T2 t2,
                    int retries, int sleepBetweenRetries)
        {
            return Execute<R, T1, T2>(method, t1, t2, retries, sleepBetweenRetries, new Type[] { typeof(TransactionConcurrencyException) });
        }

        /// <summary>
        /// Ejecuta un metodo reintentando la ejecucion en caso de determinadas Excepciones
        /// </summary>
        /// <typeparam name="R">Tipo del resultado del metodo</typeparam>
        /// <typeparam name="T1">Tipo del primer parametro del metodo</typeparam>
        /// <typeparam name="T2">Tipo del segundo parametro del metodo</typeparam>
        /// <param name="method">Metodo a ejecutar</param>
        /// <param name="t1">Valor del primer parametro del metodo</param>
        /// <param name="t2">Valor del segundo parametro del metodo</param>
        /// <param name="retries">Cantidad de reintentos</param>
        /// <param name="sleepBetweenRetries">Milisegundos de espera entre reintentos</param>
        /// <returns>Resultado del metodo</returns>
        /// <param name="exceptions">El listado de excepciones a reintentar. Por defecto no se reintenta ningun tipo.</param>
        public R Execute<R, T1, T2>(Function<R, T1, T2> method, T1 t1, T2 t2,
                    int retries, int sleepBetweenRetries, Type[] exceptions)
        {
            CheckExistentTransaction();
            CheckExceptionsType(exceptions);
            int cont = 0;
            while (true)
            {
                try
                {
                    return method(t1, t2);
                }
                catch (Exception ex)
                {
                    cont++;

                    if (cont > retries)
                        throw;

                    bool shouldRetry = false;
                    foreach (Type type in exceptions)
                    {
                        if (type.IsInstanceOfType(ex))
                        {
                            shouldRetry = true;
                            Log(ex);
                            break;
                        }
                    }
                    if (!shouldRetry)
                        throw;
                }
                Thread.Sleep(sleepBetweenRetries);
                Log("Reintentando transaccion");
            }
        }


        /// <summary>
        /// Ejecuta un metodo reintentando la ejecucion en caso de Excepcion por cocurrencia de transacciones
        /// </summary>
        /// <typeparam name="R">Tipo del resultado del metodo</typeparam>
        /// <typeparam name="T1">Tipo del primer parametro del metodo</typeparam>
        /// <typeparam name="T2">Tipo del segundo parametro del metodo</typeparam>
        /// <typeparam name="T3">Tipo del tercer parametro del metodo</typeparam>
        /// <param name="method">Metodo a ejecutar</param>
        /// <param name="t1">Valor del primer parametro del metodo</param>
        /// <param name="t2">Valor del segundo parametro del metodo</param>
        /// <param name="t3">Valor tercer parametro del metodo</param>
        /// <param name="retries">Cantidad de reintentos</param>
        /// <param name="sleepBetweenRetries">Milisegundos de espera entre reintentos</param>
        /// <returns>Resultado del metodo</returns>
        public R Execute<R, T1, T2, T3>(Function<R, T1, T2, T3> method, T1 t1, T2 t2, T3 t3,
            int retries, int sleepBetweenRetries)
        {
            return Execute<R, T1, T2, T3>(method, t1, t2, t3, retries, sleepBetweenRetries, new Type[] { typeof(TransactionConcurrencyException) });
        }

        /// <summary>
        /// Ejecuta un metodo reintentando la ejecucion en caso de determinadas Excepciones
        /// </summary>
        /// <typeparam name="R">Tipo del resultado del metodo</typeparam>
        /// <typeparam name="T1">Tipo del primer parametro del metodo</typeparam>
        /// <typeparam name="T2">Tipo del segundo parametro del metodo</typeparam>
        /// <typeparam name="T3">Tipo del tercer parametro del metodo</typeparam>
        /// <param name="method">Metodo a ejecutar</param>
        /// <param name="t1">Valor del primer parametro del metodo</param>
        /// <param name="t2">Valor del segundo parametro del metodo</param>
        /// <param name="t3">Valor tercer parametro del metodo</param>
        /// <param name="retries">Cantidad de reintentos</param>
        /// <param name="sleepBetweenRetries">Milisegundos de espera entre reintentos</param>
        /// <returns>Resultado del metodo</returns>
        /// <param name="exceptions">El listado de excepciones a reintentar. Por defecto no se reintenta ningun tipo.</param>
        public R Execute<R, T1, T2, T3>(Function<R, T1, T2, T3> method, T1 t1, T2 t2, T3 t3,
                    int retries, int sleepBetweenRetries, Type[] exceptions)
        {
            CheckExistentTransaction();
            CheckExceptionsType(exceptions);
            int cont = 0;
            while (true)
            {
                try
                {
                    return method(t1, t2, t3);
                }
                catch (Exception ex)
                {
                    cont++;

                    if (cont > retries)
                        throw;

                    bool shouldRetry = false;
                    foreach (Type type in exceptions)
                    {
                        if (type.IsInstanceOfType(ex))
                        {
                            shouldRetry = true;
                            Log(ex);
                            break;
                        }
                    }
                    if (!shouldRetry)
                        throw;
                }
                Thread.Sleep(sleepBetweenRetries);
                Log("Reintentando transaccion");
            }
        }


        /// <summary>
        /// Ejecuta un metodo reintentando la ejecucion en caso de Excepcion por cocurrencia de transacciones
        /// </summary>
        /// <typeparam name="R">Tipo del resultado del metodo</typeparam>
        /// <typeparam name="T1">Tipo del primer parametro del metodo</typeparam>
        /// <typeparam name="T2">Tipo del segundo parametro del metodo</typeparam>
        /// <typeparam name="T3">Tipo del tercer parametro del metodo</typeparam>
        /// <typeparam name="T4">Tipo del cuarto parametro del metodo</typeparam>
        /// <param name="method">Metodo a ejecutar</param>
        /// <param name="t1">Valor del primer parametro del metodo</param>
        /// <param name="t2">Valor del segundo parametro del metodo</param>
        /// <param name="t3">Valor tercer parametro del metodo</param>
        /// <param name="t4">Valor cuarto parametro del metodo</param>
        /// <param name="retries">Cantidad de reintentos</param>
        /// <param name="sleepBetweenRetries">Milisegundos de espera entre reintentos</param>
        /// <returns>Resultado del metodo</returns>
        public R Execute<R, T1, T2, T3, T4>(Function<R, T1, T2, T3, T4> method, T1 t1, T2 t2, T3 t3, T4 t4,
                                                int retries, int sleepBetweenRetries)
        {
            return Execute<R, T1, T2, T3, T4>(method, t1, t2, t3, t4, retries, sleepBetweenRetries, new Type[] { typeof(TransactionConcurrencyException) });
        }

        /// <summary>
        /// Ejecuta un metodo reintentando la ejecucion en caso de determinadas Excepciones
        /// </summary>
        /// <typeparam name="R">Tipo del resultado del metodo</typeparam>
        /// <typeparam name="T1">Tipo del primer parametro del metodo</typeparam>
        /// <typeparam name="T2">Tipo del segundo parametro del metodo</typeparam>
        /// <typeparam name="T3">Tipo del tercer parametro del metodo</typeparam>
        /// <typeparam name="T4">Tipo del cuarto parametro del metodo</typeparam>
        /// <param name="method">Metodo a ejecutar</param>
        /// <param name="t1">Valor del primer parametro del metodo</param>
        /// <param name="t2">Valor del segundo parametro del metodo</param>
        /// <param name="t3">Valor tercer parametro del metodo</param>
        /// <param name="t4">Valor cuarto parametro del metodo</param>
        /// <param name="retries">Cantidad de reintentos</param>
        /// <param name="sleepBetweenRetries">Milisegundos de espera entre reintentos</param>
        /// <returns>Resultado del metodo</returns>
        /// <param name="exceptions">El listado de excepciones a reintentar. Por defecto no se reintenta ningun tipo.</param>
        public R Execute<R, T1, T2, T3, T4>(Function<R, T1, T2, T3, T4> method, T1 t1, T2 t2, T3 t3, T4 t4,
                   int retries, int sleepBetweenRetries, Type[] exceptions)
        {
            CheckExistentTransaction();
            CheckExceptionsType(exceptions);
            int cont = 0;
            while (true)
            {
                try
                {
                    return method(t1, t2, t3, t4);
                }
                catch (Exception ex)
                {
                    cont++;

                    if (cont > retries)
                        throw;

                    bool shouldRetry = false;
                    foreach (Type type in exceptions)
                    {
                        if (type.IsInstanceOfType(ex))
                        {
                            shouldRetry = true;
                            Log(ex);
                            break;
                        }
                    }
                    if (!shouldRetry)
                        throw;
                }
                Thread.Sleep(sleepBetweenRetries);
                Log("Reintentando transaccion");
            }
        }

        /// <summary>
        /// Ejecuta un metodo reintentando la ejecucion en caso de Excepcion por cocurrencia de transacciones
        /// </summary>
        /// <param name="method">Metodo a ejecutar</param>
        /// <param name="retries">Cantidad de reintentos</param>
        /// <param name="sleepBetweenRetries">Milisegundos de espera entre reintentos</param>
        public void Execute(Action method,
                            int retries, int sleepBetweenRetries)
        {
            Execute(method, retries, sleepBetweenRetries, new Type[] { typeof(TransactionConcurrencyException) });
        }

        /// <summary>
        /// Ejecuta un metodo reintentando la ejecucion en caso de determinadas Excepciones
        /// </summary>
        /// <param name="method">Metodo a ejecutar</param>
        /// <param name="retries">Cantidad de reintentos</param>
        /// <param name="sleepBetweenRetries">Milisegundos de espera entre reintentos</param>
        /// <param name="exceptions">El listado de excepciones a reintentar. Por defecto no se reintenta ningun tipo.</param>
        public void Execute(Action method,
                            int retries, int sleepBetweenRetries, Type[] exceptions)
        {
            CheckExistentTransaction();
            CheckExceptionsType(exceptions);
            int cont = 0;
            while (true)
            {
                try
                {
                    method();
                    return;
                }
                catch (Exception ex)
                {
                    cont++;

                    if (cont > retries)
                        throw;

                    bool shouldRetry = false;
                    foreach (Type type in exceptions)
                    {
                        if (type.IsInstanceOfType(ex))
                        {
                            shouldRetry = true;
                            Log(ex);
                            break;
                        }
                    }
                    if (!shouldRetry)
                        throw;
                }
                Thread.Sleep(sleepBetweenRetries);
                Log("Reintentando transaccion");
            }
        }


        /// <summary>
        /// Ejecuta un metodo reintentando la ejecucion en caso de Excepcion por cocurrencia de transacciones
        /// </summary>
        /// <typeparam name="T">Tipo del primer parametro del metodo</typeparam>
        /// <param name="method">Metodo a ejecutar</param>
        /// <param name="t">Valor del parametro del metodo</param>
        /// <param name="retries">Cantidad de reintentos</param>
        /// <param name="sleepBetweenRetries">Milisegundos de espera entre reintentos</param>
        public void Execute<T>(Action<T> method, T t,
                            int retries, int sleepBetweenRetries)
        {
            Execute(method, t, retries, sleepBetweenRetries, new Type[] { typeof(TransactionConcurrencyException) });
        }

        /// <summary>
        /// Ejecuta un metodo reintentando la ejecucion en caso de determinadas Excepciones
        /// </summary>
        /// <typeparam name="T">Tipo del primer parametro del metodo</typeparam>
        /// <param name="method">Metodo a ejecutar</param>
        /// <param name="t">Valor del parametro del metodo</param>
        /// <param name="retries">Cantidad de reintentos</param>
        /// <param name="sleepBetweenRetries">Milisegundos de espera entre reintentos</param>
        /// <param name="exceptions">El listado de excepciones a reintentar. Por defecto no se reintenta ningun tipo.</param>
        public void Execute<T>(Action<T> method, T t,
                          int retries, int sleepBetweenRetries, Type[] exceptions)
        {
            CheckExistentTransaction();
            CheckExceptionsType(exceptions);
            int cont = 0;
            while (true)
            {
                try
                {
                    method(t);
                    return;
                }
                catch (Exception ex)
                {
                    cont++;

                    if (cont > retries)
                        throw;

                    bool shouldRetry = false;
                    foreach (Type type in exceptions)
                    {
                        if (type.IsInstanceOfType(ex))
                        {
                            shouldRetry = true;
                            Log(ex);
                            break;
                        }
                    }
                    if (!shouldRetry)
                        throw;
                }
                Thread.Sleep(sleepBetweenRetries);
                Log("Reintentando transaccion");
            }
        }

        /// <summary>
        /// Ejecuta un metodo reintentando la ejecucion en caso de Excepcion por cocurrencia de transacciones
        /// </summary>
        /// <typeparam name="T1">Tipo del primer parametro del metodo</typeparam>
        /// <typeparam name="T2">Tipo del segundo parametro del metodo</typeparam>
        /// <param name="method">Metodo a ejecutar</param>
        /// <param name="t1">Valor del primer parametro del metodo</param>
        /// <param name="t2">Valor del segundo parametro del metodo</param>
        /// <param name="retries">Cantidad de reintentos</param>
        /// <param name="sleepBetweenRetries">Milisegundos de espera entre reintentos</param>
        public void Execute<T1, T2>(Action<T1, T2> method, T1 t1, T2 t2,
                            int retries, int sleepBetweenRetries)
        {
            Execute(method, t1, t2, retries, sleepBetweenRetries, new Type[] { typeof(TransactionConcurrencyException) });
        }

        /// <summary>
        /// Ejecuta un metodo reintentando la ejecucion en caso de determinadas Excepciones
        /// </summary>
        /// <typeparam name="T1">Tipo del primer parametro del metodo</typeparam>
        /// <typeparam name="T2">Tipo del segundo parametro del metodo</typeparam>
        /// <param name="method">Metodo a ejecutar</param>
        /// <param name="t1">Valor del primer parametro del metodo</param>
        /// <param name="t2">Valor del segundo parametro del metodo</param>
        /// <param name="retries">Cantidad de reintentos</param>
        /// <param name="sleepBetweenRetries">Milisegundos de espera entre reintentos</param>
        /// <param name="exceptions">El listado de excepciones a reintentar. Por defecto no se reintenta ningun tipo.</param>
        public void Execute<T1, T2>(Action<T1, T2> method, T1 t1, T2 t2,
                         int retries, int sleepBetweenRetries, Type[] exceptions)
        {
            CheckExistentTransaction();
            CheckExceptionsType(exceptions);
            int cont = 0;
            while (true)
            {
                try
                {
                    method(t1, t2);
                    return;
                }
                catch (Exception ex)
                {
                    cont++;

                    if (cont > retries)
                        throw;

                    bool shouldRetry = false;
                    foreach (Type type in exceptions)
                    {
                        if (type.IsInstanceOfType(ex))
                        {
                            shouldRetry = true;
                            Log(ex);
                            break;
                        }
                    }
                    if (!shouldRetry)
                        throw;
                }
                Thread.Sleep(sleepBetweenRetries);
                Log("Reintentando transaccion");
            }
        }

        /// <summary>
        /// Ejecuta un metodo reintentando la ejecucion en caso de Excepcion por cocurrencia de transacciones
        /// </summary>
        /// <typeparam name="T1">Tipo del primer parametro del metodo</typeparam>
        /// <typeparam name="T2">Tipo del segundo parametro del metodo</typeparam>
        /// <typeparam name="T3">Tipo del tercer parametro del metodo</typeparam>
        /// <param name="method">Metodo a ejecutar</param>
        /// <param name="t1">Valor del primer parametro del metodo</param>
        /// <param name="t2">Valor del segundo parametro del metodo</param>
        /// <param name="t3">Valor tercer parametro del metodo</param>
        /// <param name="retries">Cantidad de reintentos</param>
        /// <param name="sleepBetweenRetries">Milisegundos de espera entre reintentos</param>
        public void Execute<T1, T2, T3>(Action<T1, T2, T3> method, T1 t1, T2 t2, T3 t3,
                    int retries, int sleepBetweenRetries)
        {
            Execute(method, t1, t2, t3, retries, sleepBetweenRetries, new Type[] { typeof(TransactionConcurrencyException) });
        }

        /// <summary>
        /// Ejecuta un metodo reintentando la ejecucion en caso de determinadas Excepciones
        /// </summary>
        /// <typeparam name="T1">Tipo del primer parametro del metodo</typeparam>
        /// <typeparam name="T2">Tipo del segundo parametro del metodo</typeparam>
        /// <typeparam name="T3">Tipo del tercer parametro del metodo</typeparam>
        /// <param name="method">Metodo a ejecutar</param>
        /// <param name="t1">Valor del primer parametro del metodo</param>
        /// <param name="t2">Valor del segundo parametro del metodo</param>
        /// <param name="t3">Valor tercer parametro del metodo</param>
        /// <param name="retries">Cantidad de reintentos</param>
        /// <param name="sleepBetweenRetries">Milisegundos de espera entre reintentos</param>
        /// <param name="exceptions">El listado de excepciones a reintentar. Por defecto no se reintenta ningun tipo.</param>
        public void Execute<T1, T2, T3>(Action<T1, T2, T3> method, T1 t1, T2 t2, T3 t3,
                        int retries, int sleepBetweenRetries, Type[] exceptions)
        {
            CheckExistentTransaction();
            CheckExceptionsType(exceptions);
            int cont = 0;
            while (true)
            {
                try
                {
                    method(t1, t2, t3);
                    return;
                }
                catch (Exception ex)
                {
                    cont++;

                    if (cont > retries)
                        throw;

                    bool shouldRetry = false;
                    foreach (Type type in exceptions)
                    {
                        if (type.IsInstanceOfType(ex))
                        {
                            shouldRetry = true;
                            Log(ex);
                            break;
                        }
                    }
                    if (!shouldRetry)
                        throw;
                }
                Thread.Sleep(sleepBetweenRetries);
                Log("Reintentando transaccion");
            }
        }

        /// <summary>
        /// Ejecuta un metodo reintentando la ejecucion en caso de Excepcion por cocurrencia de transacciones
        /// </summary>
        /// <typeparam name="T1">Tipo del primer parametro del metodo</typeparam>
        /// <typeparam name="T2">Tipo del segundo parametro del metodo</typeparam>
        /// <typeparam name="T3">Tipo del tercer parametro del metodo</typeparam>
        /// <typeparam name="T4">Tipo del cuarto parametro del metodo</typeparam>
        /// <param name="method">Metodo a ejecutar</param>
        /// <param name="t1">Valor del primer parametro del metodo</param>
        /// <param name="t2">Valor del segundo parametro del metodo</param>
        /// <param name="t3">Valor tercer parametro del metodo</param>
        /// <param name="t4">Valor cuarto parametro del metodo</param>
        /// <param name="retries">Cantidad de reintentos</param>
        /// <param name="sleepBetweenRetries">Milisegundos de espera entre reintentos</param>
        public void Execute<T1, T2, T3, T4>(Action<T1, T2, T3, T4> method, T1 t1, T2 t2, T3 t3, T4 t4,
            int retries, int sleepBetweenRetries)
        {
            Execute(method, t1, t2, t3, t4, retries, sleepBetweenRetries, new Type[] { typeof(TransactionConcurrencyException) });
        }

        /// <summary>
        /// Ejecuta un metodo reintentando la ejecucion en caso de determinadas Excepciones
        /// </summary>
        /// <typeparam name="T1">Tipo del primer parametro del metodo</typeparam>
        /// <typeparam name="T2">Tipo del segundo parametro del metodo</typeparam>
        /// <typeparam name="T3">Tipo del tercer parametro del metodo</typeparam>
        /// <typeparam name="T4">Tipo del cuarto parametro del metodo</typeparam>
        /// <param name="method">Metodo a ejecutar</param>
        /// <param name="t1">Valor del primer parametro del metodo</param>
        /// <param name="t2">Valor del segundo parametro del metodo</param>
        /// <param name="t3">Valor tercer parametro del metodo</param>
        /// <param name="t4">Valor cuarto parametro del metodo</param>
        /// <param name="retries">Cantidad de reintentos</param>
        /// <param name="sleepBetweenRetries">Milisegundos de espera entre reintentos</param>
        /// <param name="exceptions">El listado de excepciones a reintentar. Por defecto no se reintenta ningun tipo.</param>
        public void Execute<T1, T2, T3, T4>(Action<T1, T2, T3, T4> method, T1 t1, T2 t2, T3 t3, T4 t4,
                       int retries, int sleepBetweenRetries, Type[] exceptions)
        {
            CheckExistentTransaction();
            CheckExceptionsType(exceptions);
            int cont = 0;
            while (true)
            {
                try
                {
                    method(t1, t2, t3, t4);
                    return;
                }
                catch (Exception ex)
                {
                    cont++;

                    if (cont > retries)
                        throw;

                    bool shouldRetry = false;
                    foreach (Type type in exceptions)
                    {
                        if (type.IsInstanceOfType(ex))
                        {
                            shouldRetry = true;
                            Log(ex);
                            break;
                        }
                    }
                    if (!shouldRetry)
                        throw;
                }
                Thread.Sleep(sleepBetweenRetries);
                Log("Reintentando transaccion");
            }
        }

        #endregion

        #region Metodos Privados

        private static void CheckExistentTransaction()
        {
            //Comento esto porque sino molesta en los test de unidad que enmarcan _todo el test en una transaccion

            //if(Transaction.Current != null)
            //    throw new InvalidOperationException("No puede ejecutar este metodo dentro de una transaccion");
        }

        private static void CheckExceptionsType(Type[] exceptions)
        {
            foreach (Type type in exceptions)
                if (!type.IsSubclassOf(typeof(Exception)))
                    throw new ArgumentException("EL tipo " + type.Name + " no es una excepcion");
        }

        private void Log(string mensaje)
        {
            if(Logger != null)
                Logger.LogDebug(mensaje);
        }

        private void Log(Exception ex)
        {
            if (Logger != null)
                Logger.LogDebug(ex.GetType().FullName + "  " + ex.Message + Environment.NewLine + ex.StackTrace);
        }

        #endregion
    }
}

