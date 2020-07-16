using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Transactions;

namespace Cno.Roca.Core.Data
{
    public class TxManager
    { 
        public static ITxScope CreateTxScope(TxType txType, TransactionOptions txOptions)
        {
            var currentTxScope = GetCurrentTxScope();
            if(txType == TxType.Distributed)
            {
                if(currentTxScope is LocalTxScope)
                    throw new ApplicationException("No es posible anidar una Tx Distribuida dentro de una Tx Local");
                return new DistributedTxScope(txOptions); 
            }
            else
            {
                if(currentTxScope is DistributedTxScope)
                    return new DistributedTxScope(txOptions);
                if (currentTxScope is NullTxScope)
                    return new LocalTxScopeRoot(txOptions);
                return new LocalTxScope(txOptions);
            }
                
        }

        public static ITxScope CreateTxScope(TransactionOptions txOptions)
        {
            return CreateTxScope(TxType.Local, txOptions);
        }

        public static ITxScope CreateTxScope(TxType txType)
        {
            var txOptions = new TransactionOptions { IsolationLevel = IsolationLevel.Serializable };
            return CreateTxScope(txType, txOptions);
        }

        public static ITxScope CreateTxScope()
        {
            var txOptions = new TransactionOptions {IsolationLevel = IsolationLevel.Serializable};
            return CreateTxScope(TxType.Local, txOptions);
        }

        public static ITxScope CreateTxScope(Transaction parentTransaction)
        {
            return new DistributedTxScope(parentTransaction);
        }



        public static ITxScope GetCurrentTxScope()
        {
            var txScope = LocalTxScope.GetCurrentTxScope();
            if (txScope == null)
                txScope = DistributedTxScope.GetCurrentTxScope();
            if (txScope == null)
                return new NullTxScope();
            return txScope;
        }
    }
}
