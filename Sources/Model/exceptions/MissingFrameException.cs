using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model.Exceptions
{
    /// <summary>
    /// Represents an occur that can occur if a frame is missing.
    /// </summary>
    [Serializable]
    public class MissingFrameException : Exception
    {
        /// <summary>
        /// Create a new instance of a MissingFrameException.
        /// </summary>
        public MissingFrameException()
        { }

        /// <summary>
        /// Create a new instance of a MissingFrameException.
        /// </summary>
        /// <param name="message">The exception's message.</param>
        public MissingFrameException(string message) : base(message)
        { }

        /// <summary>
        /// Create a new instance of a MissingFrameException.
        /// </summary>
        /// <param name="message">The message of the exception.</param>
        /// <param name="innerException">The inner exception.</param>
        public MissingFrameException(string message, Exception innerException) : base(message, innerException)
        { }

        /// <summary>
        /// Create a new instance of a MissingFrameException.
        /// </summary>
        /// <param name="info">A serialization information.</param>
        /// <param name="context">A streaming context of the exception.</param>
        protected MissingFrameException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
