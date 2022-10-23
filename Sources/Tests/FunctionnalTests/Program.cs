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
    /// <summary>
    /// Main class of the functionnal tests.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Change the methods call here to see all the funcitonnal tests of the project.
        /// </summary>
        /// <param name="args">The program arguments.</param>
        public static void Main(string[] args)
        {
            //TestFrameEF.TestFrameEntity();
            //TestPlayersEF.PlayerFuctionnalTests();
            //TestStubModel.TestDisplayPlayerLauncher();
            ConsoleAppBowlingScorer.TestDisplayPlayerLauncher();
        }
    }
}
