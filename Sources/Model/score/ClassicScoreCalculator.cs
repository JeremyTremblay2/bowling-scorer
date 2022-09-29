using Model.Exceptions;
using Model.Score.Frame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Model.Score
{
    /// <summary>
    /// Compute the score of one frame by using the classic bowling scoring rules that you can found here :
    /// https://www.playerssports.net/page/bowling-rules
    /// </summary>
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
                if (classic0.IsStrike|| classic0.IsSpare) 
                    calculatedScore = Frame1IsNormalOrLast(classic0, scoreBoard, calculatedScore, selectedFrameIdx, nextFrame);
                else 
                    calculatedScore = classic0.ThrowResults[0].ToInt() + classic0.ThrowResults[1].ToInt();
                classic0.ScoreValue = calculatedScore;
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new MissingFrameException("frame1 missing");
            }
        }

        /// <summary>
        /// Called when classi0 contains a STRIKE or a SPARE
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
            calculatedScore += ThrowResult.STRIKE.ToInt();
            if (classicLast1 != null)
            {
                if (classic0.IsStrike) calculatedScore = EncounterLastFrameDirectlyStrike(classicLast1, calculatedScore);
                else if (classic0.IsSpare) calculatedScore = EncounterLastFrameDirectlySpare(classicLast1, calculatedScore);
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
        private int EncounterLastFrameDirectlySpare(ClassicLastFrame classicLast1, int calculatedScore)
        {
            return calculatedScore + classicLast1.ThrowResults[0].ToInt();
        }

        /// <summary>
        /// Called when the Frame2 is a last frame
        /// </summary>
        /// <param name="classicLast2"></param>
        /// <param name="calculatedScore"></param>
        /// <returns></returns>
        private int EncounterLastFrameSecondly(ClassicLastFrame classicLast2, int calculatedScore)
        {
            if (classicLast2.ThrowResults[1] == ThrowResult.STRIKE || classicLast2.ThrowResults[1] == ThrowResult.SPARE)
            {
                calculatedScore += ThrowResult.STRIKE.ToInt();
            }
            else
            {
                calculatedScore += classicLast2.ThrowResults[0].ToInt();
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
            if (classic0.IsStrike)
            {
                if (classic1.IsStrike)
                {
                    calculatedScore = Classic1IsStrike(scoreBoard, selectedFrameIdx, calculatedScore); 
                }
                else if (classic1.IsSpare)
                {
                    calculatedScore += ThrowResult.SPARE.ToInt();
                }
                else
                {
                    calculatedScore = calculatedScore + nextFrame.ThrowResults[0].ToInt()
                                                      + nextFrame.ThrowResults[1].ToInt();
                }
            }
            else if (classic0.IsSpare)
            {
                if (classic1.IsStrike)
                {
                    calculatedScore += ThrowResult.STRIKE.ToInt();
                }
                else
                {
                    calculatedScore += nextFrame.ThrowResults[0].ToInt();
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

                calculatedScore += ThrowResult.STRIKE.ToInt();

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
            if (classic2.IsStrike)
            {
                calculatedScore += ThrowResult.STRIKE.ToInt();
            }
            else
            {
                calculatedScore += classic2.ThrowResults[0].ToInt();
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
            if (classicLast1.ThrowResults[1] == ThrowResult.SPARE)
            {
                return calculatedScore + ThrowResult.SPARE.ToInt();
            }
            else
            {
                return calculatedScore + classicLast1.ThrowResults[0].ToInt()
                                       + classicLast1.ThrowResults[1].ToInt();
            }
        }

        /// <summary>
        /// Compute the scoreValue of a ClassicLastFrame
        /// </summary>
        /// <param name="frame0"></param>
        private void ComputeClassicLastFrame(ClassicLastFrame frame0)
        {
            int computedScore = 0;

            if (frame0.ThrowResults[0] == ThrowResult.STRIKE || frame0.ThrowResults[1] == ThrowResult.SPARE)
            {
                computedScore += ThrowResult.STRIKE.ToInt();

                if (frame0.ThrowResults[0] == ThrowResult.STRIKE) 
                    computedScore = ClassicLastFirstThrowIsStrike(frame0, computedScore);
                else if (frame0.ThrowResults[1] == ThrowResult.SPARE) 
                    computedScore = ClassicLastFirstThrowIsSpare(frame0, computedScore);
            }
            else
            {
                computedScore = computedScore + frame0.ThrowResults[0].ToInt()
                                              + frame0.ThrowResults[0].ToInt();
            }

            frame0.ScoreValue = computedScore;
        }

        /// <summary>
        /// Called when the first throw of the ClassicLastFrame is a SPARE
        /// </summary>
        /// <param name="frame0"></param>
        /// <param name="computedScore"></param>
        /// <returns></returns>
        private static int ClassicLastFirstThrowIsSpare(ClassicLastFrame frame0, int computedScore)
        {
            if (frame0.ThrowResults[2] == ThrowResult.STRIKE)
            {
                computedScore += ThrowResult.STRIKE.ToInt();
            }
            else
            {
                computedScore += frame0.ThrowResults[2].ToInt();
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
                computedScore += ThrowResult.STRIKE.ToInt();
                if (frame0.ThrowResults[2] == ThrowResult.STRIKE)
                {
                    computedScore += ThrowResult.STRIKE.ToInt();
                }
            }
            else
            {
                if (frame0.ThrowResults[2] == ThrowResult.SPARE)
                {
                    computedScore += ThrowResult.SPARE.ToInt();
                }
                else
                {
                    computedScore = computedScore + frame0.ThrowResults[1].ToInt()
                                                  + frame0.ThrowResults[2].ToInt();
                }
            }

            return computedScore;
        }
    }
}
