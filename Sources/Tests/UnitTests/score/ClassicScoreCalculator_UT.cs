using Model.exceptions;
using Model.score;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.score
{
    public class ClassicScoreCalculator_UT
    {
        [Theory]
        [InlineData(false, 5, ThrowResult.ONE, ThrowResult.TWO, ThrowResult.NINE, ThrowResult.ZERO, ThrowResult.TWO, ThrowResult.TREE, ThrowResult.NINE, ThrowResult.ZERO, ThrowResult.TWO, ThrowResult.TREE)]
        [InlineData(false, 19, ThrowResult.ONE, ThrowResult.TWO, ThrowResult.NINE, ThrowResult.ZERO, ThrowResult.TWO, ThrowResult.SPAIR, ThrowResult.NINE, ThrowResult.ZERO, ThrowResult.TWO, ThrowResult.TREE)]
        [InlineData(false, 17, ThrowResult.ONE, ThrowResult.TWO, ThrowResult.NINE, ThrowResult.ZERO, ThrowResult.NONE, ThrowResult.STRIKE, ThrowResult.FOUR, ThrowResult.TREE, ThrowResult.TWO, ThrowResult.TREE)]
        [InlineData(false, 20, ThrowResult.ONE, ThrowResult.TWO, ThrowResult.NINE, ThrowResult.ZERO, ThrowResult.NONE, ThrowResult.STRIKE, ThrowResult.TWO, ThrowResult.SPAIR, ThrowResult.TWO, ThrowResult.TREE)]
        [InlineData(false, 22, ThrowResult.ONE, ThrowResult.TWO, ThrowResult.NINE, ThrowResult.ZERO, ThrowResult.NONE, ThrowResult.STRIKE, ThrowResult.NONE, ThrowResult.STRIKE, ThrowResult.TWO, ThrowResult.TREE)]
        [InlineData(false, 22, ThrowResult.ONE, ThrowResult.TWO, ThrowResult.NINE, ThrowResult.ZERO, ThrowResult.NONE, ThrowResult.STRIKE, ThrowResult.NONE, ThrowResult.STRIKE, ThrowResult.TWO, ThrowResult.SPAIR)]
        [InlineData(false, 30, ThrowResult.ONE, ThrowResult.TWO, ThrowResult.NINE, ThrowResult.ZERO, ThrowResult.NONE, ThrowResult.STRIKE, ThrowResult.NONE, ThrowResult.STRIKE, ThrowResult.NONE, ThrowResult.STRIKE)]
        public void TestUpdateFromFrame(bool throwException, int exceptedResult, ThrowResult f1Val1, ThrowResult f1Val2, ThrowResult f2Val1, ThrowResult f2Val2,
            ThrowResult f3Val1, ThrowResult f3Val2, ThrowResult f4Val1, ThrowResult f4Val2, ThrowResult f5Val1, ThrowResult f5Val2)
        {
            IScoreCalculator scoreCalculator = new ClassicScoreCalculator();
            Frame f1 = new Frame(2, 1);
            Frame f2 = new Frame(2, 2);
            Frame f3 = new Frame(2, 3);
            Frame f4 = new Frame(2, 4);
            Frame f5 = new Frame(2, 5);
            List<Frame> frames = new List<Frame>();

            f1.AddThrow(0, f1Val1);
            f1.AddThrow(1, f1Val2);
            f2.AddThrow(0, f2Val1);
            f2.AddThrow(1, f2Val2);
            f3.AddThrow(0, f3Val1);
            f3.AddThrow(1, f3Val2);
            f4.AddThrow(0, f4Val1);
            f4.AddThrow(1, f4Val2);
            f5.AddThrow(0, f5Val1);
            f5.AddThrow(1, f5Val2);
            frames.Add(f1);
            frames.Add(f2);
            frames.Add(f3);
            frames.Add(f4);
            frames.Add(f5);

            if (throwException)
            {
                Assert.Throws<MissingFrameException>(() => scoreCalculator.UpdateFromFrame(2, frames));
            }

            scoreCalculator.UpdateFromFrame(2, frames);
            Assert.Equal(exceptedResult, f3.ScoreValue);
        }
    }
}
