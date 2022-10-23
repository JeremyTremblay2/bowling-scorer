using Entities;
using Entity2Model;
using FrameModel.Frame;
using FrameModel.Frame.ThrowResults;
using Microsoft.EntityFrameworkCore;
using Model.Games;
using Model.Players;
using Model.Score;
using Model.Score.Rules;
using Stub;
using static System.Console;

namespace FunctionnalTests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //TestPlayersEF.TestPlayerDatabase();
            TestStubModel.TestDisplayPlayerLauncher();
        }
    }
}
