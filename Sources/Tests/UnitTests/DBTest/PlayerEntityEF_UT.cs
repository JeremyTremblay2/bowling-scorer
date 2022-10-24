using Entities;
using Entity2Model;
using Model.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.DBTest
{
    public class PlayerEntityEF_UT
    {
        [Fact]
        public void PlayerExtensionShouldTransformPlayerIntoPlayerEntity()
        {
            Player player = new Player(42, "Toto", "image");
            PlayerEntity entity = player.ToEntity();
            Assert.Equal(42, entity.ID);
            Assert.Equal("Toto", entity.Name);
            Assert.Equal("image", entity.Image);
        }

        [Fact]
        public void PlayerExtensionShouldTransformPlayerEntityIntoPlayer()
        {
            PlayerEntity entity = new PlayerEntity
            {
                ID = 21,
                Name = "francis",
                Image = "roger.png"
            };
            Player player = entity.ToModel();
            Assert.Equal(21, entity.ID);
            Assert.Equal("francis", entity.Name);
            Assert.Equal("roger.png", entity.Image);
        }
    }
}
