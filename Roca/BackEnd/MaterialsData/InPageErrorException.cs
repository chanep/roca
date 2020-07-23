using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cno.Roca.BackEnd.Materials.Data
{
    /// <summary>
    /// Excepcion de la aplicacion Roca con mensaje para ser mostrado dentro de la pagina donde se genero el error 
    /// (no redirije a la pagina general de errores)
    /// </summary>
    [Serializable]
    public class InPageErrorException : RocaException
    {
        public InPageErrorException()
            : base()
        {
        }

        public InPageErrorException(string message)
            : base(message)
        {
        }

        public InPageErrorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public InPageErrorException(System.Runtime.Serialization.SerializationInfo info,
                                       System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}
