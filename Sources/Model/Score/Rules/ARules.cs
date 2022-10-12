using FrameWriterModel.Frame;
using FrameWriterModel.Frame.ThrowResults;
using FrameWriterModel.Writer;
using Model.Score.Rules.Calculator;
using Model.Score.Rules.Retriever;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Score.Rules
{
    /// <summary>
    /// Define all the rules that will be applied during a game
    /// </summary>
    public abstract class ARules
    {
        /// <summary>
        /// Choosen calculator
        /// </summary>
        protected IScoreCalculator scoreCalculator;

        /// <summary>
        /// This retriever allow the project to precise which ThrowResult can be added to a specific frame and index.
        /// </summary>
        protected IPossibleThrowResultsRetriever throwResultsRetriever;

        /// <summary>
        /// Writers, this is a list because it is possible to have different writing rules depending of the given AFrame.
        /// </summary>
        protected List<AFrameWriter> writers;

        /// <summary>
        /// Use the given writer and calculator to apply specified rules
        /// </summary>
        /// <param name="scoreCalculator"></param>
        /// <param name="writers"></param>
        protected ARules(IScoreCalculator scoreCalculator, IPossibleThrowResultsRetriever throwResultsRetriever, List<AFrameWriter> writers)
        {
            this.scoreCalculator = scoreCalculator;
            this.throwResultsRetriever = throwResultsRetriever;
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
        public int CalculateScore(List<AFrame> scoreBoard)
        {
            return scoreCalculator.CalculateScore(scoreBoard);
        }

        /// <summary>
        /// Update the last frame by using a calculator
        /// </summary>
        /// <param name="scoreBoard"></param>
        /// <returns></returns>
        public void UpdateLastFrame(List<AFrame> scoreBoard)
        {
            scoreCalculator.UpdateLastFrame(scoreBoard);
        }

        /// <summary>
        /// Update a specific frame by using the calculator
        /// </summary>
        /// <param name="index"></param>
        /// <param name="scoreBoard"></param>
        public void UpdateFromFrame(int index, List<AFrame> scoreBoard)
        {
            scoreCalculator.UpdateFromFrame(index, scoreBoard);
        }

        /// <summary>
        /// Returns a collection of the ThrowResult that can be added to a specific frame and index.
        /// </summary>
        /// <param name="frameToAdd">The frame to add a throw result.</param>
        /// <param name="indexToAdd">The index of theframe to add the throw result.</param>
        public IEnumerable<ThrowResult> GetPossibleThrowResults(AFrame frameToAdd, int indexToAdd) {
            return throwResultsRetriever.GetPossibleThrowResults(frameToAdd, indexToAdd);
        }
    }
}
