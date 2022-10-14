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
    public class Game : IEquatable<Game>
    {

        private readonly IList<Player> _players;

        private readonly Dictionary<Player, ScoreTable> _scores;

        /// <summary>
        /// A unique identifier for the game.
        /// </summary>
        public int ID { get; private set; }

        public bool IsFinished { get; private set; }

        public DateTime CreationDate { get; private set; }

        public int CurrentTurn { get; private set; }

        public Player CurrentPlayer { get; private set; }

        public ReadOnlyDictionary<Player, ScoreTable> Scores { get; private set; }

        public ReadOnlyCollection<Player> Players { get; private set; }

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
            CurrentTurn = 0;
            CurrentPlayer = Players.First();
            IsFinished = false;
        }

        public Game(ARules rules, IEnumerable<Player> players) : this(rules, 0, players)
        {
        }

        public bool NextTurn()
        {
            if (IsFinished) throw new InvalidOperationException("The game is finished, impossible to pass to the next turn.");
            if (CurrentPlayer == null) return false;
            if (!IsFrameCorrect(Scores[CurrentPlayer].Frames[CurrentTurn]))
                throw new InvalidOperationException($"The last frame of {CurrentPlayer} is not complete, impossible to change the turn.");

            // Game is considered as "Finished" but edition is always possible.
            if (CurrentTurn == Scores[CurrentPlayer].Frames.Count - 1
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
        /// <exception cref="InvalidOperationException">If the game is marked over and an editing attempt takes place.</exception>
        public bool EditResult(Player affectedPlayer, AFrame frameToReplace)
        {
            if (IsFinished) throw new InvalidOperationException("Impossible to edit a result of the game because the game is finished.");
            if (affectedPlayer == null || !_players.Contains(affectedPlayer) 
                || frameToReplace == null || frameToReplace.FrameNumberLabel < 0
                || frameToReplace.FrameNumberLabel > Scores[affectedPlayer].Frames.Count) return false;

            ScoreTable scoreTable = Scores[affectedPlayer];
            AFrame actualFrame = Scores[affectedPlayer].Frames[frameToReplace.FrameNumberLabel];

            if (frameToReplace.ThrowResults.Count != actualFrame.ThrowResults.Count) return false;
            if (!scoreTable.IsFrameComplete(frameToReplace)) return false;
            if (!IsFrameCorrect(frameToReplace)) return false;

            // If the player has already added a result to his score at this specific frame before edit it.
            if (frameToReplace.FrameNumberLabel < CurrentTurn
                || (frameToReplace.FrameNumberLabel == CurrentTurn && affectedPlayer < CurrentPlayer)) {
                for (int i = 0; i < actualFrame.ThrowResults.Count; i++) {
                    scoreTable.WriteValue(actualFrame, i, frameToReplace.ThrowResults[i]);
                }
                scoreTable.UpdateFromFrame(frameToReplace.FrameNumberLabel);
                return true;
            }
            return false;
        }

        public bool AddResultToCurrentPlayer(int index, ThrowResult throwResult)
        {
            if (IsFinished) throw new InvalidOperationException("Impossible to add a result to the game because the game is finished.");
            if (CurrentPlayer == null) return false;

            ScoreTable scoreTable = Scores[CurrentPlayer];

            if (!GetPossibleThrowResults(CurrentPlayer, CurrentTurn, index).Contains(throwResult))
            {
                throw new InvalidOperationException($"Impossible to add the result {throwResult.ToChar()} to the score table of the current player, "
                    + "because the it is not a valid value.");
            }

            scoreTable.WriteValue(CurrentTurn, index, throwResult);
            scoreTable.UpdateFromFrame(CurrentTurn);
            return true;
        }

        public void Finish()
        {
            IEnumerable<KeyValuePair<Player, ScoreTable>> invalidScoreTables = Scores.Where(s => !s.Value.IsScoreTableComplete());
            if (!invalidScoreTables.Any())
            {
                IsFinished = true;
            }
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

        public override int GetHashCode()
        {
            return HashCode.Combine(ID);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (ReferenceEquals(obj, this)) return true;
            if (obj.GetType() != typeof(Game)) return false;
            return Equals(obj as Game);
        }

        public bool Equals(Game other)
        {
            return other != null && other.ID.Equals(ID);
        }

        public override string ToString()
        {
            StringBuilder builder = new("[Game ");
            builder.Append(ID).Append("] ");
            _ = IsFinished == true ? builder.Append("Finished at ") : builder.Append("In progress since at ");
            builder.AppendLine(CreationDate.ToString());
            foreach (KeyValuePair<Player, ScoreTable> scores in Scores)
            {
                builder.Append(scores.Key.ToString()).AppendLine(":");
                builder.AppendLine(scores.Value.ToString());
            }
            return builder.ToString();
        }

        private void AddPlayers(IEnumerable<Player> players, ARules rules)
        {
            foreach (Player p in players)
            {
                AddPlayer(p, rules);
            }
        }

        private void AddPlayer(Player player, ARules rules)
        {
            if (player != null && !_scores.ContainsKey(player))
            {
                _scores.Add(player, new ScoreTable(rules));
                _players.Add(player);
            }
        }

        private bool IsFrameCorrect(AFrame frame)
        {
            if (frame == null) return false;
            object copy = frame.Clone();
            if (copy == null || copy is not AFrame) return false;
            AFrame emptyFrame = (AFrame) copy;
            for (int i = 0; i < emptyFrame.ThrowResults.Count; i++)
            {
                Scores[Players.First()].WriteValue(emptyFrame, i, ThrowResult.NONE);
            }

            for (int i = 0; i < emptyFrame.ThrowResults.Count; i++)
            {
                if (!Scores[Players.First()].GetPossibleThrowResults(emptyFrame, i).Contains(frame.ThrowResults[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
