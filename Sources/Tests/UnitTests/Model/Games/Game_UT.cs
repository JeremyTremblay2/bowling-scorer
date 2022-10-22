using FrameWriterModel.Frame;
using FrameWriterModel.Frame.ThrowResults;
using Model.Games;
using Model.Players;
using Model.Score;
using Model.Score.Rules;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Score.Rules;
using Xunit;

namespace UnitTests.Games
{
    /// <summary>
    /// Class used for unit testing the Game class.
    /// </summary>
    public class Game_UT
    {
        [Fact]
        public void CreateGameShouldInitCollections()
        {
            Game game = new Game(new ClassicRules(), 0, new List<Player>
            {
                new Player("Theo", "imageTheo")
            });
            Assert.NotNull(game.Players);
            Assert.NotNull(game.Scores);
        }

        [Fact]
        public void CreateGameShouldCreateCollections()
        {
            Game game = new Game(new ClassicRules(), 0, new List<Player>
            {
                new Player("Theo", "imageTheo")
            });
            Assert.Single(game.Players);
            Assert.Single(game.Scores);
        }

        [Fact]
        public void CreateGameShouldInitCreationDate()
        {
            DateTime date = DateTime.Now;
            Game game = new Game(new ClassicRules(), 0, new List<Player>
            {
                new Player("Theo", "imageTheo")
            });
            Assert.Equal(date.Date, game.CreationDate.Date);
        }

        [Fact]
        public void CreateGameShouldInitFinishedStatus()
        {
            Game game = new Game(new ClassicRules(), 0, new List<Player>
            {
                new Player("Theo", "imageTheo")
            });
            Assert.False(game.IsFinished);
        }

        [Fact]
        public void CreateGameShouldInitID()
        {
            Game game = new Game(new ClassicRules(), 8, new List<Player>
            {
                new Player("Theo", "imageTheo")
            });
            Assert.Equal(8, game.ID);
        }

        [Fact]
        public void CreateGameShouldInitTurnStatus()
        {
            DateTime date = DateTime.Now;
            Game game = new Game(new ClassicRules(), 0, new List<Player>
            {
                new Player(1, "Adrien", "adrienImage"),
                new Player(2, "Theo", "imageTheo")
            });
            Assert.Equal(1, game.CurrentTurn);
            Assert.Equal(new Player(1, "Adrien", "adrienImage"), game.CurrentPlayer);
        }

