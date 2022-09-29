using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Exceptions
{
    [Serializable]
    public class MissingFrameException : Exception
    {
        public MissingFrameException()
        { }

        public MissingFrameException(string message) : base(message)
        { }

        public MissingFrameException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
