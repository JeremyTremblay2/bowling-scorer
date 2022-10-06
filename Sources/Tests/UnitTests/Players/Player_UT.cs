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
            Player player = new(Guid.NewGuid(), "Jean", "linkToImage");
            Assert.NotNull(player.Name);
            Assert.NotNull(player.Image);
            Assert.NotNull(player.Statistics);
        }

        [Fact]
        public void CreatePlayerShouldSetPropertyValues()
        {
            Guid guid = Guid.NewGuid();
            Player player = new(guid, "Jean", "linkToImage");
            Assert.Equal("Jean", player.Name);
            Assert.Equal("linkToImage", player.Image);
            Assert.Equal(guid, player.ID);
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
                        Assert.True(player.Equals(internalPlayer));
                        Assert.True(player == internalPlayer);
                        Assert.True(player.GetHashCode() == internalPlayer.GetHashCode());
                    }
                    else
                    {
                        Assert.False(player.Equals(internalPlayer));
                        Assert.False(player == internalPlayer);
                        Assert.False(player.GetHashCode() == internalPlayer.GetHashCode());
                    }
                }
            }
        }

        [Theory]
        [MemberData(nameof(PlayerDataTest.Test_PlayerDataComparison), MemberType = typeof(PlayerDataTest))]
        public void PlayerComparisonShouldBeLogical(Player[] expectedPlayers, Player[] givenPlayers)
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
    }
}
