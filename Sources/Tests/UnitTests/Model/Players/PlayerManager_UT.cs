using Model.Players;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Players
{
    /// <summary>
    /// Represent a test class for the PlayerManager.
    /// </summary>
    public class PlayerManager_UT
    {
        [Fact]
        public void CreatePlayerManagerShouldInitCollections()
        {
            var playerManager = new PlayerManager();
            Assert.NotNull(playerManager.Players);
            Assert.NotNull(playerManager.SelectedPlayers);
        }

        [Fact]
        public void CreatePlayerManagerShouldCreateEmptyCollections()
        {
            var playerManager = new PlayerManager();
            Assert.Empty(playerManager.Players);
            Assert.Empty(playerManager.SelectedPlayers);
        }

        [Fact]
        public void PublicPlayerCollectionShouldBeReadOnly()
        {
            var playerManager = new PlayerManager();
            Assert.Equal(typeof(ReadOnlyCollection<Player>), playerManager.Players.GetType());
        }

        [Fact]
        public void PublicSelectedPlayerCollectionShouldBeReadOnly()
        {
            var playerManager = new PlayerManager();
            Assert.Equal(typeof(ReadOnlyCollection<Player>), playerManager.SelectedPlayers.GetType());
        }

        [Fact]
        public void AddPlayerToManagerShouldAddPlayer()
        {
            PlayerManager playerManager = new();
            playerManager.AddPlayer("Jean", "image");
            Assert.Single(playerManager.Players);
            Assert.Equal(0, playerManager.Players.First().ID);
            Assert.Equal("Jean", playerManager.Players.First().Name);
            Assert.Equal("image", playerManager.Players.First().Image);
        }

        [Theory]
        [InlineData(12, "Jean", "imageJean")]
        [InlineData(4, "Theo", "imageTheo")]
        [InlineData(2, "Elliott", "insectPicture")]
        [InlineData(0, "Thomas", "defaultImage")]
        [InlineData(9, "Mickael", "bigImage")]
        [InlineData(7, "Robin", "imageRobin")]
        public void AddPlayerToManagerShouldUpdateCollections(int ID, string name, string image)
        {
            PlayerManager playerManager = new();
            Player player = new(ID, name, image);
            playerManager.AddPlayer(player);
            Assert.Single(playerManager.Players);
            Assert.Equal(player.ID, playerManager.Players.First().ID);
            Assert.Equal(player.Name, playerManager.Players.First().Name);
            Assert.Equal(player.Image, playerManager.Players.First().Image);
        }

        [Theory]
        [MemberData(nameof(PlayerManagerDataTest.Data_AddPlayerToManager), MemberType = typeof(PlayerManagerDataTest))]
        public void AddPlayerToExistingManagerShouldUpdateCollections(bool expectedResult,
                                                                      IEnumerable<Player> expectedPlayers,
                                                                      PlayerManager playerManager,
                                                                      Player playerToBeAdded)
        {
            bool result = playerManager.AddPlayer(playerToBeAdded);
            Assert.Equal(expectedResult, result);
            Assert.Equal(expectedPlayers.Count(), playerManager.Players.Count);
            Assert.All(expectedPlayers, action: p => playerManager.Players.Contains(p));
        }

        [Theory]
        [MemberData(nameof(PlayerManagerDataTest.Data_AddMultiplePlayersToManager), MemberType = typeof(PlayerManagerDataTest))]
        public void AddMultiplePlayersShouldUpdateCollection(int expectedResult,
                                                    IEnumerable<Player> expectedPlayers,
                                                    IEnumerable<Player> expectedAddedPlayers,
                                                    PlayerManager playerManager,
                                                    params Player[] playersToAdd)
        {
            var addedPlayers = playerManager.AddPlayers(playersToAdd);
            Assert.Equal(expectedResult, addedPlayers.Count());
            Assert.All(expectedAddedPlayers, p => addedPlayers.Contains(p));

            Assert.All(expectedPlayers.Except(expectedAddedPlayers), p => addedPlayers.Contains(p));
            Assert.All(expectedPlayers, p => playerManager.Players.Contains(p));
        }

        [Fact]
        public void EditPlayerShouldThrowInvalidOperationException()
        {
            PlayerManager playerManager = new();
            Assert.Throws<InvalidOperationException>(() => playerManager.EditPlayer(200, "Toto", "image"));
        }

        [Theory]
        [InlineData("", "newImage")]
        [InlineData("     ", "otherImage")]
        [InlineData(null, "lastImage")]
        public void EditPlayerShouldThrowArgumentNullException(string newName, string newImage)
        {
            PlayerManager playerManager = new();
            playerManager.AddPlayer(new Player(12, "Toto", "image"));
            Assert.Throws<ArgumentNullException>(() => playerManager.EditPlayer(12, newName, newImage));
        }

        [Theory]
        [InlineData("Francis", "otherImage")]
        [InlineData("Theotime", "   ")]
        [InlineData("Keel", "Kong")]
        [InlineData("A", "   ")]
        [InlineData("TooLongName", "tooSmallImage")]
        public void EditPlayerShouldEditPlayer(string newName, string newImage)
        {
            PlayerManager playerManager = new();
            playerManager.AddPlayer(new Player(12, "Toto", "image"));
            playerManager.EditPlayer(12, newName, newImage);
            Assert.Equal(newName, playerManager.Players.First().Name);
            Assert.Equal(newImage, playerManager.Players.First().Image);
        }

        [Theory]
        [MemberData(nameof(PlayerManagerDataTest.Data_RemovePlayers), MemberType = typeof(PlayerManagerDataTest))]
        public void RemovePlayerShouldRemovePlayerFromCollections(bool expectedResult,
                                                                  PlayerManager manager,
                                                                  Player playerToRemove)
        {
            int sizeBeforeRemoving = manager.Players.Count;
            bool result;
            if (playerToRemove != null)
            {
                result = manager.RemovePlayer(playerToRemove.ID);
            }
            else
            {
                result = manager.RemovePlayer(playerToRemove);
            }
            Assert.Equal(expectedResult, result);
            Assert.Equal(sizeBeforeRemoving - (expectedResult ? 1 : 0), manager.Players.Count);
        }

        [Theory]
        [InlineData(12, "Jean", "imageJean")]
        [InlineData(4, "Theo", "imageTheo")]
        [InlineData(2, "Elliott", "insectPicture")]
        [InlineData(0, "Thomas", "defaultImage")]
        [InlineData(9, "Mickael", "bigImage")]
        [InlineData(7, "Robin", "imageRobin")]
        public void AddSelectedPlayerToManagerShouldUpdateCollections(int ID, string name, string image)
        {
            Player player = new(ID, name, image);
            PlayerManager playerManager = new(player);
            playerManager.AddSelectedPlayer(player);
            Assert.Single(playerManager.SelectedPlayers);
            Assert.Equal(player.ID, playerManager.SelectedPlayers.First().ID);
            Assert.Equal(player.Name, playerManager.SelectedPlayers.First().Name);
            Assert.Equal(player.Image, playerManager.SelectedPlayers.First().Image);
        }

        [Theory]
        [MemberData(nameof(PlayerManagerDataTest.Data_AddSelectedPlayerToManager), MemberType = typeof(PlayerManagerDataTest))]
        public void AddSelectedPlayerToExistingManagerShouldUpdateCollections(bool expectedResult,
                                                                      IEnumerable<Player> expectedPlayers,
                                                                      PlayerManager playerManager,
                                                                      Player playerToBeAdded)
        {
            bool result = playerManager.AddSelectedPlayer(playerToBeAdded);
            Assert.Equal(expectedResult, result);
            Assert.Equal(expectedPlayers.Count(), playerManager.SelectedPlayers.Count);
            Assert.All(expectedPlayers, action: p => playerManager.SelectedPlayers.Contains(p));
        }

        [Theory]
        [MemberData(nameof(PlayerManagerDataTest.Data_AddMultipleSelectedPlayersToManager), MemberType = typeof(PlayerManagerDataTest))]
        public void AddMultipleSelectedPlayersShouldUpdateCollection(int expectedResult,
                                                    IEnumerable<Player> expectedPlayers,
                                                    IEnumerable<Player> expectedAddedPlayers,
                                                    PlayerManager playerManager,
                                                    params Player[] playersToAdd)
        {
            var addedPlayers = playerManager.AddSelectedPlayers(playersToAdd);
            Assert.Equal(expectedResult, addedPlayers.Count());
            Assert.All(expectedAddedPlayers, p => addedPlayers.Contains(p));

            Assert.All(expectedPlayers.Except(expectedAddedPlayers), p => addedPlayers.Contains(p));
            Assert.All(expectedPlayers, p => playerManager.SelectedPlayers.Contains(p));
        }

        [Theory]
        [MemberData(nameof(PlayerManagerDataTest.Data_RemoveSelectedPlayers), MemberType = typeof(PlayerManagerDataTest))]
        public void RemoveSelectedPlayerShouldRemovePlayerFromCollections(bool expectedResult,
                                                                  PlayerManager manager,
                                                                  Player playerToRemove)
        {
            int sizeBeforeRemoving = manager.SelectedPlayers.Count;
            bool result = manager.RemoveSelectedPlayer(playerToRemove);
            Assert.Equal(expectedResult, result);
            Assert.Equal(sizeBeforeRemoving - (expectedResult ? 1 : 0), manager.SelectedPlayers.Count);
        }

        [Fact]
        public void ClearSelectedPlayersShouldRemoveAllPlayers()
        {
            Player player1 = new Player(2, "Adrien", "adrienImage");
            Player player2 = new Player(3, "Laurent", "laurentImage");
            PlayerManager playerManager = new(player1, player2);
            playerManager.AddSelectedPlayers(player1, player2);
            Assert.Equal(2, playerManager.SelectedPlayers.Count);
            playerManager.ClearSelectedPlayers();
            Assert.Empty(playerManager.SelectedPlayers);
        }

        [Theory]
        [MemberData(nameof(PlayerManagerDataTest.Data_FindPlayersFromID), MemberType = typeof(PlayerManagerDataTest))]
        public void FindPlayerFromIDShouldRetrievePlayerFromCollectionsWhenPresent(Player expectedPlayer,
                                                                                   PlayerManager playerManager,
                                                                                   int playerID)
        {
            Player player = playerManager.GetPlayerFromID(playerID);
            Assert.Equal(expectedPlayer, player);
        }

        [Theory]
        [MemberData(nameof(PlayerManagerDataTest.Data_EqualsManagers), MemberType = typeof(PlayerManagerDataTest))]
        public void EqualsShouldReturnsLogicalValue(bool expectedResult,
                                                    object playerManager1,
                                                    object playerManager2)
        {
            if (playerManager1 != null)
            {
                Assert.Equal(expectedResult, playerManager1.Equals(playerManager2));
            }
            if (playerManager2 != null)
            {
                Assert.Equal(expectedResult, playerManager2.Equals(playerManager1));
            }
            if (playerManager1 == null || playerManager2 == null) return;
            if (expectedResult)
            {
                Assert.Equal(playerManager1.GetHashCode(), playerManager2.GetHashCode());
            }
            else
            {
                Assert.NotEqual(playerManager1.GetHashCode(), playerManager2.GetHashCode());
            }
        }
    }
}
