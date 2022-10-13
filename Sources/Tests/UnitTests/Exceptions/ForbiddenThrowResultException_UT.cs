using FrameWriterModel.Exceptions;
using System;
using System.Runtime.Serialization;
using Xunit;

namespace UnitTests.Exceptions
{
    /// <summary>
    /// Unit test class for the ForbiddenThrowResultException.
    /// </summary>
    public class ForbiddenThrowResultException_UT
    {
        /// <summary>
        /// Private class for testing the protected constructor of the exception.
        /// </summary>
        internal class AnotherException : ForbiddenThrowResultException
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
        public void ThrowForbiddenThrowResultExceptionWithoutMessageShouldThrowForbiddenThrowResultException()
        {
            Assert.ThrowsAsync<ForbiddenThrowResultException>(() => throw new ForbiddenThrowResultException());
        }

        [Fact]
        public void ThrowForbiddenThrowResultExceptionWithMessageShouldThrowForbiddenThrowResultException()
        {
            Assert.ThrowsAsync<ForbiddenThrowResultException>(() => throw new ForbiddenThrowResultException("Frame not found !"));
        }

        [Fact]
        public void ThrowForbiddenThrowResultExceptionWithInnerExceptionShouldThrowForbiddenThrowResultException()
        {
            Assert.ThrowsAsync<ForbiddenThrowResultException>(() => throw new ForbiddenThrowResultException("Frame not found !", new Exception()));
        }

        [Fact]
        public void ThrowForbiddenThrowResultExceptionWithStreamingContextShouldThrowForbiddenThrowResultException()
        {
            Assert.ThrowsAsync<ForbiddenThrowResultException>(() 
                => throw new AnotherException(new SerializationInfo(GetType(), new FormatterConverter()), new StreamingContext()));
        }
    }
}
