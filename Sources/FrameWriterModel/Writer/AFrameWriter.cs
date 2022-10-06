using FrameWriterModel.Frame;
using FrameWriterModel.Frame.ThrowResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWriterModel.Writer
{
    public abstract class AFrameWriter
    {
        /// <summary>
        /// Used by AFrameWrither's children, this method allow childrens to call the WriteThrow method even if the children is in other assembly
        /// This conception keep the encapsulation of the AFrame's WriteThrow safe, only AFrameWriter's childrens can write in a AFrame
        /// </summary>
        /// <param name="frame">The frame which will receive the new throw</param>
        /// <param name="index">The slot number</param>
        /// <param name="throwResult">The result to write</param>
        protected void WriteValueInFrame(AFrame frame, int index, ThrowResult throwResult)
        {
            frame.WriteThrow(index, throwResult);
        }

        /// <summary>
        /// Define this method to allow your children to write a Throw in a AFrame
        /// </summary>
        /// <param name="frame">The frame which will receive the new throw</param>
        /// <param name="index">The slot number</param>
        /// <param name="throwResult">The result to write</param>
        public abstract void WriteValue(AFrame frame, int index, ThrowResult throwResult);
    }
}
