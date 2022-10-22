using FrameModel.Frame.ThrowResults;
using Model.Score;
using Model.Score.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Threading.Tasks;

namespace UnitTests.Score.Rules
{
    /// <summary>
    /// Represent data for unit testing the ClassicFullScoreTableDetector class.
    /// </summary>
    public class ClassicFullScoreTableDetectorDataTest
    {
        public static IEnumerable<object[]> Data_ScoreTables()
        {
            // Create a null score table.
            yield return new object[] {
                false,
                null,
            };

            // Insert a correct score table.
            yield return new object[] {
                true,
                new ScoreTable(new ClassicRules()).ToScoreTable(new ThrowResult[][]
                {
                    new ThrowResult[]
                    {
                        ThrowResult.TWO,
                        ThrowResult.ONE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.ZERO,
                        ThrowResult.EIGHT,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.FOUR,
                        ThrowResult.TWO,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.NONE,
                        ThrowResult.STRIKE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.TWO,
                        ThrowResult.SEVEN,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.NINE,
                        ThrowResult.SPARE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.FOUR,
                        ThrowResult.THREE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.SEVEN,
                        ThrowResult.ONE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.EIGHT,
                        ThrowResult.ZERO,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.STRIKE,
                        ThrowResult.STRIKE,
                        ThrowResult.TWO,
                    },
                }),
            };

            // Incorrect because of the second frame which is empty.
            yield return new object[] {
                false,
                new ScoreTable(new ClassicRules()).ToScoreTable(new ThrowResult[][]
                {
                    new ThrowResult[]
                    {
                        ThrowResult.TWO,
                        ThrowResult.ONE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.NONE,
                        ThrowResult.NONE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.FOUR,
                        ThrowResult.TWO,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.NONE,
                        ThrowResult.STRIKE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.TWO,
                        ThrowResult.SEVEN,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.NINE,
                        ThrowResult.SPARE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.FOUR,
                        ThrowResult.THREE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.SEVEN,
                        ThrowResult.ONE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.EIGHT,
                        ThrowResult.ZERO,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.STRIKE,
                        ThrowResult.STRIKE,
                        ThrowResult.TWO,
                    },
                }),
            };

            // Incorrect because of the third frame which has a NONE value in the first box.
            yield return new object[] {
                false,
                new ScoreTable(new ClassicRules()).ToScoreTable(new ThrowResult[][]
                {
                    new ThrowResult[]
                    {
                        ThrowResult.TWO,
                        ThrowResult.ONE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.ZERO,
                        ThrowResult.EIGHT,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.NONE,
                        ThrowResult.TWO,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.NONE,
                        ThrowResult.STRIKE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.TWO,
                        ThrowResult.SEVEN,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.NINE,
                        ThrowResult.SPARE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.FOUR,
                        ThrowResult.THREE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.SEVEN,
                        ThrowResult.ONE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.EIGHT,
                        ThrowResult.ZERO,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.STRIKE,
                        ThrowResult.STRIKE,
                        ThrowResult.TWO,
                    },
                }),
            };

            // Incorrect because of the third frame which has a NONE value in the second box.
            yield return new object[] {
                false,
                new ScoreTable(new ClassicRules()).ToScoreTable(new ThrowResult[][]
                {
                    new ThrowResult[]
                    {
                        ThrowResult.TWO,
                        ThrowResult.ONE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.ZERO,
                        ThrowResult.EIGHT,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.FOUR,
                        ThrowResult.NONE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.NONE,
                        ThrowResult.STRIKE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.TWO,
                        ThrowResult.SEVEN,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.NINE,
                        ThrowResult.SPARE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.FOUR,
                        ThrowResult.THREE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.SEVEN,
                        ThrowResult.ONE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.EIGHT,
                        ThrowResult.ZERO,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.STRIKE,
                        ThrowResult.STRIKE,
                        ThrowResult.TWO,
                    },
                }),
            };

            // Incorrect because of the last frame which is empty.
            yield return new object[] {
                false,
                new ScoreTable(new ClassicRules()).ToScoreTable(new ThrowResult[][]
                {
                    new ThrowResult[]
                    {
                        ThrowResult.TWO,
                        ThrowResult.ONE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.ZERO,
                        ThrowResult.EIGHT,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.FOUR,
                        ThrowResult.TWO,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.NONE,
                        ThrowResult.STRIKE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.TWO,
                        ThrowResult.SEVEN,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.NINE,
                        ThrowResult.SPARE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.FOUR,
                        ThrowResult.THREE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.SEVEN,
                        ThrowResult.ONE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.EIGHT,
                        ThrowResult.ZERO,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.NONE,
                        ThrowResult.NONE,
                        ThrowResult.NONE,
                    },
                }),
            };

            // Incorrect because of the last frame which has first box at NONE.
            yield return new object[] {
                false,
                new ScoreTable(new ClassicRules()).ToScoreTable(new ThrowResult[][]
                {
                    new ThrowResult[]
                    {
                        ThrowResult.TWO,
                        ThrowResult.ONE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.ZERO,
                        ThrowResult.EIGHT,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.FOUR,
                        ThrowResult.TWO,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.NONE,
                        ThrowResult.STRIKE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.TWO,
                        ThrowResult.SEVEN,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.NINE,
                        ThrowResult.SPARE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.FOUR,
                        ThrowResult.THREE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.SEVEN,
                        ThrowResult.ONE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.EIGHT,
                        ThrowResult.ZERO,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.NONE,
                        ThrowResult.STRIKE,
                        ThrowResult.TWO,
                    },
                }),
            };

            // Incorrect because of the last frame which has second box at NONE.
            yield return new object[] {
                false,
                new ScoreTable(new ClassicRules()).ToScoreTable(new ThrowResult[][]
                {
                    new ThrowResult[]
                    {
                        ThrowResult.TWO,
                        ThrowResult.ONE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.ZERO,
                        ThrowResult.EIGHT,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.FOUR,
                        ThrowResult.TWO,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.NONE,
                        ThrowResult.STRIKE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.TWO,
                        ThrowResult.SEVEN,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.NINE,
                        ThrowResult.SPARE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.FOUR,
                        ThrowResult.THREE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.SEVEN,
                        ThrowResult.ONE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.EIGHT,
                        ThrowResult.ZERO,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.STRIKE,
                        ThrowResult.NONE,
                        ThrowResult.TWO,
                    },
                }),
            };

            // Incorrect because of the last frame which has third box at NONE but it has a STRIKE.
            yield return new object[] {
                false,
                new ScoreTable(new ClassicRules()).ToScoreTable(new ThrowResult[][]
                {
                    new ThrowResult[]
                    {
                        ThrowResult.TWO,
                        ThrowResult.ONE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.ZERO,
                        ThrowResult.EIGHT,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.FOUR,
                        ThrowResult.TWO,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.NONE,
                        ThrowResult.STRIKE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.TWO,
                        ThrowResult.SEVEN,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.NINE,
                        ThrowResult.SPARE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.FOUR,
                        ThrowResult.THREE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.SEVEN,
                        ThrowResult.ONE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.EIGHT,
                        ThrowResult.ZERO,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.ZERO,
                        ThrowResult.STRIKE,
                        ThrowResult.NONE,
                    },
                }),
            };

            // A correct configuration with a spare at the last frame.
            yield return new object[] {
                true,
                new ScoreTable(new ClassicRules()).ToScoreTable(new ThrowResult[][]
                {
                    new ThrowResult[]
                    {
                        ThrowResult.TWO,
                        ThrowResult.ONE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.ZERO,
                        ThrowResult.EIGHT,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.FOUR,
                        ThrowResult.TWO,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.NONE,
                        ThrowResult.STRIKE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.TWO,
                        ThrowResult.SEVEN,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.NINE,
                        ThrowResult.SPARE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.FOUR,
                        ThrowResult.THREE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.SEVEN,
                        ThrowResult.ONE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.EIGHT,
                        ThrowResult.ZERO,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.THREE,
                        ThrowResult.SPARE,
                        ThrowResult.FIVE,
                    },
                }),
            };

            // An incorrect configuration with a strike at first throw but with the second box empty.
            yield return new object[] {
                false,
                new ScoreTable(new ClassicRules()).ToScoreTable(new ThrowResult[][]
                {
                    new ThrowResult[]
                    {
                        ThrowResult.TWO,
                        ThrowResult.ONE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.ZERO,
                        ThrowResult.EIGHT,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.FOUR,
                        ThrowResult.TWO,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.NONE,
                        ThrowResult.STRIKE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.TWO,
                        ThrowResult.SEVEN,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.NINE,
                        ThrowResult.SPARE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.FOUR,
                        ThrowResult.THREE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.SEVEN,
                        ThrowResult.ONE,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.EIGHT,
                        ThrowResult.ZERO,
                    },
                    new ThrowResult[]
                    {
                        ThrowResult.STRIKE,
                        ThrowResult.TWO,
                        ThrowResult.NONE,
                    },
                }),
            };
        }
    }
}
