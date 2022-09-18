using Model.score;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.score
{
    public class Frame_UT
    {
        [Fact]
        public void TestConstructorWithValidSize()
        {
            Frame frame = new Frame(2, 1);
            Assert.Equal(2, frame.ThrowResults.Count);
        }

        [Fact]
        public void TestConstructorWithZeroSize()
        {
            Assert.Throws<ArgumentException>(() => new Frame(0, 1));
        }

        [Fact]
        public void TestConstructorWithNegativeSize()
        {
            Assert.Throws<ArgumentException>(() => new Frame(-1, 1));
        }

        [Fact]
        public void TestConstructorWithValidFrameNumber()
        {
            Frame frame = new Frame(2, 1);
            Assert.Equal(1, frame.FrameNumber);
        }

        [Fact]
        public void TestConstructorWithInValidFrameNumber()
        {
            Frame frame = new Frame(2, -1);
            Assert.Equal(1, frame.FrameNumber);
        }

        [Fact]
        public void TestConstructorWithZeroFrameNumber()
        {
            Frame frame = new Frame(2, 0);
            Assert.Equal(1, frame.FrameNumber);
        }

        [Fact]
        public void TestScoreValueMoreThanZero()
        {
            Frame f = new Frame(2, 1);
            f.ScoreValue = 2;
            Assert.Equal(2, f.ScoreValue);
        }

        [Fact]
        public void TestScoreValueZero()
        {
            Frame f = new Frame(2, 1);
            f.ScoreValue = 0;
            Assert.Equal(0, f.ScoreValue);
        }

        [Fact]
        public void TestScoreValueLessThanZero()
        {
            Frame f = new Frame(2, 1);
            f.ScoreValue = -4;
            Assert.Equal(0, f.ScoreValue);
        }

        [Fact]
        public void TestAddThrowRightIndex()
        {
            Frame f = new Frame(2, 1);
            f.AddThrow(0, ThrowResult.ZERO);
            Assert.Equal(ThrowResult.ZERO, f.ThrowResults.ToArray()[0]);
        }

        [Fact]
        public void TestAddThrowBadIndex()
        {
            Frame f = new Frame(2, 1);
            Assert.Throws<IndexOutOfRangeException>(() => f.AddThrow(-1, ThrowResult.ZERO));
            Assert.Throws<IndexOutOfRangeException>(() => f.AddThrow(5, ThrowResult.ZERO));
        }
    }
}
