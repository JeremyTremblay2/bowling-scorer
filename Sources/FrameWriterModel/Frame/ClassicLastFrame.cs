using FrameModel.Frame.ThrowResults;
using FrameModel.Writer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameModel.Frame
{
    /// <summary>
    /// The ClassicLastFrame represent a classic frame with 3 boxes.
    /// </summary>
    public class ClassicLastFrame : AFrame
    {
        private const int CLASSIC_LAST_SIZE = 3;
        private static readonly AFrameWriter frameWriter = new ClassicLastFrameWriter();

        /// <summary>
        /// Constructor of ClassicLastFrame
        /// </summary>
        /// <param name="frameNumberLabel">The number of the Frame</param>
        public ClassicLastFrame(int frameNumberLabel) : base(frameNumberLabel, CLASSIC_LAST_SIZE)
        {
        }

        /// <summary>
        /// Constructor of ClassicLastFrame with Id
        /// </summary>
        /// <param name="frameNumberLabel">The number of the Frame</param>
        public ClassicLastFrame(int id, int frameNumberLabel) : base(id, frameNumberLabel, CLASSIC_LAST_SIZE)
        {
        }

        /// <summary>
        /// Constructor of ClassicLastFrame with an Id and some values in the slots
        /// </summary>
        /// <param name="id"></param>
        /// <param name="frameNumberLabel"></param>
        public ClassicLastFrame(int id, int frameNumberLabel, ThrowResult throwResult1, ThrowResult throwResult2, ThrowResult throwResult3) : this(id, frameNumberLabel)
        {
            frameWriter.WriteValue(this, 0, throwResult1);
            frameWriter.WriteValue(this, 1, throwResult2);
            frameWriter.WriteValue(this, 2, throwResult3);
        }

        /// <summary>
        /// Constructor of ClassicLastFrame with default ThrowResults
        /// </summary>
        /// <param name="frameNumberLabel">The number of the Frame</param>
        public ClassicLastFrame(int frameNumberLabel, ThrowResult throwResult1, ThrowResult throwResult2, ThrowResult throwResult3) : this(0, frameNumberLabel, throwResult1, throwResult2, throwResult3)
        {
        }

        /// <summary>
        /// Create a new instace of ClassicLastFrame.
        /// </summary>
        /// <returns>A copy of this.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public override object Clone()
        {
            return new ClassicLastFrame(FrameNumberLabel, ThrowResults[0], ThrowResults[1], ThrowResults[2]);
        }
    }
}