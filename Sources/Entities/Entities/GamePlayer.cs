using Entities.Entities.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class GamePlayer
    {
        public int GameId { get; set; }
        public GameEntity GameEntity { get; set; }
        public int PlayerId { get; set; }
        public PlayerEntity PlayerEntity { get; set; }
    }
}
