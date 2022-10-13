using FrameWriterModel.Frame;
using FrameWriterModel.Frame.ThrowResults;
using Model.Score.Rules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Score
{
    /// <summary>
    /// A Score table contains Frames, she represents the grid score table that we can see in bowling
    /// This score table apply the given injected rules (in constructor) to compute score, write scores...
    /// </summary>
    public class ScoreTable
    {
        /// <summary>
        /// The ScoreTable content
        /// </summary>
        private readonly List<AFrame> _scoreTable;
        /// <summary>
        /// The ScoreTable content
        /// </summary>
        public ReadOnlyCollection<AFrame> FrameScoreTable;
        /// <summary>
        /// Rules used in this class
        /// </summary>
        private readonly ARules rules;

        /// <summary>
        /// Total score of the table, computed with the give, rules
        /// </summary>
        public int TotalScore => rules.CalculateScore(_scoreTable);

        /// <summary>
        /// A table of score, the content and rules applied are defined by the given Rules
        /// </summary>
        /// <param name="rules"></param>
        public ScoreTable(ARules rules)
        {
            this.rules = rules;
            _scoreTable = rules.GenerateScoreTable();
            FrameScoreTable = new ReadOnlyCollection<AFrame>(_scoreTable);
        }

        /// <summary>
        /// Write a value in a frame at the given index by using given rules at the constructor
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="index"></param>
        /// <param name="throwResult"></param>
        public void WriteValue(AFrame frame, int index, ThrowResult throwResult)
        {
            rules.WriteValue(frame, index, throwResult);
        }

        /// <summary>
        /// Update the last frame of the ScoreBoard by using the rules
        /// </summary>
        public void UpdateLastFrame()
        {
            rules.UpdateLastFrame(_scoreTable);
        }

        /// <summary>
        /// Update the given frame in the ScoreBoard by using the rules
        /// </summary>
        /// <param name="index"></param>
        public void UpdateFromFrame(int index)
        {
            rules.UpdateFromFrame(index, _scoreTable);
        }
    }
}
