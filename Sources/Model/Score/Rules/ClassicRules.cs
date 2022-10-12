using FrameWriterModel.Frame;
using FrameWriterModel.Frame.ThrowResults;
using FrameWriterModel.Writer;
using Model.Score.Rules.Calculator;
using Model.Score.Rules.Retriever;
using System.Collections.Generic;

namespace Model.Score.Rules
{
    /// <summary>
    /// Represents the classic rules of a traditionnal bowling game.
    /// </summary>
    public class ClassicRules : ARules
    {
        /// <summary>
        /// Use a classic calculator and classicWriters to apply classic game rules
        /// </summary>
        public ClassicRules() : base(new ClassicScoreCalculator(), new ClassicPossibleThrowResultsRetriever(),
            new List<AFrameWriter> { new ClassicFrameWriter(), new ClassicLastFrameWriter()})
        {
        }

        /// <summary>
        /// Write a value in the given AFrame by using the writers.
        /// Here if a ClassicFrame is given, a ClassicFrameWriter will be used to write the result
        /// if a ClassicLastFrame is given, a ClassicLastFrameWriter will be used to write the result
        /// </summary>
        /// <param name="frame">The frame to write into.</param>
        /// <param name="index">The index of the frame.</param>
        /// <param name="throwResult">The throw result to write.</param>
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
