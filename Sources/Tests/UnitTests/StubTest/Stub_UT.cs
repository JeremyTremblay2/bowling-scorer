using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Stub;
using Model.Games;
using Model.Players;
using Model.Score.Rules;

namespace UnitTests.StubTest
{
    public class Stub_UT
    {
        [Fact]
        public void TestIfAddGameIsLocked()
        {
            Stub.Stub stub = new();
            //Assert.Throws<NotImplementedException>(() => stub.AddGame(null));
        }
    }
}
