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
        /// <param name="id"></param>
        /// <param name="frameNumberLabel"></param>
        public ClassicFrame(int id, int frameNumberLabel) : base(id, frameNumberLabel, CLASSIC_SIZE)
        {
        }

        /// <summary>
        /// Constructor of ClassicFrame with an Id and some values in the slots
        /// </summary>
        /// <param name="id"></param>
        /// <param name="frameNumberLabel"></param>
        public ClassicFrame(int id, int frameNumberLabel, ThrowResult throwResult1, ThrowResult throwResult2) : this(id, frameNumberLabel)
        {
            frameWriter.WriteValue(this, 0, throwResult1);
            frameWriter.WriteValue(this, 1, throwResult2);
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
        public ClassicFrame(int frameNumberLabel, ThrowResult throwResult1, ThrowResult throwResult2) : this(frameNumberLabel)
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
