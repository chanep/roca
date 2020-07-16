using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Oracle.DataAccess.Client;
using IsolationLevel=System.Transactions.IsolationLevel;

namespace Cno.Roca.Core.Data
{
    public class DistributedTxScope : BaseTxScope
    {
        private TransactionScope _txScope;
        private bool _isolationLevelCorrected;

        protected new DistributedTxScope RootTxScope
        {
            get
            {
                return (DistributedTxScope) base.RootTxScope;
            }
        }

        internal DistributedTxScope(Transaction parentTransaction)
        {
            _txScope = new TransactionScope(parentTransaction);
        }

        internal DistributedTxScope(TransactionOptions txOptions)
        {
            _txScope = new TransactionScope(TransactionScopeOption.Required, txOptions);
        }

        public override bool KeepConnectionOpen
        {
            get { return false; }
        }

        public override void Complete()
        {
            _txScope.Complete();
            //Console.WriteLine("Complete scope {0}. LocalId {1}", Id, GetTxId());
        }

        public override bool HasConnection()
        {
            return false;
        }

        public override OracleConnection GetConnection()
        {
            throw new InvalidOperationException("Esta Clase no almacena conexion a DB");
        }


        /// <summary>
        /// Solo se corrige el isolationLevel si es necesario. 
        /// La Transaccion se maneja implicitamente la clase TransactionScope
        /// </summary>
        /// <param name="conn"></param>
        public override void BeginOrContinueTransaction(OracleConnection conn)
        {
            if(MustSetIsolationLevel())
                SetSerializableIsolationLevel(conn);
        }

        public static BaseTxScope GetCurrentTxScope()
        {
            return CurrentTxScope;
        }


        public override void Dispose()
        {
            
            base.Dispose();
            _txScope.Dispose();
            //Console.WriteLine("Dispose scope {0}. LocalId {1}", Id, GetTxId());     
        }

        private string GetTxId()
        {
            try
            {
                return Transaction.Current.TransactionInformation.LocalIdentifier;
            }
            catch (Exception)
            {
                return "";
            }
        }

        private bool MustSetIsolationLevel()
        {
            return
                ((Transaction.Current != null) &&
                 (Transaction.Current.IsolationLevel == IsolationLevel.Serializable) &&
                 !RootTxScope._isolationLevelCorrected
                 );
        }


        /// <summary>
        /// Por el momento el Oracle Data Provider tiene un problema con el manejo de los 
        /// IsolationLevel de las transacciones. No toma correctamente el nivel Serializable,
        /// siempre trabaja en el nivel ReadCommitted
        /// </summary>
        private void SetSerializableIsolationLevel(IDbConnection connection)
        {
            var csBuilder = new OracleConnectionStringBuilder(connection.ConnectionString);
            csBuilder.Pooling = false;
            OracleConnection conn = null;
            OracleCommand cmd = null;
            try
            {
                conn = new OracleConnection(csBuilder.ConnectionString);
                conn.Open();
                cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SET TRANSACTION ISOLATION LEVEL SERIALIZABLE";
                cmd.ExecuteNonQuery();
                RootTxScope._isolationLevelCorrected = true;
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                if (conn != null)
                    conn.Dispose();
            }
        }
    }
}
