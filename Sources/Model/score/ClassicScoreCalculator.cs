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
            AFrame frame0;
            ClassicFrame classic0;
            ClassicLastFrame classicLast0;

            try
            {
                frame0 = frames[index];
                classic0 = frame0 as ClassicFrame;
                classicLast0 = frame0 as ClassicLastFrame;
                if (classic0 != null) ComputeClassicFrame(classic0, frames);
                else if (classicLast0 != null) ComputeClassicLastFrame(classicLast0);
                if (index - 1 >= 0) ComputeClassicFrame(frames[index - 1] as ClassicFrame, frames);
                if (index - 2 >= 0) ComputeClassicFrame(frames[index - 2] as ClassicFrame, frames);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ArgumentException("The given index is out of range");
            }
        }

        public void UpdateLastFrame(List<AFrame> frames)
        {
            UpdateFromFrame(frames.Count - 1, frames);
        }

        /// <summary>
        /// Compute the score value of a classic frame
        /// </summary>
        /// <param name="classic0">Frame to compute</param>
        /// <param name="scoreBoard">The scoreboard</param>
        /// <exception cref="ArgumentException"></exception>
        private void ComputeClassicFrame(ClassicFrame classic0, List<AFrame> scoreBoard)
        {
            int calculatedScore = 0;
            int selectedFrameIdx;
            AFrame nextFrame;

            selectedFrameIdx = scoreBoard.IndexOf(classic0);
            try
            {
                nextFrame = scoreBoard[selectedFrameIdx + 1];
                if (classic0.isStrike() || classic0.isSpair()) 
                    calculatedScore = Frame1IsNormalOrLast(classic0, scoreBoard, calculatedScore, selectedFrameIdx, nextFrame);
                else 
                    calculatedScore = ThrowResultExtension.ToInt(classic0.ThrowResults[0]) + ThrowResultExtension.ToInt(classic0.ThrowResults[1]);
                classic0.ScoreValue = calculatedScore;
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new MissingFrameException();
            }
        }

        /// <summary>
        /// Called when classi0 contains a STRIKE or a SPAIR
        /// </summary>
        /// <param name="classic0"></param>
        /// <param name="scoreBoard"></param>
        /// <param name="calculatedScore"></param>
        /// <param name="selectedFrameIdx"></param>
        /// <param name="nextFrame"></param>
        /// <returns></returns>
        private int Frame1IsNormalOrLast(ClassicFrame classic0, List<AFrame> scoreBoard, int calculatedScore, int selectedFrameIdx, AFrame nextFrame)
        {
            ClassicLastFrame classicLast1;
            ClassicFrame classic1;

            classicLast1 = nextFrame as ClassicLastFrame;
            classic1 = nextFrame as ClassicFrame;
            calculatedScore += 10;
            if (classicLast1 != null)
            {
                if (classic0.isStrike()) calculatedScore = EncounterLastFrameDirectlyStrike(classicLast1, calculatedScore);
                else if (classic0.isSpair()) calculatedScore = EncounterLastFrameDirectlySpair(classicLast1, calculatedScore);
            }
            else if (classic1 != null)
            {
                calculatedScore = FirstIsClassicFrame(nextFrame, classic0, classic1, scoreBoard, calculatedScore, selectedFrameIdx);
            }

            return calculatedScore;
        }

        /// <summary>
        /// Called when the Frame1 is a last frame
        /// </summary>
        /// <param name="classicLast1"></param>
        /// <param name="calculatedScore"></param>
        /// <returns></returns>
        private int EncounterLastFrameDirectlySpair(ClassicLastFrame classicLast1, int calculatedScore)
        {
            return calculatedScore + ThrowResultExtension.ToInt(classicLast1.ThrowResults[0]);
        }

        /// <summary>
        /// Called when the Frame2 is a last frame
        /// </summary>
        /// <param name="classicLast2"></param>
        /// <param name="calculatedScore"></param>
        /// <returns></returns>
        private int EncounterLastFrameSecondly(ClassicLastFrame classicLast2, int calculatedScore)
        {
            if (classicLast2.ThrowResults[1] == ThrowResult.STRIKE || classicLast2.ThrowResults[1] == ThrowResult.SPAIR)
            {
                calculatedScore += 10;
            }
            else
            {
                calculatedScore += ThrowResultExtension.ToInt(classicLast2.ThrowResults[0]);
            }
            return calculatedScore;
        }

        /// <summary>
        /// Called when frame1 is ClassicFrame
        /// </summary>
        /// <param name="nextFrame"></param>
        /// <param name="classic0"></param>
        /// <param name="classic1"></param>
        /// <param name="scoreBoard"></param>
        /// <param name="calculatedScore"></param>
        /// <param name="selectedFrameIdx"></param>
        /// <returns></returns>
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
                    calculatedScore += 10;
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
                    calculatedScore += 10;
                }
                else
                {
                    calculatedScore += ThrowResultExtension.ToInt(nextFrame.ThrowResults[0]);
                }
            }
            return calculatedScore;
        }

        /// <summary>
        /// Called when frame1 contains a STRIKE
        /// </summary>
        /// <param name="scoreBoard"></param>
        /// <param name="selectedFrameIdx"></param>
        /// <param name="calculatedScore"></param>
        /// <returns></returns>
        /// <exception cref="MissingFrameException"></exception>
        private int Classic1IsStrike(List<AFrame> scoreBoard, int selectedFrameIdx, int calculatedScore)
        {
            AFrame nextNextFrame;
            ClassicLastFrame classicLast2;
            ClassicFrame classic2;

            try
            {
                nextNextFrame = scoreBoard[selectedFrameIdx + 2];
                classicLast2 = nextNextFrame as ClassicLastFrame;
                classic2 = nextNextFrame as ClassicFrame;
                calculatedScore += 10;
                if (nextNextFrame is ClassicLastFrame) calculatedScore = EncounterLastFrameSecondly(classicLast2, calculatedScore);
                else if (nextNextFrame is ClassicFrame) calculatedScore = ComputeWithClassic2(classic2, calculatedScore);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new MissingFrameException();
            }
            
            return calculatedScore;
        }

        /// <summary>
        /// Called when frame2 is ClassicFrame
        /// </summary>
        /// <param name="classic2"></param>
        /// <param name="calculatedScore"></param>
        /// <returns></returns>
        private int ComputeWithClassic2(ClassicFrame classic2, int calculatedScore)
        {
            if (classic2.isStrike())
            {
                calculatedScore += 10;
            }
            else
            {
                calculatedScore += ThrowResultExtension.ToInt(classic2.ThrowResults[0]);
            }
            return calculatedScore;
        }

        /// <summary>
        /// Called when we frame1 sur a ClassicLastFrame
        /// </summary>
        /// <param name="classicLast1"></param>
        /// <param name="calculatedScore"></param>
        /// <returns></returns>
        private int EncounterLastFrameDirectlyStrike(ClassicLastFrame classicLast1, int calculatedScore)
        {
            if (classicLast1.ThrowResults[1] == ThrowResult.SPAIR)
            {
                return calculatedScore + ThrowResultExtension.ToInt(ThrowResult.SPAIR) 
                                       + ThrowResultExtension.ToInt(classicLast1.ThrowResults[2]);
            }
            else
            {
                return calculatedScore + ThrowResultExtension.ToInt(classicLast1.ThrowResults[0]) 
                                       + ThrowResultExtension.ToInt(classicLast1.ThrowResults[1]);
            }
        }

        /// <summary>
        /// Compute the scoreValue of a ClassicLastFrame
        /// </summary>
        /// <param name="frame0"></param>
        private void ComputeClassicLastFrame(ClassicLastFrame frame0)
        {
            int computedScore = 0;

            if (frame0.ThrowResults[0] == ThrowResult.STRIKE || frame0.ThrowResults[1] == ThrowResult.SPAIR)
            {
                computedScore += 10;
                if (frame0.ThrowResults[0] == ThrowResult.STRIKE) 
                    computedScore = ClassicLastFirstThrowIsStrike(frame0, computedScore);
                else if (frame0.ThrowResults[1] == ThrowResult.SPAIR) 
                    computedScore = ClassicLastFirstThrowIsSpair(frame0, computedScore);
            }
            else
            {
                computedScore = computedScore + ThrowResultExtension.ToInt(frame0.ThrowResults[0])
                                              + ThrowResultExtension.ToInt(frame0.ThrowResults[0]);
            }

            frame0.ScoreValue = computedScore;
        }

        /// <summary>
        /// Called when the first throw of the ClassicLastFrame is a SPAIR
        /// </summary>
        /// <param name="frame0"></param>
        /// <param name="computedScore"></param>
        /// <returns></returns>
        private static int ClassicLastFirstThrowIsSpair(ClassicLastFrame frame0, int computedScore)
        {
            if (frame0.ThrowResults[2] == ThrowResult.STRIKE)
            {
                computedScore += 10;
            }
            else
            {
                computedScore += ThrowResultExtension.ToInt(frame0.ThrowResults[2]);
            }

            return computedScore;
        }

        /// <summary>
        /// Called when the first throw of the ClassicLastFrame is a STRIKE
        /// </summary>
        /// <param name="frame0"></param>
        /// <param name="computedScore"></param>
        /// <returns></returns>
        private static int ClassicLastFirstThrowIsStrike(ClassicLastFrame frame0, int computedScore)
        {
            if (frame0.ThrowResults[1] == ThrowResult.STRIKE)
            {
                computedScore += 10;
                if (frame0.ThrowResults[2] == ThrowResult.STRIKE)
                {
                    computedScore += 10;
                }
            }
            else
            {
                if (frame0.ThrowResults[2] == ThrowResult.SPAIR)
                {
                    computedScore += 10;
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
