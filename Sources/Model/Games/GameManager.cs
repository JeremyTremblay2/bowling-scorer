using Model.Players;
using Model.Score.Rules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Games
{
    public class GameManager
    {
        private IList<Game> _games;

        public IReadOnlyCollection<Game> Games { get; private set; }

        public Game CurrentGame { get; private set; }

        public GameManager()
        {
            _games = new List<Game>();
            Games = new ReadOnlyCollection<Game>(_games);
        }

        public bool AddGame(Game game)
        {
            if (game != null)
            {
                if (CurrentGame != null)
                {
                    throw new InvalidOperationException("There is currently a game not finished, impossible to add a new game.");
                }
                _games.Add(game);
                if (!game.IsFinished)
                {
                    CurrentGame = game;
                    CurrentGame.GameStatusChanged += CurrentGameStatusUpdated; // Subscription to the new game.
                }
                return true;
            }
            return false;
        }

        public bool AddGame(ARules rules, int ID, IEnumerable<Player> players)
            => AddGame(new Game(rules, ID, players));

        public bool RemoveGame(int ID)
        {
            if (CurrentGame.ID == ID)
            {
                CurrentGame = null;
            }
            return _games.Remove(GetGameFromID(ID));
        }

        public Game GetGameFromID(int ID) => _games.FirstOrDefault(g => g.ID.Equals(ID));

        private void CurrentGameStatusUpdated(object sender, GameStatusChangedEventArgs e)
        {
            if (e.GameIsFinished)
            {
                CurrentGame = null;
            }
        }
    }
}
