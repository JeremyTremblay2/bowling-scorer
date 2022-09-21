using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.score
{
    internal class ClassicScoreCalculator : IScoreCalculator
    {
        public int CalculateScore(List<Frame> frames)
        {
            int total = 0;
            foreach (Frame frame in frames)
            {
                total = total + frame.ScoreValue;
            }
            return total;
        }

        public void UpdateFromFrame(int index, List<Frame> frames)
        {
            Frame frame = frames[index];
            if (IsStrike(frame) && frames.Count - (index + 1) >= 2)
            {
                
            }
            Frame precedent;
            if (index - 1 >= 0)
                precedent = frames[index - 1];
            else
            {
                
            }
        }

        public void UpdateLastFrame(List<Frame> frames)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Check if the given frame contains a STRIKE
        /// </summary>
        /// <param name="frame"></param>
        /// <returns></returns>
        private Boolean IsStrike(Frame frame)
        {
            return frame.ThrowResults[1] == ThrowResult.STRIKE;
        }
    }
}
