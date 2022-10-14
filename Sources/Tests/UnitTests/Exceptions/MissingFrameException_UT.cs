using FrameWriterModel.Exceptions;
using Model.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static UnitTests.Exceptions.ForbiddenThrowResultException_UT;

namespace UnitTests.Exceptions
{
    /// <summary>
    /// Unit test class for the MissingFrameException class.
    /// </summary>
    public class MissingFrameException_UT
    {
        /// <summary>
        /// Private class for testing the protected constructor of the exception.
        /// </summary>
        internal class AnotherException : MissingFrameException
        {
            /// <summary>
            /// Create a new instance of a AnotherException.
            /// </summary>
            public AnotherException() : base()
            { }

            /// <summary>
            /// Create a new instance of a AnotherException.
            /// </summary>
            /// <param name="message">The exception's message.</param>
            public AnotherException(string message) : base(message)
            { }

            /// <summary>
            /// Create a new instance of a AnotherException.
            /// </summary>
            /// <param name="message">The message of the exception.</param>
            /// <param name="innerException">The inner exception.</param>
            public AnotherException(string message, Exception innerException) : base(message, innerException)
            { }

            /// <summary>
            /// Create a new instance of a AnotherException.
            /// </summary>
            /// <param name="info">A serialization information.</param>
            /// <param name="context">A streaming context of the exception.</param>
            public AnotherException(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }
        }

            [Fact]
        public void ThrowMissingFrameExceptionWithoutMessageShouldThrowMissingFrameException()
        {
            Assert.ThrowsAsync<MissingFrameException>(() => throw new MissingFrameException());
        }

        [Fact]
        public void ThrowMissingFrameExceptionWithMessageShouldThrowMissingFrameException()
        {
            Assert.ThrowsAsync<MissingFrameException>(() => throw new MissingFrameException("Frame not found !"));
        }

        [Fact]
        public void ThrowMissingFrameExceptionWithInnerExceptionShouldThrowMissingFrameException()
        {
            Assert.ThrowsAsync<MissingFrameException>(() => throw new MissingFrameException("Frame not found !", new Exception())) ;
        }

        [Fact]
        public void ThrowMissingFrameExceptionWithStreamingContextShouldThrowForbiddenThrowResultException()
        {
            Exception exception = new AnotherException(new SerializationInfo(GetType(), new FormatterConverter()), new StreamingContext());
            Assert.ThrowsAsync<ForbiddenThrowResultException>(() => throw exception);
        }
    }
}
