using FrameModel.Frame;
using FrameModel.Frame.ThrowResults;
using FrameModel.Writer;
using Model.Games;
using Model.Score.Rules.Calculator;
using Model.Score.Rules.Detector;
using Model.Score.Rules.Retriever;
using System;
using System.Collections.Generic;

namespace Model.Score.Rules
{
    /// <summary>
    /// Represents the classic rules of a traditionnal bowling game.
    /// </summary>
    public class ClassicRules : ARules, IEquatable<ClassicRules>
    {
        /// <summary>
        /// Use a classic calculator and classicWriters to apply classic game rules
        /// </summary>
        public ClassicRules() : base(new ClassicScoreCalculator(), new ClassicPossibleThrowResultsRetriever(),
            new ClassicFullScoreTableDetector(), new List<AFrameWriter> { new ClassicFrameWriter(), new ClassicLastFrameWriter()})
        {
        }

        public override List<AFrame> GenerateScoreTable()
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

        /// <summary>
        /// Determines whether the two object instances are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the actual object.</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, False.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (ReferenceEquals(obj, this)) return true;
            if (obj.GetType() != typeof(ClassicRules)) return false;
            return Equals(obj as ClassicRules);
        }

        /// <summary>
        /// Determines whether the two object instances are equal.
        /// </summary>
        /// <param name="other">The other classic rules to compare with the actual classic rules.</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, False.</returns>
        public bool Equals(ClassicRules other)
        {
            return true; // Two classic rules are identicals.
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return 13;
        }
    }
}
