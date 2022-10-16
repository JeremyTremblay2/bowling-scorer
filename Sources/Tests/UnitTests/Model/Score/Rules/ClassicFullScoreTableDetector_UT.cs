using Model.Score;
using Model.Score.Rules.Detector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Score.Rules
{
    /// <summary>
    /// Class used for unit testing the ClassicFullScoreTableDetector class.
    /// </summary>
    public class ClassicFullScoreTableDetector_UT
    {
        [Theory]
        [MemberData(nameof(ClassicFullScoreTableDetectorDataTest.Data_ScoreTables),
            MemberType = typeof(ClassicFullScoreTableDetectorDataTest))]
        public void IsScoreTableCompleteShouldReturnLogicalValues(bool expectedResult, ScoreTable scoreTable)
        {
            IFullScoreTableDetector scoreTableDetector = new ClassicFullScoreTableDetector();
            bool result = scoreTableDetector.IsScoreTableComplete(scoreTable);
            Assert.Equal(expectedResult, result);
        }
    }
}
