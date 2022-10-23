using FrameModel.Frame;
using FrameModel.Frame.ThrowResults;
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
        [InlineData(2, ThrowResult.SEVEN, ThrowResult.TWO)]
        [InlineData(6, ThrowResult.SIX, ThrowResult.SPARE)]
        [InlineData(5, ThrowResult.NONE, ThrowResult.STRIKE)]
        [InlineData(7, ThrowResult.ONE, ThrowResult.ONE)]
        [InlineData(8, ThrowResult.ZERO, ThrowResult.ZERO)]
        [InlineData(11, ThrowResult.NINE, ThrowResult.TWO)]
        [InlineData(1, ThrowResult.EIGHT, ThrowResult.ZERO)]
        public void CloneClassicFrameShouldReturnNewDeepCopy(int frameNumber, ThrowResult throwResult1, ThrowResult throwResult2)
        {
            AFrame frame = new ClassicFrame(frameNumber, throwResult1, throwResult2);
            ClassicFrame copy = (ClassicFrame) frame.Clone();
            Assert.All(copy.ThrowResults, t => frame.ThrowResults.Contains(t));
            Assert.Equal(frame.FrameNumberLabel, copy.FrameNumberLabel);
            Assert.Equal(frame, copy);
            Assert.Equal(frame.GetHashCode(), copy.GetHashCode());
            Assert.False(ReferenceEquals(frame, copy));
        }
    }
}

