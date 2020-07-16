using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Cno.Roca.Core.Data
{
    public class NullTxScope : ITxScope
    {
        public  bool KeepConnectionOpen
        {
            get { return false; }
        }

        public void Complete()
        {
            
        }

        public bool HasConnection()
        {
            return false;
        }

        public OracleConnection GetConnection()
        {
            return null;
        }

        public void BeginOrContinueTransaction(OracleConnection conn)
        {
            
        }

        public void Dispose()
        {
            
        }
    }
}
