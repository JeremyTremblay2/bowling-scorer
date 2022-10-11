using Model.Exceptions;
using Model.Score;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Model.Score.Rules;
<<<<<<<< HEAD:Sources/Tests/UnitTests/score/Rules/Writers/ClassicLastFrameWriter_UT.cs
using FrameWriterModel.Frame.ThrowResults;
using FrameWriterModel.Frame;
using FrameWriterModel.Writer;
using FrameWriterModel.Exceptions;

namespace UnitTests.score.Rules.Writers
========
using FrameWriterModel.Frame.ThrowResults;
using FrameWriterModel.Frame;
using FrameWriterModel.Writer;
using FrameWriterModel.Exceptions;

namespace UnitTests.Score.Rules
>>>>>>>> master:Sources/Tests/UnitTests/Score/Rules/ClassicLastFrameWriter_UT.cs
{
    public class ClassicLastFrameWriter_UT
    {
        [Theory]
        [InlineData(false, ThrowResult.TWO, ThrowResult.TWO)]
        [InlineData(true, ThrowResult.SPARE, ThrowResult.NONE)]
        [InlineData(false, ThrowResult.STRIKE, ThrowResult.STRIKE)]
        public void Test_WriteFirstThrow(bool throwExcep, ThrowResult resultToWrite, ThrowResult exceptedWritenResult)
        {
            ClassicLastFrame classic = new ClassicLastFrame(1);
            AFrameWriter writer = new ClassicLastFrameWriter();
            if (throwExcep)
            {
                Assert.Throws<ForbiddenThrowResultException>(() => { writer.WriteValue(classic, 0, resultToWrite); });
                return;
            }
            writer.WriteValue(classic, 0, resultToWrite);
            Assert.Equal(exceptedWritenResult, classic.ThrowResults[0]);
        }

        [Fact]
        public void Test_WriteSecondThrow()
        {
            ClassicLastFrame classic = new ClassicLastFrame(1);
            AFrameWriter writer = new ClassicLastFrameWriter();
            writer.WriteValue(classic, 1, ThrowResult.TREE);
            Assert.Equal(ThrowResult.TREE, classic.ThrowResults[1]);
        }


        [Theory]
        [InlineData(false, ThrowResult.STRIKE, ThrowResult.STRIKE, ThrowResult.STRIKE, ThrowResult.STRIKE)]
        [InlineData(false, ThrowResult.STRIKE, ThrowResult.STRIKE, ThrowResult.NINE, ThrowResult.SPARE)]
        [InlineData(true, ThrowResult.STRIKE, ThrowResult.STRIKE, ThrowResult.TWO, ThrowResult.TWO)]
        [InlineData(true, ThrowResult.STRIKE, ThrowResult.STRIKE, ThrowResult.NONE, ThrowResult.NONE)]
        public void Test_WriteThridThrow(bool throwExcep, ThrowResult resultToWrite, ThrowResult exceptedWritenResult, ThrowResult firstSlotResult, ThrowResult secondSlotResult)
        {
            ClassicLastFrame classic = new ClassicLastFrame(1);
            AFrameWriter writer = new ClassicLastFrameWriter();
            writer.WriteValue(classic, 0, firstSlotResult);
            writer.WriteValue(classic, 1, secondSlotResult);
            if (throwExcep)
            {
                Assert.Throws<ForbiddenThrowResultException>(() => { writer.WriteValue(classic, 2, resultToWrite); });
                return;
            }
            writer.WriteValue(classic, 2, resultToWrite);
            Assert.Equal(exceptedWritenResult, classic.ThrowResults[2]);
        }
    }
}
