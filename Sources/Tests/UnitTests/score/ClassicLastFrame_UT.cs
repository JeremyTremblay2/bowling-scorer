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
        [InlineData(false, ThrowResult.TWO, ThrowResult.TWO)]
        [InlineData(true, ThrowResult.SPAIR, ThrowResult.NONE)]
        [InlineData(false, ThrowResult.STRIKE, ThrowResult.STRIKE)]
        public void Test_WriteFirstThrow(bool throwExcep, ThrowResult resultToWrite, ThrowResult exceptedWritenResult)
        {
            ClassicLastFrame classic = new ClassicLastFrame(1);
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
            ClassicLastFrame classic = new ClassicLastFrame(1);
            classic.WriteSecondThrow(ThrowResult.TREE);
            Assert.Equal(ThrowResult.TREE, classic.ThrowResults[1]);
        }


        [Theory]
        [InlineData(false, ThrowResult.STRIKE, ThrowResult.STRIKE, ThrowResult.STRIKE, ThrowResult.STRIKE)]
        [InlineData(false, ThrowResult.STRIKE, ThrowResult.STRIKE, ThrowResult.NINE, ThrowResult.SPAIR)]
        [InlineData(true, ThrowResult.STRIKE, ThrowResult.STRIKE, ThrowResult.TWO, ThrowResult.TWO)]
        [InlineData(true, ThrowResult.STRIKE, ThrowResult.STRIKE, ThrowResult.NONE, ThrowResult.NONE)]
        public void Test_WriteThridThrow(bool throwExcep, ThrowResult resultToWrite, ThrowResult exceptedWritenResult, ThrowResult firstSlotResult, ThrowResult secondSlotResult)
        {
            ClassicLastFrame classic = new ClassicLastFrame(1);
            classic.WriteFirstThrow(firstSlotResult);
            classic.WriteSecondThrow(secondSlotResult);
            if (throwExcep)
            {
                Assert.Throws<ForbiddenThrowResultException>(() => { classic.WriteThridThrow(resultToWrite); });
                return;
            }
            classic.WriteThridThrow(resultToWrite);
            Assert.Equal(exceptedWritenResult, classic.ThrowResults[2]);
        }
    }
}
