using FrameWriterModel.Frame.ThrowResults;
using Model.Players;
using Model.Score;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Threading.Tasks;
using Model.Score.Rules;
using Model.Games;
using FrameWriterModel.Frame;

namespace UnitTests.Games
{
    /// <summary>
    /// Class used to store data test for unit testing.
    /// </summary>
    public static class GameDataTest
    {
        /// <summary>
        /// Fill in a score table given in parameters with the throw results specified.
        /// </summary>
        /// <param name="scoreTable">The score table to fill in.</param>
        /// <param name="throwResults">The throw results to write into.</param>
        /// <returns>The score table updated.</returns>
        public static ScoreTable ToScoreTable(this ScoreTable scoreTable, ThrowResult[][] throwResults)
        {
            for (int i = 0; i < throwResults.Length; i++)
            {
                var currentFrame = throwResults[i];
                for (int j = 0; j < currentFrame.Length; j++)
                {
                    scoreTable.WriteValue(i, j, throwResults[i][j]);
                }
            }
            return scoreTable;
        }

        public static ScoreTable scoreTableComplete = new ScoreTable(new ClassicRules()).ToScoreTable(new ThrowResult[][]
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
            }
        });

        public static ScoreTable anotherScoreTableComplete = new ScoreTable(new ClassicRules()).ToScoreTable(new ThrowResult[][]
        {
            new ThrowResult[]
            {
                ThrowResult.THREE,
                ThrowResult.FIVE,
            },
            new ThrowResult[]
            {
                ThrowResult.TWO,
                ThrowResult.ZERO,
            },
            new ThrowResult[]
            {
                ThrowResult.FIVE,
                ThrowResult.SPARE,
            },
            new ThrowResult[]
            {
                ThrowResult.ZERO,
                ThrowResult.FOUR,
            },
            new ThrowResult[]
            {
                ThrowResult.ONE,
                ThrowResult.EIGHT,
            },
            new ThrowResult[]
            {
                ThrowResult.SEVEN,
                ThrowResult.ZERO,
            },
            new ThrowResult[]
            {
                ThrowResult.NONE,
                ThrowResult.STRIKE,
            },
            new ThrowResult[]
            {
                ThrowResult.THREE,
                ThrowResult.THREE,
            },
            new ThrowResult[]
            {
                ThrowResult.SIX,
                ThrowResult.ZERO,
            },
            new ThrowResult[]
            {
                ThrowResult.SEVEN,
                ThrowResult.SPARE,
                ThrowResult.FIVE,
            }
        });

        public static ScoreTable scoreTableNotComplete = new ScoreTable(new ClassicRules()).ToScoreTable(new ThrowResult[][]
        {
            new ThrowResult[]
            {
                ThrowResult.FOUR,
                ThrowResult.SPARE,
            },
            new ThrowResult[]
            {
                ThrowResult.NONE,
                ThrowResult.STRIKE,
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
                ThrowResult.NINE,
                ThrowResult.ZERO,
            },
            new ThrowResult[]
            {
                ThrowResult.ZERO,
                ThrowResult.SPARE,
            },
            new ThrowResult[]
            {
                ThrowResult.FIVE,
                ThrowResult.FOUR,
            },
            new ThrowResult[]
            {
                ThrowResult.SEVEN,
                ThrowResult.TWO,
            },
            new ThrowResult[]
            {
                ThrowResult.ZERO,
                ThrowResult.ZERO,
            },
            new ThrowResult[]
            {
                ThrowResult.NONE,
                ThrowResult.NONE,
                ThrowResult.NONE,
            }
        });

        public static IEnumerable<object[]> Data_AddPlayers()
        {
            // Add some players.
            yield return new object[] {
                new Player[]
                {
                    new Player(12, "Marlon", "marlonImage"),
                    new Player(45, "Vincent", "vincentImage"),
                    new Player(85, "Jas", "jasImage"),
                    new Player(75, "Pierre", "pierreImage"),
                },
                new Player[]
                {
                    new Player(12, "Marlon", "marlonImage"),
                    new Player(45, "Vincent", "vincentImage"),
                    new Player(85, "Jas", "jasImage"),
                    new Player(75, "Pierre", "pierreImage"),
                },
            };

            // Add some players with null values inside.
            yield return new object[] {
                new Player[]
                {
                    new Player(12, "Marlon", "marlonImage"),
                    new Player(45, "Vincent", "vincentImage"),
                    new Player(85, "Jas", "jasImage"),
                    new Player(75, "Pierre", "pierreImage"),
                },
                new Player[]
                {
                    new Player(12, "Marlon", "marlonImage"),
                    new Player(45, "Vincent", "vincentImage"),
                    null,
                    new Player(85, "Jas", "jasImage"),
                    new Player(75, "Pierre", "pierreImage"),
                    null,
                    null,
                },
            };

            // Add some players with same player many times.
            yield return new object[] {
                new Player[]
                {
                    new Player(87, "Marlon", "marlonImage"),
                    new Player(65, "Vincent", "vincentImage"),
                    new Player(25, "Jas", "jasImage"),
                    new Player(54, "Pierre", "pierreImage"),
                },
                new Player[]
                {
                    new Player(87, "Marlon", "marlonImage"),
                    new Player(65, "Vincent", "vincentImage"),
                    new Player(25, "Jas", "jasImage"),
                    new Player(87, "Leo", "leoImage"), // same as the 1st.
                    new Player(54, "Pierre", "pierreImage"),
                    new Player(65, "Robin", "robinImage"), // same as the second
                },
            };

            // Add some players with same player many times and null values.
            yield return new object[] {
                new Player[]
                {
                    new Player(78, "Marlon", "marlonImage"),
                    new Player(12, "Vincent", "vincentImage"),
                    new Player(23, "Jas", "jasImage"),
                    new Player(75, "Pierre", "pierreImage"),
                },
                new Player[]
                {
                    new Player(78, "Marlon", "marlonImage"),
                    new Player(78, "Leo", "leoImage"), // same as the 1st.
                    null,
                    new Player(12, "Vincent", "vincentImage"),
                    new Player(23, "Jas", "jasImage"),
                    null,
                    new Player(75, "Pierre", "pierreImage"),
                    null,
                    new Player(12, "Robin", "robinImage"), // same as the second
                },
            };
        }

        public static IEnumerable<object[]> Data_CreateGame()
        {
            // Create a game with two score tables and players. It should works.
            yield return new object[] {
                false,
                false,
                2,
                new Dictionary<Player, ScoreTable>
                {
                    { new Player(12, "Maya", "beeImage"), scoreTableComplete },
                    { new Player(52, "Toto", "totoImage"), anotherScoreTableComplete },
                },
                new ClassicRules(),
            };

            // Create a game with no rules, should not works.
            yield return new object[] {
                false,
                true,
                0,
                new Dictionary<Player, ScoreTable>
                {
                    { new Player(12, "Maya", "beeImage"), scoreTableComplete },
                    { new Player(52, "Toto", "totoImage"), anotherScoreTableComplete },
                },
                null,
            };

            // Create a game with a score table not complete, should works but with only two player and score table.
            yield return new object[] {
                false,
                false,
                2,
                new Dictionary<Player, ScoreTable>
                {
                    { new Player(12, "Maya", "beeImage"), scoreTableComplete },
                    { new Player(52, "Toto", "totoImage"), scoreTableNotComplete },
                },
                new ClassicRules(),
            };

            // Create a game with two score table not complete, should not works.
            yield return new object[] {
                false,
                false,
                2,
                new Dictionary<Player, ScoreTable>
                {
                    { new Player(12, "Maya", "beeImage"), scoreTableNotComplete },
                    { new Player(52, "Toto", "totoImage"), scoreTableNotComplete },
                },
                new ClassicRules(),
            };

            // Create a game with two score table complete and one not complete, should works.
            yield return new object[] {
                false,
                false,
                3,
                new Dictionary<Player, ScoreTable>
                {
                    { new Player(12, "Maya", "beeImage"), scoreTableComplete },
                    { new Player(52, "Toto", "totoImage"), anotherScoreTableComplete },
                    { new Player(22, "Toto", "totoImage"), scoreTableNotComplete },
                },
                new ClassicRules(),
            };
        }

        public static IEnumerable<object[]> Data_FinishGame()
        {
            yield return new object[] {
                false,
                new Dictionary<Player, ScoreTable>
                {
                    { new Player(12, "Maya", "beeImage"), scoreTableComplete },
                    { new Player(52, "Toto", "totoImage"), anotherScoreTableComplete },
                },
                new ClassicRules(),
            };

            yield return new object[] {
                true,
                new Dictionary<Player, ScoreTable>
                {
                    { new Player(12, "Maya", "beeImage"), scoreTableComplete },
                    { new Player(52, "Toto", "totoImage"), scoreTableNotComplete },
                },
                new ClassicRules(),
            };

            yield return new object[] {
                true,
                new Dictionary<Player, ScoreTable>
                {
                    { new Player(12, "Maya", "beeImage"), scoreTableComplete },
                    { new Player(52, "Toto", "totoImage"), anotherScoreTableComplete },
                    { new Player(22, "Toto", "totoImage"), scoreTableNotComplete },
                },
                new ClassicRules(),
            };
        }

        public static IEnumerable<object[]> Data_EqualsGame()
        {
            yield return new object[] {
                true,
                new Game(new ClassicRules(), new Dictionary<Player, ScoreTable>
                {
                    { new Player(12, "Maya", "beeImage"), scoreTableComplete },
                    { new Player(52, "Toto", "totoImage"), anotherScoreTableComplete },
                }, false),
                new Game(new ClassicRules(), new List<Player>
                {
                    new Player(23, "Jas", "jasImage"),
                    new Player(75, "Pierre", "pierreImage"),
                })
            };

            yield return new object[] {
                true,
                new Game(new ClassicRules(), 21, new List<Player>
                {
                    new Player(87, "Marlon", "marlonImage"),
                    new Player(65, "Vincent", "vincentImage"),
                    new Player(25, "Jas", "jasImage"),
                }),
                new Game(new ClassicRules(), 21, new List<Player>
                {
                    new Player(23, "Jas", "jasImage"),
                    new Player(75, "Pierre", "pierreImage"),
                })
            };

            yield return new object[] {
                false,
                new Game(new ClassicRules(), 21, new List<Player>
                {
                    new Player(87, "Marlon", "marlonImage"),
                    new Player(65, "Vincent", "vincentImage"),
                    new Player(25, "Jas", "jasImage"),
                }),
                new Game(new ClassicRules(), new List<Player>
                {
                    new Player(23, "Jas", "jasImage"),
                    new Player(75, "Pierre", "pierreImage"),
                })
            };

            yield return new object[] {
                false,
                new Game(new ClassicRules(), 21, new List<Player>
                {
                    new Player(87, "Marlon", "marlonImage"),
                    new Player(65, "Vincent", "vincentImage"),
                }),
                new Game(new ClassicRules(), 14, new List<Player>
                {
                    new Player(23, "Jas", "jasImage"),
                    new Player(75, "Pierre", "pierreImage"),
                    new Player(25, "Jas", "jasImage"),
                })
            };

            yield return new object[] {
                false,
                new Game(new ClassicRules(), 21, new List<Player>
                {
                    new Player(87, "Marlon", "marlonImage"),
                    new Player(65, "Vincent", "vincentImage"),
                }),
                "toto"
            };

            yield return new object[] {
                false,
                new Game(new ClassicRules(), 21, new List<Player>
                {
                    new Player(87, "Marlon", "marlonImage"),
                    new Player(65, "Vincent", "vincentImage"),
                }),
                null
            };

            Game game = new Game(new ClassicRules(), 21, new List<Player>
                {
                    new Player(87, "Marlon", "marlonImage"),
                    new Player(65, "Vincent", "vincentImage"),
                });

            yield return new object[] {
                true,
                game,
                game,
            };
        }

        public static IEnumerable<object[]> Data_AddResults()
        {
            yield return new object[] {
                false,
                true,
                new Game(new ClassicRules(), new List<Player>
                {
                    new Player(23, "Jas", "jasImage"),
                    new Player(75, "Pierre", "pierreImage"),
                }),
                0,
                ThrowResult.FOUR
            };

            yield return new object[] {
                false,
                true,
                new Game(new ClassicRules(), new List<Player>
                {
                    new Player(23, "Jas", "jasImage"),
                    new Player(75, "Pierre", "pierreImage"),
                }),
                0,
                ThrowResult.NINE
            };

            // Should not work because we can't write a strike in the first slot.
            yield return new object[] {
                true,
                false,
                new Game(new ClassicRules(), new List<Player>
                {
                    new Player(23, "Jas", "jasImage"),
                    new Player(75, "Pierre", "pierreImage"),
                }),
                0,
                ThrowResult.STRIKE
            };

            yield return new object[] {
                false,
                false,
                new Game(new ClassicRules(), new Dictionary<Player, ScoreTable>
                {
                    { new Player(12, "Maya", "beeImage"), scoreTableComplete },
                    { new Player(52, "Toto", "totoImage"), anotherScoreTableComplete },
                }, false),
                0,
                ThrowResult.NINE
            };

            yield return new object[] {
                true,
                false,
                new Game(new ClassicRules(), new Dictionary<Player, ScoreTable>
                {
                    { new Player(12, "Maya", "beeImage"), scoreTableComplete },
                    { new Player(52, "Toto", "totoImage"), anotherScoreTableComplete },
                }, true),
                0,
                ThrowResult.NINE
            };
        }

        public static IEnumerable<object[]> Data_EditResults()
        {
            yield return new object[] {
                false,
                true,
                new Game(new ClassicRules(), new Dictionary<Player, ScoreTable>
                {
                    { new Player(12, "Maya", "beeImage"), scoreTableComplete },
                    { new Player(52, "Toto", "totoImage"), anotherScoreTableComplete },
                }, false),
                new Player(12, "Maya", "beeImage"),
                new ClassicFrame(5, ThrowResult.FOUR, ThrowResult.FOUR),
            };

            // Should not works because of the null player.
            yield return new object[] {
                true,
                false,
                new Game(new ClassicRules(), new Dictionary<Player, ScoreTable>
                {
                    { new Player(12, "Maya", "beeImage"), scoreTableComplete },
                    { new Player(52, "Toto", "totoImage"), anotherScoreTableComplete },
                }, false),
                null,
                new ClassicFrame(5, ThrowResult.FOUR, ThrowResult.FOUR),
            };

            // Should not works because of the unkwon player.
            yield return new object[] {
                true,
                false,
                new Game(new ClassicRules(), new Dictionary<Player, ScoreTable>
                {
                    { new Player(12, "Maya", "beeImage"), scoreTableComplete },
                    { new Player(52, "Toto", "totoImage"), anotherScoreTableComplete },
                }, false),
                new Player(13, "Toto", "totoImage"), //unknow because of the ID.
                new ClassicFrame(5, ThrowResult.FOUR, ThrowResult.FOUR),
            };

            // Should not works because of the null frame.
            yield return new object[] {
                true,
                false,
                new Game(new ClassicRules(), new Dictionary<Player, ScoreTable>
                {
                    { new Player(12, "Maya", "beeImage"), scoreTableComplete },
                    { new Player(52, "Toto", "totoImage"), anotherScoreTableComplete },
                }, false),
                new Player(52, "Toto", "totoImage"),
                null,
            };

            // Should not works because of the too high frame number.
            yield return new object[] {
                true,
                false,
                new Game(new ClassicRules(), new Dictionary<Player, ScoreTable>
                {
                    { new Player(12, "Maya", "beeImage"), scoreTableComplete },
                    { new Player(52, "Toto", "totoImage"), anotherScoreTableComplete },
                }, false),
                new Player(52, "Toto", "totoImage"),
                new ClassicFrame(11, ThrowResult.FOUR, ThrowResult.FOUR),
            };

            // Should not works because of the classic last frame values (3 value, but 2 are expected).
            yield return new object[] {
                false,
                false,
                new Game(new ClassicRules(), new Dictionary<Player, ScoreTable>
                {
                    { new Player(12, "Maya", "beeImage"), scoreTableComplete },
                    { new Player(52, "Toto", "totoImage"), anotherScoreTableComplete },
                }, false),
                new Player(52, "Toto", "totoImage"),
                new ClassicLastFrame(5, ThrowResult.STRIKE, ThrowResult.FOUR, ThrowResult.STRIKE),
            };

            // Should not works because of the empty frame.
            yield return new object[] {
                false,
                false,
                new Game(new ClassicRules(), new Dictionary<Player, ScoreTable>
                {
                    { new Player(12, "Maya", "beeImage"), scoreTableComplete },
                    { new Player(52, "Toto", "totoImage"), anotherScoreTableComplete },
                }, false),
                new Player(52, "Toto", "totoImage"),
                new ClassicFrame(5, ThrowResult.NONE, ThrowResult.TWO),
            };

            // Should not works because of the empty frame.
            yield return new object[] {
                false,
                false,
                new Game(new ClassicRules(), new Dictionary<Player, ScoreTable>
                {
                    { new Player(12, "Maya", "beeImage"), scoreTableComplete },
                    { new Player(52, "Toto", "totoImage"), anotherScoreTableComplete },
                }, false),
                new Player(52, "Toto", "totoImage"),
                new ClassicFrame(5, ThrowResult.ONE, ThrowResult.NONE),
            };

            // Should not works because the total of the frame is higher than 10.
            yield return new object[] {
                false,
                false,
                new Game(new ClassicRules(), new Dictionary<Player, ScoreTable>
                {
                    { new Player(12, "Maya", "beeImage"), scoreTableComplete },
                    { new Player(52, "Toto", "totoImage"), anotherScoreTableComplete },
                }, false),
                new Player(52, "Toto", "totoImage"),
                new ClassicFrame(5, ThrowResult.NINE, ThrowResult.NINE),
            };

            // Should not works because the total of the frame is higher than 10.
            yield return new object[] {
                false,
                false,
                new Game(new ClassicRules(), new Dictionary<Player, ScoreTable>
                {
                    { new Player(12, "Maya", "beeImage"), scoreTableComplete },
                    { new Player(52, "Toto", "totoImage"), anotherScoreTableComplete },
                }, false),
                new Player(52, "Toto", "totoImage"),
                new ClassicFrame(5, ThrowResult.THREE, ThrowResult.SEVEN),
            };

            // Should works.
            yield return new object[] {
                false,
                true,
                new Game(new ClassicRules(), new Dictionary<Player, ScoreTable>
                {
                    { new Player(12, "Maya", "beeImage"), scoreTableComplete },
                    { new Player(52, "Toto", "totoImage"), anotherScoreTableComplete },
                }, false),
                new Player(52, "Toto", "totoImage"),
                new ClassicFrame(5, ThrowResult.THREE, ThrowResult.SIX),
            };

            // Should works.
            yield return new object[] {
                false,
                true,
                new Game(new ClassicRules(), new Dictionary<Player, ScoreTable>
                {
                    { new Player(12, "Maya", "beeImage"), scoreTableComplete },
                    { new Player(52, "Toto", "totoImage"), anotherScoreTableComplete },
                }, true),
                new Player(12, "Maya", "beeImage"),
                new ClassicFrame(9, ThrowResult.FIVE, ThrowResult.FOUR),
            };

            // Should works.
            yield return new object[] {
                false,
                true,
                new Game(new ClassicRules(), new Dictionary<Player, ScoreTable>
                {
                    { new Player(12, "Maya", "beeImage"), scoreTableComplete },
                    { new Player(52, "Toto", "totoImage"), anotherScoreTableComplete },
                }, true),
                new Player(12, "Maya", "beeImage"),
                new ClassicFrame(9, ThrowResult.ZERO, ThrowResult.SPARE),
            };

            // Should works.
            yield return new object[] {
                false,
                true,
                new Game(new ClassicRules(), new Dictionary<Player, ScoreTable>
                {
                    { new Player(12, "Maya", "beeImage"), scoreTableComplete },
                    { new Player(52, "Toto", "totoImage"), anotherScoreTableComplete },
                }, true),
                new Player(12, "Maya", "beeImage"),
                new ClassicFrame(2, ThrowResult.NONE, ThrowResult.STRIKE),
            };

            // Should works.
            yield return new object[] {
                false,
                true,
                new Game(new ClassicRules(), new Dictionary<Player, ScoreTable>
                {
                    { new Player(12, "Maya", "beeImage"), scoreTableComplete },
                    { new Player(52, "Toto", "totoImage"), anotherScoreTableComplete },
                }, true),
                new Player(12, "Maya", "beeImage"),
                new ClassicFrame(1, ThrowResult.THREE, ThrowResult.FIVE),
            };
        }

        public static IEnumerable<object[]> Data_TurnChange()
        {
            Player player1 = new Player(12, "Maya", "beeImage");
            Player player2 = new Player(52, "Toto", "totoImage");
            Player player3 = new Player(33, "Paul", "paulImage");

            yield return new object[] {
                true,
                player2,
                1,
                new Game(new ClassicRules(), new List<Player>
                {
                    player1,
                    player2,
                }),
                2
            };

            yield return new object[] {
                true,
                player1,
                2,
                new Game(new ClassicRules(), new List<Player>
                {
                    player1,
                }),
                2
            };

            yield return new object[] {
                true,
                player1,
                3,
                new Game(new ClassicRules(), new List<Player>
                {
                    player1,
                }),
                4
            };

            yield return new object[] {
                true,
                player1,
                6,
                new Game(new ClassicRules(), new List<Player>
                {
                    player1,
                }),
                10
            };

            yield return new object[] {
                true,
                player1,
                10,
                new Game(new ClassicRules(), new List<Player>
                {
                    player1,
                }),
                18
            };

            // When the last frame is complete, the current player is normally set to null and the current turn is at 10.
            yield return new object[] {
                false,
                null,
                10,
                new Game(new ClassicRules(), new List<Player>
                {
                    player1,
                }),
                20
            };

            yield return new object[] {
                true,
                player1,
                2,
                new Game(new ClassicRules(), new List<Player>
                {
                    player1,
                    player2,
                }),
                4
            };

            yield return new object[] {
                true,
                player1,
                6,
                new Game(new ClassicRules(), new List<Player>
                {
                    player1,
                    player2,
                }),
                20
            };

            yield return new object[] {
                true,
                player2,
                10,
                new Game(new ClassicRules(), new List<Player>
                {
                    player1,
                    player2,
                }),
                38
            };

            yield return new object[] {
                false,
                null,
                10,
                new Game(new ClassicRules(), new List<Player>
                {
                    player1,
                    player2,
                }),
                40
            };

            yield return new object[] {
                true,
                player1,
                2,
                new Game(new ClassicRules(), new List<Player>
                {
                    player1,
                    player2,
                    player3
                }),
                6
            };

            yield return new object[] {
                true,
                player2,
                8,
                new Game(new ClassicRules(), new List<Player>
                {
                    player1,
                    player2,
                    player3
                }),
                44
            };

            yield return new object[] {
                false,
                null,
                10,
                new Game(new ClassicRules(), new List<Player>
                {
                    player1,
                    player2,
                    player3
                }),
                60
            };
        }
    }
}
