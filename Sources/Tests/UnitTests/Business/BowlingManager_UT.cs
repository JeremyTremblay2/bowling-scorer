using Business;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Business
{
    public class BowlingManager_UT
    {

        private static BowlingManager manager;
        public static void Init()
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
                    manager = servicesProvider.GetRequiredService<BowlingManager>();
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

        [Fact]
        public void BowlingManagerWithStubShouldThrowException()
        {
            Assert.ThrowsAsync<NotImplementedException>(() => manager.AddGame(default));
            Assert.ThrowsAsync<NotImplementedException>(() => manager.AddPlayer(default));
            Assert.ThrowsAsync<NotImplementedException>(() => manager.AddPlayers(default));
            Assert.ThrowsAsync<NotImplementedException>(() => manager.EditPlayer(default, default, default));
            Assert.ThrowsAsync<NotImplementedException>(() => manager.GetGameFromID(default));
            Assert.ThrowsAsync<NotImplementedException>(() => manager.GetGamesFromPlayer(default, default, default));
            Assert.ThrowsAsync<NotImplementedException>(() => manager.GetPlayerFromID(default));
            Assert.ThrowsAsync<NotImplementedException>(() => manager.GetPlayerFromName(default, default, default));
            Assert.ThrowsAsync<NotImplementedException>(() => manager.RemoveGame(default));
            Assert.ThrowsAsync<NotImplementedException>(() => manager.RemovePlayer(default));
        }

        [Fact]
        public void BowlingManagerWithStubShouldGetPlayers()
        {
            Init();
            var players = manager.GetPlayers(0, 20).Result;
            Assert.Equal(15, players.Count());
        }

        [Fact]
        public void BowlingManagerWithStubShouldGetGames()
        {
            Init();
            var games = manager.GetGames(0, 20).Result;
            Assert.Equal(3, games.Count());
        }
    }
}
