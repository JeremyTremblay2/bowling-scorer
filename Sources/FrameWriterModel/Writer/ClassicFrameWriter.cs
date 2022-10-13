using FrameWriterModel.Exceptions;
using FrameWriterModel.Frame;
using FrameWriterModel.Frame.ThrowResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWriterModel.Writer
{
    public class ClassicFrameWriter : AFrameWriter
    {
        /// <summary>
        /// Write a throw in the selected slot of the given AFrame
        /// </summary>
        /// <param name="frame">The frame which will receive the new throw</param>
        /// <param name="index">The slot number</param>
        /// <param name="throwResult">The result to write</param>
        /// <exception cref="ArgumentOutOfRangeException">If index < 0 or index > 1</exception>
        /// <exception cref="ForbiddenThrowResultException">If you can't write this ThrowResult in this slot</exception>
        public override void WriteValue(AFrame frame, int index, ThrowResult throwResult)
        {
            if (index >= 2 || index < 0) throw new ArgumentOutOfRangeException(nameof(index), 
                "The given index is out of range, the classic frame has only 2 slots. (index 0 and 1)");
            if (index == 0)
            {
                if (throwResult == ThrowResult.STRIKE || throwResult == ThrowResult.SPARE)
                {
                    throw new ForbiddenThrowResultException("You can't write a STRIKE or a SPARE in the first slot of a ClassicFrame");
                }
                WriteValueInFrame(frame, 0, throwResult);
            }
            else
            {
                WriteValueInFrame(frame, 1, throwResult);
            }
        }
    }
}
