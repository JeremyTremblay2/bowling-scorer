using Model.Exceptions;
using Model.Score;
using Model.Score.Frame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.score.Rules
{
    public class ClassicFrameWriter : IFrameWriter
    {
        public void WriteValue(AFrame frame, int index, ThrowResult throwResult)
        {
            if (index >= 2 || index < 0) throw new ArgumentOutOfRangeException("The given index is out of range, the classic frame has only 2 slots. (index 0 and 1)");
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

            }
        }
    }
}
