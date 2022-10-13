﻿using FrameWriterModel.Frame;
using FrameWriterModel.Frame.ThrowResults;
using FrameWriterModel.Writer;
using Model.Score.Rules;
using Model.Score.Rules.Calculator;
using Model.Score.Rules.Retriever;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Score.Rules
{
    /// <summary>
    /// Class used to do some unit tests on the ARules class.
    /// </summary>
    public class ARules_UT
    {
        private class ARuleTester : ARules
        {
            public ARuleTester(IScoreCalculator scoreCalculator, IPossibleThrowResultsRetriever throwResultsRetriever, 
                List<AFrameWriter> writers) : base(scoreCalculator, throwResultsRetriever, writers)
            {
            }

            public override void WriteValue(AFrame frame, int index, ThrowResult throwResult)
            {
                throw new NotImplementedException();
            }

            /// <summary>
            /// Used only for test
            /// </summary>
            /// <returns></returns>
            public List<AFrameWriter> GetWriters()
            {
                return writers;
            }

            /// <summary>
            /// Used only for test
            /// </summary>
            /// <returns></returns>
            public IScoreCalculator GetCalculator()
            {
                return scoreCalculator;
            }

            public override List<AFrame> GenerateScoreTable()
            {
                throw new NotImplementedException();
            }

            public IPossibleThrowResultsRetriever GetThrowResultsRetriever()
            {
                return throwResultsRetriever;
            }
        }

        [Fact]
        public void Test_ARulesConstructor()
        {
            IScoreCalculator scoreCalculator = new ClassicScoreCalculator();
            IPossibleThrowResultsRetriever throwResultsRetriever = new ClassicPossibleThrowResultsRetriever();
            AFrameWriter classicFrameWriter = new ClassicFrameWriter();
            AFrameWriter classicLastFrameWriter = new ClassicLastFrameWriter();
            ARules aRules = new ARuleTester(scoreCalculator, throwResultsRetriever, 
                new List<AFrameWriter> { classicFrameWriter, classicLastFrameWriter});
            ARuleTester aRuleTester = aRules as ARuleTester;
            Assert.NotNull(aRuleTester);
            Assert.Equal(scoreCalculator, aRuleTester.GetCalculator());
            Assert.Equal(throwResultsRetriever, aRuleTester.GetThrowResultsRetriever());
            Assert.Equal(classicFrameWriter, aRuleTester.GetWriters()[0]);
            Assert.Equal(classicLastFrameWriter, aRuleTester.GetWriters()[1]);
        }
    }
}
