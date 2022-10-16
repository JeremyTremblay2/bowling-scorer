using Model.Games;
using Model.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    /// <summary>
    /// Interface used by DataManagers, allow the developer to choose the persistance mode by using
    /// polymorphism (strategy). Contains methods to add, ge tand remove players and games.
    /// </summary>
    public interface IDataManager
    {
        /// <summary>
        /// Add a game to the Data Manager.
        /// </summary>
        /// <param name="game">The game to add.</param>
        /// <returns>A boolean indicating if the game was added.</returns>
        bool AddGame(Game game);

        /// <summary>
        /// Reove a game from the Data Manager.
        /// </summary>
        /// <param name="game">The game to remove</param>
        /// <returns>A boolean indicating if the game was removed.</returns>
        IEnumerable<Game> RemoveGame(Game game);
        
        /// <summary>
        /// Get a game from its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The game found.</returns>
        Game GetGameFromID(int id);

        /// <summary>
        /// Get the number of game specified, from the starting index given ordered by the ID.
        /// </summary>
        /// <param name="index">The index to start the recuperation of the game.</param>
        /// <param name="count">The number of games to get.</param>
        /// <returns>A collection of the games retrieved.</returns>
        IEnumerable<Game> GetGames(int index, int count);

        /// <summary>
        /// Get a collection of games in which the specified player has participated, ordered by the creation date.
        /// </summary>
        /// <param name="player">The player who participate to the games.</param>
        /// <param name="index">The starting index of the receverd process.</param>
        /// <param name="count">The number of game to retrive after the index.</param>
        /// <returns>A collection of games in which the specified player has participated.</returns>
        IEnumerable<Game> GetGamesFromPlayer(Player player, int index, int count);

        /// <summary>
        /// Add a player to the DataManager.
        /// </summary>
        /// <param name="player">The player to add.</param>
        /// <returns>A boolean indicating if the player was added.</returns>
        bool AddPlayer(Player player);

        /// <summary>
        /// Add a collection of player to the DataManager.
        /// </summary>
        /// <param name="players">The players to add.</param>
        /// <returns>A collection of the player added to the manager.</returns>
        IEnumerable<Player> AddPlayers(Player[] players);

        /// <summary>
        /// Change the name and the image of the specified player.
        /// </summary>
        /// <param name="player">The player to edit.</param>
        /// <param name="name">The name player's name.</param>
        /// <param name="image">The new player's image.</param>
        /// <returns>A boolean indicating if the player was succesfully updated.</returns>
        bool EditPlayer(Player player, string name, string image);

        /// <summary>
        /// Remove a player from the manager.
        /// </summary>
        /// <param name="player">The player to remove.</param>
        /// <returns>A boolean indicating if the player was removed.</returns>
        bool RemovePlayer(Player player);

        /// <summary>
        /// Get the first players specified from the stated index ordered by the ID.
        /// </summary>
        /// <param name="index">The index to get the first players.</param>
        /// <param name="count">The number of players to get.</param>
        /// <returns>The collection of players retrieve.</returns>
        IEnumerable<Player> GetPlayers(int index, int count);
        
        /// <summary>
        /// Get a plyer from its ID.
        /// </summary>
        /// <param name="id">The id of the player to get.</param>
        /// <returns>The player retrieve from its ID.</returns>
        Player GetPlayerFromID(int id);

        /// <summary>
        /// Get the first players specified from the stated index with a name containing 
        /// the substring given, ordered by the name.
        /// </summary>
        /// <param name="index">The index to get the first players.</param>
        /// <param name="count">The number of players to get.</param>
        /// <param name="substring">The substring contained in the name of the players.</param>
        /// <returns>The collection of players retrieve.</returns>
        IEnumerable<Player> GetPlayerFromName(string substring, int index, int count);
    }
}
