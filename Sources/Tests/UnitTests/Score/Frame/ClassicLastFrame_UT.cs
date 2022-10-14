using FrameWriterModel.Frame;
using FrameWriterModel.Frame.ThrowResults;
using Model.Exceptions;
using Model.Score;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Score.Frame
{
    public class ClassicLastFrame_UT
    {
        [Theory]
        [InlineData(2, 2)]
        [InlineData(1, 1)]
        [InlineData(1, 0)]
        [InlineData(1, -1)]
        public void Test_Constructor(int exceptedLabel, int label)
        {
            AFrame classicLastFrame = new ClassicLastFrame(label);
            Assert.Equal(exceptedLabel, classicLastFrame.FrameNumberLabel);
        }

        [Theory]
        [InlineData(2, ThrowResult.SEVEN, ThrowResult.SPARE, ThrowResult.ONE)]
        [InlineData(6, ThrowResult.SIX, ThrowResult.STRIKE, ThrowResult.TWO)]
        [InlineData(5, ThrowResult.NONE, ThrowResult.STRIKE, ThrowResult.SEVEN)]
        [InlineData(7, ThrowResult.ONE, ThrowResult.ONE, ThrowResult.NONE)]
        [InlineData(8, ThrowResult.ZERO, ThrowResult.ZERO, ThrowResult.NONE)]
        [InlineData(11, ThrowResult.NINE, ThrowResult.SPARE, ThrowResult.FOUR)]
        [InlineData(1, ThrowResult.STRIKE, ThrowResult.STRIKE, ThrowResult.STRIKE)]
        public void CloneClassicLastFrameShouldReturnNewDeepCopy(int frameNumber, ThrowResult throwResult1,
                                                                 ThrowResult throwResult2, ThrowResult throwResult3)
        {
            AFrame frame = new ClassicLastFrame(frameNumber, throwResult1, throwResult2, throwResult3);
            ClassicLastFrame copy = (ClassicLastFrame) frame.Clone();
            Assert.All(copy.ThrowResults, t => frame.ThrowResults.Contains(t));
            Assert.Equal(frame.FrameNumberLabel, copy.FrameNumberLabel);
            Assert.Equal(frame, copy);
            Assert.Equal(frame.GetHashCode(), copy.GetHashCode());
            Assert.False(ReferenceEquals(frame, copy));
        }
    }
}
