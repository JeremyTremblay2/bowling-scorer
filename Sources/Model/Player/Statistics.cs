using Model.score;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Player
{
    public class Statistics : IEquatable<Statistics>
    {
        private readonly IDictionary<ThrowResult, int> throwResults;
        private readonly IList<int> gamesID;
        private readonly IList<int> scores;

        public int NumberOfVictory { get; private set; }
        
        public int NumberOfDefeat { get; private set; }

        public int NumberOfGames { get; private set; }

        public int BestScore { get; private set; }

        public ReadOnlyCollection<int> Scores { get; private set; }

        public ReadOnlyDictionary<ThrowResult, int> ThrowResults { get; private set; }

        public ReadOnlyCollection<int> GamesID { get; private set; }

        public double MediumThrow
        {
            get => throwResults.Values.Average();
        }

        public double MediumScore
        {
            get => Scores.Average();
        }

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

        public void AddThrowResult(ThrowResult throwResult)
        {
            throwResults.Add(throwResult, 1);
        }

        public void AddGame(int gameID)
        {
            if (!gamesID.Contains(gameID))
            {
                gamesID.Add(gameID);
            }
            // Implements logic here to add scores and stats.
        }

        public void RemoveGame(int gameID)
        {
            gamesID.Remove(gameID);
            // Implements logic here to remove scores and stats.
        }

        public override bool Equals(Object other)
        {
            if (other == null) return false;
            if (other == this) return true;
            if (other.GetType() != this.GetType()) return false;
            return Equals((ThrowResult) other);
        }

        public bool Equals(Statistics other)
        {
            return other != null
                && other.Scores.SequenceEqual(Scores);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Scores);
        }

        public override string ToString()
        {
            StringBuilder builder = new();
            _ = builder.Append("Statistics of ").Append(NumberOfGames).AppendLine(" games: ")
                .Append("V: ").Append(NumberOfVictory).Append(", D: ").Append(NumberOfDefeat)
                .Append(", Best Score: ").Append(BestScore).Append(", Medium Throw: ")
                .Append(MediumThrow).Append(", Medium Score: ").Append(MediumScore);
            return builder.ToString();
        }

        private void AddScore(int score)
        {
            if (score > BestScore)
            {
                BestScore = score;
            }
            scores.Add(score);
        }
    }
}
