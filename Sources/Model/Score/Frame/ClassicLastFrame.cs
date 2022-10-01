using Model.Exceptions;
using Model.Score.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Score.Frame
{
    /// <summary>
    /// The last ClassiFrame in the ScoreBoard in bowling
    /// </summary>
    public class ClassicLastFrame : AFrame
    {
        private const int CLASSIC_LAST_SIZE = 3;
        private static readonly IFrameWriter frameWriter = new ClassicLastFrameWriter();

        /// <summary>
        /// Constructor of ClassicLastFrame
        /// </summary>
        /// <param name="frameNumberLabel">The number of the Frame</param>
        public ClassicLastFrame(int frameNumberLabel) : base(frameNumberLabel, CLASSIC_LAST_SIZE)
        {
        }

        /// <summary>
        /// Constructor of ClassicLastFrame with default ThrowResults
        /// </summary>
        /// <param name="frameNumberLabel">The number of the Frame</param>
        public ClassicLastFrame(int frameNumberLabel, ThrowResult throwResult1, ThrowResult throwResult2, ThrowResult throwResult3) : base(frameNumberLabel, 3)
        {
            frameWriter.WriteValue(this, 0, throwResult1);
            frameWriter.WriteValue(this, 1, throwResult2);
            frameWriter.WriteValue(this, 2, throwResult3);
        }
    }
}
