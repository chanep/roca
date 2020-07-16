using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Cno.Roca.Core.Data;
using Cno.Roca.CoreData;
using Oracle.DataAccess.Client;
using Cno.Roca.CoreData.Entity;

namespace Cno.Roca.Core.Entity
{
    public abstract class Dal<K, T> : IDal<K, T>
            where T : Entity<K>, new()
    {
        protected OracleConnection _connection;
        protected IDbConnectionManager _dbConnectionManager;


        protected Dictionary<string, string> MapeoDb
        {
            get; set;
        }

        protected Dal(IDbConnectionManager dbConnectionManager)
        {
            _dbConnectionManager = dbConnectionManager;
        }

        public void ExecuteNonQuery(OracleCommand command)
        {
            try
            {
                OpenConnection();
                command.Connection = _connection;
                LogCommand(command);
                command.ExecuteNonQuery();
                LogEndCommand(command);
            }
            catch (OracleException ex)
            {
                if (IsTransactionConcurrencyException(ex))
                {
                    throw new TransactionConcurrencyException("La transaccion colisiono con otra", ex);
                }
                throw;
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                CloseConnection();
            }
        }

        protected void OpenConnection()
        {
            _connection = (OracleConnection)_dbConnectionManager.OpenConnection();
        }

        protected void CloseConnection()
        {
            _dbConnectionManager.CloseConnection(_connection);
        }

        /// <summary>
        /// Transforma la excepcion arrojada por el comando Create
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        protected virtual Exception TransformCreateException(OracleException ex)
        {
            if (ex.Number >= 20000 && ex.Number <= 20999)
            {
                return UserDefinedException(ex);
            }
            if (ex.Number == 1)
            {
                return new ApplicationException("Ya existe un registro con esa clave", ex);
            }
            return ex;
        }

        /// <summary>
        /// Transforma la excepcion arrojada por el comando Update
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        protected virtual Exception TransformUpdateException(OracleException ex)
        {
            if (ex.Number >= 20000 && ex.Number <= 20999)
            {
                return UserDefinedException(ex);
            }
            return ex;
        }

        /// <summary>
        /// Transforma la excepcion arrojada por el comando Delete
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        protected virtual Exception TransformDeleteException(OracleException ex)
        {
            if (ex.Number >= 20000 && ex.Number <= 20999)
            {
                return UserDefinedException(ex);
            }
            if (ex.Number == 2292)
            {
                return new ApplicationException("No se puede eliminar el registro. Existen datos relacionados.", ex);
            }
            return ex;
        }

        static Exception UserDefinedException(OracleException ex)
        {
            var relevantMessage = OracleMessage.Match(ex.Message).Groups[1].Value;
            throw new ApplicationException(relevantMessage, ex);
        }



        protected bool IsTransactionConcurrencyException(OracleException ex)
        {
            if (ex.Number == 8177)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Lo usa la logica de acceso a datos para setearle a la entidad el id que autogenero
        /// la base de datos al insertar el nuevo registro
        /// </summary>
        protected virtual void SetEntityId(object id, T entity)
        {
            throw new NotImplementedException("La logica de acceso a datos " + GetType().FullName +
                                              " no implementa el metodo SetEntityId");
        }

        /// <summary>
        /// Puede redefinirlo una EntidadB que deriva de una EntidadA pero que quiere reutilizar el EntidadADal
        /// </summary>
        /// <returns></returns>
        protected virtual T GetEntityInstance()
        {
            return new T();
        }

        protected bool ConvertCharToBool(char c)
        {
            if (c.ToString().ToUpper() == "N")
            {
                return false;
            }
            if (c.ToString().ToUpper() == "Y")
            {
                return true;
            }
            throw new ArgumentException("El caracter debe ser N o Y");
        }

        protected char? ConvertBoolToChar(bool? b)
        {
            if (b == null)
            {
                return null;
            }
            if (b.Value)
            {
                return 'Y';
            }
            return 'N';
        }

        /// <summary>
        /// Se usa en los Enum en donde el valor 0 significa Indefinido, en este caso devuelve null
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        protected int? ConvertEnumToNulleable(int i)
        {
            if (i == 0)
            {
                return null;
            }
            return i;
        }

        protected void LogCommand(DbCommand command)
        {
            (new DBLogger(CoreConfig.LogDBFileName, CoreConfig.LogDBCommands, CoreConfig.TraceDBCommands)).LogCommand(command);
        }

        protected void LogEndCommand(DbCommand command)
        {
            (new DBLogger(CoreConfig.LogDBFileName, CoreConfig.LogDBCommands, CoreConfig.TraceDBCommands)).LogEndCommand(command);
        }

        static readonly Regex OracleMessage = new Regex(@"^ORA-\d+: (.+?)(?=ORA-\d+: )",
                                                        RegexOptions.Compiled | RegexOptions.Singleline);

        protected bool HasColumn(IDataRecord dr, string columnName)
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }

        public abstract T Get(K id);
        public abstract IList<T> GetAll();
        public abstract T Create(T entity);
        public abstract void Update(T entity);
        public abstract void Delete(T entity);
    }
}
