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
        public void TestIfStubReturnTheRightData()
        {
            IList<Game> games = new List<Game>();
            Player p1 = new Player("Jeremy", "jeremy.png");
            Player p2 = new Player("Mickaël", "mickael.png");
            Player p3 = new Player("Jaques", "jaques.png");
            Player p4 = new Player("luckas", "luckas.png");
            Player p5 = new Player("Mathis", "mathis.png");
            Game game1 = new Game(new ClassicRules(), new List<Player>() { p1, p2 });
            Game game2 = new Game(new ClassicRules(), new List<Player>() { p2, p3, p4 });
            Game game3 = new Game(new ClassicRules(), new List<Player>() { p2, p3, p4, p5 });
            games.Add(game1);
            games.Add(game2);
            games.Add(game3);

            Stub.Stub stub = new();
            List<Game> loadedGames = (List<Game>)stub.GetGames();
            Assert.Equal(games.Count, loadedGames.Count);
            Assert.Equal(games[0], loadedGames[0]);
            Assert.Equal(games[1], loadedGames[1]);
            Assert.Equal(games[2], loadedGames[2]);
        }

        [Fact]
        public void TestIfAddGameIsLocked()
        {
            Stub.Stub stub = new();
            Assert.Throws<NotImplementedException>(() => stub.AddGame(null));
        }
    }
}
