using FrameModel.Frame;
using FrameModel.Frame.ThrowResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Score.Rules.Retriever
{
    /// <summary>
    /// A ClassicPossibleThrowResultsRetriever is a class capable of retrieving the different types of ThrowResults 
    /// that can be entered in a box of a given frame in accordance with previously established classic Bowling rules.
    /// </summary>
    public class ClassicPossibleThrowResultsRetriever : IPossibleThrowResultsRetriever
    {
        /// <summary>
        /// Method returning a collection of the different types of ThrowResult that can be entered in a cell of a given frame.
        /// </summary>
        /// <param name="frameToAdd">The frame from which the possible scores will be extracted.</param>
        /// <param name="indexToAdd">The index representing the box of the frame concerned by this recovery.</param>
        /// <returns>A collection of possible ThrowResult to add at the frame.</returns>
        public IEnumerable<ThrowResult> GetPossibleThrowResults(AFrame frameToAdd, int indexToAdd)
        {
            if (frameToAdd == null) return Enumerable.Empty<ThrowResult>();
            if (frameToAdd is ClassicFrame classicFrame) return ComputeClassicFrame(classicFrame, indexToAdd);
            else if(frameToAdd is ClassicLastFrame classicLastFrame) return ComputeClassicLastFrame(classicLastFrame, indexToAdd);
            else return Enumerable.Empty<ThrowResult>();
        }

        /// <summary>
        /// Compute a ClassicFrame and returns his possible throwResult from the given index.
        /// </summary>
        /// <param name="frame">The frame to compute.</param>
        /// <param name="indexToAdd">The index where to add a throw result.</param>
        /// <returns>A collection of possible ThrowResult to add at the frame.</returns>
        private IEnumerable<ThrowResult> ComputeClassicFrame(ClassicFrame frame, int indexToAdd)
        {
            if (indexToAdd >= 2 || indexToAdd < 0) return Enumerable.Empty<ThrowResult>();
            List<ThrowResult> throwResults = new List<ThrowResult> { ThrowResult.NONE };

            // Manage the first box.
            if (indexToAdd == 0)
            {
                if (frame.ThrowResults[1] == ThrowResult.NONE 
                    || frame.ThrowResults[1] == ThrowResult.ZERO
                    || frame.ThrowResults[1] == ThrowResult.SPARE)
                {
                    throwResults.AddRange(0.ToThrowResults(9));
                }
                else if (frame.ThrowResults[1].IsThrowResultBetween(1, 8))
                {
                    throwResults.AddRange(0.ToThrowResults(9 - frame.ThrowResults[1].ToInt()));
                }
            }
            // Manage the second box, which can has Strikes and Spares.
            else
            {
                if (frame.ThrowResults[0] == ThrowResult.NONE)
                {
                    throwResults.AddRange(0.ToThrowResults(9));
                    throwResults.Add(ThrowResult.STRIKE);
                }
                else if (frame.ThrowResults[0].IsThrowResultBetween(0, 9))
                {
                    throwResults.AddRange(0.ToThrowResults(9 - frame.ThrowResults[0].ToInt()));
                    throwResults.Add(ThrowResult.SPARE);
                }
            }

            return throwResults;
        }

        /// <summary>
        /// Compute a ClassicLastFrame and returns his possible throwResult from the given index.
        /// </summary>
        /// <param name="frame">The frame to compute.</param>
        /// <param name="indexToAdd">The index where to add a throw result.</param>
        /// <returns>A collection of possible ThrowResult to add at the frame.</returns>
        private IEnumerable<ThrowResult> ComputeClassicLastFrame(ClassicLastFrame frame, int indexToAdd)
        {
            if (indexToAdd >= 3 || indexToAdd < 0) return Enumerable.Empty<ThrowResult>();
            List<ThrowResult> throwResults = new List<ThrowResult> { ThrowResult.NONE };

            if (indexToAdd == 0)
            {
                throwResults.AddRange(0.ToThrowResults(9));
                throwResults.Add(ThrowResult.STRIKE);
            }
            else if (indexToAdd == 1)
            {
                if (frame.ThrowResults[0] == ThrowResult.NONE
                    || frame.ThrowResults[0] == ThrowResult.STRIKE)
                {
                    throwResults.AddRange(0.ToThrowResults(9));
                    throwResults.Add(ThrowResult.STRIKE);
                }
                else
                {
                    throwResults.AddRange(0.ToThrowResults(9 - frame.ThrowResults[0].ToInt()));
                    throwResults.Add(ThrowResult.SPARE);
                }
            }
            else
            {
                if (frame.ThrowResults[0] == ThrowResult.STRIKE
                    || frame.ThrowResults[1] == ThrowResult.STRIKE
                    || frame.ThrowResults[1] == ThrowResult.SPARE) 
                {
                    throwResults.AddRange(0.ToThrowResults(9));
                    throwResults.Add(ThrowResult.STRIKE);
                }
            }

            return throwResults;
        }
    }
}
