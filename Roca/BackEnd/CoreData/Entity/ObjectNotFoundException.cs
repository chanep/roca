using System;
using System.Collections.Generic;
using System.Text;

namespace Cno.Roca.CoreData.Entity
{
    [Serializable]
    public class ObjectNotFoundException : ApplicationException
    {
        public ObjectNotFoundException()
            : base()
        {
        }

        public ObjectNotFoundException(string message)
            : base(message)
        {
        }

        public ObjectNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ObjectNotFoundException(System.Runtime.Serialization.SerializationInfo info,
                                       System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}
