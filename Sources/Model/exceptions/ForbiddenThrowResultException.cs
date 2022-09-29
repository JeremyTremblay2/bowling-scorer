using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Exceptions
{
    [Serializable]
    public class ForbiddenThrowResultException : Exception
    {
        public ForbiddenThrowResultException()
        { }

        public ForbiddenThrowResultException(string message) : base(message)
        { }

        public ForbiddenThrowResultException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
