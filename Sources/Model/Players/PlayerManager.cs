using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Players
{
    public class PlayerManager : IEquatable<PlayerManager>
    {
        private readonly IList<Player> players;

        public IReadOnlyCollection<Player> Players { get; private set; }

        public PlayerManager()
        {
            players = new List<Player>();
            Players = new ReadOnlyCollection<Player>(players);
        }

        public void AddPlayer(string name, string image)
            => AddPlayer(new Player(name, image));

        public void AddPlayer(Player player)
        {
            if (player == null || players.Contains(player)) return;
            players.Add(player);

        }

        public void AddPlayers(params Player[] players)
        {
            foreach (Player player in players)
            {
                AddPlayer(player);
            }
        }

        public void EditPlayer(int id, string name, string image)
        {
            Player player = GetPlayerFromID(id);
            if (player == null)
            {
                throw new InvalidOperationException("The player ID passed in parameter cannot be null.");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "The name cannot be null or empty");
            }
            player.Name = name;
            player.Image = image;
        }

        public bool RemovePlayer(int id)
            => players.Remove(GetPlayerFromID(id));

        public Player GetPlayerFromID(int id)
            => players.FirstOrDefault(p => p.ID.Equals(id));

        /// <summary>
        /// Determines whether the two object instances are equal.
        /// </summary>
        /// <param name="other">The object to compare with the actual object.</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, False.</returns>
        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Player)) return false;
            return Equals((PlayerManager) obj);
        }

        /// <summary>
        /// Determines whether the two object instances are equal.
        /// </summary>
        /// <param name="other">The player to compare with the actual player.</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, False.</returns>
        public bool Equals(PlayerManager other)
        {
            return other.Players.Equals(Players);
        }

        /// <summary>
        /// Returns a string representing a PlayerManager.
        /// </summary>
        /// <returns>A string representing a PlayerManager.</returns>
        public override string ToString()
        {
            StringBuilder builder = new();
            foreach (Player player in players)
            {
                builder.Append("-").Append(player.ToString());
            }
            return builder.ToString();
        }
    }
}
