using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Cno.Roca.Core.Data
{
    public interface ITxScope : IDisposable
    {
        bool KeepConnectionOpen { get; }
        void Complete();
        bool HasConnection();
        OracleConnection GetConnection();
        void BeginOrContinueTransaction(OracleConnection conn);
    }
}
