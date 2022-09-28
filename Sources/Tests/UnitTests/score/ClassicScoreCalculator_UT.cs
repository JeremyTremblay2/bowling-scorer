﻿using Model.exceptions;
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
        public static IEnumerable<object[]> Data_UpdateFromFrameClassic()
        {
            yield return new object[] {
                false,
                new List<AFrame>
                {
                    new ClassicFrame(1, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(2, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(3, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(4, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(5, ThrowResult.TREE, ThrowResult.TWO),
                    new ClassicFrame(6, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(7, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(8, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(9, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicLastFrame(10, ThrowResult.ONE, ThrowResult.TWO, ThrowResult.NONE)
                },
                4,
                new List<int>{ 0, 0, 3, 3, 5, 0, 0, 0, 0, 0 }
            };
            yield return new object[] {
                false,
                new List<AFrame>
                {
                    new ClassicFrame(1, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(2, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(3, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(4, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(5, ThrowResult.TREE, ThrowResult.SPAIR),
                    new ClassicFrame(6, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(7, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(8, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(9, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicLastFrame(10, ThrowResult.ONE, ThrowResult.TWO, ThrowResult.NONE)
                },
                4,
                new List<int>{ 0, 0, 3, 3, 11, 0, 0, 0, 0, 0 }
            };
            yield return new object[] {
                false,
                new List<AFrame>
                {
                    new ClassicFrame(1, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(2, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(3, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(4, ThrowResult.ONE, ThrowResult.SPAIR),
                    new ClassicFrame(5, ThrowResult.TREE, ThrowResult.TWO),
                    new ClassicFrame(6, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(7, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(8, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(9, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicLastFrame(10, ThrowResult.ONE, ThrowResult.TWO, ThrowResult.NONE)
                },
                4,
                new List<int>{ 0, 0, 3, 13, 5, 0, 0, 0, 0, 0 }
            };
            yield return new object[] {
                false,
                new List<AFrame>
                {
                    new ClassicFrame(1, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(2, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(3, ThrowResult.ONE, ThrowResult.SPAIR),
                    new ClassicFrame(4, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(5, ThrowResult.TREE, ThrowResult.TWO),
                    new ClassicFrame(6, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(7, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(8, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(9, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicLastFrame(10, ThrowResult.ONE, ThrowResult.TWO, ThrowResult.NONE)
                },
                4,
                new List<int>{ 0, 0, 11, 3, 5, 0, 0, 0, 0, 0 }
            };
            yield return new object[] {
                false,
                new List<AFrame>
                {
                    new ClassicFrame(1, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(2, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(3, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(4, ThrowResult.ONE, ThrowResult.SPAIR),
                    new ClassicFrame(5, ThrowResult.TREE, ThrowResult.SPAIR),
                    new ClassicFrame(6, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(7, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(8, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(9, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicLastFrame(10, ThrowResult.ONE, ThrowResult.TWO, ThrowResult.NONE)
                },
                4,
                new List<int>{ 0, 0, 3, 13, 11, 0, 0, 0, 0, 0 }
            };
            yield return new object[] {
                false,
                new List<AFrame>
                {
                    new ClassicFrame(1, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(2, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(3, ThrowResult.ONE, ThrowResult.SPAIR),
                    new ClassicFrame(4, ThrowResult.ONE, ThrowResult.SPAIR),
                    new ClassicFrame(5, ThrowResult.TREE, ThrowResult.SPAIR),
                    new ClassicFrame(6, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(7, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(8, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(9, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicLastFrame(10, ThrowResult.ONE, ThrowResult.TWO, ThrowResult.NONE)
                },
                4,
                new List<int>{ 0, 0, 11, 13, 11, 0, 0, 0, 0, 0 }
            };
            yield return new object[] {
                false,
                new List<AFrame>
                {
                    new ClassicFrame(1, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(2, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(3, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(4, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(5, ThrowResult.NONE, ThrowResult.STRIKE),
                    new ClassicFrame(6, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(7, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(8, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(9, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicLastFrame(10, ThrowResult.ONE, ThrowResult.TWO, ThrowResult.NONE)
                },
                4,
                new List<int>{ 0, 0, 3, 3, 13, 0, 0, 0, 0, 0 }
            };
            yield return new object[] {
                false,
                new List<AFrame>
                {
                    new ClassicFrame(1, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(2, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(3, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(4, ThrowResult.NONE, ThrowResult.STRIKE),
                    new ClassicFrame(5, ThrowResult.TREE, ThrowResult.TWO),
                    new ClassicFrame(6, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(7, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(8, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(9, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicLastFrame(10, ThrowResult.ONE, ThrowResult.TWO, ThrowResult.NONE)
                },
                4,
                new List<int>{ 0, 0, 3, 15, 5, 0, 0, 0, 0, 0 }
            };
            yield return new object[] {
                false,
                new List<AFrame>
                {
                    new ClassicFrame(1, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(2, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(3, ThrowResult.NONE, ThrowResult.STRIKE),
                    new ClassicFrame(4, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(5, ThrowResult.TREE, ThrowResult.TWO),
                    new ClassicFrame(6, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(7, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(8, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(9, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicLastFrame(10, ThrowResult.ONE, ThrowResult.TWO, ThrowResult.NONE)
                },
                4,
                new List<int>{ 0, 0, 13, 3, 5, 0, 0, 0, 0, 0 }
            };
            yield return new object[] {
                false,
                new List<AFrame>
                {
                    new ClassicFrame(1, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(2, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(3, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(4, ThrowResult.ONE, ThrowResult.STRIKE),
                    new ClassicFrame(5, ThrowResult.NONE, ThrowResult.STRIKE),
                    new ClassicFrame(6, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(7, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(8, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(9, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicLastFrame(10, ThrowResult.ONE, ThrowResult.TWO, ThrowResult.NONE)
                },
                4,
                new List<int>{ 0, 0, 3, 21, 13, 0, 0, 0, 0, 0 }
            };
            yield return new object[] {
                false,
                new List<AFrame>
                {
                    new ClassicFrame(1, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(2, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(3, ThrowResult.ONE, ThrowResult.STRIKE),
                    new ClassicFrame(4, ThrowResult.ONE, ThrowResult.STRIKE),
                    new ClassicFrame(5, ThrowResult.NONE, ThrowResult.STRIKE),
                    new ClassicFrame(6, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(7, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(8, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(9, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicLastFrame(10, ThrowResult.ONE, ThrowResult.TWO, ThrowResult.NONE)
                },
                4,
                new List<int>{ 0, 0, 30, 21, 13, 0, 0, 0, 0, 0 }
            };

        }

        [Theory]
        [MemberData(nameof(Data_UpdateFromFrameClassic))]
        public void Test_UpdateFromFrameClassic(bool throwExep, List<AFrame> scoreBoard, int indexToBeUpdated, List<int> exceptedScoresValue)
        {
            IScoreCalculator calculator = new ClassicScoreCalculator();
            if (throwExep)
            {
                Assert.Throws<MissingFrameException>(() => { calculator.UpdateFromFrame(indexToBeUpdated, scoreBoard); });
                return;
            }
            calculator.UpdateFromFrame(indexToBeUpdated, scoreBoard);
            for(int i = 0; i < exceptedScoresValue.Count - 1; i++)
            {
                Assert.Equal(exceptedScoresValue[i], scoreBoard[i].ScoreValue);
            }
        }

        public static IEnumerable<object[]> Data_UpdateFromFrameClassicMaybeLast()
        {
            yield return new object[] {
                false,
                new List<AFrame>
                {
                    new ClassicFrame(1, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(2, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(3, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(4, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(5, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(6, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(7, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(8, ThrowResult.NONE, ThrowResult.STRIKE),
                    new ClassicFrame(9, ThrowResult.NONE, ThrowResult.STRIKE),
                    new ClassicLastFrame(10, ThrowResult.ONE, ThrowResult.TWO, ThrowResult.NONE)
                },
                7,
                new List<int>{ 0, 0, 0, 0, 0, 3, 3, 21, 0, 0 }
            };
            yield return new object[] {
                false,
                new List<AFrame>
                {
                    new ClassicFrame(1, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(2, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(3, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(4, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(5, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(6, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(7, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(8, ThrowResult.NONE, ThrowResult.STRIKE),
                    new ClassicFrame(9, ThrowResult.NONE, ThrowResult.STRIKE),
                    new ClassicLastFrame(10, ThrowResult.STRIKE, ThrowResult.TWO, ThrowResult.NONE)
                },
                7,
                new List<int>{ 0, 0, 0, 0, 0, 3, 3, 30, 0, 0 }
            };
            yield return new object[] {
                false,
                new List<AFrame>
                {
                    new ClassicFrame(1, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(2, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(3, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(4, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(5, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(6, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(7, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(8, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(9, ThrowResult.NONE, ThrowResult.STRIKE),
                    new ClassicLastFrame(10, ThrowResult.STRIKE, ThrowResult.STRIKE, ThrowResult.FIVE)
                },
                8,
                new List<int>{ 0, 0, 0, 0, 0, 0, 3, 3, 30, 0 }
            };
            yield return new object[] {
                false,
                new List<AFrame>
                {
                    new ClassicFrame(1, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(2, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(3, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(4, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(5, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(6, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(7, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(8, ThrowResult.ONE, ThrowResult.TWO),
                    new ClassicFrame(9, ThrowResult.NONE, ThrowResult.SPAIR),
                    new ClassicLastFrame(10, ThrowResult.STRIKE, ThrowResult.STRIKE, ThrowResult.FIVE)
                },
                8,
                new List<int>{ 0, 0, 0, 0, 0, 0, 3, 3, 20, 0 }
            };
        }

        [Theory]
        [MemberData(nameof(Data_UpdateFromFrameClassicMaybeLast))]
        public void Test_UpdateFromFrameClassicMaybeLast(bool throwExep, List<AFrame> scoreBoard, int indexToBeUpdated, List<int> exceptedScoresValue)
        {
            IScoreCalculator calculator = new ClassicScoreCalculator();
            if (throwExep)
            {
                Assert.Throws<MissingFrameException>(() => { calculator.UpdateFromFrame(indexToBeUpdated, scoreBoard); });
                return;
            }
            calculator.UpdateFromFrame(indexToBeUpdated, scoreBoard);
            for (int i = 0; i < exceptedScoresValue.Count - 1; i++)
            {
                Assert.Equal(exceptedScoresValue[i], scoreBoard[i].ScoreValue);
            }
        }

        [Fact]
        public void Test_CalculateScore()
        {
            List<AFrame> scoreBoard = new List<AFrame>
                {
                    new ClassicFrame(1),
                    new ClassicFrame(2),
                    new ClassicFrame(3),
                    new ClassicFrame(4),
                    new ClassicFrame(5),
                    new ClassicFrame(6),
                    new ClassicFrame(7),
                    new ClassicFrame(8),
                    new ClassicFrame(9),
                    new ClassicLastFrame(10)
                };
            List<int> exceptedResults = new List<int> { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };
            foreach (AFrame score in scoreBoard)
            {
                score.ScoreValue = 10;
            }
            IScoreCalculator calculator = new ClassicScoreCalculator();
            calculator.CalculateScore(scoreBoard);
            for (int i = 0; i < exceptedResults.Count - 1; i++)
            {
                Assert.Equal(exceptedResults[i], scoreBoard[i].CumulativeScore);
            }
        }
    }
}