        [Fact]
        public void CreateGameWithNullPlayerCollectionShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Game(new ClassicRules(), 0, null));
        }

        [Fact]
        public void CreateGameWithNullRulesCollectionShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Game(null, 0, new List<Player>
            {
                new Player("Theo", "imageTheo")
            }));
        }

        [Fact]
        public void CreateGameWithNoPlayersShouldThrowInvalidOperationException()
        {
            Assert.Throws<ArgumentException>(() => new Game(new ClassicRules(), 0, new List<Player>()));
        }

        [Theory]
        [MemberData(nameof(GameDataTest.Data_AddPlayers), MemberType = typeof(GameDataTest))]
        public void CreateGameWithPlayersShouldAddThesePlayersToTheGame(Player[] expectedPlayers,
                                                                        Player[] playersToAdd)
        {
            Game game = new Game(new ClassicRules(), playersToAdd);
            Assert.Equal(expectedPlayers.Length, game.Players.Count);
            Assert.Equal(expectedPlayers.Length, game.Scores.Count);
            Assert.All(game.Players, p => expectedPlayers.Contains(p));
            Assert.All(game.Scores.Keys, p => expectedPlayers.Contains(p));
        }

        [Theory]
        [MemberData(nameof(GameDataTest.Data_CreateGame), MemberType = typeof(GameDataTest))]
        public void CreateGameWithExistingScoreTablesShouldCreateGameOrNotDependingOnTheParameters(bool expectedArgumentException,
                                                                                                   bool expectedArgumentNullException,
                                                                                                   int expectedNumberOfPlayers,
                                                                                                   IDictionary<Player, ScoreTable> scoresToAdd,
                                                                                                   ARules rulesToAdd)
        {
            if (expectedArgumentException)
            {
                Assert.Throws<ArgumentException>(() => new Game(rulesToAdd, scoresToAdd, false));
            }
            else if (expectedArgumentNullException)
            {
                Assert.Throws<ArgumentNullException>(() => new Game(rulesToAdd, scoresToAdd, false));
            }
            else
            {
                Game game = new Game(rulesToAdd, scoresToAdd, false);
                Assert.Equal(expectedNumberOfPlayers, game.Players.Count);
                Assert.All(game.Scores.Keys, p => scoresToAdd.ContainsKey(p));
                Assert.All(game.Scores.Values, p => scoresToAdd.Values.Contains(p));
                Assert.Null(game.CurrentPlayer);
                Assert.Equal(10, game.CurrentTurn);
                Assert.False(game.IsFinished);
            }
        }

        [Theory]
        [MemberData(nameof(GameDataTest.Data_FinishGame), MemberType = typeof(GameDataTest))]
        public void FinishGameShouldMakesGameReadOnly(bool expectedInvalidOperationException,
                                                      IDictionary<Player, ScoreTable> scoresToAdd,
                                                      ARules rulesToAdd)
        {
            if (expectedInvalidOperationException)
            {
                Assert.Throws<InvalidOperationException>(() => new Game(rulesToAdd, scoresToAdd, false));
            }
            else
            {
                Game game = new Game(rulesToAdd, scoresToAdd, true);



                Assert.Null(game.CurrentPlayer);
                Assert.Equal(10, game.CurrentTurn);
                Assert.True(game.IsFinished);
            }
        }

        [Theory]
        [MemberData(nameof(GameDataTest.Data_EqualsGame), MemberType = typeof(GameDataTest))]
        public void EqualsShouldReturnsLogicalValues(bool areEquals, Game game1, [AllowNull] object game2)
        {
            if (areEquals)
            {
                Assert.Equal(game1, game2);
                Assert.Equal(game1, game2);
                Assert.Equal(game2.GetHashCode(), game1.GetHashCode());
            }
            else
            {
                if (game2 != null) Assert.NotEqual(game1, game2);
                if (game1 != null) Assert.NotEqual(game1, game2);
                if (game2 != null && game1 != null) Assert.NotEqual(game1.GetHashCode(), game2.GetHashCode());
            }
        }

        [Theory]
        [MemberData(nameof(GameDataTest.Data_AddResults), MemberType = typeof(GameDataTest))]
        public void AddResultToCurrentPlayerShouldAddTheResultToTheTableScoreWhenTheResultIsCorrect(bool expectedInvalidOperationException,
                                                                                                    bool expectedResult,
                                                                                                    Game game,
                                                                                                    int indexToAdd,
                                                                                                    ThrowResult throwResultToAdd)
        {
            if (expectedInvalidOperationException)
            {
                Assert.Throws<InvalidOperationException>(() => game.AddResultToCurrentPlayer(indexToAdd, throwResultToAdd));
            }
            else
            {
                bool result = game.AddResultToCurrentPlayer(indexToAdd, throwResultToAdd);
                Assert.Equal(expectedResult, result);
                if (game.CurrentPlayer != null)
                    Assert.Equal(throwResultToAdd, game.Scores[game.CurrentPlayer].Frames[0].ThrowResults[indexToAdd]);
            }
        }

        [Theory]
        [MemberData(nameof(GameDataTest.Data_EditResults), MemberType = typeof(GameDataTest))]
        public void EditResultShouldEditAResultWhenTheParametersAreCorrects(bool expectedArgumentException,
                                                                            bool expectedResult,
                                                                            Game game,
                                                                            Player affectedPlayer,
                                                                            AFrame frameToAdd)
        {
            if (expectedArgumentException)
            {
                Assert.Throws<ArgumentException>(() => game.EditResult(affectedPlayer, frameToAdd));
            }
            else
            {
                Assert.NotEqual(frameToAdd.ThrowResults, game.Scores[affectedPlayer].Frames[frameToAdd.FrameNumberLabel].ThrowResults);
                bool result = game.EditResult(affectedPlayer, frameToAdd);
                Assert.Equal(expectedResult, result);
                if (expectedResult)
                {
                    Assert.Equal(frameToAdd.ThrowResults, game.Scores[affectedPlayer].Frames[frameToAdd.FrameNumberLabel - 1].ThrowResults);
                }
                else
                {
                    Assert.NotEqual(frameToAdd.ThrowResults, game.Scores[affectedPlayer].Frames[frameToAdd.FrameNumberLabel - 1].ThrowResults);
                }
            }
        }

        [Fact]
        public void GetPossibleThrowResultsShouldReturnsEmptyArray()
        {
            Game game = new Game(new ClassicRules(), new List<Player>
            {
                new Player(12, "Maya", "beeImage"),
                new Player(52, "Toto", "totoImage"),
            });
            Assert.Empty(game.GetPossibleThrowResults(null, 0, 0));
        }

        [Theory]
        [MemberData(nameof(GameDataTest.Data_TurnChange), MemberType = typeof(GameDataTest))]
        public void NextTurnShouldChangeTheTurnAndCurrentPlayer(bool expectedResult,
                                                                Player expectedCurrentPlayer,
                                                                int expectedCurrentTurn,
                                                                Game game,
                                                                int numberOfAdd)
        {
            bool result = false;

            for (int i = 0; i < numberOfAdd; i++)
            {
                game.AddResultToCurrentPlayer(i % 2, ThrowResult.ONE);
                if (i % 2 == 1)
                {
                    result = game.NextTurn();
                }
            }
            if ((numberOfAdd - 1) % 2 == 0)
            {
                result = game.NextTurn();
            }

            Assert.Equal(expectedResult, result);
            Assert.Equal(expectedCurrentTurn, game.CurrentTurn);
            Assert.Equal(expectedCurrentPlayer, game.CurrentPlayer);
        }

        [Fact]
        public void ChangeTurnWhileCurrentFrameIsIncorrectShouldThrowInvalidOperationException()
        {
            Game game = new Game(new ClassicRules(), new List<Player>
            {
                new Player(12, "Maya", "beeImage"),
                new Player(52, "Toto", "totoImage"),
            });
            game.AddResultToCurrentPlayer(0, ThrowResult.ZERO);
            game.AddResultToCurrentPlayer(1, ThrowResult.NONE);
            Assert.Throws<InvalidOperationException>(() => game.NextTurn());
        }

        [Fact]
        public void ChangeTurnWhileFinishGameShouldThrowInvalidOperationException()
        {
            Game game = new Game(new ClassicRules(), new Dictionary<Player, ScoreTable>
            {
                { new Player(12, "Maya", "beeImage"), GameDataTest.scoreTableComplete },
                { new Player(52, "Toto", "totoImage"), GameDataTest.anotherScoreTableComplete },
            }, true);

            Assert.Throws<InvalidOperationException>(() => game.NextTurn());
        }

        [Fact]
        public void GetWinnerWhenGameHasOnlyOnePlayerShouldReturnsNull()
        {
            Game game = new Game(new ClassicRules(), new Dictionary<Player, ScoreTable>
            {
                { new Player(12, "Maya", "beeImage"), GameDataTest.scoreTableComplete },
            }, true);

            Assert.Null(game.Winner);
        }

        [Fact]
        public void GetLoserWhenGameHasOnlyOnePlayerShouldReturnsNull()
        {
            Game game = new Game(new ClassicRules(), new Dictionary<Player, ScoreTable>
            {
                { new Player(12, "Maya", "beeImage"), GameDataTest.scoreTableComplete },
            }, true);

            Assert.Null(game.Loser);
        }

        [Fact]
        public void GetWinnerWhenGameIsNotFinishedShouldReturnsNull()
        {
            Game game = new Game(new ClassicRules(), new Dictionary<Player, ScoreTable>
            {
                { new Player(12, "Maya", "beeImage"), GameDataTest.scoreTableComplete },
                { new Player(52, "Toto", "totoImage"), GameDataTest.anotherScoreTableComplete },
            }, false);

            Assert.Null(game.Winner);
        }

        [Fact]
        public void GetLoserWhenGameIsNotFinishedShouldReturnsNull()
        {
            Game game = new Game(new ClassicRules(), new Dictionary<Player, ScoreTable>
            {
                { new Player(12, "Maya", "beeImage"), GameDataTest.scoreTableComplete },
                { new Player(52, "Toto", "totoImage"), GameDataTest.anotherScoreTableComplete },
            }, false);

            Assert.Null(game.Loser);
        }

        [Fact]
        public void GetWinnerWhenGameIsFinishedWithMultiplePlayersShouldReturnsTheWinner()
        {
            Game game = new Game(new ClassicRules(), new Dictionary<Player, ScoreTable>
            {
                { new Player(12, "Maya", "beeImage"), GameDataTest.scoreTableComplete },
                { new Player(52, "Toto", "totoImage"), GameDataTest.anotherScoreTableComplete },
            }, true);

            Assert.NotNull(game.Winner);
            Assert.Equal(new Player(12, "Maya", "beeImage"), game.Winner);
        }

        [Fact]
        public void GetLoserWhenGameIsFinishedWithMultiplePlayersShouldReturnsTheLoser()
        {
            Game game = new Game(new ClassicRules(), new Dictionary<Player, ScoreTable>
            {
                { new Player(12, "Maya", "beeImage"), GameDataTest.scoreTableComplete },
                { new Player(52, "Toto", "totoImage"), GameDataTest.anotherScoreTableComplete },
            }, true);

            Assert.NotNull(game.Loser);
            Assert.Equal(new Player(52, "Toto", "totoImage"), game.Loser);
        }
    }
}
