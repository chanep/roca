using System;
using System.Collections.Generic;
using System.Text;

namespace Cno.Roca.CoreData.Entity
{
    [Serializable]
    public class InvalidValueException : ApplicationException
    {
        public InvalidValueException()
            : base()
        {
        }

        public InvalidValueException(string message)
            : base(message)
        {
        }

        public InvalidValueException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public InvalidValueException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}
