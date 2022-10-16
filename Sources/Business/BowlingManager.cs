using Model.Games;
using Model.Players;
using Model.Score.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    /// <summary>
    /// The BowlingManager is the main access point to the Bowling app.
    /// It contains a DataManager capable of performing addition, deletion and modification on the collections.
    /// It is simply a facade, which relays each action to another manager.
    /// </summary>
    public class BowlingManager
    {
        private IDataManager dataManager;

        private GameManager gameManager;

        private PlayerManager playerManager;

        public BowlingManager(IDataManager dataManager, ARules rules)
        {
            this.dataManager = dataManager ?? throw new ArgumentNullException(nameof(dataManager), "The DataManager given to the manager cannot be null.");
            gameManager = new GameManager();
            playerManager = new PlayerManager();
        }

        /// <summary>
        /// Add a game to the Data Manager.
        /// </summary>
        /// <param name="game">The game to add.</param>
        /// <returns>A boolean indicating if the game was added.</returns>
        public bool AddGame(Game game)
        {
            return dataManager.AddGame(game);
        }

        /// <summary>
        /// Reove a game from the Data Manager.
        /// </summary>
        /// <param name="game">The game to remove</param>
        /// <returns>A boolean indicating if the game was removed.</returns>
        public IEnumerable<Game> RemoveGame(Game game)
        {
            return dataManager.RemoveGame(game);
        }

        /// <summary>
        /// Get a game from its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The game found.</returns>
        public Game GetGameFromID(int id)
        {
            return dataManager.GetGameFromID(id);
        }

        /// <summary>
        /// Get the number of game specified, from the starting index given ordered by the ID.
        /// </summary>
        /// <param name="index">The index to start the recuperation of the game.</param>
        /// <param name="count">The number of games to get.</param>
        /// <returns>A collection of the games retrieved.</returns>
        public IEnumerable<Game> GetGames(int index, int count)
        {
            return dataManager.GetGames(index, count);
        }

        /// <summary>
        /// Get a collection of games in which the specified player has participated, ordered by the creation date.
        /// </summary>
        /// <param name="player">The player who participate to the games.</param>
        /// <param name="index">The starting index of the receverd process.</param>
        /// <param name="count">The number of game to retrive after the index.</param>
        /// <returns>A collection of games in which the specified player has participated.</returns>
        public IEnumerable<Game> GetGamesFromPlayer(Player player, int index, int count)
        {
            return GetGamesFromPlayer(player, index, count);
        }

        /// <summary>
        /// Add a player to the DataManager.
        /// </summary>
        /// <param name="player">The player to add.</param>
        /// <returns>A boolean indicating if the player was added.</returns>
        public bool AddPlayer(Player player)
        {
            return dataManager.AddPlayer(player);
        }

        /// <summary>
        /// Add a collection of player to the DataManager.
        /// </summary>
        /// <param name="players">The players to add.</param>
        /// <returns>A collection of the player added to the manager.</returns>
        public IEnumerable<Player> AddPlayers(Player[] players)
        {
            return dataManager.AddPlayers(players);
        }

        /// <summary>
        /// Change the name and the image of the specified player.
        /// </summary>
        /// <param name="player">The player to edit.</param>
        /// <param name="name">The name player's name.</param>
        /// <param name="image">The new player's image.</param>
        /// <returns>A boolean indicating if the player was succesfully updated.</returns>
        public bool EditPlayer(Player player, string name, string image)
        {
            return dataManager.EditPlayer(player, name, image);
        }

        /// <summary>
        /// Remove a player from the manager.
        /// </summary>
        /// <param name="player">The player to remove.</param>
        /// <returns>A boolean indicating if the player was removed.</returns>
        public bool RemovePlayer(Player player)
        {
            return dataManager.RemovePlayer(player);
        }

        /// <summary>
        /// Get the first players specified from the stated index ordered by the ID.
        /// </summary>
        /// <param name="index">The index to get the first players.</param>
        /// <param name="count">The number of players to get.</param>
        /// <returns>The collection of players retrieve.</returns>
        public IEnumerable<Player> GetPlayers(int index, int count)
        {
            return dataManager.GetPlayers(index, count);
        }

        /// <summary>
        /// Get a plyer from its ID.
        /// </summary>
        /// <param name="id">The id of the player to get.</param>
        /// <returns>The player retrieve from its ID.</returns>
        public Player GetPlayerFromID(int id)
        {
            return dataManager.GetPlayerFromID(id);
        }

        /// <summary>
        /// Get the first players specified from the stated index with a name containing 
        /// the substring given, ordered by the name.
        /// </summary>
        /// <param name="index">The index to get the first players.</param>
        /// <param name="count">The number of players to get.</param>
        /// <param name="substring">The substring contained in the name of the players.</param>
        /// <returns>The collection of players retrieve.</returns>
        public IEnumerable<Player> GetPlayerFromName(string substring, int index, int count)
        {
            return dataManager.GetPlayerFromName(substring, index, count);
        }


        /// <summary>
        /// Add a player to the selected players.
        /// </summary>
        /// <param name="player">The player to add.</param>
        /// <returns>A boolean indicating if the player was added.</returns>
        public bool AddSelectedPlayer(Player player)
        {
            return playerManager.AddSelectedPlayer(player);
        }

        /// <summary>
        /// Add a collection of player to the selected players.
        /// </summary>
        /// <param name="players">The players to add.</param>
        /// <returns>A collection of the player added to the selected players collection.</returns>
        public IEnumerable<Player> AddSelectedPlayers(Player[] players)
        {
            return playerManager.AddSelectedPlayers(players);
        }

        /// <summary>
        /// Remove a player from the selection.
        /// </summary>
        /// <param name="player">The player to remove.</param>
        /// <returns>A boolean indicating if the player was removed.</returns>
        public bool RemoveSelectedPlayer(Player player)
        {
            return playerManager.RemoveSelectedPlayer(player);
        }

        /// <summary>
        /// Clear the collection of selected players.
        /// </summary>
        public void ClearSelectedPlayers()
        {
            playerManager.ClearSelectedPlayers();
        }
    }
}
