using FrameWriterModel.Frame;
using FrameWriterModel.Frame.ThrowResults;
using Model.Players;
using Model.Score;
using Model.Score.Rules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Games
{
    /// <summary>
    /// Represent a event taking place when the status of the game change.
    /// </summary>
    public class GameStatusChangedEventArgs
    {
        /// <summary>
        /// The new game status after the modification.
        /// </summary>
        public bool GameIsFinished { get; private set; }

        /// <summary>
        /// Change the status of the game by the new value.
        /// </summary>
        /// <param name="status">The new status to apply.</param>
        public GameStatusChangedEventArgs(bool status) => GameIsFinished = status;
    }

    /// <summary>
    /// A Game represent a Bowling Game. It contains data about the current turn, the creation date of the game, if this one is finished or not...
    /// It contains also a dictionnary linking the players and their results (ScoreTables). 
    /// Once a game is created, it is impossible to add or remove another player.
    /// A Game is a unique cycle: it is possible to change the turn once a frame is correctly fill in, but it is impossible to go back.
    /// However, it is possible to edit a frame already fill in by a player.
    /// This class also makes available methods to get possible results for a special configuration, and other stuff.
    /// </summary>
    public class Game : IEquatable<Game>
    {

        private readonly IList<Player> _players;

        private readonly IDictionary<Player, ScoreTable> _scores;

        private bool isFinished;

        /// <summary>
        /// A unique identifier for the game.
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        /// A boolean indicating if the game is finished or not. Once a game is finished, it CANNOT be edited.
        /// </summary>
        public bool IsFinished
        {
            get => isFinished;
            private set
            {
                isFinished = value;
                OnGameStatusChanged(isFinished);
            }
        }

        /// <summary>
        /// The creation date of the game.
        /// </summary>
        public DateTime CreationDate { get; private set; }

        /// <summary>
        /// The current turn (which also represent the current frame in the score table). Starts at 0.
        /// </summary>
        public int CurrentTurn { get; private set; }

        /// <summary>
        /// The player who is currently filling in his Frame.
        /// </summary>
        public Player CurrentPlayer { get; private set; }

        /// <summary>
        /// A dictionnary that links the player to their scores.
        /// </summary>
        public ReadOnlyDictionary<Player, ScoreTable> Scores { get; private set; }

        /// <summary>
        /// The players who participate to the game.
        /// </summary>
        public ReadOnlyCollection<Player> Players { get; private set; }

        /// <summary>
        /// Event representing the modification of the status of the game (finished or not).
        /// </summary>
        public event EventHandler<GameStatusChangedEventArgs> GameStatusChanged;

        /// <summary>
        /// Create a new instance of Game.
        /// </summary>
        /// <param name="rules">The rules used by the game.</param>
        /// <param name="ID">The ID of the Game.</param>
        /// <param name="players">The players that will participate to the game.</param>
        public Game(ARules rules, int ID, IEnumerable<Player> players)
        {
            if (players == null)
            {
                throw new ArgumentNullException(nameof(players), "The player collection given in parameter cannot be null.");
            }
            if (rules == null)
            {
                throw new ArgumentNullException(nameof(rules), "The rules given in parameter cannot be null.");
            }

            _scores = new Dictionary<Player, ScoreTable>();
            Scores = new ReadOnlyDictionary<Player, ScoreTable>(_scores);
            _players = new List<Player>();
            Players = new ReadOnlyCollection<Player>(_players);

            AddPlayers(players, rules);
            if (Players.Count <= 0)
            {
                throw new ArgumentException("Impossible to create a game because the collection "
                    + "must contains at least one player.", nameof(players));
            }
            this.ID = ID;
            CreationDate = DateTime.Now;
            CurrentTurn = 1;
            CurrentPlayer = Players.First();
            IsFinished = false;
        }

        /// <summary>
        /// Create a new instance of Game with a default ID.
        /// </summary>
        /// <param name="rules">The rules used by the game.</param>
        /// <param name="players">The players that will participate to the game.</param>
        public Game(ARules rules, IEnumerable<Player> players) : this(rules, 0, players)
        { }

        /// <summary>
        /// Create a new instance of Game.
        /// This Game will be already fill with scores from players and table scores. It is also possible to mark it as finished.
        /// </summary>
        /// <param name="rules">The rules of the game. It must be the same as the rules of the table scores or the couple of data will be ignored.</param>
        /// <param name="scores">A dictionnary of players and table scores (the table scores must be full).</param>
        /// <param name="isFinished">Preise if you want the game already finished or not. Be sure the game is really finished, 
        /// because an InvalidOperationException will be throw if it is not the case.
        /// </param>
        /// <param name="ID">The ID of the game (facultative).</param>
        /// <exception cref="ArgumentException">If the collection does not contains at least one player and score table valids.</exception>
        public Game(ARules rules, IDictionary<Player, ScoreTable> scores, bool isFinished, int ID = 0) : this(rules, ID, scores.Keys)
        {
            _scores.Clear();
            _players.Clear();
            var validScores = scores.Where(s => s.Key != null && s.Value != null);
            foreach (KeyValuePair<Player, ScoreTable> score in validScores)
            {
                // be sure we are manipulating score tables full with same rules.
                if (score.Value.AreRulesEquals(rules) && score.Value.IsScoreTableComplete()) 
                {
                    AddPlayer(score.Key, score.Value);
                }
            }
            if (_scores.Count == 0)
            {
                throw new ArgumentException("Impossible to create a game because the collection "
                    + "must contains at least one player and score table.", nameof(scores));
            }

            CurrentPlayer = null;
            CurrentTurn = _scores[_players.First()].Frames.Count;

            // If the game is finished, initialize the game like it should be at the end, when score tables are complete.
            if (isFinished)
            {
                Finish(); // Call this to ensure the score tables given are valid.
            }
        }

        /// <summary>
        /// Change the current turn to allow another player to fill in his result frame.
        /// It is possile to change the turn until the game will be considered as finished, when the score tables will be full.
        /// Throw an exception if the game is finished or if the last player has not complete his frame before.
        /// </summary>
        /// <returns>A boolean indicating if the turn was changed.</returns>
        /// <exception cref="InvalidOperationException">If the game is finished or if the last player has not complete his frame before.</exception>
        public bool NextTurn()
        {
            if (IsFinished) throw new InvalidOperationException("The game is finished, impossible to pass to the next turn.");
            if (CurrentPlayer == null) return false;
            if (!Scores[CurrentPlayer].IsFrameComplete(Scores[CurrentPlayer].Frames[CurrentTurn - 1]))
                throw new InvalidOperationException($"The last frame of {CurrentPlayer} is not complete, impossible to change the turn.");

            // Game is considered as "Finished" but edition is always possible.
            if (CurrentTurn == Scores[CurrentPlayer].Frames.Count
                && Players.Last().Equals(CurrentPlayer))
            {
                CurrentPlayer = null;
                return false;
            }

            int index = Players.IndexOf(CurrentPlayer) + 1;
            if ((index % Players.Count) == 0)
            {
                CurrentTurn++;
                CurrentPlayer = Players.First();
            }
            else
            {
                CurrentPlayer = Players[index];
            }
            return true;
        }

        /// <summary>
        /// Returns a collection of the ThrowResult that can be added to a specific player, frame and index. 
        /// Returns an empty collection if the player was not found in the game.
        /// </summary>
        /// <param name="player">The player concerned by these possible results.</param>
        /// <param name="indexFrameToAdd">The index of the frame to add a throw result.</param>
        /// <param name="indexToAdd">The index of the box of the frame to add the throw result.</param>
        public IEnumerable<ThrowResult> GetPossibleThrowResults(Player player, int indexFrameToAdd, int indexToAdd)
        {
            if (Players.Contains(player) && Scores[player] != null)
            {
                return Scores[player].GetPossibleThrowResults(indexFrameToAdd, indexToAdd);
            }
            return Enumerable.Empty<ThrowResult>();
        }

        /// <summary>
        /// Allow editting the results of the table score.
        /// Take in parameter the new Frame with the values that will replace the old values.
        /// Returns a boolean indicating if the frame has been modified.
        /// </summary>
        /// <param name="affectedPlayer">The player whose stats will be changed.</param>
        /// <param name="frameToReplace">The new frame to add. </param>
        /// <param name="frameIndex">The index of the frame to replace.</param>
        /// <returns>A boolean indicating if the edition has worked.</returns>
        /// <exception cref="ArgumentException">If the game is marked over and an editing attempt takes place.</exception>
        public bool EditResult(Player affectedPlayer, AFrame frameToReplace)
        {
            if (affectedPlayer == null || !_players.Contains(affectedPlayer) 
                || frameToReplace == null || frameToReplace.FrameNumberLabel < 0
                || frameToReplace.FrameNumberLabel > Scores[affectedPlayer].Frames.Count) 
                throw new ArgumentException("One of the argument given is not or not valid. "
                    + $"affectedPlayer: {affectedPlayer}, frameToReplace: {frameToReplace}.");

            ScoreTable scoreTable = Scores[affectedPlayer];
            AFrame actualFrame = Scores[affectedPlayer].Frames[frameToReplace.FrameNumberLabel - 1];

            if (frameToReplace.ThrowResults.Count != actualFrame.ThrowResults.Count) return false;
            if (!scoreTable.IsFrameComplete(frameToReplace)) return false;
            if (!IsFrameCorrect(frameToReplace)) return false;

            // If the player has already added a result to his score at this specific frame before edit it.
            if (frameToReplace.FrameNumberLabel < CurrentTurn
                || (frameToReplace.FrameNumberLabel == CurrentTurn && 
                (CurrentPlayer == null || affectedPlayer < CurrentPlayer))) {
                for (int i = 0; i < actualFrame.ThrowResults.Count; i++) {
                    scoreTable.WriteValue(actualFrame, i, frameToReplace.ThrowResults[i]);
                }
                scoreTable.UpdateFromFrame(frameToReplace.FrameNumberLabel - 1);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Add a result throw result in the score table of the current player of the game.
        /// </summary>
        /// <param name="index">The frame index where to write the score.</param>
        /// <param name="throwResult">The result to write into.</param>
        /// <returns>A boolean indicating if the throw result was added to the score table.</returns>
        /// <exception cref="InvalidOperationException">If the game is finished or if the throw result was not allowed to be putted here.</exception>
        public bool AddResultToCurrentPlayer(int index, ThrowResult throwResult)
        {
            if (IsFinished) throw new InvalidOperationException("Impossible to add a result to the game because the game is finished.");
            if (CurrentPlayer == null) return false;

            ScoreTable scoreTable = Scores[CurrentPlayer];

            if (!GetPossibleThrowResults(CurrentPlayer, CurrentTurn - 1, index).Contains(throwResult))
            {
                throw new InvalidOperationException($"Impossible to add the result {throwResult.ToChar()} to the score table of the current player, "
                    + "because the it is not a valid value.");
            }

            scoreTable.WriteValue(CurrentTurn - 1, index, throwResult);
            scoreTable.UpdateFromFrame(CurrentTurn - 1);
            return true;
        }

        /// <summary>
        /// Finish the game if all score tables are complete, and make it unmodifiable.
        /// </summary>
        /// <exception cref="InvalidOperationException">If not all the score tables are complete.</exception>
        public void Finish()
        {
            IEnumerable<KeyValuePair<Player, ScoreTable>> invalidScoreTables = Scores.Where(s => !s.Value.IsScoreTableComplete());
            if (!invalidScoreTables.Any())
            {
                IsFinished = true;
            }
            // Sould NEVER appened in the actual configuration.
            else
            {
                StringBuilder builder = new("Impossible to finish the game because ");
                builder.Append(invalidScoreTables.Count()).AppendLine(" score tables are not complete.")
                    .AppendLine("Here are the players who do not have their scoreboard filled in correctly: ");
                foreach (KeyValuePair<Player, ScoreTable> score in invalidScoreTables)
                {
                    builder.AppendLine(score.Key.ToString());
                }
                throw new InvalidOperationException(builder.ToString());
            }
        }

        /// <summary>
        /// Invoke the event for the game status changed.
        /// </summary>
        /// <param name="args">The arguments of the game status changed event.</param>
        private void OnGameStatusChanged(GameStatusChangedEventArgs args)
            => GameStatusChanged?.Invoke(this, args);

        /// <summary>
        /// Invoke the event for the game status changed.
        /// </summary>
        /// <param name="status">The new status of the game.</param>
        private void OnGameStatusChanged(bool status)
            => GameStatusChanged?.Invoke(this, new GameStatusChangedEventArgs(status));

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(ID);
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
            if (obj.GetType() != typeof(Game)) return false;
            return Equals(obj as Game);
        }

        /// <summary>
        /// Determines whether the two object instances are equal.
        /// </summary>
        /// <param name="other">The other game to compare with the actual game.</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, False.</returns>
        public bool Equals(Game other)
        {
            return other != null && other.ID.Equals(ID);
        }

        /// <summary>
        /// Returns a string representing a game.
        /// </summary>
        /// <returns>A string representing a game.</returns>
        public override string ToString()
        {
            StringBuilder builder = new("[Game ");
            builder.Append(ID).Append("] ");
            _ = IsFinished ? builder.Append("Finished at ") : builder.Append("In progress since at ");
            builder.AppendLine(CreationDate.ToString());
            foreach (KeyValuePair<Player, ScoreTable> scores in Scores)
            {
                builder.Append(scores.Key.ToString()).AppendLine(":");
                builder.AppendLine(scores.Value.ToString());
            }
            return builder.ToString();
        }

        /// <summary>
        /// Add a collection of players into the dictionnary and the list.
        /// </summary>
        /// <param name="players">The players to be added.</param>
        /// <param name="rules">The rules of the game.</param>
        private void AddPlayers(IEnumerable<Player> players, ARules rules)
        {
            foreach (Player p in players)
            {
                AddPlayer(p, new ScoreTable(rules));
            }
        }

        /// <summary>
        /// Add a player into the dictionnary and the list.
        /// </summary>
        /// <param name="player">The player to be added.</param>
        /// <param name="rules">The rules of the game.</param>
        private void AddPlayer(Player player, ScoreTable scoreTable)
        {
            if (player != null && !_scores.ContainsKey(player))
            {
                _scores.Add(player, scoreTable);
                _players.Add(player);
            }
        }

        /// <summary>
        /// Check if a frame is correct, it means if a frame respect the Bowling rules defined when the game is created.
        /// </summary>
        /// <param name="frame">The frame to check if it is correct.</param>
        /// <returns>A boolean indicating if the frame is correct.</returns>
        private bool IsFrameCorrect(AFrame frame)
        {
            object copy = frame.Clone();
            if (copy is not AFrame) return false;
            AFrame emptyFrame = (AFrame) copy;
            emptyFrame.CleanFrame();

            for (int i = 0; i < emptyFrame.ThrowResults.Count; i++)
            {
                if (!Scores[Players.First()].GetPossibleThrowResults(emptyFrame, i).Contains(frame.ThrowResults[i]))
                {
                    return false;
                }
                else
                {
                    Scores[Players.First()].WriteValue(emptyFrame, i, frame.ThrowResults[i]);
                }
            }
            return true;
        }
    }
}
