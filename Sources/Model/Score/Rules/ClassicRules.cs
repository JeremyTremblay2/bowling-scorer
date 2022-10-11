using FrameWriterModel.Frame;
using FrameWriterModel.Frame.ThrowResults;
using FrameWriterModel.Writer;
using Model.Score.Rules.Calculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Score.Rules
{
    public class ClassicRules : ARules
    {
        public ClassicRules() : base(new ClassicScoreCalculator(), new List<AFrameWriter> { new ClassicFrameWriter(), new ClassicLastFrameWriter()})
        {
        }

        public override void WriteValue(AFrame frame, int index, ThrowResult throwResult)
        {
            ClassicFrame classicFrame = frame as ClassicFrame;
            ClassicLastFrame classicLastFrame = frame as ClassicLastFrame;

            if (classicFrame != null)
            {
                writers[0].WriteValue(frame, index, throwResult);
            }
            else if (classicLastFrame != null)
            {
                writers[1].WriteValue(frame, index, throwResult);
            }
        }
    }
}
