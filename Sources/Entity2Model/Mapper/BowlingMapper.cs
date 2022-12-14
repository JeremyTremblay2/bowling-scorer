
using Entities;
using Model.Players;

namespace Entity2Model.Mapper
{
    /// <summary>
    /// Class containing all the mappers used in our Bowling Scorer app.
    /// </summary>
    public static class BowlingMapper
    {
        /// <summary>
        /// Mapper used to do the correspondance between player entities and pleyr models.
        /// </summary>
        public static readonly IMapper<PlayerEntity, Player> Players = new Mapper<PlayerEntity, Player>();

        /// <summary>
        /// Clear all the mappers of the project.
        /// </summary>
        public static void Reset()
        {
            Players.Clear();
        }
    }
}
