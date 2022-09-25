using Model.exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.score
{
    /// <summary>
    /// The classic Frame in bowling
    /// </summary>
    public class ClassicFrame : AFrame
    {
        /// <summary>
        /// Constructor of ClassicFrame
        /// </summary>
        /// <param name="frameNumberLabel">The number of the Frame</param>
        public ClassicFrame(int frameNumberLabel) : base(frameNumberLabel, 2)
        {
        }

        /// <summary>
        /// Constructor of ClassicFrame with default ThrowResultValues
        /// </summary>
        /// <param name="frameNumberLabel">The number of the Frame</param>
        public ClassicFrame(int frameNumberLabel, ThrowResult throwResult1, ThrowResult throwResult2) : base(frameNumberLabel, 2)
        {
            WriteFirstThrow(throwResult1);
            WriteSecondThrow(throwResult2);
        }

        /// <summary>
        /// Write a throw in the first slot of the frame
        /// </summary>
        /// <param name="throwResult">Result to write</param>
        /// <exception cref="ArgumentException">If the result cannot be written here (We can't write a SPAIR or a STRIKE in the first slot)</exception>
        public void WriteFirstThrow(ThrowResult throwResult)
        {
            if (throwResult == ThrowResult.STRIKE || throwResult == ThrowResult.SPAIR)
            {
                throw new ForbiddenThrowResultException("You can't write a STRIKE or a SPAIR in the first slot of a ClassicFrame");
            }
            WriteThrow(0, throwResult);
        }

        /// <summary>
        /// Write a throw in the second slot of the frame
        /// </summary>
        /// <param name="throwResult">Result to write</param>
        public void WriteSecondThrow(ThrowResult throwResult)
        {
            WriteThrow(1, throwResult);
        }

        public bool isSpair()
        {
            return ThrowResults[1] == ThrowResult.SPAIR;
        }

        public bool isStrike()
        {
            return ThrowResults[1] == ThrowResult.STRIKE;
        }
    }
}
