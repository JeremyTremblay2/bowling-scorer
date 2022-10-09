using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Players;
using Xunit;

namespace UnitTests.Players
{
    /// <summary>
    /// Unit tests class used for the players.
    /// </summary>
    public class Player_UT
    {
        [Fact]
        public void CreatePlayerShouldInitializeProperties()
        {
            Player player = new(10, "Jean", "linkToImage");
            Assert.NotNull(player.Name);
            Assert.NotNull(player.Image);
            Assert.NotNull(player.Statistics);
        }

        [Fact]
        public void CreatePlayerShouldSetPropertyValues()
        {
            Player player = new(10, "Jean", "linkToImage");
            Assert.Equal("Jean", player.Name);
            Assert.Equal("linkToImage", player.Image);
            Assert.Equal(10, player.ID);
        }

        [Theory]
        [InlineData(false, "Jean", "linkToImage")]
        [InlineData(true, "", "linkToImage")]
        [InlineData(false, "Jean", "")]
        [InlineData(true, "", "")]
        [InlineData(false, "Jean", "    ")]
        [InlineData(true, "    ", "linkToImage")]
        [InlineData(true, "    ", "    ")]
        public void CreatePlayerShouldThrowArgumentException(bool throwException, string name, string image)
        {
            if (throwException)
            {
                Assert.Throws<ArgumentNullException>(() => new Player(name, image));
            }
            else
            {
                Player player = new(name, image);
                Assert.Equal(image, player.Image);
                Assert.Equal(name, player.Name);
            }
        }

        [Theory]
        [MemberData(nameof(PlayerDataTest.Test_PlayerDataEquality), MemberType = typeof(PlayerDataTest))]
        public void EqualsShouldReturnsLogicalValue(IEnumerable<Player> players)
        {
            foreach (Player player in players)
            {
                foreach (Player internalPlayer in players)
                {
                    if (player.ID.Equals(internalPlayer.ID))
                    {
                        // The player is cast into an object to check also the Equals method.
                        Assert.True(player.Equals((object)internalPlayer));
                        Assert.True(player == internalPlayer);
                        Assert.True(player.GetHashCode() == internalPlayer.GetHashCode());
                    }
                    else
                    {
                        Assert.False(player.Equals((object)internalPlayer));
                        Assert.False(player == internalPlayer);
                        Assert.False(player.GetHashCode() == internalPlayer.GetHashCode());
                    }
                }
            }
        }

        [Theory]
        [MemberData(nameof(PlayerDataTest.Test_PlayerDataComparison), MemberType = typeof(PlayerDataTest))]
        public void EqualityOperatorsShouldBeLogical(Player[] expectedPlayers, Player[] givenPlayers)
        {
            for (int i = 0; i < expectedPlayers.Length - 1; i++)
            {
                Assert.True(expectedPlayers[i] <= expectedPlayers[i + 1]);
                Assert.True(expectedPlayers[i+1] >= expectedPlayers[i]);
            }

            var sortedPlayers = givenPlayers.ToList();
            sortedPlayers.Sort();

            for (int i = 0; i < sortedPlayers.Count - 1; i++)
            {
                Assert.True(sortedPlayers[i] == expectedPlayers[i]);
                Assert.False(sortedPlayers[i] != expectedPlayers[i]);
            }

            Assert.True(expectedPlayers.SequenceEqual(sortedPlayers));
        }

        [Fact]
        public void ComparisonOpeartorsShouldReturnLogicValues()
        {
            Player player1 = new(1, "Jean", "linkToImage");
            Player player2 = new(1, "Adrien", "linkToImage");
            Player player3 = new(2, "Maxime", "linkToImage");
            Assert.True(player1 > player2);
            Assert.True(player2 < player1);
            Assert.True(player3 > player2);
        }

        [Fact]
        public void EqualityOperatorShouldReturnsNullWhenEqualsNullValue()
        {
            Player player1 = null;
            Player player2 = null;
            Assert.True(player1 == player2);
        }
    }
}
