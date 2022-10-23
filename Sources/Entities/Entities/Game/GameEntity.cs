using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities.Game
{
    public class GameEntity
    {
        public int Id { get; set; }

        public ICollection<GamePlayer> GamePlayer { get; set; } = new List<GamePlayer>();
    }
}
