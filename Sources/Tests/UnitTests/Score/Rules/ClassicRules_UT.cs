using FrameWriterModel.Frame;
using FrameWriterModel.Frame.ThrowResults;
using Model.Score.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Score.Rules
{
    public class ClassicRules_UT
    {
        [Fact]
        public void Test_ARulesConstructor()
        {
            ARules aRules = new ClassicRules();
            AFrame frame = new ClassicFrame(1);
            aRules.WriteValue(frame, 0, ThrowResult.TREE);

        }
    }
}
