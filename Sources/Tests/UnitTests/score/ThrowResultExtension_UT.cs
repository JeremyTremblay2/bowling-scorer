using FrameWriterModel.Frame.ThrowResults;
using Model.Score;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Score
{
    public class ThrowResultExtension_UT
    {
        [Theory]
        [InlineData(ThrowResult.NONE, 0)]
        [InlineData(ThrowResult.ZERO, 0)]
        [InlineData(ThrowResult.ONE, 1)]
        [InlineData(ThrowResult.TWO, 2)]
        [InlineData(ThrowResult.TREE, 3)]
        [InlineData(ThrowResult.FOUR, 4)]
        [InlineData(ThrowResult.FIVE, 5)]
        [InlineData(ThrowResult.SIX, 6)]
        [InlineData(ThrowResult.SEVEN, 7)]
        [InlineData(ThrowResult.EIGHT, 8)]
        [InlineData(ThrowResult.NINE, 9)]
        [InlineData(ThrowResult.SPARE, 10)]
        [InlineData(ThrowResult.STRIKE, 10)]
        public void ToInt_Test(ThrowResult valueToConvert, int valueExcepted)
        {
            Assert.Equal(valueExcepted, valueToConvert.ToInt());
        }
    }
}
