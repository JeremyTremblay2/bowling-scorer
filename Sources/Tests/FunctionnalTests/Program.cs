using Entities;
using Entity2Model;
using Microsoft.EntityFrameworkCore;
using Model.Games;
using Model.Players;
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
