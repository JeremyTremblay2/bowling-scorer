using Model.score;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.score
{
    public class AFrame_UT
    {
        // Use MOCK (library for instantiate Abstract class but not realy, test the constructor's method

        [Theory]
        [InlineData(false, 1, 2)]
        public void Test_Constructor(bool throwException, int frameNumberLabel, int nbThrows)
        {
            if (throwException)
            {
                Assert.Throws<ArgumentException>(() => {  });
            }
        }
    }
}
