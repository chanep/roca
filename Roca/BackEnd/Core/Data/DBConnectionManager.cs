using System;
using System.Configuration;
using System.Data.Common;
using System.Runtime.CompilerServices;
using Oracle.DataAccess.Client;

namespace Cno.Roca.Core.Data
{

    public class DbConnectionManager : IDbConnectionManager
    {
        public string ConnectionString
        {
            get; set;
        }

        public DbConnectionManager(string connString)
        {
            ConnectionString = connString;
        }



        [MethodImpl(MethodImplOptions.Synchronized)]
        public DbConnection OpenConnection()
        {
            ITxScope txscope = TxManager.GetCurrentTxScope();

            if (txscope.HasConnection())
                return txscope.GetConnection();

            OracleConnection conn = null;
            try
            {
                conn = new OracleConnection(ConnectionString);
                conn.Open();
                txscope.BeginOrContinueTransaction(conn);
                return conn;
            }
            catch
            {
                if(conn != null)
                    conn.Dispose();
                throw;
            }
        }

        public void CloseConnection(DbConnection connection)
        {
            ITxScope txscope = TxManager.GetCurrentTxScope();
            if (connection != null && !txscope.KeepConnectionOpen)
                connection.Dispose();
        }
    }
}
