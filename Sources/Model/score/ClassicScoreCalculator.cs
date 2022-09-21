using Model.exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Model.score
{
    public class ClassicScoreCalculator : IScoreCalculator
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
            ComputeOneFrame(index, frames);
        }

        public void UpdateLastFrame(List<Frame> frames)
        {
            int index = frames.Count - 1;
            ComputeOneFrame(index, frames);
        }

        private void ComputeOneFrame(int selectedFrameIdx, List<Frame> scoreBoard)
        {
            Frame frame = scoreBoard[selectedFrameIdx];
            int calculatedScore = 0;
            if (frame.isStrike() || frame.isSpair())
            {
                Frame nextFrame = scoreBoard[selectedFrameIdx + 1];
                if (nextFrame == null) throw new MissingFrameException();
                calculatedScore = 10;
                if (frame.isStrike())
                {
                    if (nextFrame.isStrike())
                    {
                        Frame nextNextFrame = scoreBoard[selectedFrameIdx + 2];
                        if (nextNextFrame == null) throw new MissingFrameException();
                        calculatedScore = calculatedScore + 10;
                        if (nextNextFrame.isStrike())
                        {
                            calculatedScore = calculatedScore + 10;
                        }
                        else
                        {
                            calculatedScore = calculatedScore + ThrowResultExtension.ToInt(nextNextFrame.ThrowResults[0]);
                        }
                    }
                    else if (nextFrame.isSpair())
                    {
                        calculatedScore = calculatedScore + 10;
                    }
                    else
                    {
                        calculatedScore = calculatedScore + ThrowResultExtension.ToInt(nextFrame.ThrowResults[0])
                                                          + ThrowResultExtension.ToInt(nextFrame.ThrowResults[1]);
                    }
                }
                else if (frame.isSpair())
                {
                    if (nextFrame.isStrike())
                    {
                        calculatedScore = calculatedScore + 10;
                    }
                    else
                    {
                        calculatedScore = calculatedScore + ThrowResultExtension.ToInt(nextFrame.ThrowResults[0]);
                    }
                }
            }
            else
            {
                calculatedScore = ThrowResultExtension.ToInt(frame.ThrowResults[0]) + ThrowResultExtension.ToInt(frame.ThrowResults[1]);
            }
            frame.ScoreValue = calculatedScore;
        }
    }
}
