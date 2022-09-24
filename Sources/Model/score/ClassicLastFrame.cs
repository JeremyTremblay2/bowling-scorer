using Model.exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.score
{
    /// <summary>
    /// The last ClassiFrame in the ScoreBoard in bowling
    /// </summary>
    public class ClassicLastFrame : AFrame
    {
        /// <summary>
        /// Constructor of ClassicLastFrame
        /// </summary>
        /// <param name="frameNumberLabel">The number of the Frame</param>
        public ClassicLastFrame(int frameNumberLabel) : base(frameNumberLabel, 3)
        {
        }

        /// <summary>
        /// Constructor of ClassicLastFrame with default ThrowResults
        /// </summary>
        /// <param name="frameNumberLabel">The number of the Frame</param>
        public ClassicLastFrame(int frameNumberLabel, ThrowResult throwResult1, ThrowResult throwResult2, ThrowResult throwResult3) : base(frameNumberLabel, 3)
        {
            WriteFirstThrow(throwResult1);
            WriteSecondThrow(throwResult2);
            WriteThridThrow(throwResult3);
        }

        /// <summary>
        /// Write a throw in the first slot of the frame
        /// </summary>
        /// <param name="throwResult">Result to write</param>
        /// <exception cref="ArgumentException">If the result cannot be written here (We can't write a SPAIR in the first slot)</exception>
        public void WriteFirstThrow(ThrowResult throwResult)
        {
            if (throwResult == ThrowResult.SPAIR)
            {
                throw new ForbiddenThrowResultException("You can't write a SPAIR in the first slot of a ClassicLastFrame");
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

        /// <summary>
        /// Write a throw in the third slot of the frame
        /// </summary>
        /// <param name="throwResult">Result to write</param>
        /// <exception cref="ForbiddenThrowResultException">If the result cannot be written here (We can't write something in the third slot
        /// if we didn't made a STRIKE or a SPAIR previously)</exception>
        public void WriteThridThrow(ThrowResult throwResult)
        {
            if (throwResult == ThrowResult.NONE) WriteThrow(2, throwResult);
            else if (ThrowResults[0] != ThrowResult.STRIKE 
                && ThrowResults[1] != ThrowResult.STRIKE 
                && ThrowResults[1] != ThrowResult.SPAIR)
            {
                throw new ForbiddenThrowResultException("You can't write a third result if you didn't made a STRIKE in one of the two pervious slot");
            }
            WriteThrow(2, throwResult);
        }

        public override bool isSpair()
        {
            return ThrowResults[2] == ThrowResult.SPAIR 
                || ThrowResults[1] == ThrowResult.SPAIR;
        }

        public override bool isStrike()
        {
            return ThrowResults[2] == ThrowResult.STRIKE 
                || ThrowResults[1] == ThrowResult.STRIKE 
                || ThrowResults[0] == ThrowResult.STRIKE;
        }
    }
}
