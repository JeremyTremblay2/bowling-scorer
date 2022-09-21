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
        public int CalculateScore(List<AFrame> frames)
        {
            int total = 0;
            foreach (AFrame frame in frames)
            {
                total = total + frame.ScoreValue;
            }
            return total;
        }

        public void UpdateFromFrame(int index, List<AFrame> frames)
        {
            AFrame frame0 = frames[index];
            if (frame0 == null) return;
            if (frame0 is ClassicFrame)
            {
                ClassicFrame classic0 = (ClassicFrame)frame0;
                if (index + 1 == 8)
                {
                    //la prochaine frame est une last
                }
                else
                {
                    ComputeClassicFrame(classic0, frames);
                    ComputeOlderClassicFrames(index, frames);
                }
            }
            else if (frame0 is ClassicLastFrame)
            {
                ComputeClassicLastFrame((ClassicLastFrame)frame0);
            }
        }

        public void UpdateLastFrame(List<AFrame> frames)
        {
            int index = frames.Count - 1;
            AFrame frame0 = frames[index];
            if (frame0 == null) return;
            if (frame0 is ClassicFrame)
            {
                ClassicFrame classic0 = (ClassicFrame)frame0;
                ComputeClassicFrame(classic0, frames);
            }
            else if (frame0 is ClassicLastFrame)
            {
                ComputeClassicLastFrame((ClassicLastFrame)frame0);
            }
        }

        private void ComputeOlderClassicFrames(int index, List<AFrame> frames)
        {
            ComputeClassicFrame((ClassicFrame)frames[index - 1], frames);
            ComputeClassicFrame((ClassicFrame)frames[index - 2], frames);
        }

        private void ComputeClassicLastFrame(ClassicLastFrame frame0)
        {
            int computedScore = 0;
            if (frame0.ThrowResults[0] == ThrowResult.STRIKE)
            {
                computedScore = computedScore + 10;
                if (frame0.ThrowResults[1] == ThrowResult.STRIKE)
                {
                    computedScore = computedScore + 10;
                    if (frame0.ThrowResults[2] == ThrowResult.STRIKE)
                    {
                        computedScore = computedScore + 10;
                    }
                }
                else
                {
                    if (frame0.ThrowResults[2] == ThrowResult.SPAIR)
                    {
                        computedScore = computedScore + 10;
                    }
                    else
                    {
                        computedScore = computedScore + ThrowResultExtension.ToInt(frame0.ThrowResults[1])
                                                      + ThrowResultExtension.ToInt(frame0.ThrowResults[2]);
                    }
                }
            }
            else
            {
                if (frame0.ThrowResults[1] == ThrowResult.SPAIR)
                {
                    computedScore = computedScore + 10;
                    if (frame0.ThrowResults[2] == ThrowResult.STRIKE)
                    {
                        computedScore = computedScore + 10;
                    }
                    else
                    {
                        computedScore = computedScore + ThrowResultExtension.ToInt(frame0.ThrowResults[2]);
                    }
                }
                else
                {
                    computedScore = computedScore + ThrowResultExtension.ToInt(frame0.ThrowResults[0])
                                                  + ThrowResultExtension.ToInt(frame0.ThrowResults[0]);
                }
            }
            frame0.ScoreValue = computedScore;
        }

        private void ComputeClassicFrame(ClassicFrame frame0, List<AFrame> scoreBoard)
        {
            int calculatedScore = 0;
            int selectedFrameIdx = scoreBoard.IndexOf(frame0);
            if (selectedFrameIdx == -1) throw new ArgumentException("The given frame is not in the given list of AFrames");
            if (frame0.isStrike() || frame0.isSpair())
            {
                AFrame nextFrame = scoreBoard[selectedFrameIdx + 1];
                if (nextFrame == null) throw new MissingFrameException();
                calculatedScore = 10;
                if (frame0.isStrike())
                {
                    if (nextFrame.isStrike())
                    {
                        AFrame nextNextFrame = scoreBoard[selectedFrameIdx + 2];
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
                else if (frame0.isSpair())
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
                calculatedScore = ThrowResultExtension.ToInt(frame0.ThrowResults[0]) + ThrowResultExtension.ToInt(frame0.ThrowResults[1]);
            }
            frame0.ScoreValue = calculatedScore;
        }
    }
}
