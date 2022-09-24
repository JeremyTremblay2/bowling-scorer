using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.score
{
    /// <summary>
    /// ScoreCalculator is used to compute the results in frames
    /// </summary>
    public interface IScoreCalculator
    {
        /// <summary>
        /// Calculate the total score of a Frame list
        /// </summary>
        /// <param name="frames">List of frames</param>
        /// <returns>The total score calculated</returns>
        public int CalculateScore(List<AFrame> frames);

        /// <summary>
        /// Update the score of the last Frame
        /// </summary>
        /// <param name="frames">List of frames</param>
        public void UpdateLastFrame(List<AFrame> frames);
        /// <summary>
        /// Update the score from a specific Frame
        /// </summary>
        /// <param name="index">Index of the frame to update</param>
        /// <param name="frames">List of frames</param>
        public void UpdateFromFrame(int index, List<AFrame> frames);
    }
}
