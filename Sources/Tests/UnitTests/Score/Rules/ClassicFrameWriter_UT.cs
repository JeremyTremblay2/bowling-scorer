using Model.Exceptions;
using Model.Score.Frame;
using Model.Score;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Model.Score.Rules;

namespace UnitTests.Score.Rules
{
    public class ClassicFrameWriter_UT
    {
        [Theory]
        [InlineData(false, ThrowResult.TWO, ThrowResult.TWO)]
        [InlineData(true, ThrowResult.SPARE, ThrowResult.NONE)]
        [InlineData(true, ThrowResult.STRIKE, ThrowResult.NONE)]
        public void Test_WriteFirstThrow(bool throwExcep, ThrowResult resultToWrite, ThrowResult exceptedWritenResult)
        {
            ClassicFrame classic = new ClassicFrame(1);
            IFrameWriter frameWriter = new ClassicFrameWriter();
            if (throwExcep)
            {
                Assert.Throws<ForbiddenThrowResultException>(() => { frameWriter.WriteValue(classic, 0, resultToWrite); });
                return;
            }
            frameWriter.WriteValue(classic, 0, resultToWrite);
            Assert.Equal(exceptedWritenResult, classic.ThrowResults[0]);
        }

        [Fact]
        public void Test_WriteSecondThrow()
        {
            ClassicFrame classic = new ClassicFrame(1);
            IFrameWriter frameWriter = new ClassicFrameWriter();
            frameWriter.WriteValue(classic, 1, ThrowResult.TREE);
            Assert.Equal(ThrowResult.TREE, classic.ThrowResults[1]);
        }
    }
}
