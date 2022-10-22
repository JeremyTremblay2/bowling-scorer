
using Entities;
using Model.Players;

namespace Entity2Model.Mapper
{
    public class BowlingMapper
    {
        public static IMapper<PlayerEntity, Player> Players = new Mapper<PlayerEntity, Player>();
    }
}
