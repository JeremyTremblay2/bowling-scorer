using Entities.Entities;
using Entities.Entities.Game;
using Entities.Frame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    /// <summary>
    /// This Entity represents a Player in a database.
    /// </summary>
    public class PlayerEntity
    {
        /// <summary>
        /// The ID of the player.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The name of the player.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// The image of the player.
        /// </summary>
        public string? Image { get; set; }

        public ICollection<GamePlayer> GamePlayer { get; set; } = new List<GamePlayer>();
    }
}
