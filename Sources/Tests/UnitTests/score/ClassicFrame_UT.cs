using Model.Exceptions;
using Model.Score;
using Model.Score.Frame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.score
{
    public class ClassicFrame_UT
    {
        [Theory]
        [InlineData(2, 2)]
        [InlineData(1, 1)]
        [InlineData(1, 0)]
        [InlineData(1, -1)]
        public void Test_Constructor(int exceptedLabel, int label)
        {
            AFrame classicFrame = new ClassicFrame(label);
            Assert.Equal(exceptedLabel, classicFrame.FrameNumberLabel);
        }

        [Theory]
        [InlineData(false, ThrowResult.TWO, ThrowResult.TWO)]
        [InlineData(true, ThrowResult.SPARE, ThrowResult.NONE)]
        [InlineData(true, ThrowResult.STRIKE, ThrowResult.NONE)]
        public void Test_WriteFirstThrow(bool throwExcep, ThrowResult resultToWrite, ThrowResult exceptedWritenResult)
        {
            ClassicFrame classic = new ClassicFrame(1);
            if (throwExcep)
            {
                Assert.Throws<ForbiddenThrowResultException>(() => { classic.WriteFirstThrow(resultToWrite); });
                return;
            }
            classic.WriteFirstThrow(resultToWrite);
            Assert.Equal(exceptedWritenResult, classic.ThrowResults[0]);
        }

        [Fact]
        public void Test_WriteSecondThrow()
        {
            ClassicFrame classic = new ClassicFrame(1);
            classic.WriteSecondThrow(ThrowResult.TREE);
            Assert.Equal(ThrowResult.TREE, classic.ThrowResults[1]);
        }
    }
}
