using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Model.score
{
    /// <summary>
    /// Frame represents a Frame in bowling, this class stores lap's data
    /// </summary>
    public class Frame
    {
        /// <summary>
        /// Contains all results 
        /// </summary>
        private ThrowResult[] throwResults;
        public ReadOnlyCollection<ThrowResult> ThrowResults
        {
            get;
            private set;
        }
        /// <summary>
        /// The score value of the frame, this field will be calculated by IScoreCalculator
        /// </summary>
        private int scoreValue;
        public int ScoreValue
        {
            get { return scoreValue; }
            set {
                if (value < 0) scoreValue = 0;
                else scoreValue = value;
            }
        }

        private int frameNumber;
        public int FrameNumber
        {
            get
            {
                return frameNumber;
            }
            private set
            {
                if (value <= 0) frameNumber = 1;
                else frameNumber = value;
            }
        }

        /// <summary>
        /// Constructor of Frame
        /// </summary>
        /// <param name="numberOfThrow">The maximum number of throws that the Frame can contains</param>
        /// <exception cref="ArgumentException">If the given number of throw is <= 0</exception>
        public Frame(int numberOfThrow, int frameNumber)
        {
            if (numberOfThrow <= 0)
            {
                throw new ArgumentException("The number of throw must be > 0");
            }
            throwResults = new ThrowResult[numberOfThrow];
            for (int i = 0; i < numberOfThrow; i++)
            {
                throwResults[i] = ThrowResult.NONE;
            }
            ThrowResults = new ReadOnlyCollection<ThrowResult>(throwResults);
            FrameNumber = frameNumber;
        }

        public Frame(int frameNumber, ThrowResult[] results) : this(results.Length, frameNumber)
        {
            for (int i = 0; i < results.Length; i++)
            {
                throwResults[i] = results[i];
            }
        }

        /// <summary>
        /// Set a ThrowResult in the specified slot of the ThrowResult array 
        /// </summary>
        /// <param name="index">Specified by the user, can't be greater than ThrowResult's size</param>
        /// <param name="throwResult">Throw result to put in the array</param>
        public void AddThrow(int index, ThrowResult throwResult) 
        {
            throwResults[index] = throwResult;
        }

        public bool isStrike()
        {
            return throwResults[1] == ThrowResult.STRIKE;
        }

        public bool isSpair()
        {
            return throwResults[1] == ThrowResult.SPAIR;
        }
    }
}
