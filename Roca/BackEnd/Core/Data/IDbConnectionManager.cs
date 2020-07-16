using System.Data.Common;
using System.Runtime.CompilerServices;

namespace Cno.Roca.Core.Data
{
    public interface IDbConnectionManager
    {
        string ConnectionString { get; set; }

        [MethodImpl(MethodImplOptions.Synchronized)]
        DbConnection OpenConnection();

        void CloseConnection(DbConnection connection);
    }
}