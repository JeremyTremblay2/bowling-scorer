using Model.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Score.Frame
{
    /// <summary>
    /// The classic Frame in bowling
    /// </summary>
    public class ClassicFrame : AFrame
    {
        private const int CLASSIC_SIZE = 2;
        public bool IsSpare => ThrowResults[1] == ThrowResult.SPARE;
        public bool IsStrike => ThrowResults[1] == ThrowResult.STRIKE;

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
            WriteFirstThrow(throwResult1);
            WriteSecondThrow(throwResult2);
        }

        /// <summary>
        /// Write a throw in the first slot of the frame
        /// </summary>
        /// <param name="throwResult">Result to write</param>
        /// <exception cref="ArgumentException">If the result cannot be written here (We can't write a SPARE or a STRIKE in the first slot)</exception>
        public void WriteFirstThrow(ThrowResult throwResult)
        {
            if (throwResult == ThrowResult.STRIKE || throwResult == ThrowResult.SPARE)
            {
                throw new ForbiddenThrowResultException("You can't write a STRIKE or a SPARE in the first slot of a ClassicFrame");
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
    }
}
