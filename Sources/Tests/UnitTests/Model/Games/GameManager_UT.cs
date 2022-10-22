using Model.Games;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Games
{
    /// <summary>
    /// Unit test class used to test the GameManager class.
    /// </summary>
    public class GameManager_UT
    {
        [Fact]
        public void CreateGameManagerShouldInitializeCollection()
        {
            GameManager gameManager = new();
            Assert.NotNull(gameManager.Games);
        }

        [Fact]
        public void CreateGameManagerShouldCreateEmptyCollection()
        {
            GameManager gameManager = new();
            Assert.Empty(gameManager.Games);
        }

        [Fact]
        public void CreateGameManagerShouldCreateReadOnlyCollection()
        {
            GameManager gameManager = new();
            Assert.Equal(typeof(ReadOnlyCollection<Game>), gameManager.Games.GetType());
        }

        [Fact]
        public void CreateGameManagerShouldInitializeCurrentGame()
        {
            GameManager gameManager = new();
            Assert.Null(gameManager.CurrentGame);
        }

        [Theory]
        [MemberData(nameof(GameManagerDataTest.Data_AddGames), MemberType = typeof(GameManagerDataTest))]
        public void AddGamesToGameManagerShouldAddGames(bool expectedResult,
                                                        bool expectedInvalidOperationException,
                                                        int indexExpectedToFail,
                                                        bool expectedCurrentGameNull,
                                                        int expectedNumberOfGames,
                                                        Game[] gamesToAdd,
                                                        GameManager manager)
        {
            bool result = false;
            for (int i = 0; i < gamesToAdd.Length; i++)
            {
                if (expectedInvalidOperationException && i == indexExpectedToFail)
                {
                    Assert.Throws<InvalidOperationException>(() => manager.AddGame(gamesToAdd[i]));
                }
                else
                {
                    result = manager.AddGame(gamesToAdd[i]);
                }
            }
            if (!expectedInvalidOperationException)
            {
                Assert.Equal(expectedResult, result);
                Assert.Equal(expectedNumberOfGames, manager.Games.Count);
                Assert.Equal(expectedCurrentGameNull, manager.CurrentGame == null);
            }
        }

        [Theory]
        [MemberData(nameof(GameManagerDataTest.Data_RemoveGames), MemberType = typeof(GameManagerDataTest))]
        public void RemoveGameShouldRemoveGameFromCollection(bool expectedResult,
                                                             bool expectedCurrentGameNull,
                                                             GameManager manager,
                                                             int ID)
        {
            bool result = manager.RemoveGame(ID);
            Assert.Equal(expectedResult, result);
            Assert.Equal(expectedCurrentGameNull, manager.CurrentGame == null);
        }

        [Fact]
        public void ChangeStatusCurrentGameShouldFireTheEventAndSetToNullTheCurrentGame()
        {
            GameManager manager = new GameManager();
            manager.AddGame(GameManagerDataTest.gameNotFinished);
            Assert.NotNull(manager.CurrentGame);
            Assert.Single(manager.Games);
            manager.CurrentGame.Finish();
            Assert.Null(manager.CurrentGame);
            Assert.Single(manager.Games);
        }

        [Theory]
        [MemberData(nameof(GameManagerDataTest.Data_Equals), MemberType = typeof(GameManagerDataTest))]
        public void EqualsShouldReturnsLogicalValue(bool expectedResult,
                                                    object manager1,
                                                    object manager2)
        {
            if (manager1 != null) Assert.Equal(expectedResult, manager1.Equals(manager2));
            if (manager2 != null) Assert.Equal(expectedResult, manager2.Equals(manager1));
            if (manager1 is GameManager && manager2 is GameManager && expectedResult) Assert.Equal(manager1.GetHashCode(), manager2.GetHashCode());
        }
    }
}
