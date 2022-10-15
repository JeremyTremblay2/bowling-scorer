using Model.Games;
using Model.Players;
using Model.Score;
using Model.Score.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Games
{
    public class GameManagerDataTest
    {
        public static Game gameFinished = new Game(new ClassicRules(), new Dictionary<Player, ScoreTable>
        {
            { new Player(12, "Maya", "beeImage"), GameDataTest.scoreTableComplete },
            { new Player(52, "Toto", "totoImage"), GameDataTest.anotherScoreTableComplete },
        }, true, 23);

        public static Game gameNotFinished = new Game(new ClassicRules(), new Dictionary<Player, ScoreTable>
        {
            { new Player(12, "Maya", "beeImage"), GameDataTest.scoreTableComplete },
        }, false, 12);

        public static Game gameNotFinished2 = new Game(new ClassicRules(), new Dictionary<Player, ScoreTable>
        {
            { new Player(12, "Maya", "beeImage"), GameDataTest.scoreTableComplete },
            { new Player(52, "Toto", "totoImage"), GameDataTest.scoreTableNotComplete },
        }, false, 12);

        
        public static IEnumerable<object[]> Data_RemoveGames()
        {
            GameManager manager1 = new GameManager();
            manager1.AddGame(gameFinished);

            GameManager manager2 = new GameManager();
            manager2.AddGame(gameFinished);
            manager2.AddGame(gameNotFinished);

            GameManager manager3 = new GameManager();
            manager3.AddGame(gameFinished);
            manager3.AddGame(gameNotFinished);

            yield return new object[]
            {
                true,
                true,
                manager1,
                gameFinished.ID
            };

            yield return new object[]
            {
                true,
                false,
                manager2,
                gameFinished.ID
            };

            yield return new object[]
            {
                true,
                true,
                manager2,
                gameNotFinished.ID
            };

            yield return new object[]
            {
                false,
                false,
                manager3,
                17
            };
        }

        public static IEnumerable<object[]> Data_AddGames()
        {
            yield return new object[]
            {
                true,
                false,
                0,
                true,
                1,
                new Game[]
                {
                    gameFinished
                },
                new GameManager()
            };

            yield return new object[]
            {
                true,
                false,
                0,
                false,
                1,
                new Game[]
                {
                    gameNotFinished
                },
                new GameManager()
            };

            yield return new object[]
            {
                true,
                false,
                0,
                false,
                1,
                new Game[]
                {
                    gameNotFinished2
                },
                new GameManager()
            };

            yield return new object[]
            {
                false,
                true,
                1,
                false,
                2,
                new Game[]
                {
                    gameNotFinished,
                    gameNotFinished2
                },
                new GameManager()
            };

            yield return new object[]
            {
                true,
                false,
                0,
                false,
                2,
                new Game[]
                {
                    gameFinished,
                    gameNotFinished2
                },
                new GameManager()
            };
        }

        public static IEnumerable<object[]> Data_Equals()
        {
            GameManager manager1 = new GameManager();
            GameManager manager2 = new GameManager();
            GameManager manager3 = new GameManager();
            GameManager manager4 = new GameManager();

            manager2.AddGame(gameFinished);
            manager3.AddGame(gameNotFinished);
            manager4.AddGame(gameNotFinished);

            yield return new object[]
            {
                true,
                manager1,
                manager1
            };

            yield return new object[]
            {
                true,
                manager2,
                manager2
            };

            yield return new object[]
            {
                true,
                manager3,
                manager3
            };

            yield return new object[]
            {
                false,
                manager1,
                null
            };

            yield return new object[]
            {
                false,
                null,
                manager1
            };

            yield return new object[]
            {
                false,
                12,
                manager1
            };

            yield return new object[]
            {
                false,
                manager1,
                23
            };

            yield return new object[]
            {
                false,
                manager1,
                manager2,
            };

            yield return new object[]
            {
                false,
                manager1,
                manager3,
            };

            yield return new object[]
            {
                false,
                manager2,
                manager3,
            };
            yield return new object[]
            {
                true,
                manager3,
                manager4,
            };
        }
    }
}
