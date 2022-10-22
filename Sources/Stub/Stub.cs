using Business;
using Model.Games;
using Model.Players;
using Model.Score.Rules;

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

        public Task<bool> AddPlayer(Player player)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Player>> AddPlayers(Player[] players)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditPlayer(Player player, string name, string image)
        {
            throw new NotImplementedException();
        }

        public Task<Game> GetGameFromID(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Game>> GetGames(int index, int count)
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
            return (Task<IEnumerable<Game>>) games;
        }

        public Task<IEnumerable<Game>> GetGamesFromPlayer(Player player, int index, int count)
        {
            throw new NotImplementedException();
        }

        public Task<Player> GetPlayerFromID(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Player>> GetPlayerFromName(string substring, int index, int count)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Player>> GetPlayers(int index, int count)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveGame(Game game)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemovePlayer(Player player)
        {
            throw new NotImplementedException();
        }

        Task<bool> IDataManager.AddGame(Game game)
        {
            throw new NotImplementedException();
        }
    }
}
