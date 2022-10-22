using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameModel.Frame.ThrowResults;

namespace FrameModel.Frame
{
    /// <summary>
    /// Represents a Frame
    /// </summary>
    public abstract class AFrame : IEquatable<AFrame>, ICloneable
    {
        /// <summary>
        /// The Id of the Frame
        /// </summary>
        public int ID { get => _id;}
        private readonly int _id;

        /// <summary>
        /// The name of the Frame
        /// </summary>
        public int FrameNumberLabel
        {
            get { return frameNumberLabel; }
            private set
            {
                if (value < 1) frameNumberLabel = 1;
                else frameNumberLabel = value;
            }
        }
        private int frameNumberLabel;

        /// <summary>
        /// Results of each Throw contained in the Frame
        /// </summary>
        private readonly ThrowResult[] throwResults;
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
        /// Property that contains CumulativeScore, computed and updated by another class 
        /// </summary>
        public int CumulativeScore
        {
            get { return cumulativeScore; }
            set
            {
                if (value < 0) cumulativeScore = 0;
                else cumulativeScore = value;
            }
        }
        private int cumulativeScore;

        /// <summary>
        /// Property returning a boolean indicating if the Frame is empty.
        /// </summary>
        public bool IsEmpty
        {
            get => throwResults.All(t => t == ThrowResult.NONE);
        }

        /// <summary>
        /// Build a Frame with the specified label and the specified number of slots
        /// </summary>
        /// <param name="frameNumberLabel">Name of the Frame</param>
        /// <param name="nbThrows">Number of slots for the ThrowResults</param>
        /// <exception cref="ArgumentException"></exception>
        protected AFrame(int frameNumberLabel, int nbThrows) : this (0, frameNumberLabel, nbThrows)
        {
        }

        /// <summary>
        /// Build a frame and specify the Id
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="frameNumberLabel"></param>
        /// <param name="nbThrows"></param>
        protected AFrame(int Id, int frameNumberLabel, int nbThrows)
        {
            if (nbThrows <= 0)
            {
                throw new ArgumentException("The number of throw must be > 0");
            }
            FrameNumberLabel = frameNumberLabel;
            throwResults = new ThrowResult[nbThrows];
            ThrowResults = new ReadOnlyCollection<ThrowResult>(throwResults);
            _id = Id;
            CleanFrame();
        }

        /// <summary>
        /// Write a ThrowResult in the specified slot
        /// </summary>
        /// <param name="index"></param>
        /// <param name="throwResult"></param>
        internal void WriteThrow(int index, ThrowResult throwResult)
        {
            throwResults[index] = throwResult;
        }

        /// <summary>
        /// Set all Frame's slots to ThrowResult.NONE
        /// </summary>
        public void CleanFrame()
        {
            for (int i = 0; i < throwResults.Length; i++)
            {
                throwResults[i] = ThrowResult.NONE;
            }
        }

        /// <summary>
        /// Create a new instance of AFram by cloning the current object.
        /// </summary>
        /// <returns>A copy of this.</returns>
        public abstract object Clone();

        /// <summary>
        /// Determines whether the two object instances are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the actual object.</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, False.</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj == this) return true;
            if (!obj.GetType().Equals(GetType())) return false;
            return Equals(obj as AFrame);
        }

        /// <summary>
        /// Determines whether the two object instances are equal.
        /// </summary>
        /// <param name="other">The frame to compare with the actual frame.</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, False.</returns>
        public bool Equals(AFrame? other)
        {
            return other != null && FrameNumberLabel.Equals(other.FrameNumberLabel);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(FrameNumberLabel);
        }

        /// <summary>
        /// Returns a string representing a frame.
        /// </summary>
        /// <returns>A string representing a frame.</returns>
        public override string ToString()
        {
            StringBuilder builder = new($"[{FrameNumberLabel}] - Score: {ScoreValue} / Total: {CumulativeScore} - ");
            for (int i = 0; i < throwResults.Length; i++)
            {
                if (i >= throwResults.Length - 1)
                {
                    builder.Append($" {throwResults[i]} ");
                }
                else
                {
                    builder.Append($" {throwResults[i]} |");
                }
            }
            return builder.ToString();
        }
    }
}
