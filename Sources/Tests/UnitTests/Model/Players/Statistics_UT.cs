using Model.Games;
using Model.Players;
using System;
using System.Linq;
using UnitTests.Model.Players;
using Xunit;

namespace UnitTests.Players
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
            Assert.NotNull(statistics.Games);
        }

        [Fact]
        public void CollectionsShouldBeEmptyWhenInitialized()
        {
            Statistics statistics = new();
            Assert.NotEmpty(statistics.ThrowResults);
            Assert.Empty(statistics.Scores);
            Assert.Empty(statistics.Games);
        }

        [Fact]
        public void StatisticsShouldBeEqualToZeroWhenInitialized()
        {
            Statistics statistics = new();
            Assert.Equal(0, statistics.BestScore);
            Assert.Equal(0, statistics.NumberOfGames);
            Assert.Equal(0, statistics.NumberOfDefeat);
            Assert.Equal(0, statistics.NumberOfVictory);
        }

        [Fact]
        public void CalcultatedPropertiesShouldReturnsZeroWhenInitialized()
        {
            Statistics statistics = new();
            Assert.Equal(0, statistics.MediumScore);
            Assert.Equal(0, statistics.MediumScore);
        }

        [Theory]
        [MemberData(nameof(StatisticsDataTest.Test_StatisticsDataAddGame), MemberType = typeof(StatisticsDataTest))]
        public void AddGameShouldUpdateStatistics(bool expectedLastResult,
                                                  int expectedNumberOfGames,
                                                  int expectedNumberOfVictories,
                                                  int expectedNumberOfDefeat,
                                                  int expectedBestScore,
                                                  int expectedNumberOfThrowResults,
                                                  double expectedMediumThrow,
                                                  double expectedMediumScore,
                                                  Player player,
                                                  Game[] gamesToAdd)
        {
            bool result = false;
            foreach (Game game in gamesToAdd)
            {
                result = player.AddGame(game);
            }
            Assert.Equal(expectedLastResult, result);
            Assert.Equal(expectedNumberOfGames, player.Statistics.Games.Count);
            Assert.Equal(expectedNumberOfVictories, player.Statistics.NumberOfVictory);
            Assert.Equal(expectedNumberOfDefeat, player.Statistics.NumberOfDefeat);
            Assert.Equal(expectedBestScore, player.Statistics.BestScore);
            Assert.Equal(expectedNumberOfThrowResults, player.Statistics.ThrowResults.Values.Sum());
            Assert.Equal(expectedMediumThrow, Math.Round(player.Statistics.MediumThrow, 1));
            Assert.Equal(expectedMediumScore, Math.Round(player.Statistics.MediumScore, 1));
        }

        [Theory]
        [MemberData(nameof(StatisticsDataTest.Test_StatisticsRemoveAddGame), MemberType = typeof(StatisticsDataTest))]
        public void RemoveGameShouldUpdateStatistics(bool expectedLastResult,
                                                     int expectedNumberOfGames,
                                                     int expectedNumberOfVictories,
                                                     int expectedNumberOfDefeat,
                                                     int expectedBestScore,
                                                     int expectedNumberOfThrowResults,
                                                     double expectedMediumThrow,
                                                     double expectedMediumScore,
                                                     Player player,
                                                     Game[] gamesToRemove)
        {
            bool result = false;
            foreach (Game game in gamesToRemove)
            {
                result = player.RemoveGame(game);
            }
            Assert.Equal(expectedLastResult, result);
            Assert.Equal(expectedNumberOfGames, player.Statistics.Games.Count);
            Assert.Equal(expectedNumberOfVictories, player.Statistics.NumberOfVictory);
            Assert.Equal(expectedNumberOfDefeat, player.Statistics.NumberOfDefeat);
            Assert.Equal(expectedBestScore, player.Statistics.BestScore);
            Assert.Equal(expectedNumberOfThrowResults, player.Statistics.ThrowResults.Values.Sum());
            Assert.Equal(expectedMediumThrow, Math.Round(player.Statistics.MediumThrow, 1));
            Assert.Equal(expectedMediumScore, Math.Round(player.Statistics.MediumScore, 1));
        }

        [Theory]
        [MemberData(nameof(StatisticsDataTest.Test_StatisticsDataEquality), MemberType = typeof(StatisticsDataTest))]
        public void EqualsAndHashCodeShouldReturnLogicalValues(bool expectedResult, 
                                                               Statistics statistics1, 
                                                               Statistics statistics2)
        {
            Assert.Equal(expectedResult, statistics1 == statistics2);
            Assert.Equal(!expectedResult, statistics1 != statistics2);
            if (statistics1 != null) Assert.Equal(expectedResult, statistics1.Equals(statistics2));
            if (statistics2 != null) Assert.Equal(expectedResult, statistics2.Equals(statistics1));
            if (statistics1 != null && statistics2 != null) {
                Assert.Equal(expectedResult, statistics1.GetHashCode().Equals(statistics2.GetHashCode()));
            }
        }

        [Fact]
        public void ComparisonOperatorsShouldReturnLogicValues()
        {
            Statistics statistics1 = StatisticsDataTest.playerEmpty.Statistics;
            Statistics statistics2 = StatisticsDataTest.player83.Statistics;
            Statistics statistics3 = StatisticsDataTest.player104.Statistics;
            Assert.True(statistics1 < statistics2);
            Assert.True(statistics1 <= statistics2);
            Assert.True(statistics2 > statistics1);
            Assert.True(statistics2 >= statistics1);
            Assert.True(statistics3 > statistics2);
            Assert.True(statistics3 >= statistics2);
            Assert.True(statistics3 >= statistics1);
        }

        [Fact]
        public void EqualityOperatorShouldReturnsNullWhenEqualsNullValue()
        {
            Statistics Statistics1 = null;
            Statistics Statistics2 = null;
            Assert.True(Statistics1 == Statistics2);
        }
    }
}
