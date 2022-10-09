using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Model.Players
{
    /// <summary>
    /// A PlayerManager represent a database that contains the different players contained in the application.
    /// It provides methods to add, remove and edit players.
    /// </summary>
    public class PlayerManager : IEquatable<PlayerManager>
    {
        /// <summary>
        /// The private read-only collection of player.
        /// </summary>
        private readonly IList<Player> players;

        /// <summary>
        /// Property containing a read-only collection of players.
        /// </summary>
        public IReadOnlyCollection<Player> Players { get; private set; }

        /// <summary>
        /// Create a new PlayerManager.
        /// </summary>
        public PlayerManager(params Player[] players)
        {
            this.players = new List<Player>();
            Players = new ReadOnlyCollection<Player>(players);
            AddPlayers(players);
        }

        /// <summary>
        /// Create and add a new player to this manager.
        /// </summary>
        /// <param name="name">The name of the player.</param>
        /// <param name="image">The image of the player.</param>
        /// <returns>A boolean indicating if the player was added.</returns>
        public bool AddPlayer(string name, string image)
            => AddPlayer(new Player(name, image));

        /// <summary>
        /// Add a player to the collection if it is not already present and if it is not null.
        /// </summary>
        /// <param name="player">The player to add.</param>
        /// <returns>A boolean indicating if the player was added.</returns>
        public bool AddPlayer(Player player)
        {
            if (player == null || players.Contains(player)) return false;
            players.Add(player);
            return true;
        }

        /// <summary>
        /// Add a collection of players into the manager, and return the players added.
        /// </summary>
        /// <param name="players">The players to be added.</param>
        /// <returns>A collecion of the players that have been added to the collection.</returns>
        public IEnumerable<Player> AddPlayers(params Player[] players)
        {
            List<Player> result = new();
            if (players == null) return result;
            foreach (Player player in players)
            {
                if (AddPlayer(player))
                {
                    result.Add(player);
                }
            }
            return result;
        }

        /// <summary>
        /// Edit a player identified from it's ID, and change his name and his image.
        /// Throw an InvalidOperationException if the given player is null and a ArgumentNullException if his name is blank.
        /// </summary>
        /// <param name="id">The player's ID.</param>
        /// <param name="name">The new player name.</param>
        /// <param name="image">the new player's image.</param>
        /// <exception cref="InvalidOperationException">Thrown if the given player is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the player's name is empty or null.</exception>
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

        /// <summary>
        /// Remove a player from his ID.
        /// </summary>
        /// <param name="id">The player's ID.</param>
        /// <returns>A boolean indicating if the player as removed.</returns>
        public bool RemovePlayer(int id)
            => players.Remove(GetPlayerFromID(id));
        
        /// <summary>
        /// Get a player from his ID or null if no player has been founded.
        /// </summary>
        /// <param name="id">The ID of the player sought.</param>
        /// <returns>The corresponding player or null.</returns>
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
            return other.players.Equals(players);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(players);
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
                builder.Append('-').Append(player.ToString());
            }
            return builder.ToString();
        }
    }
}
