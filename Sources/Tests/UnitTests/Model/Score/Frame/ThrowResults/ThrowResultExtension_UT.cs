using FrameWriterModel.Frame.ThrowResults;
using Xunit;

namespace UnitTests.Score.Frame.ThrowResults
{
    /// <summary>
    /// Unit test class for the ThrowResultExtension class.
    /// </summary>
    public class ThrowResultExtension_UT
    {
        [Theory]
        [InlineData(0, ThrowResult.NONE)]
        [InlineData(0, ThrowResult.ZERO)]
        [InlineData(1, ThrowResult.ONE)]
        [InlineData(2, ThrowResult.TWO)]
        [InlineData(3, ThrowResult.THREE)]
        [InlineData(4, ThrowResult.FOUR)]
        [InlineData(5, ThrowResult.FIVE)]
        [InlineData(6, ThrowResult.SIX)]
        [InlineData(7, ThrowResult.SEVEN)]
        [InlineData(8, ThrowResult.EIGHT)]
        [InlineData(9, ThrowResult.NINE)]
        [InlineData(10, ThrowResult.SPARE)]
        [InlineData(10, ThrowResult.STRIKE)]
        public void ConvertThrowResultToIntShouldReturnLogicalValue(int expectedResult, ThrowResult resultToConvert)
        {
            Assert.Equal(expectedResult, resultToConvert.ToInt());
        }

        [Theory]
        [InlineData(ThrowResult.ZERO, 0, true)]
        [InlineData(ThrowResult.ONE, 1, true)]
        [InlineData(ThrowResult.TWO, 2, true)]
        [InlineData(ThrowResult.THREE, 3, true)]
        [InlineData(ThrowResult.FOUR, 4, true)]
        [InlineData(ThrowResult.FIVE, 5, true)]
        [InlineData(ThrowResult.SIX, 6, true)]
        [InlineData(ThrowResult.SEVEN, 7, true)]
        [InlineData(ThrowResult.EIGHT, 8, true)]
        [InlineData(ThrowResult.NINE, 9, true)]
        [InlineData(ThrowResult.SPARE, 10, false)]
        [InlineData(ThrowResult.STRIKE, 10, true)]
        public void ConvertIntToThrowResultShouldReturnLogicalValue(ThrowResult expectedResult, int resultToConvert, bool isStrike)
        {
            Assert.Equal(expectedResult, resultToConvert.ToThrowResult(isStrike));
        }

        [Theory]
        [InlineData('-', ThrowResult.NONE)]
        [InlineData('0', ThrowResult.ZERO)]
        [InlineData('1', ThrowResult.ONE)]
        [InlineData('2', ThrowResult.TWO)]
        [InlineData('3', ThrowResult.THREE)]
        [InlineData('4', ThrowResult.FOUR)]
        [InlineData('5', ThrowResult.FIVE)]
        [InlineData('6', ThrowResult.SIX)]
        [InlineData('7', ThrowResult.SEVEN)]
        [InlineData('8', ThrowResult.EIGHT)]
        [InlineData('9', ThrowResult.NINE)]
        [InlineData('/', ThrowResult.SPARE)]
        [InlineData('X', ThrowResult.STRIKE)]
        public void ConvertThrowResultToCharShouldReturnLogicalValue(char expectedResult, ThrowResult resultToConvert)
        {
            Assert.Equal(expectedResult, resultToConvert.ToChar());
        }
    }
}
