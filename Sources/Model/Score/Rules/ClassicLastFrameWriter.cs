using Model.Exceptions;
using Model.Score;
using Model.Score.Frame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Score.Rules
{
    public class ClassicLastFrameWriter : IFrameWriter
    {
        public void WriteValue(AFrame frame, int index, ThrowResult throwResult)
        {
            if (index >= 3 || index < 0) throw new ArgumentOutOfRangeException("The given index is out of range, the classic last frame has only 3 slots. (index 0, 1 and 2)");
            if (!(frame is ClassicLastFrame)) throw new ArgumentException("Only ClassicLastFrame accepted");
            if (index == 0)
            {
                if (throwResult == ThrowResult.SPARE)
                {
                    throw new ForbiddenThrowResultException("You can't write a SPARE in the first slot of a ClassicLastFrame");
                }
                frame.WriteThrow(0, throwResult);
            }
            else if (index == 1)
            {
                frame.WriteThrow(1, throwResult);
            }
            else
            {
                if (throwResult == ThrowResult.NONE) frame.WriteThrow(2, throwResult);
                else if (frame.ThrowResults[0] != ThrowResult.STRIKE
                    && frame.ThrowResults[1] != ThrowResult.STRIKE
                    && frame.ThrowResults[1] != ThrowResult.SPARE)
                {
                    throw new ForbiddenThrowResultException("You can't write a third result if you didn't made a STRIKE in one of the two pervious slot");
                }
                frame.WriteThrow(2, throwResult);
            }
        }
    }
}
