using System;
using System.Collections.Generic;
using System.Text;

namespace Cno.Roca.CoreData.Entity
{
    [Serializable]
    public class DataIntegrityException : ApplicationException
    {
        public DataIntegrityException()
            : base()
        {
        }

        public DataIntegrityException(string message)
            : base(message)
        {
        }

        public DataIntegrityException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public DataIntegrityException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}
