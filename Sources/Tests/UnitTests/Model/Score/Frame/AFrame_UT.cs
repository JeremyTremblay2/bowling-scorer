using FrameWriterModel.Frame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Score.Frame
{
    public class AFrame_UT
    {
        // Use MOCK (library for instantiate Abstract class but not realy, test the constructor's method

        /// <summary>
        /// Class for testing directly the implemented methods of AFrame
        /// </summary>
        private class BadFrame : AFrame
        {
            public BadFrame(int frameNumberLabel, int nbThrows) : base(frameNumberLabel, nbThrows)
            {
            }

            public override object Clone()
            {
                throw new NotImplementedException();
            }
        }
        [Fact]
        public void Test_Constructor()
        {
            Assert.Throws<ArgumentException>(() => { new BadFrame(1, -1); });
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(0, 1)]
        [InlineData(-1, 1)]
        public void Test_FrameNumberLabelProperty(int number, int exceptedLabel)
        {
            AFrame frame = new BadFrame(number, 1);
            Assert.Equal(exceptedLabel, frame.FrameNumberLabel);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(0, 0)]
        [InlineData(-1, 0)]
        public void Test_FrameScoreValueProperty(int value, int exceptedValue)
        {
            AFrame frame = new BadFrame(1, 1);
            frame.ScoreValue = value;
            Assert.Equal(exceptedValue, frame.ScoreValue);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(0, 0)]
        [InlineData(-1, 0)]
        public void Test_FrameCumulativeScoreProperty(int cumulScore, int exceptedCumulScore)
        {
            AFrame frame = new BadFrame(1, 1);
            frame.CumulativeScore = cumulScore;
            Assert.Equal(exceptedCumulScore, frame.CumulativeScore);
        }

        public static IEnumerable<object[]> Data_Equals()
        {
            yield return new object[]
            {
                false,
                null
            };
            yield return new object[]
            {
                false,
                new object()
            };
            yield return new object[]
            {
                true,
                new BadFrame(1, 1)
            };
        }

        [Theory]
        [MemberData(nameof(Data_Equals))]
        public void Test_Equals(bool exceptedResult, object frame2)
        {
            AFrame frame1 = new BadFrame(1, 1);
            Assert.Equal(exceptedResult, frame1.Equals(frame2));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void Test_GetHashCode(int labelNumber)
        {
            AFrame frame = new BadFrame(labelNumber, 2);
            AFrame anotherFrame = new BadFrame(labelNumber, 20);
            Assert.Equal(anotherFrame.GetHashCode(), frame.GetHashCode());
        }
    }
}
