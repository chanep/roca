using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Cno.Roca.Core.Data;
using Cno.Roca.CoreData;
using Cno.Roca.CoreData.Entity;
using Oracle.DataAccess.Client;

namespace Cno.Roca.Core.Entity
{
	/// <summary>
	/// Clase base de todas las logicas de acceso a base de datos Oracle
	/// </summary>
	/// <typeparam name="K">Tipo del id de la entidad</typeparam>
	/// <typeparam name="T">Tipo de la entidad</typeparam>
    public abstract class DalBasic<K, T> : Dal<K, T> 
        where T : Entity<K>, new()
	{

		#region IDal<K,T> Members

	    protected DalBasic(IDbConnectionManager dbConnectionManager) : base(dbConnectionManager)
	    {
	        
	    }

	    public override T Get(K id)
		{
			OpenConnection();
			OracleDataReader dataReader = null;
			OracleCommand command = null;
			try
			{
				command = BuildGetCommand(id);
				command.Connection = _connection;
				LogCommand(command);
				dataReader = command.ExecuteReader();
				LogEndCommand(command);
				if (dataReader.Read())
				{
					return BuildEntity(dataReader);
				}
				else
				{
					return null;
				}
			}
			finally
			{
				if ((dataReader != null) && !dataReader.IsClosed)
				{
					dataReader.Dispose();
				}
				if (command != null)
				{
					command.Dispose();
				}
				CloseConnection();
			}
		}

		/// <summary>
		/// Para gets con criterios de busqueda particulares (que no son por Id)
		/// </summary>
		/// <param name="command"></param>
		/// <returns></returns>
		public virtual T Get(OracleCommand command)
		{
            return Get<T>(command, BuildEntity);
		}

        /// <summary>
        /// Para gets con criterios de busqueda particulares (que no son por Id)
        /// </summary>
        /// <param name="command"></param>
        /// <param name="buildEntity">funcion que genera un custom data a partir de un datareader</param>
        /// <returns></returns>
        public virtual TData Get<TData>(OracleCommand command, Func<OracleDataReader, TData> buildEntity) where TData : class 
        {
            OpenConnection();
            OracleDataReader dataReader = null;
            try
            {
                command.Connection = _connection;
                LogCommand(command);
                dataReader = command.ExecuteReader();
                LogEndCommand(command);
                if (dataReader.Read())
                {
                    return buildEntity(dataReader);
                }
                else
                {
                    return null;
                }
            }
            finally
            {
                if ((dataReader != null) && !dataReader.IsClosed)
                {
                    dataReader.Dispose();
                }
                if (command != null)
                {
                    command.Dispose();
                }
                CloseConnection();
            }
        }

		public override IList<T> GetAll()
		{
			OpenConnection();
			OracleCommand command = null;
			OracleDataReader dataReader = null;
			try
			{
				command = BuildGetAllCommand();
				command.Connection = _connection;
				LogCommand(command);
				dataReader = command.ExecuteReader();
				LogEndCommand(command);
				var entities = new List<T>();
				while (dataReader.Read())
				{
					entities.Add(BuildEntity(dataReader));
				}
				return entities;
			}
			finally
			{
				if ((dataReader != null) && !dataReader.IsClosed)
				{
					dataReader.Dispose();
				}
				if (command != null)
				{
					command.Dispose();
				}
				CloseConnection();
			}
		}

        public virtual IList<T> GetAll(Expression<Func<T, bool>> condition, params Expression<Func<T, Direction>>[] orders)
        {
            OracleCommand command = BuildGetAllCommand();
            var whereGen = new SqlQueryGen<T>(MapeoDb, condition, orders);
            command.CommandText += " WHERE " + whereGen.Query;
            foreach (var param in whereGen.QueryParameters)
            {
                command.Parameters.Add(param);
            }
            return GetAll<T>(command, BuildEntity);
        }

		/// <summary>
		/// Para gets con criterios de busqueda particulares
		/// </summary>
		/// <param name="command"></param>
		/// <returns></returns>
		public virtual IList<T> GetAll(OracleCommand command)
		{
			return GetAll(command, -1);
		}


		/// <summary>
		/// Para gets con criterios de busqueda particulares
		/// </summary>
		/// <param name="command"></param>
		/// <param name="cant">cantidad de registros maximos que devolvera el query</param>
		/// <returns></returns>
		public virtual IList<T> GetAll(OracleCommand command, int cant)
		{
			return GetAll<T>(command, BuildEntity, cant);
		}

        /// <summary>
        /// Para gets con criterios de busqueda particulares
        /// </summary>
        /// <param name="command"></param>
        /// <param name="buildEntity">funcion que genera un custom data a partir de un datareader</param>
        /// <returns></returns>
        public virtual IList<TData> GetAll<TData>(OracleCommand command, Func<OracleDataReader, TData> buildEntity) where TData : class
        {
            return GetAll(command, buildEntity, -1);
        }

        /// <summary>
        /// Para gets con criterios de busqueda particulares
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cant">cantidad de registros maximos que devolvera el query</param>
        /// <param name="buildEntity">funcion que genera un custom data a partir de un datareader</param>
        /// <returns></returns>
        public virtual IList<TData> GetAll<TData>(OracleCommand command, Func<OracleDataReader, TData> buildEntity, int cant) where TData : class
        {
            OracleDataReader dataReader = null;
            OpenConnection();
            try
            {
                command.Connection = _connection;
                LogCommand(command);
                dataReader = command.ExecuteReader();
                LogEndCommand(command);
                var entities = new List<TData>();
                while (dataReader.Read())
                {
                    entities.Add(buildEntity(dataReader));
                    if (entities.Count == cant)
                    {
                        break;
                    }
                }
                return entities;
            }
            finally
            {
                if ((dataReader != null) && !dataReader.IsClosed)
                {
                    dataReader.Dispose();
                }
                if (command != null)
                {
                    command.Dispose();
                }

                CloseConnection();
            }
        }

		public DataTable GetDataTable(OracleCommand command, string tableName)
		{
			OpenConnection();
			try
			{
				command.Connection = _connection;
				LogCommand(command);
				using (var reader = command.ExecuteReader(CommandBehavior.SingleResult))
				{
					LogEndCommand(command);
					var table = new DataTable { TableName = tableName };
					table.Load(reader);
					return table;
				}
			}
			finally
			{
				CloseConnection();
			}
		}

		public override T Create(T entity)
		{
			OracleCommand command = null;
			OpenConnection();
			try
			{
				command = BuildCreateCommand(entity);
				command.Connection = _connection;
				LogCommand(command);
				command.ExecuteNonQuery();
				LogEndCommand(command);
				if (command.Parameters.Contains("p_id") && (command.Parameters["p_id"].Direction == ParameterDirection.Output))
				{
					SetEntityId(command.Parameters["p_id"].Value, entity);
				}
				return entity;
			}
			catch (OracleException ex)
			{
				if (IsTransactionConcurrencyException(ex))
				{
					throw new TransactionConcurrencyException("La transaccion colisiono con otra", ex);
				}
				throw TransformCreateException(ex);
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

		public override void Update(T entity)
		{
			OracleCommand command = null;
			try
			{
				OpenConnection();
				command = BuildUpdateCommand(entity);
				command.Connection = _connection;
				LogCommand(command);
				int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 0)
                    throw new Exception("Error al actualizar. No existe el registro");
				LogEndCommand(command);
			}
			catch (OracleException ex)
			{
				if (IsTransactionConcurrencyException(ex))
				{
					throw new TransactionConcurrencyException("La transaccion colisiono con otra", ex);
				}
				throw TransformUpdateException(ex);
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

		public override void Delete(T entity)
		{
			OracleCommand command = null;
			try
			{
				OpenConnection();
				command = BuildDeleteCommand(entity);
				command.Connection = _connection;
				LogCommand(command);
				int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 0)
                    throw new Exception("Error al eliminar. No existe el registro");
				LogEndCommand(command);
			}
			catch (OracleException ex)
			{
				if (IsTransactionConcurrencyException(ex))
				{
					throw new TransactionConcurrencyException("La transaccion colisiono con otra", ex);
				}
				throw TransformDeleteException(ex);
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

		#endregion



        protected abstract OracleCommand BuildGetCommand(K id);
        protected abstract OracleCommand BuildGetAllCommand();
        protected abstract OracleCommand BuildCreateCommand(T entityData);
        protected abstract OracleCommand BuildUpdateCommand(T entity);
        protected abstract OracleCommand BuildDeleteCommand(T entity);
        protected abstract T BuildEntity(OracleDataReader dataReader);


	}
}