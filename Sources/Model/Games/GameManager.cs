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
    /// <summary>
    /// A GameManager contains a collection of all games in the app. 
    /// It also contains the part in progress, but this may be zero if there is no part in progress. 
    /// There can therefore only be one game in progress at a time.
    /// </summary>
    public class GameManager : IEquatable<GameManager>
    {
        private IList<Game> _games;

        /// <summary>
        /// Contains all the game in the app, also the current game even if it is not finished.
        /// </summary>
        public ReadOnlyCollection<Game> Games { get; private set; }

        /// <summary>
        /// Reference to the CurrentGame. Designates the unfinished part. Is null if there is no game in progress.
        /// </summary>
        public Game CurrentGame { get; private set; }

        /// <summary>
        /// Create a new instance of a GameManager.
        /// </summary>
        public GameManager()
        {
            _games = new List<Game>();
            Games = new ReadOnlyCollection<Game>(_games);
        }

        /// <summary>
        /// Add a game to the GameManager and to the CurrentGame if it is not finished.
        /// </summary>
        /// <param name="game">The game to add.</param>
        /// <returns>A boolean indicating if the game was added.</returns>
        /// <exception cref="InvalidOperationException">
        /// If there is actually a game not finished and if the game given is not finished too.
        /// </exception>
        public bool AddGame(Game game)
        {
            if (game != null)
            {
                if (CurrentGame != null && !game.IsFinished)
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

        /// <summary>
        /// Add a game to the GameManager and to the CurrentGame if it is not finished.
        /// </summary>
        /// <param name="rules">The rules applied to the new Game.</param>
        /// <param name="ID">The ID of the new Game.</param>
        /// <param name="players">The players to add at the new Game.</param>
        /// <returns>A boolean indicating if the game was added.</returns>
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

        /// <summary>
        /// Get and returns a game from its ID.
        /// </summary>
        /// <param name="ID">The ID of the game to search.</param>
        /// <returns>The game corresponding to the ID or null if no game was found.</returns>
        public Game GetGameFromID(int ID) => _games.FirstOrDefault(g => g.ID.Equals(ID));

        /// <summary>
        /// Method call when the status of the current game change.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void CurrentGameStatusUpdated(object sender, GameStatusChangedEventArgs e)
        {
            if (e.GameIsFinished)
            {
                CurrentGame.GameStatusChanged -= CurrentGameStatusUpdated;
                CurrentGame = null;
            }
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(_games);
        }

        /// <summary>
        /// Determines whether the two object instances are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the actual object.</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, False.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (ReferenceEquals(obj, this)) return true;
            if (obj.GetType() != typeof(GameManager)) return false;
            return Equals(obj as GameManager);

        }

        /// <summary>
        /// Determines whether the two object instances are equal.
        /// </summary>
        /// <param name="other">The other GameManager to compare with the actual GameManager.</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, False.</returns>
        public bool Equals(GameManager other)
        {
            return other != null && EqualityComparer<IList<Game>>.Default.Equals(_games, other._games);
        }

        /// <summary>
        /// Returns a string representing a GameManager.
        /// </summary>
        /// <returns>A string representing a GameManager.</returns>
        public override string ToString()
        {
            if (_games.Count == 0) return "No games.";
            StringBuilder builder = new();
            if (CurrentGame == null)
            {
                builder.AppendLine("No current game in the manager.");
            }
            else
            {
                builder.AppendLine("Current game in the manager: ").AppendLine(CurrentGame.ToString());
            }
            builder.AppendLine("Games present in the manager: ");
            foreach (Game game in Games)
            {
                builder.AppendLine(game.ToString());
            }
            return builder.ToString();
        }
    }
}
