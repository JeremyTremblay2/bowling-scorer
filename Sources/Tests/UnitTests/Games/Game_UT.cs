using Model.Games;
using Model.Players;
using Model.Score.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Games
{
    /// <summary>
    /// Class used for unit testing the Game class.
    /// </summary>
    public class Game_UT
    {
        [Fact]
        public void CreateGameShouldInitCollections()
        {
            Game game = new Game(new ClassicRules(), 0, new List<Player>
            {
                new Player("Theo", "imageTheo")
            });
            Assert.NotNull(game.Players);
            Assert.NotNull(game.Scores);
        }

        [Fact]
        public void CreateGameShouldCreateCollections()
        {
            Game game = new Game(new ClassicRules(), 0, new List<Player>
            {
                new Player("Theo", "imageTheo")
            });
            Assert.Single(game.Players);
            Assert.Single(game.Scores);
        }

        [Fact]
        public void CreateGameShouldInitCreationDate()
        {
            DateTime date = DateTime.Now;
            Game game = new Game(new ClassicRules(), 0, new List<Player>
            {
                new Player("Theo", "imageTheo")
            });
            Assert.Equal(date.Date, game.CreationDate.Date);
        }

        [Fact]
        public void CreateGameShouldInitFinishedStatus()
        {
            Game game = new Game(new ClassicRules(), 0, new List<Player>
            {
                new Player("Theo", "imageTheo")
            });
            Assert.False(game.IsFinished);
        }

        [Fact]
        public void CreateGameShouldInitTurnStatus()
        {
            DateTime date = DateTime.Now;
            Game game = new Game(new ClassicRules(), 0, new List<Player>
            {
                new Player(1, "Adrien", "adrienImage"),
                new Player(2, "Theo", "imageTheo")
            });
            Assert.Equal(0, game.CurrentTurn);
            Assert.Equal(new Player(1, "Adrien", "adrienImage"), game.CurrentPlayer);
        }

        [Fact]
        public void CreateGameWithNullPlayerCollectionShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Game(new ClassicRules(), 0, null));
        }

        [Fact]
        public void CreateGameWithNullRulesCollectionShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Game(null, 0, new List<Player>
            {
                new Player("Theo", "imageTheo")
            }));
        }

        [Fact]
        public void CreateGameWithNoPlayersShouldThrowInvalidOperationException()
        {
            Assert.Throws<ArgumentException>(() => new Game(new ClassicRules(), 0, new List<Player>()));
        }


    }
}
