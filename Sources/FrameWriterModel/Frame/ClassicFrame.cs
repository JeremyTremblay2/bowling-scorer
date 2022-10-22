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
    /// The classic Frame in bowling
    /// </summary>
    public class ClassicFrame : AFrame
    {
        private const int CLASSIC_SIZE = 2;
        private static readonly AFrameWriter frameWriter = new ClassicFrameWriter();
        public bool IsSpare => ThrowResults[1] == ThrowResult.SPARE;
        public bool IsStrike => ThrowResults[1] == ThrowResult.STRIKE;

        /// <summary>
        /// Constructor of ClassicFrame with an Id
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="frameNumberLabel"></param>
        public ClassicFrame(int Id, int frameNumberLabel) : base(Id, frameNumberLabel, CLASSIC_SIZE)
        {
        }

        /// <summary>
        /// Constructor of ClassicFrame
        /// </summary>
        /// <param name="frameNumberLabel">The number of the Frame</param>
        public ClassicFrame(int frameNumberLabel) : base(frameNumberLabel, CLASSIC_SIZE)
        {
        }

        /// <summary>
        /// Constructor of ClassicFrame with default ThrowResultValues
        /// </summary>
        /// <param name="frameNumberLabel">The number of the Frame</param>
        public ClassicFrame(int frameNumberLabel, ThrowResult throwResult1, ThrowResult throwResult2) : base(frameNumberLabel, 2)
        {
            frameWriter.WriteValue(this, 0, throwResult1);
            frameWriter.WriteValue(this, 1, throwResult2);
        }

        /// <summary>
        /// Create a new instance of ClassicFrame.
        /// </summary>
        /// <returns>A copy of this.</returns>
        public override object Clone()
        {
            return new ClassicFrame(FrameNumberLabel, ThrowResults[0], ThrowResults[1]);
        }
    }
}
