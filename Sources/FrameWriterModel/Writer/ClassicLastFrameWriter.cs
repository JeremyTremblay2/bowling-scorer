using FrameModel.Exceptions;
using FrameModel.Frame;
using FrameModel.Frame.ThrowResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameModel.Writer
{
    public class ClassicLastFrameWriter : AFrameWriter
    {
        /// <summary>
        /// Write a throw in the selected slot of the given AFrame
        /// </summary>
        /// <param name="frame">The frame which will receive the new throw</param>
        /// <param name="index">The slot number</param>
        /// <param name="throwResult">The result to write</param>
        /// <exception cref="ArgumentOutOfRangeException">If index < 0 or index > 2</exception>
        /// <exception cref="ForbiddenThrowResultException">If you can't write this ThrowResult in this slot</exception>
        public override void WriteValue(AFrame frame, int index, ThrowResult throwResult)
        {
            if (index >= 3 || index < 0) throw new ArgumentOutOfRangeException(nameof(index),
                "The given index is out of range, the classic last frame has only 3 slots. (index 0, 1 and 2)");
            if (index == 0)
            {
                if (throwResult == ThrowResult.SPARE)
                {
                    throw new ForbiddenThrowResultException("You can't write a SPARE in the first slot of a ClassicLastFrame");
                }
                WriteValueInFrame(frame, 0, throwResult);
            }
            else if (index == 1)
            {
                WriteValueInFrame(frame, 1, throwResult);
            }
            else
            {
                if (throwResult == ThrowResult.NONE) WriteValueInFrame(frame, 2, throwResult);
                else if (frame.ThrowResults[0] != ThrowResult.STRIKE
                    && frame.ThrowResults[1] != ThrowResult.STRIKE
                    && frame.ThrowResults[1] != ThrowResult.SPARE)
                {
                    throw new ForbiddenThrowResultException("You can't write a third result if you didn't made a STRIKE in one of the two pervious slot");
                }
                WriteValueInFrame(frame, 2, throwResult);
            }
        }
    }
}
