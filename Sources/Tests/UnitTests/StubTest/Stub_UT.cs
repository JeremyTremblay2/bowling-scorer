using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Stub;
using Model.Games;
using Model.Players;
using Model.Score.Rules;

namespace UnitTests.StubTest
{
    public class Stub_UT
    {
        [Fact]
        public async Task GetPlayersShouldReturnsACollectionOfPlayers()
        {
            Stub.Stub stub = new();
            var players = (await stub.GetPlayers(0, 10)).ToList();
            Assert.Equal(15, players.Count());
            Assert.All(players, p => Assert.NotNull(p));
        }

        [Fact]
        public async Task PlayersShouldHaveLogicalData()
        {
            Stub.Stub stub = new();
            var players = (await stub.GetPlayers(0, 10)).ToList();
            Assert.Equal("Baptiste", players[3].Name);
            Assert.Equal(7, players[4].ID);
            Assert.Equal("harveyImage", players[1].Image);
            Assert.Equal("Leo", players[6].Name);
            Assert.Equal(15, players[7].ID);
            Assert.Equal("marnieImage", players[8].Image);
        }

        [Fact]
        public async Task PlayersShouldHaveEmptyStatistics()
        {
            Stub.Stub stub = new();
            var players = (await stub.GetPlayers(0, 10)).ToList();
            foreach (Player p in players)
            {
                Assert.NotEmpty(p.Statistics.ThrowResults);
                Assert.Empty(p.Statistics.Scores);
                Assert.Empty(p.Statistics.Games);
                Assert.Equal(0, p.Statistics.BestScore);
                Assert.Equal(0, p.Statistics.NumberOfGames);
                Assert.Equal(0, p.Statistics.NumberOfDefeat);
                Assert.Equal(0, p.Statistics.NumberOfVictory);
                Assert.Equal(0, p.Statistics.MediumScore);
                Assert.Equal(0, p.Statistics.MediumScore);
            }
        }

        [Fact]
        public async Task GetGamesShouldReturnsACollectionOfGames()
        {
            Stub.Stub stub = new();
            var games = (await stub.GetGames(0, 10)).ToList();
            Assert.Equal(3, games.Count());
            Assert.All(games, g => Assert.NotNull(g));
        }

        [Fact]
        public async Task GamesShouldHaveALogicalNumberOfPlayers()
        {
            Stub.Stub stub = new();
            var games = (await stub.GetGames(0, 10)).ToList();
            Assert.Equal(2, games[0].Players.Count);
            Assert.Equal(3, games[1].Players.Count);
            Assert.Equal(4, games[2].Players.Count);
        }

        [Fact]
        public async Task GamesShouldBeFinishedOrNot()
        {
            Stub.Stub stub = new();
            var games = (await stub.GetGames(0, 10)).ToList();
            Assert.True(games[0].IsFinished);
            Assert.False(games[1].IsFinished);
            Assert.True(games[2].IsFinished);
        }

        [Fact]
        public async Task GamesShouldHaveWinnerAndLosersWhenFinished()
        {
            Stub.Stub stub = new();
            var games = (await stub.GetGames(0, 10)).ToList();
            Assert.Equal(new Player(2, "Elliot", "elliotImage"), games[0].Winner);
            Assert.Equal(new Player(4, "Harvey", "harveyImage"), games[0].Loser);
            Assert.Null(games[1].Winner);
            Assert.Null(games[1].Winner);
            Assert.Equal(new Player(4, "Harvey", "harveyImage"), games[2].Winner);
            Assert.Equal(new Player(9, "Haley", "haleyImage"), games[2].Loser);
        }

        [Fact]
        public async Task ChangeTurnShouldBeImpossibleWhenGameIsFinished()
        {
            Stub.Stub stub = new();
            var games = (await stub.GetGames(0, 10)).ToList();
            Assert.Throws<InvalidOperationException>(() => games[0].NextTurn());
            games[1].AddResultToCurrentPlayer(1, FrameModel.Frame.ThrowResults.ThrowResult.THREE);
            bool result = games[1].NextTurn();
            Assert.True(result);
            Assert.Throws<InvalidOperationException>(() => games[2].NextTurn());
        }

        [Fact]
        public void OtherMethodsShouldThrowInvalidOperationExceptions()
        {
            Stub.Stub stub = new();
            Assert.ThrowsAsync<NotImplementedException>(() => stub.AddGame(default));
            Assert.ThrowsAsync<NotImplementedException>(() => stub.AddPlayer(default));
            Assert.ThrowsAsync<NotImplementedException>(() => stub.AddPlayers(default));
            Assert.ThrowsAsync<NotImplementedException>(() => stub.EditPlayer(default, default, default));
            Assert.ThrowsAsync<NotImplementedException>(() => stub.GetGameFromID(default));
            Assert.ThrowsAsync<NotImplementedException>(() => stub.GetGamesFromPlayer(default, default, default));
            Assert.ThrowsAsync<NotImplementedException>(() => stub.GetPlayerFromID(default));
            Assert.ThrowsAsync<NotImplementedException>(() => stub.GetPlayerFromName(default, default, default));
            Assert.ThrowsAsync<NotImplementedException>(() => stub.RemoveGame(default));
            Assert.ThrowsAsync<NotImplementedException>(() => stub.RemovePlayer(default));
        }
    }
}
