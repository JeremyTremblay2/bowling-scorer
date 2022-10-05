using Model.Player;
using Xunit;

namespace UnitTests.Player
{
    /// <summary>
    /// Unit test class for statistics.
    /// </summary>
    public class Statistics_UT
    {
        [Fact]
        public void CreateStatisticsShouldInitializeCollections()
        {
            Statistics statistics = new();
            Assert.NotNull(statistics.ThrowResults);
            Assert.NotNull(statistics.Scores);
            Assert.NotNull(statistics.GamesID);
        }

        [Fact]
        public void StatisticsShouldBeEmptyWhenInitialized()
        {
            Statistics statistics = new();
            Assert.Empty(statistics.ThrowResults);
            Assert.Empty(statistics.Scores);
            Assert.Empty(statistics.GamesID);
            Assert.Equal(0, statistics.BestScore);
            Assert.Equal(0, statistics.NumberOfGames);
            Assert.Equal(0, statistics.NumberOfDefeat);
            Assert.Equal(0, statistics.NumberOfVictory);
        }

        [Fact]
        public void AddGameShouldIncrementNumberOfGames()
        {
            Statistics statistics = new();
            statistics.AddGame(21);
            Assert.Equal(1, statistics.NumberOfGames);
        }

        [Fact]
        public void RemoveGameShouldDecrementNumberOfGames()
        {
            Statistics statistics = new();
            statistics.AddGame(21);
            statistics.RemoveGame(21);
            Assert.Equal(0, statistics.NumberOfGames);
        }
    }
}
