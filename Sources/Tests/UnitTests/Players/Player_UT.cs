using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Players;
using Xunit;

namespace UnitTests.Players
{
    public class Player_UT
    {
        [Fact]
        public void CreatePlayerShouldInitializeProperties()
        {
            Player player = new(Guid.NewGuid(), "Jean", "linkToImage");
            Assert.NotNull(player.Name);
            Assert.NotNull(player.Image);
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
    }
}
