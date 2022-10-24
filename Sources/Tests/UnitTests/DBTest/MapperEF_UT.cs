using Entities;
using Entity2Model;
using Entity2Model.Mapper;
using Model.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.DBTest
{
    public class MapperEF_UT
    {
        [Fact]
        public void MapperShouldMapAndReturnsEntitiesAndModels()
        {
            IMapper<PlayerEntity, Player> mapper = new Mapper<PlayerEntity, Player>();
            Player model = new Player(21, "Toto", "image");
            PlayerEntity entity = model.ToEntity();
            mapper.Map(entity, model);
            Assert.NotNull(mapper.Get(entity));
            Assert.NotNull(mapper.Get(model));
        }

        [Fact]
        public void MapperShouldReturnsEntitiesAndModelsWthSameValues()
        {
            IMapper<PlayerEntity, Player> mapper = new Mapper<PlayerEntity, Player>();
            Player model = new Player(21, "Toto", "image");
            PlayerEntity entity = model.ToEntity();
            mapper.Map(entity, model);
            Assert.Equal(entity.ID, mapper.Get(model).ID);
            Assert.Equal(entity.Name, mapper.Get(model).Name);
            Assert.Equal(entity.Image, mapper.Get(model).Image);
            Assert.Equal(model.ID, mapper.Get(entity).ID);
            Assert.Equal(model.Name, mapper.Get(entity).Name);
            Assert.Equal(model.Image, mapper.Get(entity).Image);
        }
    }
}
