using FrameWriterModel.Frame;
using FrameWriterModel.Frame.ThrowResults;
using Model.Players;
using Model.Score.Rules.Retriever;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Score.Rules
{
    public class ClassicPossibleThrowResultsRetriever_UT
    {
        [Theory]
        [MemberData(nameof(ClassicPossibleThrowResultsRetrieverDataTest.Data_GetPossibleResults), 
            MemberType = typeof(ClassicPossibleThrowResultsRetrieverDataTest))]
        public void GetPossibleThrowResultsFromClassicRetrieverShouldReturnLogicalValues(IEnumerable<ThrowResult> expectedThrowResults,
                                                                                         AFrame frameToAdd,
                                                                                         int indexToAdd)
        {
            IPossibleThrowResultsRetriever retriever = new ClassicPossibleThrowResultsRetriever();
            IEnumerable<ThrowResult> results = retriever.GetPossibleThrowResults(frameToAdd, indexToAdd);
            Assert.Equal(expectedThrowResults, results);
            Assert.Equal(expectedThrowResults.Count(), results.Count());
            Assert.All(expectedThrowResults, action: t => results.Contains(t));
        }
    }
}
