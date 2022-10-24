using Business;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Model.Games;
using Model.Players;
using NLog;
using NLog.Extensions.Logging;
using static System.Console;

namespace FunctionnalTests
{
    public static class TestStubModel
    {
        public static void TestDisplayPlayerLauncher()
        {
            var logger = LogManager.GetCurrentClassLogger();
            try
            {
                var config = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                   .Build();

                var servicesProvider = BuildDi(config);

                using (servicesProvider as IDisposable)
                {
                    var manager = servicesProvider.GetRequiredService<BowlingManager>();
                    TestDisplayPlayer(manager);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                LogManager.Shutdown();
            }
        }

        private static IServiceProvider BuildDi(IConfiguration config)
        {
            return new ServiceCollection()
               .AddTransient<BowlingManager>() 
               .AddSingleton<IDataManager>(x => new Stub.Stub()) // Add custom Stub for persistance
               .AddLogging(loggingBuilder =>
               {
                   // configure Logging with NLog
                   loggingBuilder.ClearProviders();
                   loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                   loggingBuilder.AddNLog(config);
               })
               .BuildServiceProvider();
        }

        private static void TestDisplayPlayer(BowlingManager manager)
        {
            WriteLine("Here are the games inside the manager before loading them: ");
            DisplayGames(manager.Games);

            WriteLine("We will now load data from the Manager, please wait and see logs that will appear during the process: ");

            DisplayLine();
            WriteLine("Press enter to continue...");
            ReadKey();

            WriteLine("Loading the games from the stub...");
            var games = manager.GetGames(0, 10).Result;

            DisplayLine();
            WriteLine("Press enter to continue...");
            ReadKey();

            WriteLine("Here are the games get from the manager after loading them: ");
            DisplayGames(games);

            DisplayLine();
            WriteLine("Press enter to continue...");
            ReadKey();

            WriteLine("Here are the games IN the manager after loading them: ");
            DisplayGames(manager.Games);

            DisplayLine();
            WriteLine("Press enter to continue...");
            ReadKey();

            WriteLine("Here is the current game: ");
            WriteLine(manager.CurrentGame?.ToString());

            DisplayLine();
            WriteLine("Press enter to continue...");
            ReadKey();

            WriteLine("Here are the players present in the manager before charge them from the stub: ");
            DisplayPlayers(manager.Players);

            DisplayLine();
            WriteLine("Press enter to continue...");
            ReadKey();

            _ = manager.GetPlayers(0, 20).Result;
            WriteLine("Here are the players present in the manager after charging them from the stub: ");
            DisplayPlayers(manager.Players);

            DisplayLine();
            WriteLine("Press enter to continue...");
            ReadKey();

            WriteLine("Here is the logger associated with this test: ");
            WriteLine(LogManager.GetCurrentClassLogger()); // For fun.

            WriteLine("Press ANY key to exit");
            ReadKey();
        }

        private static void DisplayGames(IEnumerable<Game> games)
        {
            foreach (Game game in games)
            {
                WriteLine(game.ToString());
                WriteLine("-----------------");
            }
        }

        private static void DisplayPlayers(IEnumerable<Player> players)
        {
            foreach (Player player in players)
            {
                WriteLine(player.ToString());
                WriteLine("-----------------");
            }
        }

        private static void DisplayLine()
        {
            WriteLine("-----------------------------------------------------------------------");
        }
    }
}
