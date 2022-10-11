using Model.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Exceptions
{
    /// <summary>
    /// Unit test class for the MissingFrameException class.
    /// </summary>
    public class MissingFrameException_UT
    {
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
    }
}
