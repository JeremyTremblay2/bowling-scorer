using FrameWriterModel.Frame;
using FrameWriterModel.Frame.ThrowResults;
using Model.Players;
using Model.Score.Rules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Model.Score
{
    /// <summary>
    /// A Score table contains Frames, she represents the grid score table that we can see in bowling
    /// This score table apply the given injected rules (in constructor) to compute score, write scores...
    /// </summary>
    public class ScoreTable : IEquatable<ScoreTable>
    {
        /// <summary>
        /// The ScoreTable content
        /// </summary>
        private readonly IList<AFrame> _frames;

        /// <summary>
        /// Rules used in this class
        /// </summary>
        private readonly ARules rules;

        /// <summary>
        /// The ScoreTable content
        /// </summary>
        public ReadOnlyCollection<AFrame> Frames { get; private set; }

        /// <summary>
        /// Total score of the table, computed with the given rules
        /// </summary>
        public int TotalScore => rules.CalculateScore(_frames);

        /// <summary>
        /// Create a new ScoreTble with the rules specified.
        /// </summary>
        /// <param name="rules">The bowling rules.</param>
        public ScoreTable(ARules rules)
        {
            this.rules = rules;
            _frames = rules.GenerateScoreTable();
            Frames = new ReadOnlyCollection<AFrame>(_frames);
        }

        /// <summary>
        /// Write a value in a frame at the given index by using given rules at the constructor
        /// </summary>
        /// <param name="frame">The frame which will be write into.</param>
        /// <param name="index">The index of the box inside the frame.</param>
        /// <param name="throwResult">The result to write.</param>
        public void WriteValue(AFrame frame, int index, ThrowResult throwResult)
            => rules.WriteValue(frame, index, throwResult);

        /// <summary>
        /// Write a value in a frame at the given index by using given rules at the constructor.
        /// </summary>
        /// <param name="frameIndex">The index of the frame which will be write into.</param>
        /// <param name="index">The index of the box inside the frame.</param>
        /// <param name="throwResult">The result to write.</param>
        public void WriteValue(int frameIndex, int index, ThrowResult throwResult)
            => rules.WriteValue(Frames[frameIndex], index, throwResult);

        /// <summary>
        /// Update the last frame of the ScoreBoard by using the rules
        /// </summary>
        public void UpdateLastFrame() => rules.UpdateLastFrame(_frames);

        /// <summary>
        /// Update the given frame in the ScoreBoard by using the rules.
        /// </summary>
        /// <param name="index">The index of the frame to update.</param>
        public void UpdateFromFrame(int index) => rules.UpdateFromFrame(index, _frames);

        /// <summary>
        /// Returns a boolean indicating whether the scoreboard is complete according to established bowling rules.
        /// </summary>
        /// <param name="scoreTable">The score table to inspect.</param>
        /// <returns>A boolean indicating whether the scoreboard is complete or not.</returns>
        public bool IsScoreTableComplete() => rules.IsScoreTableComplete(this);

        /// <summary>
        /// Returns a collection of the ThrowResult that can be added to a specific frame and index.
        /// </summary>
        /// <param name="frameToAdd">The frame to add a throw result.</param>
        /// <param name="indexToAdd">The index of the box of the frame to add the throw result.</param>
        public IEnumerable<ThrowResult> GetPossibleThrowResults(AFrame frameToAdd, int indexToAdd)
            => rules.GetPossibleThrowResults(frameToAdd, indexToAdd);

        /// <summary>
        /// Returns a collection of the ThrowResult that can be added to a specific frame and index.
        /// </summary>
        /// <param name="indexFrameToAdd">The index of the frame to add a throw result.</param>
        /// <param name="indexToAdd">The index of the box of the frame to add the throw result.</param>
        public IEnumerable<ThrowResult> GetPossibleThrowResults(int indexFrameToAdd, int indexToAdd)
            => rules.GetPossibleThrowResults(Frames[indexFrameToAdd], indexToAdd);

        /// <summary>
        /// Returns a boolean indicating whether the frame is complete according to established bowling rules.
        /// </summary>
        /// <param name="frame">The frame to inspect.</param>
        /// <returns>A boolean indicating whether the frame is complete or not.</returns>
        public bool IsFrameComplete(AFrame frame) => rules.IsFrameComplete(frame);

        /// <summary>
        /// Returns a boolean indicating whether the rules are equals to rules defined for this ScoreTable.
        /// </summary>
        /// <param name="rules">The Rules to compare.</param>
        /// <returns>A boolean indicating whether the rules are equals or not.</returns>
        public bool AreRulesEquals(ARules rules) => this.rules.Equals(rules);

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Frames, TotalScore);
        }

        /// <summary>
        /// Determines whether the two object instances are equal.
        /// </summary>
        /// <param name="other">The object to compare with the actual object.</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, False.</returns>
        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(ScoreTable)) return false;
            return Equals((ScoreTable) obj);
        }

        /// <summary>
        /// Determines whether the two object instances are equal.
        /// </summary>
        /// <param name="other">The ScoreTable to compare with the actual ScoreTable.</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, False.</returns>
        public bool Equals(ScoreTable other)
        {
            if (other == null || other.Frames.Count != Frames.Count) return false;
            for (int i = 0; i < other.Frames.Count; i++)
            {
                if (other.Frames[i] != Frames[i]) return false;
            }
            return (TotalScore == other.TotalScore);
        }

        // <summary>
        /// Returns a string representing a ScoreTable.
        /// </summary>
        /// <returns>A string representing a ScoreTable.</returns>
        public override string ToString()
        {
            StringBuilder builder = new("[ScoreTable] - Total: \n");
            builder.AppendLine(TotalScore.ToString());
            foreach (AFrame aFrame in _frames)
            {
                builder.AppendLine(aFrame.ToString());
            }
            return builder.ToString();
        }
    }
}
