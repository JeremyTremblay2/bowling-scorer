using FrameWriterModel.Frame;
using FrameWriterModel.Frame.ThrowResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Score.Rules.Retriever
{
    /// <summary>
    /// An IPossibleThrowResultsRetriever is a class capable of retrieving the different types of ThrowResults 
    /// that can be entered in a box of a given frame in accordance with previously established Bowling rules.
    /// </summary>
    public interface IPossibleThrowResultsRetriever
    {
        /// <summary>
        /// Method returning a collection of the different types of ThrowResult that can be entered in a cell of a given frame.
        /// </summary>
        /// <param name="frameToAdd">The frame from which the possible scores will be extracted.</param>
        /// <param name="indexToAdd">The index representing the box of the frame concerned by this recovery.</param>
        /// <returns>A collection of possible ThrowResult to add at the frame.</returns>
        public IEnumerable<ThrowResult> GetPossibleThrowResults(AFrame frameToAdd, int indexToAdd);
    }
}
