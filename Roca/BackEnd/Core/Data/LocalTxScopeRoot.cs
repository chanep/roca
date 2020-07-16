using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Oracle.DataAccess.Client;

namespace Cno.Roca.Core.Data
{

    public class LocalTxScopeRoot : LocalTxScope
    {
        public OracleConnection Connection { get; set; }

        public OracleTransaction Tx { get; set; }

        public bool IncompleteScopes { get; set; }

        public LocalTxScopeRoot(TransactionOptions txOptions) : base(txOptions)
        {
        }

        public override void Dispose()
        {
            try
            {
                if(Tx != null)
                {
                    if (Completed && !IncompleteScopes)
                        Tx.Commit();
                    else
                        Tx.Rollback();
                    Tx.Dispose();    
                }

                if(Connection != null)
                    Connection.Dispose();
                
                Tx = null;
                Connection = null;
            }
            finally
            {
                base.Dispose(); 
            } 
        }
    }
}
