using FrameModel.Frame;
using FrameModel.Frame.ThrowResults;
using FrameModel.Writer;
using Model.Score.Rules.Calculator;
using Model.Score.Rules.Detector;
using Model.Score.Rules.Retriever;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Score.Rules
{
    /// <summary>
    /// Define all the rules that will be applied during a game.
    /// </summary>
    public abstract class ARules
    {
        /// <summary>
        /// Choosen calculator, used to compute scores.
        /// </summary>
        protected IScoreCalculator scoreCalculator;

        /// <summary>
        /// This retriever allow the project to precise which ThrowResult can be added to a specific frame and index.
        /// </summary>
        protected IPossibleThrowResultsRetriever throwResultsRetriever;

        protected IFullScoreTableDetector scoreTableDetector;

        /// <summary>
        /// Generate a Score table according to the defined rules.
        /// </summary>
        /// <returns></returns>
        public abstract IList<AFrame> GenerateScoreTable();
       

        /// <summary>
        /// Writers, this is a list because it is possible to have different writing rules depending of the given AFrame.
        /// </summary>
        protected IList<AFrameWriter> writers;

        /// <summary>
        /// Use the given writer and calculator to apply specified rules.
        /// </summary>
        /// <param name="scoreCalculator"></param>
        /// <param name="writers"></param>
        protected ARules(IScoreCalculator scoreCalculator, 
                         IPossibleThrowResultsRetriever throwResultsRetriever,
                         IFullScoreTableDetector scoreTableDetector,
                         IList<AFrameWriter> writers)
        {
            this.scoreCalculator = scoreCalculator;
            this.throwResultsRetriever = throwResultsRetriever;
            this.scoreTableDetector = scoreTableDetector;
            this.writers = new List<AFrameWriter>(writers);
        }

        /// <summary>
        /// Write a value in the given AFrame by using the writers. 
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="index"></param>
        /// <param name="throwResult"></param>
        public abstract void WriteValue(AFrame frame, int index, ThrowResult throwResult);

        /// <summary>
        /// Compute the score by using a calculator
        /// </summary>
        /// <param name="scoreBoard"></param>
        /// <returns></returns>
        public int CalculateScore(IList<AFrame> scoreBoard)
        {
            return scoreCalculator.CalculateScore(scoreBoard);
        }

        /// <summary>
        /// Update the last frame by using a calculator
        /// </summary>
        /// <param name="scoreBoard"></param>
        /// <returns></returns>
        public void UpdateLastFrame(IList<AFrame> scoreBoard)
        {
            scoreCalculator.UpdateLastFrame(scoreBoard);
        }

        /// <summary>
        /// Update a specific frame by using the calculator
        /// </summary>
        /// <param name="index"></param>
        /// <param name="scoreBoard"></param>
        public void UpdateFromFrame(int index, IList<AFrame> scoreBoard)
        {
            scoreCalculator.UpdateFromFrame(index, scoreBoard);
        }

        /// <summary>
        /// Returns a collection of the ThrowResult that can be added to a specific frame and index.
        /// </summary>
        /// <param name="frameToAdd">The frame to add a throw result.</param>
        /// <param name="indexToAdd">The index of the box of the frame to add the throw result.</param>
        public IEnumerable<ThrowResult> GetPossibleThrowResults(AFrame frameToAdd, int indexToAdd)
        {
            return throwResultsRetriever.GetPossibleThrowResults(frameToAdd, indexToAdd);
        }

        /// <summary>
        /// Returns a boolean indicating whether the scoreboard is complete according to established bowling rules.
        /// </summary>
        /// <param name="scoreTable">The score table to inspect.</param>
        /// <returns>A boolean indicating whether the scoreboard is complete or not.</returns>
        public bool IsScoreTableComplete(ScoreTable scoreTable)
        {
            return scoreTableDetector.IsScoreTableComplete(scoreTable);
        }

        /// <summary>
        /// Returns a boolean indicating whether the frame is complete according to established bowling rules.
        /// </summary>
        /// <param name="frame">The frame to inspect.</param>
        /// <returns>A boolean indicating whether the frame is complete or not.</returns>
        public bool IsFrameComplete(AFrame frame)
        {
            return scoreTableDetector.IsFrameComplete(frame);
        }
    }
}
