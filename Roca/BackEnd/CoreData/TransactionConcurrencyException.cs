using System;
using System.Collections.Generic;
using System.Text;

namespace Cno.Roca.CoreData
{
    /// <summary>
    /// Excepcion que se lanza cuando falla una transaccion porque colisiona con otra
    /// </summary>
    [Serializable]
    public class TransactionConcurrencyException : ApplicationException
    {
        public TransactionConcurrencyException()
            : base()
        {
        }

        public TransactionConcurrencyException(string message)
            : base(message)
        {
        }

        public TransactionConcurrencyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public TransactionConcurrencyException(System.Runtime.Serialization.SerializationInfo info,
                                       System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}
