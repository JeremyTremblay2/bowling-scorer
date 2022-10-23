
using Entities;
using Entities.Entities.Game;
using Entities.Frame;
using FrameModel.Frame;
using Model.Games;
using Model.Players;

namespace Entity2Model.Mapper
{
    /// <summary>
    /// Class containing all the mappers used in our Bowling Scorer app.
    /// </summary>
    public static class BowlingMapper
    {
        /// <summary>
        /// Mapper used to do the correspondance between player entities and player models.
        /// </summary>
        public static IMapper<PlayerEntity, Player> Players = new Mapper<PlayerEntity, Player>();

        /// <summary>
        /// Mapper used to do the correspondance between frames entities and frame models.
        /// </summary>
        public static IMapper<FrameEntity, AFrame> Frames = new Mapper<FrameEntity, AFrame>();

        /// <summary>
        /// Mapper used to do the correspondance between game entities and game models.
        /// </summary>
        public static IMapper<GameEntity, Game> Games = new Mapper<GameEntity, Game>();

    }
}
