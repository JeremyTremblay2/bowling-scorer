using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FrameWriterModel.Exceptions
{
    /// <summary>
    /// Represents an occur that can occur if a throwresult is not correct at the specific time.
    /// </summary>
    [Serializable]
    public class ForbiddenThrowResultException : Exception
    {
        /// <summary>
        /// Create a new instance of a ForbiddenThrowResultException.
        /// </summary>
        public ForbiddenThrowResultException()
        { }

        /// <summary>
        /// Create a new instance of a ForbiddenThrowResultException.
        /// </summary>
        /// <param name="message">The exception's message.</param>
        public ForbiddenThrowResultException(string message) : base(message)
        { }

        /// <summary>
        /// Create a new instance of a ForbiddenThrowResultException.
        /// </summary>
        /// <param name="message">The message of the exception.</param>
        /// <param name="innerException">The inner exception.</param>
        public ForbiddenThrowResultException(string message, Exception innerException) : base(message, innerException)
        { }

        /// <summary>
        /// Create a new instance of a ForbiddenThrowResultException.
        /// </summary>
        /// <param name="info">A serialization information.</param>
        /// <param name="context">A streaming context of the exception.</param>
        protected ForbiddenThrowResultException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
