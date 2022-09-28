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
                frame.CumulativeScore = total;
            }
            return total;
        }

        public void UpdateFromFrame(int index, List<AFrame> frames)
        {
            AFrame frame0 = frames[index];
            if (frame0 == null) return;
            ClassicFrame classic0 = frame0 as ClassicFrame;
            ClassicLastFrame classicLast0 = frame0 as ClassicLastFrame;
            if (classic0 != null)
            {
                ComputeClassicFrameMaybeLast(classic0, frames);
            }
            else if (classicLast0 != null)
            {
                ComputeClassicLastFrame(classicLast0);
            }
            ComputeClassicFrameMaybeLast(frames[index - 1] as ClassicFrame, frames);
            ComputeClassicFrameMaybeLast(frames[index - 2] as ClassicFrame, frames);
        }
        public void UpdateLastFrame(List<AFrame> frames)
        {
            UpdateFromFrame(frames.Count - 1, frames);
        }

        private void ComputeClassicFrameMaybeLast(ClassicFrame classic0, List<AFrame> scoreBoard)
        {
            int calculatedScore = 0;
            int selectedFrameIdx = scoreBoard.IndexOf(classic0);
            if (selectedFrameIdx == -1) throw new ArgumentException("The given frame is not in the given list of AFrames");
            if (classic0.isStrike() || classic0.isSpair())
            {
                calculatedScore = ClassicStrikeOrSpair(classic0, scoreBoard, selectedFrameIdx, calculatedScore);
            }
            else
            {
                calculatedScore = ThrowResultExtension.ToInt(classic0.ThrowResults[0]) + ThrowResultExtension.ToInt(classic0.ThrowResults[1]);
            }
            classic0.ScoreValue = calculatedScore;
        }

        private int ClassicStrikeOrSpair(ClassicFrame classic0, List<AFrame> scoreBoard, int selectedFrameIdx, int calculatedScore)
        {
            AFrame nextFrame = scoreBoard[selectedFrameIdx + 1];
            if (nextFrame == null) throw new MissingFrameException();
            ClassicLastFrame classicLast1 = nextFrame as ClassicLastFrame;
            ClassicFrame classic1 = nextFrame as ClassicFrame;
            calculatedScore += 10;
            if (classicLast1 != null)
            {
                if (classic0.isStrike())
                    calculatedScore = EncounterLastFrameDirectlyStrike(classicLast1, calculatedScore);
                else if (classic0.isSpair())
                    calculatedScore = EncounterLastFrameDirectlySpair(classicLast1, calculatedScore);
            }
            else if (classic1 != null)
            {
                calculatedScore = FirstIsClassicFrame(nextFrame, classic0, classic1, scoreBoard, calculatedScore, selectedFrameIdx);
            }
            else throw new ArgumentException("The given score board use bad Frame type, please use only ClassicFrame and ClassicLastFrame or create your own calculator that implements IScoreCalculator");
            return calculatedScore;
        }

        private int EncounterLastFrameDirectlySpair(ClassicLastFrame classicLast1, int calculatedScore)
        {
            return calculatedScore + ThrowResultExtension.ToInt(classicLast1.ThrowResults[0]);
        }

        private int FirstIsClassicFrame(AFrame nextFrame, ClassicFrame classic0, ClassicFrame classic1, List<AFrame> scoreBoard, int calculatedScore, int selectedFrameIdx)
        {
            if (classic0.isStrike())
            {
                if (classic1.isStrike())
                {
                    calculatedScore = Classic1IsStrike(scoreBoard, selectedFrameIdx, calculatedScore); 
                }
                else if (classic1.isSpair())
                {
                    calculatedScore = calculatedScore + 10;
                }
                else
                {
                    calculatedScore = calculatedScore + ThrowResultExtension.ToInt(nextFrame.ThrowResults[0])
                                                      + ThrowResultExtension.ToInt(nextFrame.ThrowResults[1]);
                }
            }
            else if (classic0.isSpair())
            {
                if (classic1.isStrike())
                {
                    calculatedScore = calculatedScore + 10;
                }
                else
                {
                    calculatedScore = calculatedScore + ThrowResultExtension.ToInt(nextFrame.ThrowResults[0]);
                }
            }
            return calculatedScore;
        }

        private int Classic1IsStrike(List<AFrame> scoreBoard, int selectedFrameIdx, int calculatedScore)
        {
            AFrame nextNextFrame = scoreBoard[selectedFrameIdx + 2];
            if (nextNextFrame == null) throw new MissingFrameException();
            ClassicLastFrame classicLast2 = nextNextFrame as ClassicLastFrame;
            ClassicFrame classic2 = nextNextFrame as ClassicFrame;
            calculatedScore = calculatedScore + 10;
            if (nextNextFrame is ClassicLastFrame)
            {
                calculatedScore = EncounterLastFrameSecondly(classicLast2, calculatedScore);
            }
            else if (nextNextFrame is ClassicFrame)
            {
                calculatedScore = ComputeWithClassic2(classic2, calculatedScore);
            }
            else throw new ArgumentException("The given score board use bad Frame type, please use only ClassicFrame and ClassicLastFrame or create your own calculator that implements IScoreCalculator");
            return calculatedScore;
        }

        private int ComputeWithClassic2(ClassicFrame classic2, int calculatedScore)
        {
            if (classic2.isStrike())
            {
                calculatedScore = calculatedScore + 10;
            }
            else
            {
                calculatedScore = calculatedScore + ThrowResultExtension.ToInt(classic2.ThrowResults[0]);
            }
            return calculatedScore;
        }

        private int EncounterLastFrameSecondly(ClassicLastFrame classicLast2, int calculatedScore)
        {
            if (classicLast2.ThrowResults[1] == ThrowResult.STRIKE || classicLast2.ThrowResults[1] == ThrowResult.SPAIR)
            {
                calculatedScore = calculatedScore + 10;
            }
            else
            {
                calculatedScore = calculatedScore + ThrowResultExtension.ToInt(classicLast2.ThrowResults[0]);
            }
            return calculatedScore;
        }

        private int EncounterLastFrameDirectlyStrike(ClassicLastFrame classicLast1, int calculatedScore)
        {
            if (classicLast1.ThrowResults[1] == ThrowResult.SPAIR)
            {
                return calculatedScore + ThrowResultExtension.ToInt(ThrowResult.SPAIR) + ThrowResultExtension.ToInt(classicLast1.ThrowResults[2]);
            }
            else
            {
                return calculatedScore + ThrowResultExtension.ToInt(classicLast1.ThrowResults[0]) 
                                       + ThrowResultExtension.ToInt(classicLast1.ThrowResults[1]);
            }
        }

        private void ComputeClassicLastFrame(ClassicLastFrame frame0)
        {
            int computedScore = 0;
            if (frame0.ThrowResults[0] == ThrowResult.STRIKE)
            {
                computedScore = computedScore + 10;
                computedScore = ClassicLastFirstThrowIsStrike(frame0, computedScore);
            }
            else if (frame0.ThrowResults[1] == ThrowResult.SPAIR)
            {
                computedScore = computedScore + 10;
                computedScore = ClassicLastFirstThrowIsSpair(frame0, computedScore);
            }
            else
            {
                computedScore = computedScore + ThrowResultExtension.ToInt(frame0.ThrowResults[0])
                                              + ThrowResultExtension.ToInt(frame0.ThrowResults[0]);
            }

            frame0.ScoreValue = computedScore;
        }

        private static int ClassicLastFirstThrowIsSpair(ClassicLastFrame frame0, int computedScore)
        {
            if (frame0.ThrowResults[2] == ThrowResult.STRIKE)
            {
                computedScore = computedScore + 10;
            }
            else
            {
                computedScore = computedScore + ThrowResultExtension.ToInt(frame0.ThrowResults[2]);
            }

            return computedScore;
        }

        private static int ClassicLastFirstThrowIsStrike(ClassicLastFrame frame0, int computedScore)
        {
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

            return computedScore;
        }
    }
}
