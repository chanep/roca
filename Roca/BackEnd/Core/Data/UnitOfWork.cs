using System;
using System.Transactions;

namespace Cno.Roca.Core.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ITxScope _txScope;

        public UnitOfWork() : this(IsolationLevel.Serializable, TxType.Local)
        {
        }

        public UnitOfWork(IsolationLevel isolationLevel) : this(isolationLevel, TxType.Local)
        {           
        }

        public UnitOfWork(IsolationLevel isolationLevel, TxType txType)
        {
            _txScope = TxManager.CreateTxScope(txType, new TransactionOptions() { IsolationLevel = isolationLevel });
        }

        public void Commit()
        {
            _txScope.Complete();
        }

        public void Dispose()
        {
            _txScope.Dispose();
        }
    }
}