using Model.Exceptions;
using Model.Score.Rules;
using Model.Score;
using Model.Score.Frame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Score.Rules
{
    public class ClassicFrameWriter : IFrameWriter
    {
        public void WriteValue(AFrame frame, int index, ThrowResult throwResult)
        {
            if (index >= 2 || index < 0) throw new ArgumentOutOfRangeException("The given index is out of range, the classic frame has only 2 slots. (index 0 and 1)");
            if (!(frame is ClassicFrame)) throw new ArgumentException("Only ClassicFrame accepted");
            if (index == 0)
            {
                if (throwResult == ThrowResult.STRIKE || throwResult == ThrowResult.SPARE)
                {
                    throw new ForbiddenThrowResultException("You can't write a STRIKE or a SPARE in the first slot of a ClassicFrame");
                }
                frame.WriteThrow(0, throwResult);
            }
            else
            {
                frame.WriteThrow(1, throwResult);
            }
        }
    }
}
