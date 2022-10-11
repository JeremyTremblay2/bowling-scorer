using FrameWriterModel.Frame;
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
    }
}

