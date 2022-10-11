using FrameWriterModel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Exceptions
{
    /// <summary>
    /// Unit test class for the ForbiddenThrowResultException.
    /// </summary>
    public class ForbiddenThrowResultException_UT
    {
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
    }
}
