using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Oracle.DataAccess.Client;

namespace Cno.Roca.Core.Data
{
    public abstract class BaseTxScope : ITxScope
    {
        private static Dictionary<int, BaseTxScope> _currentTxScopes = new Dictionary<int, BaseTxScope>();
        private static int _nextId = 0;
        protected static BaseTxScope CurrentTxScope
        {
            get
            {
                lock (_currentTxScopes)
                {
                    if (_currentTxScopes.ContainsKey(Thread.CurrentThread.ManagedThreadId))
                        return _currentTxScopes[Thread.CurrentThread.ManagedThreadId];
                    return null;
                }
            }
            set
            {
                lock (_currentTxScopes)
                {
                    if (value == null)
                    {
                        if (_currentTxScopes.ContainsKey(Thread.CurrentThread.ManagedThreadId))
                            _currentTxScopes.Remove(Thread.CurrentThread.ManagedThreadId);
                    }
                    else
                    {
                        if (_currentTxScopes.ContainsKey(Thread.CurrentThread.ManagedThreadId))
                            _currentTxScopes[Thread.CurrentThread.ManagedThreadId] = value;
                        else
                            _currentTxScopes.Add(Thread.CurrentThread.ManagedThreadId, value);
                    }
                }

            }
        }



        protected BaseTxScope RootTxScope
        {
            get
            {
                BaseTxScope aux = this;
                while(aux.ParentTxScope != null)
                {
                    aux = aux.ParentTxScope;
                }
                return aux;
            }
        }

        protected BaseTxScope ParentTxScope { get; set; }
        protected List<BaseTxScope> ChildTxScopes { get; set; }
        protected int Id { get; set; }

        protected internal BaseTxScope()
        {
            ChildTxScopes = new List<BaseTxScope>();
            ParentTxScope = CurrentTxScope;
            if(CurrentTxScope != null)
            {
                ParentTxScope.ChildTxScopes.Add(this);
            } 
            
            CurrentTxScope = this;
            Id = _nextId;
            _nextId++;
        }

        public abstract bool KeepConnectionOpen { get; }
        public abstract void Complete();
        public abstract bool HasConnection();
        public abstract OracleConnection GetConnection();        
        public abstract void BeginOrContinueTransaction(OracleConnection conn);

        public virtual void Dispose()
        {
            CurrentTxScope = ParentTxScope;
        }

        protected bool IsRoot()
        {
            return ReferenceEquals(this, RootTxScope);
        }

    }
}
