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
        /// <summary>
        /// Use a classic calculator and classicWriters to apply classic game rules
        /// </summary>
        /// <param name="scoreCalculator"></param>
        /// <param name="writers"></param>
        public ClassicRules() : base(new ClassicScoreCalculator(), new List<AFrameWriter> { new ClassicFrameWriter(), new ClassicLastFrameWriter()})
        {
        }

        public override List<AFrame> GenerateClassicScoreTable()
        {
            return new List<AFrame>()
            {
                new ClassicFrame(1),
                new ClassicFrame(2),
                new ClassicFrame(3),
                new ClassicFrame(4),
                new ClassicFrame(5),
                new ClassicFrame(6),
                new ClassicFrame(7),
                new ClassicFrame(8),
                new ClassicFrame(9),
                new ClassicLastFrame(10)
            };
        }

        /// <summary>
        /// Write a value in the given AFrame by using the writers.
        /// Here if a ClassicFrame is given, a ClassicFrameWriter will be used to write the result
        /// if a ClassicLastFrame is given, a ClassicLastFrameWriter will be used to write the result
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="index"></param>
        /// <param name="throwResult"></param>
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
