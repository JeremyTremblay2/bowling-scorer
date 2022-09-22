using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.score
{
    /// <summary>
    /// Represents a Frame
    /// </summary>
    public abstract class AFrame
    {
        /// <summary>
        /// The name of the Frame
        /// </summary>
        public int FrameNumberLabel
        {
            get { return frameNumberLabel; }
            set
            {
                if (value < 1) frameNumberLabel = 1;
                else frameNumberLabel = value;
            }
        }
        private int frameNumberLabel;

        /// <summary>
        /// Results of each Throw contained in the Frame
        /// </summary>
        private ThrowResult[] throwResults;
        public ReadOnlyCollection<ThrowResult> ThrowResults { get; private set; }

        /// <summary>
        /// The score value of the frame, this field will be calculated by IScoreCalculator
        /// </summary>
        public int ScoreValue
        {
            get { return scoreValue; }
            set
            {
                if (value < 0) scoreValue = 0;
                else scoreValue = value;
            }
        }
        private int scoreValue;
        
        /// <summary>
        /// Property that contains CumulativeScore, computed by another class
        /// </summary>
        public int CumulativeScoreValue {
            get { return cumulativeScoreValue; }
            set
            {
                if (value < 0) cumulativeScoreValue = 0;
                else cumulativeScoreValue = value;
            }
        }
        private int cumulativeScoreValue;

        /// <summary>
        /// Build a Frame with the specified label and the specified number of slots
        /// </summary>
        /// <param name="frameNumberLabel">Name of the Frame</param>
        /// <param name="nbThrows">Number of slots for the ThrowResults</param>
        /// <exception cref="ArgumentException"></exception>
        protected AFrame(int frameNumberLabel, int nbThrows)
        {
            if (nbThrows <= 0)
            {
                throw new ArgumentException("The number of throw must be > 0");
            }
            FrameNumberLabel = frameNumberLabel;
            throwResults = new ThrowResult[nbThrows];
            ThrowResults = new ReadOnlyCollection<ThrowResult>(throwResults);
            CleanFrame();
        }

        /// <summary>
        /// Write a ThrowResult in the specified slot
        /// </summary>
        /// <param name="index"></param>
        /// <param name="throwResult"></param>
        protected void WriteThrow(int index, ThrowResult throwResult)
        {
            throwResults[index] = throwResult;
        }

        /// <summary>
        /// Set all Frame's slots to ThrowResult.NONE
        /// </summary>
        public void CleanFrame()
        {
            for(int i = 0; i < throwResults.Length; i++)
            {
                throwResults[0] = ThrowResult.NONE;
            }
        }

        /// <summary>
        /// Two frames are equals if they have the same FrameLabelNumber
        /// </summary>
        /// <param name="obj">other</param>
        /// <returns>equality</returns>
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is AFrame)) return false;
            AFrame other = (AFrame)obj;
            return frameNumberLabel == other.FrameNumberLabel;
        }
    }
}
