using Business;
using Model.Games;
using Model.Players;
using Model.Score.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stub
{
    /// <summary>
    /// Used to generate fake data for the tests
    /// </summary>
    public class Stub : IDataManager
    {
        /// <summary>
        /// Do not call this method here, a stub only generate fake data, he can't simulate the other aspects of
        /// a persistence layer
        /// </summary>
        /// <param name="game"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void AddGame(Game game)
        {
            throw new NotImplementedException("This is a Stub, you can just use it to get fake data");
        }

        /// <summary>
        /// Return some game data, use it if you need to simulate a fake data loading
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Game> GetGames()
        {
            IList<Game> games = new List<Game>();
            Player p1 = new Player(0, "Jeremy", "jeremy.png");
            Player p2 = new Player(1, "Mickaël", "mickael.png");
            Player p3 = new Player(2, "Jaques", "jaques.png");
            Player p4 = new Player(3, "luckas", "luckas.png");
            Player p5 = new Player(4, "Mathis", "mathis.png");
            Game game1 = new Game(new ClassicRules(), new List<Player>() { p1, p2 });
            Game game2 = new Game(new ClassicRules(), new List<Player>() { p2, p3, p4 });
            Game game3 = new Game(new ClassicRules(), new List<Player>() { p2, p3, p4, p5 });
            games.Add(game1);
            games.Add(game2);
            games.Add(game3);
            return games;
        }
    }
}
