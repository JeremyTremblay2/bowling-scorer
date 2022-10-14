using FrameWriterModel.Frame.ThrowResults;
using FrameWriterModel.Writer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWriterModel.Frame
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
        /// Constructor of ClassicLastFrame with default ThrowResults
        /// </summary>
        /// <param name="frameNumberLabel">The number of the Frame</param>
        public ClassicLastFrame(int frameNumberLabel, ThrowResult throwResult1, ThrowResult throwResult2, ThrowResult throwResult3) : base(frameNumberLabel, 3)
        {
            frameWriter.WriteValue(this, 0, throwResult1);
            frameWriter.WriteValue(this, 1, throwResult2);
            frameWriter.WriteValue(this, 2, throwResult3);
        }

        /// <summary>
        /// Create a new instace of ClassicLastFrame.
        /// </summary>
        /// <returns>A copy of this.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public override object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
