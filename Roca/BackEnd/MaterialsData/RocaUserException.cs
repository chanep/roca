using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cno.Roca.BackEnd.Materials.Data
{
    /// <summary>
    /// Excepcion de la aplicacion Roca con mensaje entendible para el Usuario
    /// </summary>
    [Serializable]
    public class RocaUserException : RocaException
    {
        public RocaUserException()
            : base()
        {
        }

        public RocaUserException(string message)
            : base(message)
        {
        }

        public RocaUserException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public RocaUserException(System.Runtime.Serialization.SerializationInfo info,
                                       System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}
