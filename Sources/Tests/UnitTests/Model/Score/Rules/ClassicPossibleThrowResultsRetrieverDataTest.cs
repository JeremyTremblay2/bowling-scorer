using FrameModel.Frame;
using FrameModel.Frame.ThrowResults;
using Model.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Score.Rules
{
    /// <summary>
    /// Represent data for unit testing the ClassicPossibleThrowResultsRetriever class.
    /// </summary>
    public class ClassicPossibleThrowResultsRetrieverDataTest
    {
        public static IEnumerable<object[]> Data_GetPossibleResults()
        {
            // Insert in the first box, in an null frame.
            yield return new object[] {
                Array.Empty<ThrowResult>(),
                null,
                0
            };

            // Insert in the first box, in an empty frame at wrong index.
            yield return new object[] {
                Array.Empty<ThrowResult>(),
                new ClassicFrame(2, ThrowResult.NONE, ThrowResult.NONE),
                -1
            };

            // Insert in the first box, in an empty frame at wrong index.
            yield return new object[] {
                Array.Empty<ThrowResult>(),
                new ClassicFrame(2, ThrowResult.NONE, ThrowResult.NONE),
                4
            };

            // Insert in the first box, in an empty frame.
            yield return new object[] {
                new ThrowResult[]
                {
                    ThrowResult.NONE,
                    ThrowResult.ZERO,
                    ThrowResult.ONE,
                    ThrowResult.TWO,
                    ThrowResult.THREE,
                    ThrowResult.FOUR,
                    ThrowResult.FIVE,
                    ThrowResult.SIX,
                    ThrowResult.SEVEN,
                    ThrowResult.EIGHT,
                    ThrowResult.NINE,
                },
                new ClassicFrame(2, ThrowResult.NONE, ThrowResult.NONE),
                0
            };

            // Insert in the first box, in a frame with the second throw as 0.
            yield return new object[] {
                new ThrowResult[]
                {
                    ThrowResult.NONE,
                    ThrowResult.ZERO,
                    ThrowResult.ONE,
                    ThrowResult.TWO,
                    ThrowResult.THREE,
                    ThrowResult.FOUR,
                    ThrowResult.FIVE,
                    ThrowResult.SIX,
                    ThrowResult.SEVEN,
                    ThrowResult.EIGHT,
                    ThrowResult.NINE,
                },
                new ClassicFrame(2, ThrowResult.NONE, ThrowResult.ZERO),
                0
            };

            // Insert in the first box, in a frame with the second throw as a strike.
            yield return new object[] {
                new ThrowResult[]
                {
                    ThrowResult.NONE,
                },
                new ClassicFrame(2, ThrowResult.NONE, ThrowResult.STRIKE),
                0
            };

            // Insert in the first box, in a frame with the second throw as a spare.
            yield return new object[] {
                new ThrowResult[]
                {
                    ThrowResult.NONE,
                    ThrowResult.ZERO,
                    ThrowResult.ONE,
                    ThrowResult.TWO,
                    ThrowResult.THREE,
                    ThrowResult.FOUR,
                    ThrowResult.FIVE,
                    ThrowResult.SIX,
                    ThrowResult.SEVEN,
                    ThrowResult.EIGHT,
                    ThrowResult.NINE,
                },
                new ClassicFrame(2, ThrowResult.NONE, ThrowResult.SPARE),
                0
            };

            // Insert in the first box, in a frame with the second throw as nine.
            yield return new object[] {
                new ThrowResult[]
                {
                    ThrowResult.NONE,
                },
                new ClassicFrame(2, ThrowResult.NONE, ThrowResult.NINE),
                0
            };

            // Insert in the first box, in a frame with the second throw as 4.
            yield return new object[] {
                new ThrowResult[]
                {
                    ThrowResult.NONE,
                    ThrowResult.ZERO,
                    ThrowResult.ONE,
                    ThrowResult.TWO,
                    ThrowResult.THREE,
                    ThrowResult.FOUR,
                    ThrowResult.FIVE,
                },
                new ClassicFrame(2, ThrowResult.NONE, ThrowResult.FOUR),
                0
            };

            // Insert in the first box, in a frame with the second throw as 8.
            yield return new object[] {
                new ThrowResult[]
                {
                    ThrowResult.NONE,
                    ThrowResult.ZERO,
                    ThrowResult.ONE,
                },
                new ClassicFrame(2, ThrowResult.NONE, ThrowResult.EIGHT),
                0
            };

            // Insert in the second box, in a frame.
            yield return new object[] {
                new ThrowResult[]
                {
                    ThrowResult.NONE,
                    ThrowResult.ZERO,
                    ThrowResult.ONE,
                    ThrowResult.TWO,
                    ThrowResult.THREE,
                    ThrowResult.FOUR,
                    ThrowResult.FIVE,
                    ThrowResult.SIX,
                    ThrowResult.SEVEN,
                    ThrowResult.EIGHT,
                    ThrowResult.NINE,
                    ThrowResult.STRIKE,
                },
                new ClassicFrame(2, ThrowResult.NONE, ThrowResult.NONE),
                1
            };

            // Insert in the second box, in a frame.
            yield return new object[] {
                new ThrowResult[]
                {
                    ThrowResult.NONE,
                    ThrowResult.ZERO,
                    ThrowResult.ONE,
                    ThrowResult.TWO,
                    ThrowResult.THREE,
                    ThrowResult.FOUR,
                    ThrowResult.FIVE,
                    ThrowResult.SIX,
                    ThrowResult.SEVEN,
                    ThrowResult.EIGHT,
                    ThrowResult.NINE,
                    ThrowResult.SPARE,
                },
                new ClassicFrame(2, ThrowResult.ZERO, ThrowResult.NONE),
                1
            };

            // Insert in the second box, in a frame.
            yield return new object[] {
                new ThrowResult[]
                {
                    ThrowResult.NONE,
                    ThrowResult.ZERO,
                    ThrowResult.ONE,
                    ThrowResult.TWO,
                    ThrowResult.THREE,
                    ThrowResult.FOUR,
                    ThrowResult.FIVE,
                    ThrowResult.SIX,
                    ThrowResult.SPARE,
                },
                new ClassicFrame(2, ThrowResult.THREE, ThrowResult.NONE),
                1
            };

            // Insert in the second box, in a frame.
            yield return new object[] {
                new ThrowResult[]
                {
                    ThrowResult.NONE,
                    ThrowResult.ZERO,
                    ThrowResult.SPARE,
                },
                new ClassicFrame(2, ThrowResult.NINE, ThrowResult.NONE),
                1
            };

            // -------- Part with ClassicLastFrame ----------

            // Insert in the first box, in a frame with wrong index.
            yield return new object[] {
                Array.Empty<ThrowResult>(),
                new ClassicLastFrame(2, ThrowResult.NONE, ThrowResult.NONE, ThrowResult.NONE),
                -1
            };

            // Insert in the first box, in a frame with wrong index.
            yield return new object[] {
                Array.Empty<ThrowResult>(),
                new ClassicLastFrame(2, ThrowResult.NONE, ThrowResult.NONE, ThrowResult.NONE),
                3
            };

            // Insert in the first box, in a frame.
            yield return new object[] {
                new ThrowResult[]
                {
                    ThrowResult.NONE,
                    ThrowResult.ZERO,
                    ThrowResult.ONE,
                    ThrowResult.TWO,
                    ThrowResult.THREE,
                    ThrowResult.FOUR,
                    ThrowResult.FIVE,
                    ThrowResult.SIX,
                    ThrowResult.SEVEN,
                    ThrowResult.EIGHT,
                    ThrowResult.NINE,
                    ThrowResult.STRIKE,
                },
                new ClassicLastFrame(2, ThrowResult.NONE, ThrowResult.NONE, ThrowResult.NONE),
                0
            };

            // Insert in the first box, in a frame.
            yield return new object[] {
                new ThrowResult[]
                {
                    ThrowResult.NONE,
                    ThrowResult.ZERO,
                    ThrowResult.ONE,
                    ThrowResult.TWO,
                    ThrowResult.THREE,
                    ThrowResult.FOUR,
                    ThrowResult.FIVE,
                    ThrowResult.SIX,
                    ThrowResult.SEVEN,
                    ThrowResult.EIGHT,
                    ThrowResult.NINE,
                    ThrowResult.STRIKE,
                },
                new ClassicLastFrame(2, ThrowResult.SIX, ThrowResult.NONE, ThrowResult.NONE),
                0
            };

            // Insert in the second box, in a frame.
            yield return new object[] {
                new ThrowResult[]
                {
                    ThrowResult.NONE,
                    ThrowResult.ZERO,
                    ThrowResult.ONE,
                    ThrowResult.TWO,
                    ThrowResult.THREE,
                    ThrowResult.FOUR,
                    ThrowResult.FIVE,
                    ThrowResult.SIX,
                    ThrowResult.SEVEN,
                    ThrowResult.EIGHT,
                    ThrowResult.NINE,
                    ThrowResult.STRIKE,
                },
                new ClassicLastFrame(2, ThrowResult.NONE, ThrowResult.NONE, ThrowResult.NONE),
                1
            };

            // Insert in the second box, in a frame.
            yield return new object[] {
                new ThrowResult[]
                {
                    ThrowResult.NONE,
                    ThrowResult.ZERO,
                    ThrowResult.ONE,
                    ThrowResult.TWO,
                    ThrowResult.THREE,
                    ThrowResult.FOUR,
                    ThrowResult.FIVE,
                    ThrowResult.SIX,
                    ThrowResult.SEVEN,
                    ThrowResult.EIGHT,
                    ThrowResult.NINE,
                    ThrowResult.STRIKE,
                },
                new ClassicLastFrame(2, ThrowResult.STRIKE, ThrowResult.NONE, ThrowResult.NONE),
                1
            };

            // Insert in the second box, in a frame.
            yield return new object[] {
                new ThrowResult[]
                {
                    ThrowResult.NONE,
                    ThrowResult.ZERO,
                    ThrowResult.ONE,
                    ThrowResult.TWO,
                    ThrowResult.THREE,
                    ThrowResult.FOUR,
                    ThrowResult.FIVE,
                    ThrowResult.SIX,
                    ThrowResult.SEVEN,
                    ThrowResult.SPARE,
                },
                new ClassicLastFrame(2, ThrowResult.TWO, ThrowResult.NONE, ThrowResult.NONE),
                1
            };

            // Insert in the second box, in a frame.
            yield return new object[] {
                new ThrowResult[]
                {
                    ThrowResult.NONE,
                    ThrowResult.ZERO,
                    ThrowResult.SPARE,
                },
                new ClassicLastFrame(2, ThrowResult.NINE, ThrowResult.NONE, ThrowResult.NONE),
                1
            };

            // Insert in the second box, in a frame.
            yield return new object[] {
                new ThrowResult[]
                {
                    ThrowResult.NONE,
                    ThrowResult.ZERO,
                    ThrowResult.ONE,
                    ThrowResult.TWO,
                    ThrowResult.THREE,
                    ThrowResult.FOUR,
                    ThrowResult.FIVE,
                    ThrowResult.SIX,
                    ThrowResult.SEVEN,
                    ThrowResult.EIGHT,
                    ThrowResult.NINE,
                    ThrowResult.SPARE,
                },
                new ClassicLastFrame(2, ThrowResult.ZERO, ThrowResult.NONE, ThrowResult.NONE),
                1
            };

            // Insert in the third box, in a frame. SHould not add because of we don't have a strike or spare in the first two throws.
            yield return new object[] {
                new ThrowResult[]
                {
                    ThrowResult.NONE,
                },
                new ClassicLastFrame(2, ThrowResult.ZERO, ThrowResult.SIX, ThrowResult.NONE),
                2
            };

            // Insert in the third box, in a frame. SHould not add because of we don't have a strike or spare in the first two throws.
            yield return new object[] {
                new ThrowResult[]
                {
                    ThrowResult.NONE,
                },
                new ClassicLastFrame(2, ThrowResult.NINE, ThrowResult.NINE, ThrowResult.NONE),
                2
            };

            // Insert in the third box, in a frame. SHould not add because of we don't have a strike or spare in the first two throws.
            yield return new object[] {
                new ThrowResult[]
                {
                    ThrowResult.NONE,
                },
                new ClassicLastFrame(2, ThrowResult.THREE, ThrowResult.SIX, ThrowResult.NONE),
                2
            };

            // Insert in the third box, in a frame. SHould not add because of we don't have a strike or spare in the first two throws.
            yield return new object[] {
                new ThrowResult[]
                {
                    ThrowResult.NONE,
                },
                new ClassicLastFrame(2, ThrowResult.TWO, ThrowResult.ONE, ThrowResult.NONE),
                2
            };

            // Insert in the third box, in a frame. SHould work because of the strike or spear.
            yield return new object[] {
                new ThrowResult[]
                {
                    ThrowResult.NONE,
                    ThrowResult.ZERO,
                    ThrowResult.ONE,
                    ThrowResult.TWO,
                    ThrowResult.THREE,
                    ThrowResult.FOUR,
                    ThrowResult.FIVE,
                    ThrowResult.SIX,
                    ThrowResult.SEVEN,
                    ThrowResult.EIGHT,
                    ThrowResult.NINE,
                    ThrowResult.STRIKE,
                },
                new ClassicLastFrame(2, ThrowResult.STRIKE, ThrowResult.SIX, ThrowResult.NONE),
                2
            };

            // Insert in the third box, in a frame. SHould work because of the strike or spear.
            yield return new object[] {
                new ThrowResult[]
                {
                    ThrowResult.NONE,
                    ThrowResult.ZERO,
                    ThrowResult.ONE,
                    ThrowResult.TWO,
                    ThrowResult.THREE,
                    ThrowResult.FOUR,
                    ThrowResult.FIVE,
                    ThrowResult.SIX,
                    ThrowResult.SEVEN,
                    ThrowResult.EIGHT,
                    ThrowResult.NINE,
                    ThrowResult.STRIKE,
                },
                new ClassicLastFrame(2, ThrowResult.FIVE, ThrowResult.SPARE, ThrowResult.NONE),
                2
            };

            // Insert in the third box, in a frame. SHould work because of the strike or spear.
            yield return new object[] {
                new ThrowResult[]
                {
                    ThrowResult.NONE,
                    ThrowResult.ZERO,
                    ThrowResult.ONE,
                    ThrowResult.TWO,
                    ThrowResult.THREE,
                    ThrowResult.FOUR,
                    ThrowResult.FIVE,
                    ThrowResult.SIX,
                    ThrowResult.SEVEN,
                    ThrowResult.EIGHT,
                    ThrowResult.NINE,
                    ThrowResult.STRIKE,
                },
                new ClassicLastFrame(2, ThrowResult.TWO, ThrowResult.STRIKE, ThrowResult.NONE),
                2
            };

            // Insert in the third box, in a frame. SHould work because of the strike or spear.
            yield return new object[] {
                new ThrowResult[]
                {
                    ThrowResult.NONE,
                    ThrowResult.ZERO,
                    ThrowResult.ONE,
                    ThrowResult.TWO,
                    ThrowResult.THREE,
                    ThrowResult.FOUR,
                    ThrowResult.FIVE,
                    ThrowResult.SIX,
                    ThrowResult.SEVEN,
                    ThrowResult.EIGHT,
                    ThrowResult.NINE,
                    ThrowResult.STRIKE,
                },
                new ClassicLastFrame(2, ThrowResult.STRIKE, ThrowResult.STRIKE, ThrowResult.NONE),
                2
            };
        }
    }
}
