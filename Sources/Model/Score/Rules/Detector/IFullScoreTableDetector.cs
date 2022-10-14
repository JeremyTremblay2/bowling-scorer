using FrameWriterModel.Frame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Score.Rules.Detector
{
    /// <summary>
    /// An IFullScoreTableDetector's job is to provide a method to know if a score table is full 
    /// or not according to established bowling rules.
    /// </summary>
    public interface IFullScoreTableDetector
    {
        /// <summary>
        /// Returns a boolean indicating whether the scoreboard is complete according to established bowling rules.
        /// </summary>
        /// <param name="scoreTable">The score table to inspect.</param>
        /// <returns>A boolean indicating whether the scoreboard is complete or not.</returns>
        bool IsScoreTableComplete(ScoreTable scoreTable);

        /// <summary>
        /// Returns a boolean indicating whether the frame is complete according to established bowling rules.
        /// </summary>
        /// <param name="frame">The frame to inspect.</param>
        /// <returns>A boolean indicating whether the frame is complete or not.</returns>
        bool IsFrameComplete(AFrame frame);
    }
}
