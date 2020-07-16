using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Transactions;
using Oracle.DataAccess.Client;

namespace Cno.Roca.Core.Data
{
    public class LocalTxScope : BaseTxScope
    {
        private TransactionOptions _txOptions;
        protected bool Completed;


        protected new LocalTxScopeRoot RootTxScope
        {
            get
            {
                return (LocalTxScopeRoot) base.RootTxScope;
            }
        }


        internal LocalTxScope(TransactionOptions txOptions)
        {
            if (RootTxScope.IncompleteScopes)
                throw new TransactionAbortedException("The transaction was Aborted");
            _txOptions = txOptions;
        }




        public override bool HasConnection()
        {
            if (RootTxScope.Connection == null)
                return false;
            return true;
        }

        public override OracleConnection GetConnection()
        {
            return RootTxScope.Connection;
        }

        public override bool KeepConnectionOpen
        {
            get { return true; }
        }

        public override void Complete()
        {
            if (RootTxScope.IncompleteScopes)
                throw new TransactionAbortedException("The transaction was Aborted");
            Completed = true;
        }



        public override void BeginOrContinueTransaction(OracleConnection conn)
        {
            if (RootTxScope.Connection != null)
                throw new ApplicationException("No se puede iniciar la transaccion porque ya existe una transaccion iniciada");
            RootTxScope.Connection = conn;
            RootTxScope.Tx = conn.BeginTransaction(ConvertIsolationLevel(RootTxScope._txOptions.IsolationLevel));
        }

        private static System.Data.IsolationLevel ConvertIsolationLevel(System.Transactions.IsolationLevel isolationLevel)
        {
            if (isolationLevel == IsolationLevel.Chaos)
                return System.Data.IsolationLevel.Chaos;
            if (isolationLevel == IsolationLevel.ReadCommitted)
                return System.Data.IsolationLevel.ReadCommitted;
            if (isolationLevel == IsolationLevel.ReadUncommitted)
                return System.Data.IsolationLevel.ReadUncommitted;
            if (isolationLevel == IsolationLevel.RepeatableRead)
                return System.Data.IsolationLevel.RepeatableRead;
            if (isolationLevel == IsolationLevel.Serializable)
                return System.Data.IsolationLevel.Serializable;
            if (isolationLevel == IsolationLevel.Snapshot)
                return System.Data.IsolationLevel.Snapshot;
            return System.Data.IsolationLevel.Unspecified;
        }


        public static BaseTxScope GetCurrentTxScope()
        {
            return CurrentTxScope;
        }

        public override void Dispose()
        {
            try
            {
                if (!Completed)
                    RootTxScope.IncompleteScopes = true;
            }
            finally
            {
                base.Dispose();
            }
        }
    }
}
