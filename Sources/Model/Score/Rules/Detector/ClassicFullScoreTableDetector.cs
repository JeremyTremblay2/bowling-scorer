using FrameWriterModel.Frame;
using FrameWriterModel.Frame.ThrowResults;
using Model.Score.Rules.Retriever;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Score.Rules.Detector
{
    /// <summary>
    /// A ClassicPossibleThrowResultsRetriever's job is to provide a method to know if a score table is full 
    /// or not according to the classic bowling rules.
    /// </summary>
    public class ClassicFullScoreTableDetector : IFullScoreTableDetector
    {
        /// <summary>
        /// Returns a boolean indicating whether the scoreboard is complete according to established classic bowling rules.
        /// </summary>
        /// <param name="scoreTable">The score table to inspect.</param>
        /// <returns>A boolean indicating whether the scoreboard is complete or not.</returns>
        public bool IsScoreTableComplete(ScoreTable scoreTable)
        {
            if (scoreTable == null) return false;
            foreach (var frame in scoreTable.Frames)
            {
                if (frame is ClassicFrame classicFrame)
                {
                    if (!IsClassicFrameComplete(classicFrame)) return false;
                }
                else if (frame is ClassicLastFrame lastFrame && !IsClassicLastFrameComplete(lastFrame))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Returns a boolean indicating whether the frame is complete according to established classic bowling rules.
        /// </summary>
        /// <param name="frame">The frame to inspect.</param>
        /// <returns>A boolean indicating whether the frame is complete or not.</returns>
        public bool IsFrameComplete(AFrame frame)
        {
            if (frame is ClassicFrame classicFrame)
                return IsClassicFrameComplete(classicFrame);
            else if (frame is ClassicLastFrame lastFrame)
                return IsClassicLastFrameComplete(lastFrame);
            else return false;
        }

        /// <summary>
        /// Compute a classic frame to know if it is not completed.
        /// </summary>
        /// <param name="frame">The frame to compute.</param>
        /// <returns>A boolean indicating ifthe frame is complete.</returns>
        private bool IsClassicFrameComplete(ClassicFrame frame)
        {
            if (frame == null) return false;
            if (frame.IsEmpty) return false;
            if (frame.IsStrike) return true;
            if (frame.ThrowResults[0] == ThrowResult.NONE 
                || frame.ThrowResults[1] == ThrowResult.NONE) return false;
            return true;
        }

        /// <summary>
        /// Compute a classic last frame to know if it is not completed.
        /// </summary>
        /// <param name="frame">The frame to compute.</param>
        /// <returns>A boolean indicating ifthe frame is complete.</returns>
        private bool IsClassicLastFrameComplete(ClassicLastFrame frame)
        {
            if (frame == null) return false;
            if (frame.IsEmpty) return false;
            if (frame.ThrowResults[0] == ThrowResult.NONE
                || frame.ThrowResults[1] == ThrowResult.NONE) return false;
            if (frame.ThrowResults[2] == ThrowResult.NONE &&
                (frame.ThrowResults[0] == ThrowResult.STRIKE
                || frame.ThrowResults[1] == ThrowResult.STRIKE
                || (frame.ThrowResults[1] == ThrowResult.SPARE))) return false;
            return true;
        }
    }
}
