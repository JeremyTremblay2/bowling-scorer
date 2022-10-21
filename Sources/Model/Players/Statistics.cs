using FrameWriterModel.Frame;
using FrameWriterModel.Frame.ThrowResults;
using Model.Games;
using Model.Score;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Model.Players
{
    /// <summary>
    /// Statistics represent various data collected through different games of bowling and are intrinsic to a particular player.
    /// We can for example cite the average score, the number of games won or the best result. 
    /// This class is therefore responsible for encapsulating this data.
    /// </summary>
    public class Statistics : IEquatable<Statistics>, IComparable, IComparable<Statistics>
    {
        private readonly IDictionary<ThrowResult, int> _throwResults;
        private readonly IList<Game> _games;
        private readonly IList<int> _scores;

        /// <summary>
        /// Represents the number of total wins by a player.
        /// </summary>
        public int NumberOfVictory { get; private set; }

        /// <summary>
        /// Represents the total number of games lost by a player.
        /// </summary>
        public int NumberOfDefeat { get; private set; }

        /// <summary>
        /// Represents the total number of games a player has participated in.
        /// </summary>
        public int NumberOfGames { get; private set; }

        /// <summary>
        /// Represents the player's best score.
        /// </summary>
        public int BestScore { get; private set; }

        /// <summary>
        /// Contains all the scores achieved by the player in each game played.
        /// </summary>
        public ReadOnlyCollection<int> Scores { get; private set; }

        /// <summary>
        /// Contains the frequency of the number of pins that fall with each throw of the player.
        /// </summary>
        public ReadOnlyDictionary<ThrowResult, int> ThrowResults { get; private set; }

        /// <summary>
        /// Contains each game ID for each game played by the player.
        /// </summary>
        public ReadOnlyCollection<Game> Games { get; private set; }

        /// <summary>
        /// Returns the number of average pins knocked down in a player's throw.
        /// </summary>
        public double MediumThrow
        {
            get => _throwResults.Values.Average();
        }

        /// <summary>
        /// Returns the average score achieved by the player over all of his games.
        /// </summary>
        public double MediumScore
        {
            get => Scores.Average();
        }

        /// <summary>
        /// Create a new instance of statistics.
        /// </summary>
        public Statistics()
        {
            NumberOfVictory = NumberOfDefeat = BestScore = 0;
            _throwResults = new Dictionary<ThrowResult, int>();
            ThrowResults = new ReadOnlyDictionary<ThrowResult, int>(_throwResults);
            _games = new List<Game>();
            Games = new ReadOnlyCollection<Game>(_games);
            _scores = new List<int>();
            Scores = new ReadOnlyCollection<int>(_scores);
        }

        /// <summary>
        /// Adds a game played by the player and adds his results.
        /// </summary>
        /// <param name="game">The game to be added.</param>
        /// <returns>A boolean indicating if the game was added.</returns>
        public bool AddGame(Player player, Game game)
        {
            if (game == null || _games.Contains(game) || !game.IsFinished 
                || player == null || !game.Players.Contains(player)) return false;
            _games.Add(game);
            AddScore(game.Scores[player].TotalScore);
            UpdateNumberOfGames(game, player, +1);
            UpdateThrowResults(game.Scores[player], true);
            return true;
        }

        /// <summary>
        /// Deletes a game and all scores related to it.
        /// </summary>
        /// <param name="gameID"></param>
        public bool RemoveGame(Player player, Game game)
        {
            if (game == null || _games.Contains(game) || player == null 
                || !game.Players.Contains(player)) return false;
            _games.Remove(game);
            RemoveScore(game.Scores[player].TotalScore);
            UpdateNumberOfGames(game, player, -1);
            UpdateThrowResults(game.Scores[player], false);
            return true;
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
            if (obj.GetType() != typeof(Statistics)) return false;
            return Equals(obj as Statistics);
        }

        /// <summary>
        /// Determines whether the two object instances are equal.
        /// </summary>
        /// <param name="other">The statistics to compare with the actual stats.</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, False.</returns>
        public bool Equals(Statistics other)
        {
            return other != null
                && other.Scores.SequenceEqual(Scores);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Scores);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return string.Format("Statistics of {0} games. Victories: {1}. Defeats: {2}. \nBest Score: {3}. \nMedium Throw: {4}. \nMedium Score: {5} ", 
                NumberOfGames, NumberOfVictory, NumberOfDefeat, BestScore, MediumThrow, MediumScore);
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates 
        /// whether the current instance precedes, follows, or occurs in the same position in the sort order as 
        /// the other object.
        /// </summary>
        /// <param name="obj">The object to compare to this.</param>
        /// <returns>An integer indicating the result of the comparison.</returns>
        /// <exception cref="ArgumentException">If obj is not Statistics.</exception>
        int IComparable.CompareTo(object obj)
        {
            if (obj is not Statistics)
            {
                throw new ArgumentException("The argument is not a statistic.", nameof(obj));
            }
            return CompareTo((Statistics)obj);
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates 
        /// whether the current instance precedes, follows, or occurs in the same position in the sort order as 
        /// the other object.
        /// </summary>
        /// <param name="other">The others statistics to compare to this.</param>
        /// <returns>An integer indicating the result of the comparison.</returns>
        public int CompareTo(Statistics other)
        {
            return BestScore.CompareTo(other.BestScore);
        }

        /// <summary>
        /// Returns true if its left-hand operand is less than its right-hand operand, false otherwise.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>An integer indicating the result of the comparison.</returns>
        public static bool operator <(Statistics left, Statistics right)
            => left.CompareTo(right) < 0;

        /// <summary>
        /// Returns true if its left-hand operand is less than or equal to its right-hand operand, false otherwise.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>An integer indicating the result of the comparison.</returns>
        public static bool operator <=(Statistics left, Statistics right)
            => left.CompareTo(right) <= 0;

        /// <summary>
        /// Returns true if its left-hand operand is greater than its right-hand operand, false otherwise.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>An integer indicating the result of the comparison.</returns>
        public static bool operator >(Statistics left, Statistics right)
            => left.CompareTo(right) > 0;

        /// <summary>
        /// Returns true if its left-hand operand is greater than or equal to its right-hand operand, false otherwise.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>An integer indicating the result of the comparison.</returns>
        public static bool operator >=(Statistics left, Statistics right)
            => left.CompareTo(right) >= 0;

        /// <summary>
        /// Returns true if its left-hand operand is equals to its right-hand operand, false otherwise.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>A boolean indicating the result of the comparison.</returns>
        public static bool operator ==(Statistics left, Statistics right)
        {
            if (left is null) return right is null;
            return left.Equals(right);
        }

        /// <summary>
        /// Returns true if its left-hand operand is not equals to its right-hand operand, false otherwise.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>A boolean indicating the result of the comparison.</returns>
        public static bool operator !=(Statistics left, Statistics right)
            => !(left == right);

        /// <summary>
        /// Adds a score to the results.
        /// </summary>
        /// <param name="score">The score to be added.</param>
        private void AddScore(int score)
        {
            if (score > BestScore)
            {
                BestScore = score;
            }
            _scores.Add(score);
        }

        /// <summary>
        /// Removes a score to the results.
        /// </summary>
        /// <param name="score">The score to be removed.</param>
        private void RemoveScore(int score)
        {
            _scores.Remove(score);
            BestScore = Scores.Max();
        }

        /// <summary>
        /// Update the number of games depending if a game was added or removed.
        /// </summary>
        /// <param name="game">The game to be added.</param>
        /// <param name="player">The player to be added.</param>
        /// <param name="increment">The increment of the number of games (increasing or decreasing).</param>
        private void UpdateNumberOfGames(Game game, Player player, int increment)
        {
            NumberOfGames += increment;
            if (player.Equals(game.Winner))
            {
                NumberOfVictory += increment;
            }
            if (player.Equals(game.Loser))
            {
                NumberOfDefeat += increment;
            }
        }

        /// <summary>
        /// Update the Thorw results depending if the game was added or removed.
        /// </summary>
        /// <param name="scoreTable">The scoreboard containing the throw results.</param>
        /// <param name="willBeAdded">A boolean indicating if the results will be incremented or not.</param>
        private void UpdateThrowResults(ScoreTable scoreTable, bool willBeAdded)
        {
            foreach (AFrame frame in scoreTable.Frames)
            {
                foreach (ThrowResult throwResult in frame.ThrowResults)
                {
                    if (willBeAdded)
                    {
                        _throwResults[throwResult]++;
                    }
                    else
                    {
                        _throwResults[throwResult]--;
                    }
                }
            }
        }
    }
}
