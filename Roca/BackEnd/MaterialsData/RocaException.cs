using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cno.Roca.BackEnd.Materials.Data
{
    /// <summary>
    /// Excepcion generica de la Aplicaacion Roca
    /// </summary>
    [Serializable]
    public class RocaException : ApplicationException
    {
        public RocaException()
            : base()
        {
        }

        public RocaException(string message)
            : base(message)
        {
        }

        public RocaException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public RocaException(System.Runtime.Serialization.SerializationInfo info,
                                       System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}
