using Model.Score;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Players
{
    /// <summary>
    /// Statistics represent various data collected through different games of bowling and are intrinsic to a particular player.
    /// We can for example cite the average score, the number of games won or the best result. 
    /// This class is therefore responsible for encapsulating this data.
    /// </summary>
    public class Statistics : IEquatable<Statistics>, IComparable, IComparable<Statistics>
    {
        private readonly IDictionary<ThrowResult, int> throwResults;
        private readonly IList<int> gamesID;
        private readonly IList<int> scores;

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
        public ReadOnlyCollection<int> GamesID { get; private set; }

        /// <summary>
        /// Returns the number of average pins knocked down in a player's throw.
        /// </summary>
        public double MediumThrow
        {
            get => throwResults.Values.Average();
        }

        /// <summary>
        /// Returns the average score achieved by the player over all of his games.
        /// </summary>
        public double MediumScore
        {
            get => Scores.Average();
        }

        /// <summary>
        /// Create some new statistics.
        /// </summary>
        public Statistics()
        {
            NumberOfVictory = NumberOfDefeat = BestScore = 0;
            throwResults = new Dictionary<ThrowResult, int>();
            ThrowResults = new ReadOnlyDictionary<ThrowResult, int>(throwResults);
            gamesID = new List<int>();
            GamesID = new ReadOnlyCollection<int>(gamesID);
            scores = new List<int>();
            Scores = new ReadOnlyCollection<int>(scores);
        }

        /// <summary>
        /// Add a throw result to the player's statistics.
        /// </summary>
        /// <param name="throwResult">The throw result of the player.</param>
        public void AddThrowResult(ThrowResult throwResult)
        {
            throwResults.Add(throwResult, 1);
        }

        /// <summary>
        /// Adds a game played by the player and adds his results.
        /// </summary>
        /// <param name="gameID"></param>
        public void AddGame(int gameID)
        {
            if (!gamesID.Contains(gameID))
            {
                gamesID.Add(gameID);
            }
            NumberOfGames++;
            // Implements logic here to add scores and stats.
        }

        /// <summary>
        /// Deletes a game and all scores related to it.
        /// </summary>
        /// <param name="gameID"></param>
        public void RemoveGame(int gameID)
        {
            gamesID.Remove(gameID);
            NumberOfGames--;
            // Implements logic here to remove scores and stats.
        }

        /// <summary>
        /// Determines whether the two object instances are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the actual object.</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, False.</returns>
        public override bool Equals(Object obj)
        {
            if (obj == null) return false;
            if (obj == this) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ThrowResult) obj);
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
            StringBuilder builder = new();
            _ = builder.Append("Statistics of ").Append(NumberOfGames).AppendLine(" games: ")
                .Append("V: ").Append(NumberOfVictory).Append(", D: ").Append(NumberOfDefeat)
                .Append(", Best Score: ").Append(BestScore).Append(", Medium Throw: ")
                .Append(MediumThrow).Append(", Medium Score: ").Append(MediumScore);
            return builder.ToString();
        }

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
            scores.Add(score);
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
            return CompareTo((Statistics) obj);
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
    }
}
